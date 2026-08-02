
using AquaticFishECommerce.Application.Common.Exceptions;
using AquaticFishECommerce.Application.DTOs.Auth;
using AquaticFishECommerce.Application.DTOs.User;
using AquaticFishECommerce.Application.Interfaces.External;
using AquaticFishECommerce.Application.Interfaces.Repositories;
using AquaticFishECommerce.Application.Interfaces.Services.AuthService;
using AquaticFishECommerce.Domain.Entities;
using AutoMapper;

namespace AquaticFishECommerce.Infrastructure.Services.Business.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordResetRepository _passwordResetRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IEmailService _emailService;
        private readonly IMapper _mapper;
        private readonly IJwtService _jwtService;
        public AuthService(IUserRepository userRepository , IPasswordResetRepository passwordResetRepository , IPasswordHasher passwordHasher , IEmailService emailService , IMapper mapper , IJwtService jwtService)
        {
            _userRepository = userRepository;
            _passwordResetRepository = passwordResetRepository;
            _passwordHasher = passwordHasher;
            _emailService = emailService;
            _mapper = mapper;
            _jwtService = jwtService;
        }

        // Register a new user
        public async Task RegisterAsync(RegisterUserDto dto)
        {
            // Check if the email already exists
            if (await _userRepository.EmailExistsAsync(dto.Email))
            {
                throw new ConflictException("Email already exists.");
            }

            var user = _mapper.Map<AquaticFishECommerce.Domain.Entities.User>(dto);

            // Hash the password before saving
            user.PasswordHash = _passwordHasher.Hash(dto.Password);

            // Save the user
            await _userRepository.AddAsync(user);
        }

        // Login user and return access token
        public async Task<AuthResponseDto> LoginAsync(LoginDto dto)
        {
            var user = await _userRepository.GetByEmailAsync(dto.Email);

            if (user == null || !_passwordHasher.Verify(dto.Password, user.PasswordHash))
            {
                throw new UnauthorizedException("Invalid email or password.");
            }

            // Generate JWT access token
            var accessToken = _jwtService.GenerateAccessToken(user);
            var refreshToken = _jwtService.GenerateRefreshToken();
            var refreshTokenHash = _jwtService.HashRefreshToken(refreshToken);

            user.RefreshTokenHash = refreshTokenHash;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddMinutes(60);
            await _userRepository.UpdateAsync(user);

            return new AuthResponseDto
            {
                User = _mapper.Map<UserDto>(user),
                AccessToken = accessToken,
                RefreshToken = refreshToken

            };
        }

        public async Task<RefreshTokenResponseDto> RefreshTokenAsync(string refreshToken)
        {
            if (string.IsNullOrWhiteSpace(refreshToken))
            {
                return new RefreshTokenResponseDto
                {
                    Success = false,
                    Message = "Refresh token not found."
                };
            }

            // Hash received refresh token
            var currentRefreshTokenHash = _jwtService.HashRefreshToken(refreshToken);

            // Find user
            var user = await _userRepository.GetByRefreshTokenHashAsync(currentRefreshTokenHash);

            if (user is null)
            {
                return new RefreshTokenResponseDto
                {
                    Success = false,
                    Message = "Invalid refresh token."
                };
            }

            // Check expiry
            if (user.RefreshTokenExpiryTime is null ||
                user.RefreshTokenExpiryTime <= DateTime.UtcNow)
            {
                return new RefreshTokenResponseDto
                {
                    Success = false,
                    Message = "Refresh token has expired."
                };
            }

            // Generate new access token
            var newAccessToken = _jwtService.GenerateAccessToken(user);

            // Generate new refresh token
            var newRefreshToken = _jwtService.GenerateRefreshToken();

            // Hash new refresh token
            var newRefreshTokenHash = _jwtService.HashRefreshToken(newRefreshToken);

            // Save new values
            user.RefreshTokenHash = newRefreshTokenHash;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddMinutes(60);

            await _userRepository.UpdateAsync(user);

            return new RefreshTokenResponseDto
            {
                Success = true,
                Message = "Token refreshed successfully.",
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken
            };
        }

        // Get user by ID
        public async Task<UserDto?> GetByIdAsync(Guid id)
        {
            if (id == Guid.Empty)
                throw new BadRequestException("Id is required");

            var user = await _userRepository.GetByIdAsync(id);

            if (user == null)
            {
                throw new NotFoundException("User not found.");
            }
            return _mapper.Map<UserDto>(user);
        }

        public async Task LogoutAsync(Guid userId)
        {
            var user = await _userRepository.GetByIdAsync(userId);

            if (user is null)
                return;

            user.RefreshTokenHash = null;
            user.RefreshTokenExpiryTime = null;

            await _userRepository.UpdateAsync(user);
        }

        public async Task ForgotPasswordAsync(ForgotPasswordDto dto)
        {
            var user = await _userRepository.GetByEmailAsync(dto.Email);

            if (user == null)
                return;

            var token = Guid.NewGuid().ToString("N");

            var resetToken = new PasswordResetToken
            {
                UserId = user.Id,
                Token = token,
                ExpiresAt = DateTime.UtcNow.AddMinutes(30),
                IsUsed = false
            };

            await _passwordResetRepository.AddAsync(resetToken);

            var resetLink = $"http://localhost:5173/reset-password?token={token}";

            Console.WriteLine(_emailService == null);
            Console.WriteLine(user == null);
            Console.WriteLine(user?.Email);

            await _emailService.SendEmailAsync(
                user.Email,
                "Reset Password",
                $"""
                <h2>Reset Password</h2>
                <p>Click the link below to reset your password.</p>
                <a href="{resetLink}">Reset Password</a>
                <p>This link expires in 30 minutes.</p>
                """
            );
        }

        public async Task ResetPasswordAsync(ResetPasswordDto dto)
        {
            var resetToken =
                await _passwordResetRepository.GetByTokenAsync(dto.Token);

            if (resetToken == null)
                throw new BadRequestException("Invalid reset token.");

            if (resetToken.IsUsed)
                throw new BadRequestException("Token already used.");

            if (resetToken.ExpiresAt < DateTime.UtcNow)
                throw new BadRequestException("Token expired.");

            var user = resetToken.User;

            user.PasswordHash = _passwordHasher.Hash(dto.NewPassword);
            resetToken.IsUsed = true;
            await _userRepository.UpdateAsync(user);
            await _passwordResetRepository.UpdateAsync(resetToken);
        }
    }
}

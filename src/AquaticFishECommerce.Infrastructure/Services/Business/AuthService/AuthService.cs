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

            // Hash the password
            user.PasswordHash = _passwordHasher.Hash(dto.Password);
            await _userRepository.AddAsync(user);
        }

        // Login user services included jwt tokens
        public async Task<AuthResponseDto> LoginAsync(LoginDto dto)
        {
            var user = await _userRepository.GetByEmailAsync(dto.Email);

            if (user == null || !_passwordHasher.Verify(dto.Password, user.PasswordHash))
            {
                throw new UnauthorizedException("Invalid email or password.");
            }

            // Generate JWT access and refresh token
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

        //Refresh token service
        public async Task<RefreshTokenResponseDto> RefreshTokenAsync(string refreshToken)
        {
            if (string.IsNullOrWhiteSpace(refreshToken))
            {
                throw new UnauthorizedException("Unauthorized.");
            }

            // Hash received refresh token
            var currentRefreshTokenHash = _jwtService.HashRefreshToken(refreshToken);
            // Find user
            var user = await _userRepository.GetByRefreshTokenHashAsync(currentRefreshTokenHash);

            if (user is null)
            {
                throw new UnauthorizedException("Invalid refresh token.");
            }

            // Check expiry
            if (user.RefreshTokenExpiryTime is null ||
                user.RefreshTokenExpiryTime <= DateTime.UtcNow)
            {
                throw new UnauthorizedException("Refresh token has expired.");
            }

            // Generate new tokens
            var newAccessToken = _jwtService.GenerateAccessToken(user);
            var newRefreshToken = _jwtService.GenerateRefreshToken();
            var newRefreshTokenHash = _jwtService.HashRefreshToken(newRefreshToken);

            // Save new values
            user.RefreshTokenHash = newRefreshTokenHash;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddMinutes(60);
            await _userRepository.UpdateAsync(user);

            return new RefreshTokenResponseDto
            {
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken
            };
        }

        //Service for logout
        public async Task LogoutAsync(Guid userId)
        {
            var user = await _userRepository.GetByIdAsync(userId);

            if (user is null)
                return;

            user.RefreshTokenHash = null;
            user.RefreshTokenExpiryTime = null;

            await _userRepository.UpdateAsync(user);
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

        //Service for forgot password with send token to email
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
                ExpiresAt = DateTime.UtcNow.AddMinutes(5),
                IsUsed = false
            };

            await _passwordResetRepository.AddAsync(resetToken);

            var resetLink = $"http://localhost:5173/reset-password?token={token}";

            await _emailService.SendEmailAsync(
                user.Email,
                "Reset Password",
                $"""
                <h2>Reset Password</h2>
                <p>Click the link below to reset your password.</p>
                <a href="{resetLink}">Reset Password</a>
                <p>This link expires in 5 minutes.</p>
                """
            );
        }

        //Service for reset password of the user
        public async Task ResetPasswordAsync(ResetPasswordDto dto)
        {
            var resetToken = await _passwordResetRepository.GetByTokenAsync(dto.Token);

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

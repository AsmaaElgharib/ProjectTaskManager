using MediatR;
using ProjectTaskManager.Application.Common.Exceptions;
using ProjectTaskManager.Application.Common.Interfaces;
using ProjectTaskManager.Application.Common.Models;
using ProjectTaskManager.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTaskManager.Application.Features.Auth.Commands.Login
{
    public class LoginCommandHandler
    : IRequestHandler<
        LoginCommand,
        ApiResponse<AuthResponse>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtService _jwtService;
        private readonly IPasswordHasher _passwordHasher;

        public LoginCommandHandler(
            IUserRepository userRepository,
            IJwtService jwtService,
            IPasswordHasher passwordHasher)
        {
            _userRepository = userRepository;
            _jwtService = jwtService;
            _passwordHasher = passwordHasher;
        }

        public async Task<ApiResponse<AuthResponse>> Handle(
            LoginCommand request,
            CancellationToken cancellationToken)
        {
            var user =
                await _userRepository.GetByEmailAsync(
                    request.Email.ToLower(),
                    cancellationToken)
                ?? throw new UnauthorizedException(
                    "Invalid email or password.");

            if (!_passwordHasher.Verify(
                    request.Password,
                    user.PasswordHash))
            {
                throw new UnauthorizedException(
                    "Invalid email or password.");
            }

            var token = _jwtService.GenerateToken(user);

            return ApiResponse<AuthResponse>
                .SuccessResult(
                    new AuthResponse(
                        token,
                        user.Email,
                        user.FullName,
                        user.Role),
                    "Login successful.");
        }
    }
}

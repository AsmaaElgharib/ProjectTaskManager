using MediatR;
using ProjectTaskManager.Application.Common.Exceptions;
using ProjectTaskManager.Application.Common.Interfaces;
using ProjectTaskManager.Application.Common.Models;
using ProjectTaskManager.Domain.Entities;
using ProjectTaskManager.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTaskManager.Application.Features.Auth.Commands.Register
{
    public class RegisterCommandHandler
    : IRequestHandler<
        RegisterCommand,
        ApiResponse<AuthResponse>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtService _jwtService;
        private readonly IPasswordHasher _passwordHasher;

        public RegisterCommandHandler(
            IUserRepository userRepository,
            IJwtService jwtService,
            IPasswordHasher passwordHasher)
        {
            _userRepository = userRepository;
            _jwtService = jwtService;
            _passwordHasher = passwordHasher;
        }

        public async Task<ApiResponse<AuthResponse>> Handle(
       RegisterCommand request,
       CancellationToken cancellationToken)
        {
            if (await _userRepository.ExistsByEmailAsync(
                request.Email,
                cancellationToken))
            {
                throw new ConflictException(
                    $"A user with email '{request.Email}' already exists.");
            }

            var user = new User
            {
                FullName = request.FullName,
                Email = request.Email.ToLower(),
                PasswordHash = _passwordHasher.Hash(request.Password)
            };

            await _userRepository.AddAsync(
                user,
                cancellationToken);

            var token = _jwtService.GenerateToken(user);

            return ApiResponse<AuthResponse>
                .SuccessResult(
                    new AuthResponse(
                        token,
                        user.Email,
                        user.FullName,
                        user.Role),
                    "Registration successful.");
        }
    }
}

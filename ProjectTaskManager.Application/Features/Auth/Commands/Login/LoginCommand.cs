using MediatR;
using ProjectTaskManager.Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTaskManager.Application.Features.Auth.Commands.Login
{
    public record LoginCommand(
    string Email,
    string Password)
    : IRequest<ApiResponse<AuthResponse>>;
}

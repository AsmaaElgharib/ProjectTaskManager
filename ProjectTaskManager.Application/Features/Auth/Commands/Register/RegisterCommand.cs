using MediatR;
using ProjectTaskManager.Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTaskManager.Application.Features.Auth.Commands.Register
{
    public record RegisterCommand(
    string FullName,
    string Email,
    string Password)
    : IRequest<ApiResponse<AuthResponse>>;
}

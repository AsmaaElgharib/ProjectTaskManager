using MediatR;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using ProjectTaskManager.Application.Common.Models;
using ProjectTaskManager.Application.Features.Auth;
using ProjectTaskManager.Application.Features.Auth.Commands.Login;
using ProjectTaskManager.Application.Features.Auth.Commands;
using ProjectTaskManager.Application.Features.Auth.Commands.Register;
using RegisterRequest = ProjectTaskManager.Application.Features.Auth.RegisterRequest;
using LoginRequest = ProjectTaskManager.Application.Features.Auth.LoginRequest;

namespace ProjectTaskManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator) => _mediator = mediator;

        /// <summary>Register a new user account.</summary>
        [HttpPost("register")]
        [ProducesResponseType(typeof(ApiResponse<AuthResponse>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status409Conflict)]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request, CancellationToken cancellationToken)
        {
            var command = new RegisterCommand(request.FullName, request.Email, request.Password);
            var result = await _mediator.Send(command, cancellationToken);
            return CreatedAtAction(nameof(Register), result);
        }

        /// Authenticate and receive a JWT token
        [HttpPost("login")]
        [ProducesResponseType(typeof(ApiResponse<AuthResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Login([FromBody] LoginRequest request, CancellationToken cancellationToken)
        {
            var command = new LoginCommand(request.Email, request.Password);
            var result = await _mediator.Send(command, cancellationToken);
            return Ok(result);
        }
    }
}

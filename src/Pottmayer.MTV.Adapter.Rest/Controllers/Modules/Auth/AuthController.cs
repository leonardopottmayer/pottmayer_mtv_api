using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Pottmayer.MTV.Core.Domain.Modules.Auth.Cqrs;
using Pottmayer.MTV.Core.Domain.Modules.Auth.Dtos.Logic;
using Pottmayer.MTV.Core.Domain.Modules.Auth.Dtos.Rest;
using Tars.Adapter.Rest.Controllers;
using Tars.Adapter.Rest.Extensions;
using Tars.Contracts.Adapter.Rest;

namespace Pottmayer.MTV.Adapter.Rest.Controllers.Modules.Auth
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : DefaultController
    {
        protected readonly IMediator _mediator;
        protected readonly IMapper _mapper;

        public AuthController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost("register")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(422)]
        public async Task<IActionResult> Register([FromBody] RegisterUserRequestDto request)
        {
            var cmd = new RegisterUserCommand(_mapper.Map<RegisterUserInputDto>(request));

            var result = await _mediator.Send(cmd);

            if (result.Success)
                return Ok(new { })
                      .WithSuccessIndicator(true);

            return UnprocessableEntity(new { }).WithMessage(result.Message ?? "Failed to register user.");
        }

        [HttpPost("login")]
        [ProducesResponseType(201, Type = typeof(IApiResponse<LoginUserResponseDto>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(422)]
        public async Task<IActionResult> Login([FromBody] LoginUserRequestDto request)
        {
            var cmd = new LoginUserCommand(_mapper.Map<LoginUserInputDto>(request));

            var result = await _mediator.Send(cmd);

            if (result.Success)
                return Ok(new LoginUserResponseDto() { JwtToken = result.Output?.AuthTicket?.JwtToken ?? string.Empty })
                      .WithSuccessIndicator(true);

            return UnprocessableEntity(new { }).WithMessage(result.Message ?? "Failed to authenticate.");
        }
    }
}

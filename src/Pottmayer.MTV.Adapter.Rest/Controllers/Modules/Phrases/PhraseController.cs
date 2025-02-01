using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pottmayer.MTV.Core.Domain.Modules.Phrases.Cqrs;
using Pottmayer.MTV.Core.Domain.Modules.Phrases.Dtos;
using Pottmayer.MTV.Core.Domain.Modules.Phrases.Dtos.Logic;
using Pottmayer.MTV.Core.Domain.Modules.Phrases.Dtos.Rest;
using Tars.Adapter.Rest.Controllers;
using Tars.Adapter.Rest.Extensions;
using System.Dynamic;

namespace Pottmayer.MTV.Adapter.Rest.Controllers.Modules.Phrases
{
    [ApiController]
    [Route("api/[controller]")]
    public class PhraseController : DefaultController
    {
        protected readonly IMediator _mediator;
        protected readonly IMapper _mapper;

        public PhraseController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [Authorize]
        [HttpGet("visible")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> GetAllVisible()
        {
            var cmd = new GetAllPhrasesCommand();
            var result = await _mediator.Send(cmd);

            return Ok(new GetAllPhrasesResponseDto() { Phrases = _mapper.Map<List<PhraseDto>>(result.Phrases.Where(p => p.IsVisible)) })
                  .WithSuccessIndicator(true);
        }

        [HttpGet("random")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> GetRandom()
        {
            var cmd = new GetRandomPhraseCommand();
            var result = await _mediator.Send(cmd);

            return Ok(new GetRandomPhraseResponseDto() { Phrase = _mapper.Map<PhraseDto>(result.Phrase) })
                  .WithSuccessIndicator(true);
        }

        [Authorize]
        [HttpPost]
        [ProducesResponseType(200)]
        public async Task<IActionResult> Create([FromBody] CreatePhraseRequestDto request)
        {
            var cmd = new CreatePhraseCommand(_mapper.Map<CreatePhraseInputDto>(request));
            var result = await _mediator.Send(cmd);

            if (result.Success)
                return Ok(new CreatePhraseResponseDto() { CreatedPhrase = _mapper.Map<PhraseDto>(result.CreatedPhrase) })
                      .WithSuccessIndicator(true);

            return UnprocessableEntity(new CreatePhraseResponseDto() { CreatedPhrase = null })
                  .WithMessage(string.Join("; ", result.Errors))
                  .WithSuccessIndicator(false);
        }

        [Authorize]
        [HttpPut("{phraseId}")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> Update(long phraseId, [FromBody] UpdatePhraseRequestDto request)
        {
            ExpandoObject? rawRequestExpando = await GetRawRequestBodyAsExpandoObject();

            var cmd = new UpdatePhraseCommand(new UpdatePhraseInputDto() { PhraseId = phraseId, PhraseData = rawRequestExpando });
            var result = await _mediator.Send(cmd);

            if (result.Success)
                return Ok(new { })
                      .WithSuccessIndicator(true);

            return UnprocessableEntity(new { })
                  .WithSuccessIndicator(false)
                  .WithMessage(result.ErrorMessage ?? string.Empty);
        }

        [Authorize]
        [HttpDelete("{phraseId}")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> Delete(long phraseId)
        {
            var cmd = new DeletePhraseCommand(new DeletePhraseInputDto() { PhraseId = phraseId });
            var result = await _mediator.Send(cmd);

            if (result.Success)
                return Ok(new { })
                      .WithSuccessIndicator(true);

            return UnprocessableEntity(new { })
                  .WithSuccessIndicator(false)
                  .WithMessage(result.ErrorMessage ?? string.Empty);
        }

        [Authorize]
        [HttpPost("loadFromOldMtvJson")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> LoadFromOldMtvJson([FromBody] LoadPhrasesFromOldMTVJsonRequestDto request)
        {
            var cmd = new LoadPhrasesFromOldMTVJsonCommand(new LoadPhrasesFromOldMTVJsonInputDto() { Phrases = request.Phrases });
            var result = await _mediator.Send(cmd);

            if (result.Success)
                return Ok(new { })
                      .WithSuccessIndicator(true);

            return UnprocessableEntity(new { })
                  .WithSuccessIndicator(false);
        }
    }
}

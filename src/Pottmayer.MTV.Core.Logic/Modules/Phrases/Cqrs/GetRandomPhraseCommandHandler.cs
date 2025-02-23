using MediatR;
using Pottmayer.MTV.Core.Domain.Modules.Phrases.Cqrs;
using Pottmayer.MTV.Core.Domain.Modules.Phrases.Dtos.Logic;
using Pottmayer.MTV.Core.Domain.Modules.Phrases.Entities;
using Tars.Contracts.Cqrs;
using Tars.Core.Cqrs;

namespace Pottmayer.MTV.Core.Logic.Modules.Phrases.Cqrs
{
    public class GetRandomPhraseCommandHandler : AbstractCommandHandler<GetRandomPhraseCommand, GetRandomPhraseOutputDto>, IGetRandomPhraseCommandHandler
    {
        protected readonly IMediator _mediator;

        public GetRandomPhraseCommandHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        protected override async Task<ICommandResult<GetRandomPhraseOutputDto>> HandleAsync(GetRandomPhraseCommand request, CancellationToken cancellationToken)
        {
            var getAllPhrasesCmd = new GetAllPhrasesCommand();
            ICommandResult<GetAllPhrasesOutputDto> getAllPhrasesCmdResult = await _mediator.Send(getAllPhrasesCmd);

            Phrase? randomPhrase = getAllPhrasesCmdResult.Output?.Phrases
                .Where(p => p.IsVisible == true)
                .OrderBy(p => Guid.NewGuid())
                .FirstOrDefault();

            return randomPhrase is not null
                ? Success(new GetRandomPhraseOutputDto() { Phrase = randomPhrase })
                : Fail(new GetRandomPhraseOutputDto() { });
        }
    }
}

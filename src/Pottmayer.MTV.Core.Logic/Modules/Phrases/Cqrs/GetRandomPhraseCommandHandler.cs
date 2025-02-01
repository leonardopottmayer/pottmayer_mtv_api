using MediatR;
using Pottmayer.MTV.Core.Domain.Modules.Phrases.Cqrs;
using Pottmayer.MTV.Core.Domain.Modules.Phrases.Dtos.Logic;
using Pottmayer.MTV.Core.Domain.Modules.Phrases.Entities;
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

        protected override async Task<GetRandomPhraseOutputDto> HandleAsync(GetRandomPhraseCommand request, CancellationToken cancellationToken)
        {
            var getAllPhrasesCmd = new GetAllPhrasesCommand();
            GetAllPhrasesOutputDto getAllPhrasesCmdResult = await _mediator.Send(getAllPhrasesCmd);

            Phrase? randomPhrase = getAllPhrasesCmdResult.Phrases
                .Where(p => p.IsVisible == true)
                .OrderBy(p => Guid.NewGuid())
                .FirstOrDefault();

            return new GetRandomPhraseOutputDto() { Success = randomPhrase is not null, Phrase = randomPhrase };
        }
    }
}

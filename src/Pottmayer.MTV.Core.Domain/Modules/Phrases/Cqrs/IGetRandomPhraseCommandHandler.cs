using Pottmayer.MTV.Core.Domain.Modules.Phrases.Dtos.Logic;
using Tars.Contracts.Cqrs;
using Tars.Core.Cqrs;

namespace Pottmayer.MTV.Core.Domain.Modules.Phrases.Cqrs
{
    public class GetRandomPhraseCommand : AbstractCommand<GetRandomPhraseOutputDto> { }

    public interface IGetRandomPhraseCommandHandler : ICommandHandler<GetRandomPhraseCommand, GetRandomPhraseOutputDto> { }
}

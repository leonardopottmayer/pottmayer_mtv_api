using Pottmayer.MTV.Core.Domain.Modules.Phrases.Dtos.Logic;
using Tars.Contracts.Cqrs;
using Tars.Core.Cqrs;

namespace Pottmayer.MTV.Core.Domain.Modules.Phrases.Cqrs
{
    public class CreatePhraseCommand : AbstractCommand<CreatePhraseInputDto, CreatePhraseOutputDto>
    {
        public CreatePhraseCommand(CreatePhraseInputDto input) : base(input) { }
    }

    public interface ICreatePhraseCommandHandler : ICommandHandler<CreatePhraseCommand, CreatePhraseOutputDto> { }
}

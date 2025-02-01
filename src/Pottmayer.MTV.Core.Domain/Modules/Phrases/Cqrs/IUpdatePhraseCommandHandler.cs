using Pottmayer.MTV.Core.Domain.Modules.Phrases.Dtos.Logic;
using Tars.Contracts.Cqrs;
using Tars.Core.Cqrs;

namespace Pottmayer.MTV.Core.Domain.Modules.Phrases.Cqrs
{
    public class UpdatePhraseCommand : AbstractCommand<UpdatePhraseInputDto, UpdatePhraseOutputDto>
    {
        public UpdatePhraseCommand(UpdatePhraseInputDto input) : base(input) { }
    }

    public interface IUpdatePhraseCommandHandler : ICommandHandler<UpdatePhraseCommand, UpdatePhraseOutputDto> { }
}

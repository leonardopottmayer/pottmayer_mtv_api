using Pottmayer.MTV.Core.Domain.Modules.Phrases.Dtos.Logic;
using Tars.Contracts.Cqrs;
using Tars.Core.Cqrs;

namespace Pottmayer.MTV.Core.Domain.Modules.Phrases.Cqrs
{
    public class DeletePhraseCommand : AbstractCommand<DeletePhraseInputDto, DeletePhraseOutputDto>
    {
        public DeletePhraseCommand(DeletePhraseInputDto input) : base(input) { }
    }

    public interface IDeletePhraseCommandHandler : ICommandHandler<DeletePhraseCommand, DeletePhraseOutputDto> { }
}

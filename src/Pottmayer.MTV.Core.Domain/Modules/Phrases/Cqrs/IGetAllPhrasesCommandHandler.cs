using Pottmayer.MTV.Core.Domain.Modules.Phrases.Dtos.Logic;
using Tars.Contracts.Cqrs;
using Tars.Core.Cqrs;

namespace Pottmayer.MTV.Core.Domain.Modules.Phrases.Cqrs
{
    public class GetAllPhrasesCommand : AbstractCommand<GetAllPhrasesOutputDto> { }

    public interface IGetAllPhrasesCommandHandler : ICommandHandler<GetAllPhrasesCommand, GetAllPhrasesOutputDto> { }
}

using Pottmayer.MTV.Core.Domain.Modules.Phrases.Dtos.Logic;
using Tars.Contracts.Cqrs;
using Tars.Core.Cqrs;

namespace Pottmayer.MTV.Core.Domain.Modules.Phrases.Cqrs
{
    public class LoadPhrasesFromOldMTVJsonCommand : AbstractCommand<LoadPhrasesFromOldMTVJsonInputDto, LoadPhrasesFromOldMTVJsonOutputDto>
    {
        public LoadPhrasesFromOldMTVJsonCommand(LoadPhrasesFromOldMTVJsonInputDto input) : base(input) { }
    }

    public interface ILoadPhrasesFromOldMTVJsonCommandHandler : ICommandHandler<LoadPhrasesFromOldMTVJsonCommand, LoadPhrasesFromOldMTVJsonOutputDto> { }
}

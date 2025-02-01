using Pottmayer.MTV.Core.Domain.Modules.Phrases.Dtos.Logic;
using Tars.Contracts;

namespace Pottmayer.MTV.Core.Domain.Modules.Phrases.Dtos.Rest
{
    public class LoadPhrasesFromOldMTVJsonRequestDto : IDataTransferObject
    {
        public List<LoadPhrasesFromOldMTVJsonItemDto> Phrases { get; set; } = new();
    }
}

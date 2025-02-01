using Tars.Contracts;

namespace Pottmayer.MTV.Core.Domain.Modules.Phrases.Dtos.Logic
{
    public class LoadPhrasesFromOldMTVJsonInputDto : IDataTransferObject
    {
        public List<LoadPhrasesFromOldMTVJsonItemDto> Phrases { get; set; } = new();
    }
}

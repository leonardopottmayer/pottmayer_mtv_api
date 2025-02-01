using Tars.Contracts;

namespace Pottmayer.MTV.Core.Domain.Modules.Phrases.Dtos.Logic
{
    public class LoadPhrasesFromOldMTVJsonItemDto : IDataTransferObject
    {
        public string Phrase { get; set; }
        public string Author { get; set; }
    }
}

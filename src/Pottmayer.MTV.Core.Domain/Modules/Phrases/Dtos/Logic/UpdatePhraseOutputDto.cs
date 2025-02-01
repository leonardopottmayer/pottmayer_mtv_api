using Tars.Contracts;

namespace Pottmayer.MTV.Core.Domain.Modules.Phrases.Dtos.Logic
{
    public class UpdatePhraseOutputDto : IDataTransferObject
    {
        public bool Success { get; set; }
        public string? ErrorMessage { get; set; }
    }
}

using Tars.Contracts;

namespace Pottmayer.MTV.Core.Domain.Modules.Phrases.Dtos.Logic
{
    public class DeletePhraseOutputDto : IDataTransferObject
    {
        public bool Success { get; set; }
        public string? ErrorMessage { get; set; }
    }
}

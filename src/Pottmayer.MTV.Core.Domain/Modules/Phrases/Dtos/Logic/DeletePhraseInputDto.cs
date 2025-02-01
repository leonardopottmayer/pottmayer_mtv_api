using Tars.Contracts;

namespace Pottmayer.MTV.Core.Domain.Modules.Phrases.Dtos.Logic
{
    public class DeletePhraseInputDto : IDataTransferObject
    {
        public long PhraseId { get; set; }
    }
}

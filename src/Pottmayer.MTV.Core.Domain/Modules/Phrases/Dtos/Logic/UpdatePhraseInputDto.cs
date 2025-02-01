using Tars.Contracts;
using System.Dynamic;

namespace Pottmayer.MTV.Core.Domain.Modules.Phrases.Dtos.Logic
{
    public class UpdatePhraseInputDto : IDataTransferObject
    {
        public long PhraseId { get; set; }
        public string? Description { get; set; }
        public string? Author { get; set; }
        public bool? IsVisible { get; set; }
        public ExpandoObject? PhraseData { get; set; }
    }
}

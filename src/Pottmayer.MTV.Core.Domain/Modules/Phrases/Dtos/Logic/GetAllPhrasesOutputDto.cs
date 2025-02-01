using Pottmayer.MTV.Core.Domain.Modules.Phrases.Entities;
using Tars.Contracts;

namespace Pottmayer.MTV.Core.Domain.Modules.Phrases.Dtos.Logic
{
    public class GetAllPhrasesOutputDto : IDataTransferObject
    {
        public List<Phrase> Phrases { get; set; } = new();
    }
}

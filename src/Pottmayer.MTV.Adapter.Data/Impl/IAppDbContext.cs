using Microsoft.EntityFrameworkCore;
using Pottmayer.MTV.Core.Domain.Modules.Phrases.Entities;
using Pottmayer.MTV.Core.Domain.Modules.Users.Entities;
using Tars.Contracts.Adapter.Data;

namespace Pottmayer.MTV.Adapter.Data.Impl
{
    public interface IAppDbContext : IBaseDbContext
    {
        DbSet<User> Users { get; set; }
        DbSet<Phrase> Phrases { get; set; }
    }
}

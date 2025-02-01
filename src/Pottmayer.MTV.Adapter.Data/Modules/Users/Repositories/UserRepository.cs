using Pottmayer.MTV.Core.Domain.Modules.Users.Entities;
using Pottmayer.MTV.Core.Domain.Modules.Users.Repositories;
using Tars.Adapter.Data.Impl;

namespace Pottmayer.MTV.Adapter.Data.Modules.Users.Repositories
{
    internal class UserRepository : AbstractStandardRepository<User>, IUserRepository
    {
        public UserRepository() { }
    }
}

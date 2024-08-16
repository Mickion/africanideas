using afi.university.domain.Repositories;
using afi.university.domain.Entities.Base;
using afi.university.infrastructure.Persistence;
using afi.university.infrastructure.Repositories.Base;

namespace afi.university.infrastructure.Repositories
{
    internal class UserRepository : BaseRepository<User, ApplicationDbContext>, IUserRepository
    {
        public UserRepository(ApplicationDbContext dbContext) : base(dbContext) { }

        public override async Task<User> GetByIdAsync(int userId, bool trackChanges)
        {
            var user = await GetByConditionAsync(c => c.Id.Equals(userId), trackChanges);
            return user!.SingleOrDefault(); //TODO: Deal with this
        }

        public async Task<User> GetUserLoginsAsync(string username, string password, bool trackChanges)
        {
            var user = await GetByConditionAsync(c => c.Email!.Equals(username) && c.Password!.Equals(password), trackChanges);
            return user!.SingleOrDefault(); //TODO: Deal with this

        }
    }
}

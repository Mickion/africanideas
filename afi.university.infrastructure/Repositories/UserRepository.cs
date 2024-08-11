using afi.university.domain.Entities;
using afi.university.domain.Repositories;
using afi.university.domain.Entities.Base;
using afi.university.infrastructure.Persistence;
using afi.university.infrastructure.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using afi.university.domain.Common.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace afi.university.infrastructure.Repositories
{
    public class UserRepository : BaseRepository<User, ApplicationDbContext>, IUserRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public UserRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            this._dbContext = dbContext;      
        }

        public override async Task<User> GetByIdAsync(int id)
        {
            var user = _dbContext.Users?
                .Where(cr => cr.Id.ToString() == id.ToString())                
                .FirstOrDefault();

            await Task.CompletedTask;
            return user ?? throw new NotFoundException($"User id ({id}) not found.");

        }

        public async Task<User> GetUserLoginsAsync(string username, string password)
        {
            var user = _dbContext.Users?
                .Where(cr => cr.Email == username && cr.Password == password)
                .FirstOrDefault();

            await Task.CompletedTask;
            return user ?? throw new NotFoundException($"User ({username}) not found.");
        }

        private void FeedDefaultAdmin()
        {

        }
    }
}

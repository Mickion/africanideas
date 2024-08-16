﻿using afi.university.domain.Entities.Base;
using afi.university.domain.Repositories.Base;

namespace afi.university.domain.Repositories
{
    public interface IUserRepository: IBaseRepository<User>
    {
        Task<User> GetUserLoginsAsync(string username, string password, bool trackChanges);
    }
}

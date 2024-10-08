﻿using afi.university.domain.Entities;
using afi.university.domain.Entities.Base;
using afi.university.domain.Repositories.Base;

namespace afi.university.domain.Repositories
{
    public interface IStudentRepository: IBaseRepository<Student>
    {
        Task<User> GetStudentByEmailAsync(string email, bool trackChanges);
    }
}

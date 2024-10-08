﻿using afi.university.domain.Entities;
using afi.university.domain.Entities.Base;
using afi.university.domain.Repositories;
using afi.university.infrastructure.Persistence;
using afi.university.infrastructure.Repositories.Base;

namespace afi.university.infrastructure.Repositories
{
    internal class StudentRepository : BaseRepository<Student, ApplicationDbContext>, IStudentRepository
    {
        public StudentRepository(ApplicationDbContext dbContext) : base(dbContext) { }

        public override async Task<Student> GetByIdAsync(Guid studentId, bool trackChanges)
        {
            var students = await GetByConditionAsync(c => c.Id.Equals(studentId), trackChanges);
            return students.SingleOrDefault();
        }

        public async Task<User> GetStudentByEmailAsync(string email, bool trackChanges)
        {
            var students = await GetByConditionAsync(c => c.Id.Equals(email), trackChanges);
            return students.SingleOrDefault();
        }
    }
}

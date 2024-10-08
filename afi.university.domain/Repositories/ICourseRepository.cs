﻿using afi.university.domain.Entities;
using afi.university.domain.Repositories.Base;

namespace afi.university.domain.Repositories
{
    public interface ICourseRepository: IBaseRepository<Course>
    {
        Task<Course> GetCourseByNameAsync(string courseName, bool trackChanges);
    }
}

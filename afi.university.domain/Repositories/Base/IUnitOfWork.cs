﻿namespace afi.university.domain.Repositories.Base
{
    public interface IUnitOfWork
    {
        IUserRepository Users { get; }

        IStudentRepository Students { get; }

        ICourseRepository Courses { get; }

        ILectureRepository Lectures { get; }

        IStudentCourseRepository StudentCourses { get; }

        void SaveChangesAsync();
    }
}

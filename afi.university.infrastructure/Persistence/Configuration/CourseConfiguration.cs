using Microsoft.EntityFrameworkCore;
using afi.university.domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace afi.university.infrastructure.Persistence.Configuration
{
    internal class CourseConfiguration : IEntityTypeConfiguration<Course>
    {
        /// <summary>
        /// Feeds course test data
        /// </summary>
        /// <param name="builder"></param>
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.HasData(
                new Course
                {
                    Id= new Guid(),
                    Name = "BSC Bachelor Of Computer Science",
                    NQFLevel = 7,
                    Duration = 4
                },
                new Course
                {
                    Id = new Guid(),
                    Name = "National Diploma Information Technology",
                    NQFLevel = 6,
                    Duration = 3
                },
                new Course
                {
                    Id = new Guid(),
                    Name = "BSC Chemical engineering",
                    NQFLevel = 7,
                    Duration = 5
                },
                new Course
                {
                    Id = new Guid(),
                    Name = "National Diploma Civil engineering",
                    NQFLevel = 6,
                    Duration = 5
                }
            );

        }
    }
}

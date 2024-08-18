using afi.university.domain.Entities;
using afi.university.domain.Repositories;
using afi.university.infrastructure.Persistence;
using afi.university.infrastructure.Repositories.Base;

namespace afi.university.infrastructure.Repositories
{
    internal class LectureRepository : BaseRepository<Lecture, ApplicationDbContext>, ILectureRepository
    {
        public LectureRepository(ApplicationDbContext dbContext) : base(dbContext) { }

        public async override Task<Lecture> GetByIdAsync(Guid lectureId, bool trackChanges)
        {
            var lectures = await GetByConditionAsync(c => c.Id.Equals(lectureId), trackChanges);
            return lectures.SingleOrDefault();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace afi.university.shared.DataTransferObjects.Requests
{
    public record CourseRegistrationRequest (Guid StudentId, Guid CourseId)
    {
    }
}

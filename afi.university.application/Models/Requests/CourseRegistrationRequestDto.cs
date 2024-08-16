using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace afi.university.application.Models.Requests
{
    public class CourseRegistrationRequestDto
    {
        public Guid StudentId { get; set; }

        public string? Name { get; set; }

        public int Duration { get; set; }
    }
}

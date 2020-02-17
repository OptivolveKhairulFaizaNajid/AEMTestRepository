using System;
using System.Collections.Generic;

namespace ConsoleApp_AEM_TestDemo.Models
{
    public partial class PlatformDto
    {
        public PlatformDto()
        {
            Well = new HashSet<WellDto>();
        }

        public int Id { get; set; }
        public string UniqueName { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public virtual ICollection<WellDto> Well { get; set; }
    }
}

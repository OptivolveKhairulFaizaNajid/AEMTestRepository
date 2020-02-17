using System;
using System.Collections.Generic;

namespace ConsoleApp_AEM_TestDemo.Models
{
    public partial class WellDto
    {
        public int Id { get; set; }
        public int PlatformId { get; set; }
        public string UniqueName { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public virtual PlatformDto Platform { get; set; }
    }
}

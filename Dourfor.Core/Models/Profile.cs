using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dourfor.Core.Models
{
    public class Profile
    {
        public long Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public int Age { get; set; } 
        public DateTime UpdatedAt { get; set; }
        public bool IsActivate { get; set; }
        public string UserId { get; set; }
    }
}

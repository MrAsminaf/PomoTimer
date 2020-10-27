using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace PomoTimer.Models
{
    public class ApplicationUser : IdentityUser
    {
        public List<TimeModel> Minutes { get; set; } = new List<TimeModel>();
    }
}
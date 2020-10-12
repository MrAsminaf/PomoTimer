using Microsoft.AspNetCore.Identity;
using System;

namespace PomoTimer.Models
{
    public class ApplicationUser : IdentityUser
    {
        public TimeSpan TimeSpan { get; set; } = new TimeSpan();
    }
}
﻿using System;
using System.ComponentModel.DataAnnotations;

namespace PomoTimer.Models
{
    public class TimeModel
    {
        [Key]
        public string TimeModelId { get; set; }
        public string ApplicationUserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
        public DateTime DateTime { get; set; }
        public int Minutes { get; set; } = 0;
        public string TaskName { get; set; }
    }
}

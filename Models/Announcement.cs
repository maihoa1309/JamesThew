﻿using Project3.Models;

namespace Project3.Models
{
    public class Announcement : Base
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Type { get; set; }
        public string? Status { get; set; }
    }
}

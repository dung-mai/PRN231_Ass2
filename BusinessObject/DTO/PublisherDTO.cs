﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.DTO
{
    public class PublisherDTO
    {
        public int PubId { get; set; }
        public string PublisherName { get; set; } = null!;
        public string? City { get; set; }
        public string? State { get; set; }
        public string? Country { get; set; }
    }
}

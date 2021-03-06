﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PerformanceCalculator.Models
{
    public class Course : BaseModel
    {
        [Required]
        [MaxLength(10), MinLength(5)]
        public string Code { get; set; }
        
        [Required]
        [MaxLength(250)]
        public string Title { get; set; }
        [Required]
        [Range(1, 4)]
        public decimal Credit { get; set; }
        [Required]
        public Guid TeacherId { get; set; }
        public Teacher Teacher { get; set; }
    }
}
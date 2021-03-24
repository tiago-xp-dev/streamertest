using System.ComponentModel.DataAnnotations;
using SS_API.Enums;

namespace SS_API.Model
{
    public class Project
    {
        [Key]
        public int Id { get; set; }

        public string Image { get; set;}

        public string Name { get; set; }

        public string Why { get; set; }

        public string WhatWillWeDo { get; set; }

        public string What { get; set; }

        public ProjectStatus ProjectStatus { get; set; }

        public Course Course { get; set; }

        public int CourseId { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Rubicon_Task.Models
{
    public class postBlog
    {
        [Key]
        public string slug { get; set; }
        [Required]
        public string title { get; set; }
        [Required]
        public string description { get; set; }
        [Required]
        public string body { get; set; }
        public string[] tagList { get; set; }

    }
}
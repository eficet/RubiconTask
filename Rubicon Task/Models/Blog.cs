using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Rubicon_Task.Models
{
    public class Blog
    {

        [Key]
        public string slug { get; set; }
        [Required]
        public string title { get; set; }
        [Required]
        public string description { get; set; }
        [Required]
        public string body { get; set; }

       
        public DateTime? createdAt { get; set; }
        public DateTime? updatedAt { get; set; }

        public List<Tag> tagList { get; set; }

    }
}
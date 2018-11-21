using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Rubicon_Task.Models
{
    public class Tag
    {
        public int tagId { get; set; }
        public string tagName { get; set; }
        public List<Blog> blogs { get; set; }
    }
}
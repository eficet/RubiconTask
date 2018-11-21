using Rubicon_Task.Models.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Rubicon_Task.Controllers
{
    public class tagsController : ApiController
    {
        private BlogDb db = new BlogDb();

        public IHttpActionResult GetTags()
        {
            try { 
            var tags = db.tags.Select(t => t.tagName).ToList();
            
            var _tags = new List<Object>();
            _tags.Add(new {tags=tags});

            return Ok(_tags);
            }
            catch
            {
               return NotFound();
            }
        }
    }
}

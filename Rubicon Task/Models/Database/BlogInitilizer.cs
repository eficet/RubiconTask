using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Rubicon_Task.Models.Database
{
    public class BlogInitilizer: DropCreateDatabaseAlways<BlogDb>
    {
        protected override void Seed(BlogDb context)
        {
            var tags1 = new List<Tag> {

                new Tag { tagName = "AngularJs" },
                new Tag {tagName="React"},
                new Tag {tagName="heat"}
            };
            var tags2 = new List<Tag> {

                new Tag { tagName = "Asp.net" },
                new Tag {tagName="MomentJs"}
            };

            List<Blog> blogIt = new List<Blog>()
            {
                new Blog{slug="hej",title="hej",body="my name is fayiz",description="lets do it",tagList=tags1,createdAt=DateTime.UtcNow},
                new Blog{slug="hej-2",title="hej 1",body="this is somthing else",description="lets do it",tagList=tags2,createdAt=DateTime.UtcNow},
                new Blog{slug="hej-3",title="hej 3",body="i m done with this app",description="lets do it",tagList=tags1,createdAt=DateTime.UtcNow},
                new Blog{slug="hej-4",title="hej 4",body="i have to go home",description="lets do it",tagList=tags2,createdAt=DateTime.UtcNow},
                new Blog{slug="hej-5",title="hej 5",body="its crowded city",description="lets do it",tagList=tags1,createdAt=DateTime.UtcNow}

            };
           
            foreach(var s in blogIt)
            {
                context.blogs.Add(s);
            }

            context.SaveChanges();
            
        }
    }
}
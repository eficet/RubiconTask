using Rubicon_Task.Models;
using Rubicon_Task.Models.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Web.Http;
using System.Data.Entity;
using System.ComponentModel;

namespace Rubicon_Task.Controllers
{
    public class BlogController : ApiController
    {
        private BlogDb db = new BlogDb();
        private CheckSlug helper = new CheckSlug();

        public IHttpActionResult GetBlogs()
        {
            try
            {
                // create an anonymous type that allows me to choose the desired coulumns
                var myBlogs = db.blogs.Select(s => new
                {
                    slug = s.slug,
                    title = s.title,
                    discription = s.description,
                    budy = s.body,
                    tagLists = s.tagList.Select(t => t.tagName),
                    createdAt = s.createdAt,
                    updatedAt = s.updatedAt

                }).ToList();

                //get the nubmer of blogs in my DB
                var count = db.blogs.Count();
                //create List on anonymous object to hold my blogs and the count int
                var obj = new List<object>();
                obj.Add(new { blogPosts = myBlogs, postsCount = count });

                return Ok(obj);
            }
            catch
            {
                return NotFound();
            }
        }

        public IHttpActionResult GetBlog(string id)
        {
            try
            {
                var blogs = db.blogs.Where(a => a.slug == id);
                var _blogs = blogs.FirstOrDefault();
                if (_blogs != null)
                {
                    blogs.Select(b => new
                    {
                        blogPost = new
                        {
                            slug = b.slug,
                            title = b.title,
                            discription = b.description,
                            budy = b.body,
                            tagLists = b.tagList.Select(t => t.tagName)

                        }




                    }).ToList();
                    return Ok(_blogs);
                }
                else
                {
                    return BadRequest("The Slug does not Exist..please put and existing one");
                }

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        public IHttpActionResult PostBlog([FromBody]postBlog blog)
        {



            //declared a an an object of type Blog to fill it with the values from the request body object
            Blog post = new Blog();

            post.createdAt = DateTime.Now.ToUniversalTime();

            // call the checkMySlug method to check the slug and change it to the desired format

            blog.slug = helper.checkMySlug(blog.title);

            var tags = blog.tagList;
            var tags2 = new List<Tag>();
            try
            {
                if (tags != null)
                {
                    foreach (var tag in tags)
                    {
                        //checking if the tag exists in the database and if not it returns null not exeption
                        var comTag = db.tags.FirstOrDefault(t => t.tagName == tag);
                        if (comTag == null)
                        {
                            //add the new tag to the list - to be added after with its blog to the database
                            tags2.Add(new Tag { tagName = tag });
                        }
                        else
                        {
                            //connect my tag to an existing tag in the db
                            tags2.Add(comTag);
                        }
                    }
                }

                post.slug = helper.existingSlug(blog.slug);
                //fill my Blog object with data from its correspondent from the request body object 
                post.title = blog.title;
                post.description = blog.description;
                post.body = blog.body;
                post.tagList = tags2;
                db.blogs.Add(post);
                db.SaveChanges();

                return CreatedAtRoute("DefaultApi", new { slug = blog.slug }, post);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }



        public IHttpActionResult PutBlog([FromUri]string id, [FromBody]Blog blog)
        {
            var data = db.blogs.Include(t => t.tagList).FirstOrDefault(f => f.slug == id);

            //checking if there is a blog of the given slug
            if (data != null)
            {
                try
                {
                    // if the title is given in the request body has value 
                    if (blog.title != null)
                    {

                        //assigning a value to the slug after checking the blog title format and filter it
                        blog.slug = helper.checkMySlug(blog.title);
                        if (db.blogs.FirstOrDefault(t => t.slug == blog.slug) != null)
                        {
                            blog.slug = helper.existingSlug(blog.slug);
                        }

                        if (blog.description == null)
                        {
                            blog.description = data.description;
                        }
                        if (blog.body == null)
                        {
                            blog.body = blog.body;
                        }

                        //store the data from the old tag list in a list of tags
                        List<Tag> myTag = new List<Tag>();
                        foreach (var tag in data.tagList)
                        {
                            myTag.Add(new Tag { tagName = tag.tagName });
                        }
                        // saving the old Data in the new taglist
                        blog.tagList = myTag;

                        blog.createdAt = data.createdAt;
                        blog.updatedAt = DateTime.Now.ToUniversalTime();


                        // adding the updated blog to the database
                        db.blogs.Add(blog);
                        //deleting the old blog from the database
                        db.blogs.Remove(data);
                        db.SaveChanges();
                        return CreatedAtRoute("DefaultApi", new { slug = blog.slug }, blog);

                    }
                    else
                    {
                        //else if the title is not given then check if the description is given
                        //then assign the values if they exist and save them to the database
                        if (blog.description != null)
                        {
                            data.description = blog.description;
                        }
                        if (blog.body != null)
                        {
                            data.body = blog.body;
                        }
                        data.updatedAt = DateTime.Now.ToUniversalTime();

                        db.SaveChanges();
                        return CreatedAtRoute("DefaultApi", new { slug = blog.slug }, data);
                    }
                }
                catch (Exception e)
                {
                    return BadRequest(e.Message);
                }


            }
            else
            {
                return BadRequest("The Slug u entered is wrong");
            }

        }

        public IHttpActionResult DeleteBlog(string id)
        {
            try
            {


                var myBlog = db.blogs.Find(id);
                db.blogs.Remove(myBlog);
                db.SaveChanges();

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}

using Rubicon_Task.Models.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace Rubicon_Task.Models
{
    public class CheckSlug
    {
        private BlogDb db = new BlogDb();
        public string checkMySlug(string slug)
        {
            var mySlug = slug.ToLower();
            //reolace white space with -
            mySlug = mySlug.Replace(" ", "-");

            //replacing accented chars to English ones
            mySlug = mySlug.Replace("č", "c");
            mySlug = mySlug.Replace("š", "sh");
            mySlug = mySlug.Replace("đ", "j");
            mySlug = mySlug.Replace("ć", "c");
            mySlug = mySlug.Replace("ž", "z");

            // using regex to replace all unwanted chars other than letters and numbers to a white space
            mySlug = Regex.Replace(mySlug, @"[^a-z0-9\s-]", " ");

            //replacing the white space
            mySlug = Regex.Replace(mySlug, @"\s+", "");
            //replacing duplicated - to only one
            mySlug = Regex.Replace(mySlug, @"-+", "-");

            return mySlug;
        }

        public string existingSlug(string slug)
        {
            string returnSlug = null;
            int b = db.blogs.Count();
            var perm_slug = slug;
            // loop through the number of blogs that we have
            for (int i = 1; i < b; i++)
            {
                //check in the database if the slug is avaliable
                var perm = db.blogs.FirstOrDefault(t => t.slug == perm_slug);
                // if the db has slug with the same name
                if (perm != null)
                {
                    //add number to the slug
                    perm_slug = slug + "-" + i.ToString();

                    continue;

                }
                else
                {
                    //save the unique slug to be returned
                    returnSlug = perm_slug;
                    break;   
                }
                
            }
            return perm_slug;
        }
    }
}
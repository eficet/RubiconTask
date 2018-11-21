using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Rubicon_Task.Models.Database
{
    public class BlogDb : DbContext
    {
        // telling which connection string should be used
        public BlogDb() : base ("MyDatabase"){}
        
        // creating tables from the wanted class
        public DbSet<Blog> blogs { set; get;}
        public DbSet<Tag>  tags { set; get; }

        //Fluent API to set the relationship between the two tables and set tha names of the Bridge table and its keys
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Blog>()
                .HasMany(b => b.tagList)
                .WithMany(t => t.blogs)
                .Map(bt => {
                    bt.ToTable("BlogTags");
                    bt.MapLeftKey("blogID");
                    bt.MapRightKey("tagId");
                });

            base.OnModelCreating(modelBuilder);
        }

    }
}
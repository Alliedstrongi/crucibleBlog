﻿using crucibleBlog.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace crucibleBlog.Data
{
    public class ApplicationDbContext : IdentityDbContext<BlogUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

		public virtual DbSet<BlogLike> BlogLikes { get; set; } = default!;
		public virtual DbSet<BlogUser> BlogUsers { get; set; } = default!;
        public virtual DbSet<Category> Categories { get; set; } = default!;
        public virtual DbSet<Comment> Comments { get; set; } = default!;
        public virtual DbSet<Tag> Tags { get; set; } = default!;
        public DbSet<crucibleBlog.Models.BlogPost> BlogPost { get; set; } = default!;
    }
}
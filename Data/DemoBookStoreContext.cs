using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DemoBookStore.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace DemoBookStore.Data
{
    public class DemoBookStoreContext : IdentityDbContext<UserModel>
    {
        public DemoBookStoreContext (DbContextOptions<DemoBookStoreContext> options)
            : base(options)
        {
        }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<UserModel>().ToTable("AspNetUsers");
            modelBuilder.Entity<AuthorModel>().ToTable("Authors");
		}

		public DbSet<DemoBookStore.Models.BookModel> BookModel { get; set; } = default!;
        public DbSet<DemoBookStore.Models.AuthorModel> AuthorModel { get; set; } = default!;
        public DbSet<DemoBookStore.Models.UserModel> UserModel { get; set; } = default!;
        public DbSet<OrderModel> Orders { get; set; } = default;
        public DbSet<ReviewModel> ReviewModel { get; set; }

    }
}

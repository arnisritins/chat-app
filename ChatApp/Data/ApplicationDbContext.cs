using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using ChatApp.Data.Entities;

namespace ChatApp.Data
{
    public class ApplicationDbContext : IdentityDbContext<User, Role, int>
    {
        /// <summary>
        /// Construct DB context
        /// </summary>
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// Configure mappings
        /// </summary>
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Map Identity table names
            builder.Entity<User>(e => { e.ToTable("tUser"); });
            builder.Entity<Role>(e => { e.ToTable(name: "tRole"); });
            builder.Entity<IdentityUserRole<int>>(e => { e.ToTable("tUserRole"); });
            builder.Entity<IdentityUserClaim<int>>(e => { e.ToTable("tUserClaim"); });
            builder.Entity<IdentityUserLogin<int>>(e => { e.ToTable("tUserLogin"); });
            builder.Entity<IdentityUserToken<int>>(e => { e.ToTable("tUserToken"); });
            builder.Entity<IdentityRoleClaim<int>>(e => { e.ToTable("tRoleClaim"); });
        }
    }
}

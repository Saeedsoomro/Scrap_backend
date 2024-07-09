using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace WebApplication1.Data
{
    public class ScrapMangementAuthDbContext : IdentityDbContext
    {
        public ScrapMangementAuthDbContext(DbContextOptions<ScrapMangementAuthDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);


            var userId = "5fb9ac07-66bc-410a-904c-f1250571c298";
                var scaperId = "abea919e-42cb-4999-9205-2c0a2774206d";
            var superadminId = "cc269023-daf1-40bf-aca2-ceafcb0685ce";

            var roles = new List<IdentityRole>
                {
                    new IdentityRole
                    {
                        Id = userId,
                        ConcurrencyStamp = userId,
                        Name = "User",
                        NormalizedName = "user".ToUpper(),

                    },
                    new IdentityRole
                    {
                        Id = scaperId,
                        ConcurrencyStamp = scaperId,
                        Name = "Scrapper",
                        NormalizedName = "Scrapper".ToUpper(),

                    },
                    new IdentityRole
                    {
                        Id = superadminId,
                        ConcurrencyStamp = superadminId,
                        Name = "Super Admin",
                        NormalizedName = "superadmin".ToUpper(),

                    },
                };

                builder.Entity<IdentityRole>().HasData(roles);

        }
    }
}

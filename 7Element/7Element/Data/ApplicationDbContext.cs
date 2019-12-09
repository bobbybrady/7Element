using System;
using System.Collections.Generic;
using System.Text;
using _7Element.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace _7Element.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
            : base(options) { }
             public DbSet<ApplicationUser> ApplicationUsers { get; set; }
    }
}

﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace WebApplication4.Models
{
    public class AppDbContext : IdentityDbContext<AppUser>         
    {
        public AppDbContext(DbContextOptions<AppDbContext>options) : base(options)
        {
            
        }
    }
}

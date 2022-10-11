using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ServerApp.Models;

namespace ServerApp.Data
{
    public class SocialContext: DbContext
    {
        public SocialContext(DbContextOptions<SocialContext> options):base(options)
        {
            
        }

        public DbSet<Product> Products {get; set;}
    }
}
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository.Data
{
    public class GroceryDbContext : IdentityDbContext
    {
        public GroceryDbContext()
        {

        }
        public GroceryDbContext(DbContextOptions<GroceryDbContext> options) : base(options)
        {
            
        }

        public IList<Fruits> LoadGroceries()
        {
            IList<Fruits> fruits = new List<Fruits> {
            new Fruits() { Id = 1, Name = "Apple" },
            new Fruits() { Id = 2, Name = "Orange" },
            new Fruits() { Id = 3, Name = "Grape" }
        };
            return fruits;
        }
    }
}

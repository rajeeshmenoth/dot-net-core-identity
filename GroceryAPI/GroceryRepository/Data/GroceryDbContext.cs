using Microsoft.EntityFrameworkCore;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository.Data
{
    public class GroceryDbContext : DbContext
    {
        public GroceryDbContext(DbContextOptions<GroceryDbContext> options) : base(options)
        {
            LoadGroceries();
        }
        public DbSet<Fruits> Fruits { get; set; }

        public void LoadGroceries()
        {
            Fruits fruits = new Fruits() { Id = 1, Name = "Apple" };
            Fruits.Add(fruits);
            fruits = new Fruits() { Id = 2, Name = "Orange" };
            Fruits.Add(fruits);
            fruits = new Fruits() { Id = 3, Name = "Grape" };
            Fruits.Add(fruits);
        }

        public List<Fruits> GetGroceries()
        {
            return Fruits.Local.ToList<Fruits>();
        }
    }
}

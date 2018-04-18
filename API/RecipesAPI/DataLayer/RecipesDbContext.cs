using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer
{
    public class RecipesDbContext:DbContext
    {
      

        public DbSet<Recipe> Recipes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL("server=recipesdb.chzseohsuonx.ap-southeast-2.rds.amazonaws.com;port=3306;database=recipes;uid=dbUser;password=Wh2V0pmDj6GqjthYz42o");
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Recipe>(r => r.ToTable("Recipes"));
            base.OnModelCreating(modelBuilder);
        }
    }
}

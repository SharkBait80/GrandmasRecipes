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

        static string _ConnString;

        static string ConnString
        {
            get{
                if (!string.IsNullOrEmpty(_ConnString))
                    return _ConnString;
                else
                {
                    // Contact the SSM parameter store to get the values
                    Amazon.SimpleSystemsManagement.AmazonSimpleSystemsManagementClient simpleSystemsManagementClient = new Amazon.SimpleSystemsManagement.AmazonSimpleSystemsManagementClient();

                    var request = new Amazon.SimpleSystemsManagement.Model.GetParametersRequest();
                    request.Names = new List<string>();
                    request.Names.Add("/Dev/RDS/RecipesDb/ConnString");
                    var response=simpleSystemsManagementClient.GetParametersAsync(request).Result;
                    if (response!=null && response.Parameters!= null && response.Parameters.Count > 0)
                        return response.Parameters[0].Value;
                    else
                        return null;
                }
            }
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL(ConnString);
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Recipe>(r => r.ToTable("Recipes"));
            base.OnModelCreating(modelBuilder);
        }
    }
}

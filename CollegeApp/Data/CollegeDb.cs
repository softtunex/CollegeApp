using CollegeApp.Data.Config;
using Microsoft.EntityFrameworkCore;

namespace CollegeApp.Data
{
    public class CollegeDb:DbContext
    {
        public CollegeDb(DbContextOptions<CollegeDb> options):base(options) 
        {
            
        }
        public DbSet<Student> Students{  get; set; }
       
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Student Table
            modelBuilder.ApplyConfiguration(new StudentConfig());

            
        }
    
    
    }
}

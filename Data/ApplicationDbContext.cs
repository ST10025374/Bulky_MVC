using BulkyWeb.Models; // Access to model classes, such as Category
using Microsoft.EntityFrameworkCore; // Access to EF Core functionality

namespace BulkyWeb.Data
{  
    // Definition of ApplicationDbContext class that inherits from DbContext (EF Core's base class for working with a database)
    public class ApplicationDbContext : DbContext
    {
        ///---------------------------------------------------------------------------------------------///
        /// <summary>
        /// Constructor that accepts database context options and passes them to the base DbContext class
        /// </summary>
        /// <param name="options"></param>
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }

        ///---------------------------------------------------------------------------------------------///
        /// <summary>
        /// DbSet property for Categories, representing a table in the database
        /// This enables CRUD operations on Category entities
        /// </summary>
        public DbSet<Category> Categories { get; set; }

        ///---------------------------------------------------------------------------------------------///
        /// <summary>
        /// Overriding the OnModelCreating method to configure the model that EF Core uses to create the database
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuring the Category entity
            modelBuilder.Entity<Category>().HasData(
                // Seeding the Categories table with initial data
                // Each 'new Category' instance represents a row in the table
                new Category { Id = 1, Name = "Action", DisplayOrder = 1 }, // First category with ID 1, Name "Action", and Display Order 1
                new Category { Id = 2, Name = "SciFi", DisplayOrder = 2 }, // Second category with ID 2, Name "SciFi", and Display Order 2
                new Category { Id = 3, Name = "History", DisplayOrder = 3 } // Third category with ID 3, Name "History", and Display Order 3
            );
        }
    }
}
///-----------------------------------------------------------------< END >-----------------------------------------------------------------///
// Using directives to include necessary namespaces
using BulkyWeb.Models; // Access to model classes, such as Category
using Microsoft.EntityFrameworkCore; // Access to EF Core functionality

// Define the namespace for the current class, structuring the code within the BulkyWeb.Data area
namespace BulkyWeb.Data
{
    // Definition of ApplicationDbContext class that inherits from DbContext (EF Core's base class for working with a database)
    public class ApplicationDbContext : DbContext
    {
        // Constructor that accepts database context options and passes them to the base DbContext class
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            // The constructor body is empty because the base class handles the options
        }

        // DbSet property for Categories, representing a table in the database
        // This enables CRUD operations on Category entities
        public DbSet<Category> Categories { get; set; }

        // Overriding the OnModelCreating method to configure the model that EF Core uses to create the database
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

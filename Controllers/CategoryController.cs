// Including necessary namespaces for the functionality required in this controller
using BulkyWeb.Data; // Access to the application's DbContext (ApplicationDbContext)
using BulkyWeb.Models; // Access to the application's model classes, such as Category
using Microsoft.AspNetCore.Mvc; // Base namespace for MVC controller functionalities

// Define the namespace for the current class, structuring the code within the BulkyWeb.Controllers area
namespace BulkyWeb.Controllers
{
    // CategoryController class inheriting from Controller, making it an MVC controller
    public class CategoryController : Controller
    {
        // Field to hold the ApplicationDbContext instance
        private readonly ApplicationDbContext _db;

        // Constructor that accepts an ApplicationDbContext to enable database operations within this controller
        public CategoryController(ApplicationDbContext db)
        {
            _db = db; // Assigning the passed-in ApplicationDbContext to the private field
        }

        // Action method for handling requests to /Category/Index
        // Returns a view displaying a list of categories
        public IActionResult Index()
        {
            // Retrieves all Category entities from the database and stores them in a list
            List<Category> objCategoryList = _db.Categories.ToList();
            // Returns the view associated with this action, passing the list of categories as a model
            return View(objCategoryList);
        }

        // GET action method for displaying the category creation form
        public IActionResult Create()
        {
            return View(); // Returns the view associated with the Create action
        }

        // POST action method for handling the submission of the category creation form
        [HttpPost]
        public IActionResult Create(Category obj)
        {
            // Custom validation: checks if the Category's Name matches its DisplayOrder
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                // Adds a model error if the validation fails
                ModelState.AddModelError("name", "The DisplayOrder cannot exactly match the Name. ");
            }

            // Checks if the model state is valid after the custom validation
            if (ModelState.IsValid)
            {
                // Adds the new Category to the DbContext and saves changes to the database
                _db.Categories.Add(obj);
                _db.SaveChanges(); // Commits the changes to the database
                return RedirectToAction("Index"); // Redirects to the Index action after successful creation
            }
            return View(); // Returns the same view for corrections if the model state is invalid
        }

        // GET action method for displaying the category edit form for a specific category
        public IActionResult Edit(int? id)
        {
            // Validation to ensure an ID is provided
            if (id == null || id == 0)
            {
                return NotFound(); // Returns a NotFound result if no ID is provided
            }

            // Retrieves the Category entity by ID from the database
            Category? categoryFromDb = _db.Categories.Find(id);

            // Additional commented-out examples of retrieving a category entity
            //Category? categoryFromDb1 = _db.Categories.FirstOrDefault(u=>u.Id==id);
            //Category? categoryFromDb2 = _db.Categories.Where(u=>u.Id==id).FirstOrDefault();

            if (categoryFromDb == null)
            {
                return NotFound(); // Returns a NotFound result if no category is found with the provided ID
            }
            return View(categoryFromDb); // Returns the view associated with this action, passing the found category as a model
        }

        // POST action method for handling the submission of the category edit form
        [HttpPost]
        public IActionResult Edit(Category obj)
        {
            // Custom validation: checks if the Category's Name matches its DisplayOrder
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                // Adds a model error if the validation fails
                ModelState.AddModelError("name", "The DisplayOrder cannot exactly match the Name. ");
            }

            // Checks if the model state is valid after the custom validation
            if (ModelState.IsValid)
            {
                // Updates the existing Category in the DbContext and saves changes to the database
                _db.Categories.Update(obj);
                _db.SaveChanges(); // Commits the changes to the database
                return RedirectToAction("Index"); // Redirects to the Index action after successful update
            }
            return View(); // Returns the same view for corrections if the model state is invalid
        }
    }
}

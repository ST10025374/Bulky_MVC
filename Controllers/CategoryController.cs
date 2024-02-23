using BulkyWeb.Data; 
using BulkyWeb.Models; 
using Microsoft.AspNetCore.Mvc; 

namespace BulkyWeb.Controllers
{
    public class CategoryController : Controller
    {
        /// <summary>
        /// Field to hold the ApplicationDbContext instance
        /// </summary>
        private readonly ApplicationDbContext _db;

        //---------------------------------------------------------------------//
        /// <summary>
        /// Constructor that accepts an ApplicationDbContext to enable database operations within this controller
        /// Assigning the passed-in ApplicationDbContext to the private field
        /// </summary>
        /// <param name="db"></param>
        public CategoryController(ApplicationDbContext db)
        {
            _db = db;                                                   
        }

        //---------------------------------------------------------------------//
        /// <summary>
        /// Action method for handling requests to /Category/Index
        /// Returns a view displaying a list of categories
        /// Retrieves all Category entities from the database and stores them in a list  
        /// Returns the view associated with this action, passing the list of categories as a model
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {        
            List<Category> objCategoryList = _db.Categories.ToList();            
            return View(objCategoryList);                               
        }

        //---------------------------------------------------------------------//
        /// <summary>
        /// GET action method for displaying the category creation form
        /// Returns the view associated with the Create action
        /// </summary>
        /// <returns></returns>
        public IActionResult Create()
        {
            return View();
        }

        //---------------------------------------------------------------------//
        /// <summary>
        /// POST action method for handling the submission of the category creation form
        /// Custom validation: checks if the Category's Name matches its DisplayOrder
        /// Adds a model error if the validation fails
        /// Checks if the model state is valid after the custom validation
        /// Adds the new Category to the DbContext and saves changes to the database
        /// Commits the changes to the database
        /// Adds a success message to TempData
        /// Redirects to the Index action after successful creation
        /// Returns the same view for corrections if the model state is invalid
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        [HttpPost]                                                              
        public IActionResult Create(Category obj)
        {        
            if (obj.Name == obj.DisplayOrder.ToString())// 
            {
                ModelState.AddModelError("name", "The DisplayOrder cannot exactly match the Name. ");
            }

            if (ModelState.IsValid)
            {
                _db.Categories.Add(obj);
                _db.SaveChanges(); 
                TempData["success"] = "Category created successfully"; 
                return RedirectToAction("Index"); 
            }
            return View(); 
        }

        //---------------------------------------------------------------------//
        /// <summary>
        /// GET action method for displaying the category edit form for a specific category
        /// Validation to ensure an ID is provided
        /// Returns a NotFound result if no ID is provided
        /// Retrieves the Category entity by ID from the database
        /// Returns a NotFound result if no category is found with the provided ID
        /// Returns the view associated with this action, passing the found category as a model
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound(); 
            }

            Category? categoryFromDb = _db.Categories.Find(id);

            // Additional commented-out examples of retrieving a category entity
            //Category? categoryFromDb1 = _db.Categories.FirstOrDefault(u=>u.Id==id);
            //Category? categoryFromDb2 = _db.Categories.Where(u=>u.Id==id).FirstOrDefault();

            if (categoryFromDb == null)
            {
                return NotFound(); 
            }
            return View(categoryFromDb);  
        }

        //---------------------------------------------------------------------//
        /// <summary>
        /// POST action method for handling the submission of the category edit form
        /// Custom validation: checks if the Category's Name matches its DisplayOrder
        /// Adds a model error if the validation fails
        /// Checks if the model state is valid after the custom validation
        /// Updates the existing Category in the DbContext and saves changes to the database
        /// Commits the changes to the database
        /// Redirects to the Index action after successful update
        /// Returns the same view for corrections if the model state is invalid
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Edit(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "The DisplayOrder cannot exactly match the Name. ");
            }

            if (ModelState.IsValid)
            {
                _db.Categories.Update(obj);
                _db.SaveChanges(); 
                TempData["success"] = "Category updated successfully";
                return RedirectToAction("Index"); 
            }
            return View(); 
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound(); 
            }

            Category? categoryFromDb = _db.Categories.Find(id);            
           
            if (categoryFromDb == null)
            {
                return NotFound(); 
            }
            return View(categoryFromDb); 
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            Category obj = _db.Categories.Find(id);
            if (obj == null)
            {
                return NotFound();
            }

            _db.Categories.Remove(obj);
            _db.SaveChanges();
            TempData["success"] = "Category deleted successfully";
            return RedirectToAction("Index");
        }
    }
}
//**------------------------------------------------------------< END >------------------------------------------------------------**// 
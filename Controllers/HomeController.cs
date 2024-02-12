// Including necessary namespaces for the functionality required in this controller
using BulkyWeb.Models; // Access to the application's models, such as ErrorViewModel
using Microsoft.AspNetCore.Mvc; // Base namespace for MVC controller functionalities
using System.Diagnostics; // Provides classes that allow interaction with system processes, event logs, and performance counters

// Define the namespace for the current class, structuring the code within the BulkyWeb.Controllers area
namespace BulkyWeb.Controllers
{
    // HomeController class inheriting from Controller, making it an MVC controller
    public class HomeController : Controller
    {
        // Field to hold the logger instance
        private readonly ILogger<HomeController> _logger;

        // Constructor that accepts an ILogger<HomeController> to enable logging within this controller
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger; // Assigning the passed-in logger to the private field
        }

        // Action method for handling requests to the root URL or /Home/Index
        // Returns the default view associated with the Index action
        public IActionResult Index()
        {
            return View();
        }

        // Action method for handling requests to /Home/Privacy
        // Returns the view associated with the Privacy action
        public IActionResult Privacy()
        {
            return View();
        }

        // Action method for handling errors
        // Annotating with ResponseCache attribute to prevent caching of the error response
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            // Creates an ErrorViewModel with the current RequestId (from Activity or HttpContext)
            // and returns the view for displaying error information
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

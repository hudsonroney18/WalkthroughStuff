using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WalkthroughStuff.Models;

namespace WalkthroughStuff.Controllers
{
    public class HomeController : Controller
    {
        // Declare a private variable to hold the MovieContext instance (database context)
        private MovieContext _context;

        // Constructor that injects the MovieContext into the controller
        public HomeController(MovieContext temp)
        {
            _context = temp; // Assign the injected context to the private variable
        }

        // Index page: Returns the default view
        public IActionResult Index()
        {
            return View(); // Simply returns the view for the homepage
        }

        // JoelPage: Returns the view for the "JoelPage" (custom page)
        public IActionResult JoelPage()
        {
            return View(); // Returns the view for the JoelPage
        }

        // GET action for entering a new movie
        [HttpGet]
        public IActionResult EnterMovie()
        {
            // Retrieve the list of categories from the database
            var categories = _context.Categories.ToList();
            // Store the categories in the ViewBag to be accessed in the view
            ViewBag.Categories = categories;
            // Return an empty Movie object to bind to the form
            return View(new Movie());
        }

        // POST action for processing the entered movie data
        [HttpPost]
        public IActionResult EnterMovie(Movie response)
        {
            // Retrieve the list of categories again, to keep the form data consistent
            var categories = _context.Categories.ToList();
            ViewBag.Categories = categories;

            // Check if the submitted model (movie) is valid
            if (ModelState.IsValid)
            {
                // Add the new movie record to the database
                _context.Movies.Add(response);
                // Save the changes to the database
                _context.SaveChanges();
            }
            else
            {
                // If model is invalid, return to the form with the entered data
                return View(response);
            }

            // Redirect to the ViewMovie action after successful save
            return RedirectToAction("ViewMovie");
        }

        // Action for viewing the list of movies
        public IActionResult ViewMovie()
        {
            // Retrieve all movies from the database, including the related categories
            var movies = _context.Movies
                .Include(x => x.Category)  // Include the related Category model
                .ToList(); // Execute the query and retrieve the results

            // Return the movies to the View
            return View(movies);
        }

        // GET action for editing an existing movie
        [HttpGet]
        public IActionResult Edit(int id)
        {
            // Retrieve the list of categories for the dropdown in the edit form
            var categories = _context.Categories.ToList();
            ViewBag.Categories = categories;

            // Retrieve the movie record that needs to be edited by its ID
            var record = _context.Movies.Single(x => x.MovieId == id);
            Console.WriteLine("Edit Action Hit! " + record); // Log the record for debugging

            // Return the "EnterMovie" view, but pre-populate it with the movie data to be edited
            return View("EnterMovie", record);
        }

        // POST action for updating the movie record after editing
        [HttpPost]
        public IActionResult Edit(Movie updatedInfo)
        {
            // Update the movie record in the database
            _context.Update(updatedInfo);
            // Save the changes to the database
            _context.SaveChanges();
            // Redirect to the ViewMovie action after successful update
            return RedirectToAction("ViewMovie");
        }

        // GET action for deleting a movie
        [HttpGet]
        public IActionResult Delete(int id)
        {
            // Retrieve the movie record to be deleted by its ID
            var movie = _context.Movies.Single(x => x.MovieId == id);
            // Return the delete confirmation view with the movie data
            return View(movie);
        }

        // POST action for confirming and performing the deletion of a movie
        [HttpPost]
        public IActionResult Delete(Movie movie)
        {
            // Remove the movie record from the database
            _context.Movies.Remove(movie);
            // Save the changes to the database
            _context.SaveChanges();
            // Redirect to the ViewMovie action after successful deletion
            return RedirectToAction("ViewMovie");
        }
    }
}
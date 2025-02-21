using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WalkthroughStuff.Models
{
    public class Movie
    {
        // The MovieId is the primary key for the Movie table.
        // It's marked as [Required] because every movie must have a unique ID.
        [Required]
        public int MovieId { get; set; }

        // CategoryId is a foreign key that connects the Movie to the Category table.
        // The '?' after the int type makes this field nullable, meaning a movie might not have a category.
        [ForeignKey("CategoryId")]
        public int? CategoryId { get; set; }
        
        // Navigation property to the Category model, allowing you to reference the related Category for each movie.
        // The '?' after the Category type makes this property nullable.
        public Category? Category { get; set; }

        // The Title of the movie is required. If the user does not provide it, they will see the custom error message.
        [Required(ErrorMessage = "You must enter a title")]
        public string Title { get; set; }

        // Year of release is required and must be a valid year after 1888.
        // If the user enters an invalid year, they will see the custom error message.
        [Required(ErrorMessage = "You must enter a year")]
        [Range(1888, int.MaxValue, ErrorMessage = "You must enter a year after 1888")]
        public int Year { get; set; }

        // Director is not required, so the '?' after the string type makes it nullable.
        public string? Director { get; set; }

        // Rating is not required, so the '?' after the string type makes it nullable.
        public string? Rating { get; set; }

        // Edited is required (boolean value), and it's set to false by default if no value is provided.
        // The user must indicate whether the movie is edited or not.
        [Required(ErrorMessage = "You must put edited")]
        public bool Edited { get; set; } = false;

        // LentTo is an optional field, and if it's not provided, it defaults to an empty string.
        public string? LentTo { get; set; } = string.Empty;

        // CopiedToPlex is required (boolean value). The user must indicate whether the movie has been copied to Plex.
        [Required(ErrorMessage = "You must put copied to plex")]
        public bool CopiedToPlex { get; set; }

        // Notes is optional and can be left blank. If not provided, it defaults to an empty string.
        public string? Notes { get; set; } = string.Empty;
    }
}
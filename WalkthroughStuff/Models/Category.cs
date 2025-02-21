using System.ComponentModel.DataAnnotations;

namespace WalkthroughStuff.Models;

public class Category
{
    
    [Key]
    public int CategoryId { get; set; }
    public string CategoryName { get; set; }
    
    // Navigation property
    public ICollection<Movie> Movies { get; set; }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BooksAndAuthors.Models
{
    public class Publisher 
    {
        [Key]
        public int PublisherId { get; set; }
        [Required(ErrorMessage="Name field must be filled in!")]
        public string Name { get; set; }
        // connects many-to-many table **IMPORTANT**
        public List<Publication> HasBooks { get; set; }
        public DateTime CreatedAt { get; set; } 
        public DateTime UpdatedAt { get; set; } 

        public Publisher()
        {
            CreatedAt = DateTime.Now;
            UpdatedAt = DateTime.Now;
            HasBooks = new List<Publication>();
        }
    }
}
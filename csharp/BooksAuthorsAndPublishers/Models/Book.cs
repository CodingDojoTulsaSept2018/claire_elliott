using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BooksAndAuthors.Models
{
    public class Book
    {
        [Key]
        public int BookId { get; set; }
        [Required(ErrorMessage="Title field is required!")]
        public string Title { get; set; }
        public int AuthorId { get; set; }
        [Required(ErrorMessage="An author is required!")]
        public Author Author { get; set;}
        // connects many-to-many table **IMPORTANT**
        public List<Publication> HasPublishers { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public Book()
        {
            CreatedAt = DateTime.Now;
            UpdatedAt = DateTime.Now;
            HasPublishers = new List<Publication>();
        }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BooksAndAuthors.Models
{
    // add this annontation for future deployment, match table name in MySQL workbench
    [Table("authors")]
    public class Author
    {
        [Key]
        public int AuthorId { get; set; }
        [Required(ErrorMessage="Name field is required!")]
        public string Name { get; set; }

        public List<Book> Books { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public Author()
        {
            CreatedAt = DateTime.Now;
            UpdatedAt = DateTime.Now;
        }
    }
}
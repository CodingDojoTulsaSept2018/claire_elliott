using System;
using System.ComponentModel.DataAnnotations;

namespace BooksAndAuthors.Models
{
    public class Publication 
    {
        [Key]
        public int PublicationId { get; set; }
        public int BookId { get; set; }
        public int PublisherId { get; set; }

        public Book Book { get; set; }
        public Publisher Publisher { get; set; }

        public DateTime PublishedDate { get; set; } 

        public Publication()
        {
            PublishedDate = DateTime.Now;
        }
    }
}
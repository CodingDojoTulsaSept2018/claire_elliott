using System.Collections.Generic;

namespace BooksAndAuthors.Models
{
    public class IndexView
    {
        public Author FormAuthor;
        public Book FormBook;
        public Publisher FormPublisher;
        public Publication FormPublication;
        public List<Publication> AllPublications = new List<Publication>();
        public List<Publisher> AllPublishers = new List<Publisher>();
        public List<Author> AllAuthors = new List<Author>();
        public List<Book> AllBooks = new List<Book>();

    }
}
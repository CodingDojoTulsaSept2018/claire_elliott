using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BooksAndAuthors.Models;
using Microsoft.EntityFrameworkCore;

namespace BooksAndAuthors.Controllers
{
    public class HomeController : Controller
    {
        private Context _context;

        public HomeController(Context contextModel)
        {
            _context = contextModel;
        }
        public IActionResult Index()
        {
            IndexView ShowAuthors = new IndexView();
            ShowAuthors.AllAuthors = _context.Authors.Include(author => author.Books).ToList();
            // from books, through our connection between Books and Publications, then grabbing all Publishers from the Publication connection
            // NOTICE the TWO Include statements. Cannot use ThenInclude on Author here because Author does not link us to Publications. ThenInclude is used for your many-to-many relationship
            ShowAuthors.AllBooks = _context.Books.Include(book => book.Author).Include(book => book.HasPublishers).ThenInclude(Publication => Publication.Publisher).ToList();
            // from publishers, through our connection between Publishers and Publications, then grabbing all Books from the Publication connection
            ShowAuthors.AllPublishers = _context.Publishers.Include(publisher => publisher.HasBooks).ThenInclude(publication => publication.Book).ToList();

            return View(ShowAuthors);
        }


        [HttpPost("AddAuthor")]
        public IActionResult About(Author formAuthor)
        {
            ViewData["Message"] = "Your application description page.";
            if(ModelState.IsValid)
            {
                // add author
                _context.Authors.Add(formAuthor);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                // display the error messages
                IndexView AuthorErrors = new IndexView(){
                    FormAuthor = formAuthor,
                    AllAuthors = _context.Authors.Include(author => author.Books).ToList(),
                    AllBooks = _context.Books.Include(book => book.Author).Include(book => book.HasPublishers).ThenInclude(Publication => Publication.Publisher).ToList(),
                    AllPublishers = _context.Publishers.Include(publisher => publisher.HasBooks).ThenInclude(publication => publication.Book).ToList()
                };

                return View("Index", AuthorErrors);
            }
            // for delete
            // Author AuthorToChange = _context.Authors.FirstOrDefault(author => author.Name.Contains("Dave"));

            // _context.Authors.Remove(AuthorToChange);

        }

        [HttpPost("AddBook")]
        public IActionResult AddBook(Book formBook)
        {
            if(ModelState.IsValid)
            {

                // find the author object to add to the book
                Author author = _context.Authors.SingleOrDefault(a => a.AuthorId == formBook.AuthorId);
                // Author author = _context.Authors.SingleOrDefault(a => a.AuthorId == 2);
                // set the  book author field the the author instance
                formBook.Author = author;
                // add the book
                _context.Books.Add(formBook);
                // save the book
                _context.SaveChanges();

                return RedirectToAction("Index");
            }
            else
            {
                // display the errors
                IndexView BookErrors = new IndexView(){
                    FormBook = formBook,
                    AllAuthors = _context.Authors.Include(author => author.Books).ToList(),
                    AllBooks = _context.Books.Include(book => book.Author).Include(book => book.HasPublishers).ThenInclude(Publication => Publication.Publisher).ToList(),
                    AllPublishers = _context.Publishers.Include(publisher => publisher.HasBooks).ThenInclude(publication => publication.Book).ToList()
                };
                return View("Index", BookErrors);
            }
        }

        [HttpPost("AddPublisher")]
        public IActionResult AddPublisher(Publisher formPublisher)
        {
            ViewData["Message"] = "Your application description page.";
            if(ModelState.IsValid)
            {
                // add publisher
                _context.Publishers.Add(formPublisher);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                // display the error messages
                IndexView PublisherErrors = new IndexView(){
                    FormPublisher = formPublisher,
                    AllAuthors = _context.Authors.Include(author => author.Books).ToList(),
                    AllBooks = _context.Books.Include(book => book.Author).Include(book => book.HasPublishers).ThenInclude(Publication => Publication.Publisher).ToList(),
                    AllPublishers = _context.Publishers.Include(publisher => publisher.HasBooks).ThenInclude(publication => publication.Book).ToList()
                };

                return View("Index", PublisherErrors);
            }
            // for delete
            // Publisher PublisherToChange = _context.Publishers.FirstOrDefault(Publisher => Publisher.Name.Contains("Dave"));

            // _context.Publishers.Remove(PublisherToChange);

        }

        [HttpPost("AddPublication")]
        public IActionResult AddPublication(Publication formPublication)
        {
            ViewData["Message"] = "Your application description page.";
            if(ModelState.IsValid)
            {
                // find the book obj from id, then assign it to the FormPublication's model field Book
                formPublication.Book = _context.Books.SingleOrDefault(b => b.BookId == formPublication.BookId);
                // find the publisher obj from id, then assign it to the FormPublication's model field Publisher
                formPublication.Publisher = _context.Publishers.SingleOrDefault(b => b.PublisherId == formPublication.PublisherId);
                // add the entire FormPublication object into your db
                _context.Publications.Add(formPublication);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                // display the error messages
                IndexView PublicationErrors = new IndexView(){
                    FormPublication = formPublication,
                    AllPublications = _context.Publications.Include(Publication => Publication.Publisher).ThenInclude(Publisher => Publisher.HasBooks).ToList(),
                    AllBooks = _context.Books.Include(book => book.Author).Include(book => book.HasPublishers).ThenInclude(Publication => Publication.Publisher).ToList(),
                    AllPublishers = _context.Publishers.Include(publisher => publisher.HasBooks).ThenInclude(publication => publication.Book).ToList()
                };

                return View("Index", PublicationErrors);
            }
            // for delete
            // Publication PublicationToChange = _context.Publications.FirstOrDefault(Publication => Publication.Name.Contains("Dave"));

            // _context.Publications.Remove(PublicationToChange);

        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab
{
    public class BookRepository
    {
        private SiteDbContext _siteDbContext;

        public BookRepository(SiteDbContext siteDbContext)
        {
            _siteDbContext = siteDbContext;
        }

        public List<Book> GetBooks()
        {
            return _siteDbContext.Books.ToList();
        }

        public async Task<int> AddBookAsync(string name, string author, string genre)
        {
            var book = new Book
            {
                Name = name,
                Author = author,
                Genre = genre
            };
            _siteDbContext.Books.Add(book);
            await _siteDbContext.SaveChangesAsync();
            return (from Book in _siteDbContext.Books
                    where Book.Name == name && Book.Author == author && Book.Genre == genre
                select Book).ToList()[0].Id;
        }

        public async Task UpdateBookAsync(int id, string name, string author, string genre)
        {
            var book = _siteDbContext.Books.Find(id);
            book.Name = name;
            book.Author = author;
            book.Genre = genre;
            _siteDbContext.Books.Update(book);
            await _siteDbContext.SaveChangesAsync();
        }

        public async Task DeleteBookAsync(int id)
        {
            var book = _siteDbContext.Books.Find(id);
            _siteDbContext.Books.Remove(book);
            await _siteDbContext.SaveChangesAsync();
        }
    }
}
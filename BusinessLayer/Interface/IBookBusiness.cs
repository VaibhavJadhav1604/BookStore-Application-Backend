using CommonLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface IBookBusiness
    {
        public BookModel AddBook(BookModel bookModel);
        public BookModel UpdateBook(BookModel bookModel);
        public bool DeleteBook(int BookId);
        public List<BookModel> GetAllBooks();
        public BookModel GetBookByBookId(int BookId);
    }
}

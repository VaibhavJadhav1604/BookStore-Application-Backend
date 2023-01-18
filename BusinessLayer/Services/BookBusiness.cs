using BusinessLayer.Interface;
using CommonLayer.Models;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class BookBusiness:IBookBusiness
    {
        private IBookRepo bookRepo;
        public BookBusiness(IBookRepo bookRepo)
        {
            this.bookRepo = bookRepo;
        }
        public BookModel AddBook(BookModel bookModel)
        {
            try
            {
                return bookRepo.AddBook(bookModel);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public BookModel UpdateBook(BookModel bookModel)
        {
            try
            {
                return bookRepo.UpdateBook(bookModel);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public bool DeleteBook(int BookId)
        {
            try
            {
                return bookRepo.DeleteBook(BookId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<BookModel> GetAllBooks()
        {
            try
            {
                return bookRepo.GetAllBooks();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public BookModel GetBookByBookId(int BookId)
        {
            try
            {
                return bookRepo.GetBookByBookId(BookId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

using BusinessLayer.Interface;
using CommonLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookBusiness bookBusiness;
        public BookController(IBookBusiness bookBusiness)
        {
            this.bookBusiness = bookBusiness;
        }

        [Authorize(Roles = Role.Admin)]
        [HttpPost("AddBook")]
        public ActionResult AddBook(BookModel bookModel)
        {
            try
            {
                var result = bookBusiness.AddBook(bookModel);
                if (result != null)
                {
                    return this.Ok(new { success = true, message = "Book Added Successfully", Response = result });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "Book Adding Failed" });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Authorize(Roles = Role.Admin)]
        [HttpPut("UpdateBook")]
        public ActionResult UpdateBook(BookModel bookModel)
        {
            try
            {
                var result = bookBusiness.UpdateBook(bookModel);
                if (result != null)
                {
                    return this.Ok(new { success = true, message = "Book Updated Successfully", Response = result });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "Book Updating Failed" });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Authorize(Roles = Role.Admin)]
        [HttpDelete("DeleteBook")]
        public ActionResult DeleteBook(int BookId)
        {
            try
            {
                var result = bookBusiness.DeleteBook(BookId);
                if (result != null)
                {
                    return this.Ok(new { success = true, message = "Book Deleted Successfully", Response = result });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "Book Deleting Failed" });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet("GetAllBooks")]
        public ActionResult GetAllBooks()
        {
            try
            {
                var result = bookBusiness.GetAllBooks();

                if (result != null)
                {
                    return Ok(new { success = true, message = "Getting All Books", Response = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Something went wrong..." });
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        [HttpGet("GetBooksById")]
        public ActionResult GetBooksById(int BookId)
        {
            try
            {
                var result = bookBusiness.GetBookByBookId(BookId);

                if (result != null)
                {
                    return Ok(new { success = true, message = "Getting Book By Id", Response = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Something went wrong..." });
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}

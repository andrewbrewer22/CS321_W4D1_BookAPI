﻿using CS321_W4D1_BookAPI.ApiModels;
using CS321_W4D1_BookAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace CS321_W4D1_BookAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;

        // Constructor
        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
        }

        // GET api/books
        [HttpGet]
        public IActionResult Get()
        {
            // TODO: convert domain models to apimodels
            var bookModels = _bookService
                .GetAll().ToApiModels();

            return Ok(bookModels);
        }

        // get specific book by id
        // GET api/books/:id
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            // TODO: convert domain model to apimodel
            var book = _bookService.Get(id);
            if (book == null) return NotFound();
            return Ok(book.ToApiModel());
        }

        // create a new book
        // POST api/books
        [HttpPost]
        public IActionResult Post([FromBody] BookModel newBook)
        {
            try
            {
                // TODO: convert apimodel to domain model
                // add the new book
                _bookService.Add(newBook.ToDomainModel());
            }
            catch (System.Exception ex)
            {
                ModelState.AddModelError("AddBook", ex.GetBaseException().Message);
                return BadRequest(ModelState);
            }

            return CreatedAtAction("Get", new { Id = newBook.Id }, newBook);
        }

        // PUT api/books/:id
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] BookModel updatedBook)
        {
            var book = _bookService.Update(updatedBook.ToDomainModel());
            if (book == null) return NotFound();
            return Ok(book.ToApiModel());
            //return Ok(updatedBook);
        }

        // DELETE api/books/:id
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var book = _bookService.Get(id);
            if (book == null) return NotFound();
            _bookService.Remove(book);
            return NoContent();
        }

        [HttpGet("publishers/{publisherId}/books")]
        public IActionResult GetBooksForPublisher(int publisherId)
        {
            var books = _bookService.GetBooksForPublisher(publisherId);

            return Ok(books.ToApiModels());
        }

        [HttpGet("authors/{authorId}/books")]
        public IActionResult GetBooksForAuthor(int authorId)
        {
            var books = _bookService.GetBooksForAuthor(authorId);

            return Ok(books.ToApiModels());
        }
    }
}

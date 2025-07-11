﻿using API.Dtos.CatalogDtos;
using API.Mapping;
using API.RequestHelpers;
using Core.Entities;
using Core.Interfaces;
using Core.Specificatios;
using Core.Specificatios.Params;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController(IUnitOfWork unit, ILogger<BookController> logger) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<CatalogBookDto>>> GetBooks([FromQuery] BookSpecParams bookSpecParams)
        {
            logger.LogInformation("Fetching books with params: {@Params}", bookSpecParams);

            var spec = new BookCatalogSpecification(bookSpecParams);
            var books = await unit.Repository<Book>().ListWithSpecAsync(spec);
            var count = await unit.Repository<Book>().CountAsync(spec);
            var booksDto = books.Select(CatalogMapping.MapBookToDto).ToList();

            var pagination = new Pagination<CatalogBookDto>(bookSpecParams.PageIndex, bookSpecParams.PageSize, count, booksDto);

            logger.LogInformation("Fetched {Count} books for page {PageIndex}", booksDto.Count, bookSpecParams.PageIndex);

            return Ok(pagination);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<SingleBookDto>> GetBook(int id)
        {
            var spec = new SingleBookSpecification(id);
            var book = await unit.Repository<Book>().GetEntityWithSpec(spec);

            if (book == null)
            {
                logger.LogWarning("Book with id {Id} not found at {Path}", id, HttpContext.Request.Path);
                return NotFound();
            }

            var result = CatalogMapping.MapBookToSingleDto(book);
            logger.LogInformation("Book with id {Id} retrieved successfully at {Path}", id, HttpContext.Request.Path);

            return result;
        }

    }
}

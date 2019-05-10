using System.Collections.Generic;
using System.Threading.Tasks;
using angu.Data;
using angu.Dtos;
using angu.models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace angu.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IDatingRespository _repo;
        private readonly IMapper _mapper;

        public BookController(IDatingRespository repository, IMapper mapper)
        {
            _repo = repository;
            _mapper = mapper;
        }


    [HttpPost]
    public async  Task<IActionResult> CreateBook(BookDtoForCreate book)
    {

        var newbook = _mapper.Map<Book>(book); 
         _repo.Add(newbook);

        await _repo.SaveAll();

        return StatusCode(201);
    }

    [HttpGet]

    public async Task<IActionResult> GetAllBooks()
    {
        var books = await _repo.GetBooks();
        var response = _mapper.Map<IEnumerable<BookListTodo>>(books);


        return Ok(response);
    }

    [HttpGet("{id}")]

    public async Task<IActionResult> GetBookById(long id)
    {
        var book = await _repo.GetBookById(id);
        var response = _mapper.Map<BookDetailDto>(book);

        return Ok(response);
    }
    }
}
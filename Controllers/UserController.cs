using System.Collections.Generic;
using System.Threading.Tasks;
using angu.Data;
using angu.Dtos;
using angu.Helpers;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace angu.Controllers
{
    [ServiceFilter(typeof(LogUserActionFilter))]
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IDatingRespository _repo;
        private readonly IMapper _mapper;

        public UserController(IDatingRespository repository, IMapper mapper)
        {
            _repo = repository;
            _mapper = mapper;
        }

    [HttpGet]

    public async Task<IActionResult> GetAllUsers()
    {
        var users = await _repo.GetUsers();
        var response = _mapper.Map<IEnumerable<UserForListDto>>(users);

        return Ok(response);
    }

    [HttpGet("{id}")]

    public async Task<IActionResult> GetUser(long id)
    {
        var user = await _repo.GetUserById(id);
        var response = _mapper.Map<UserDetail>(user);

        return Ok(response);
    }






    }
}
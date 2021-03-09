using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")] //controller placeholder in []

    public class UsersController : ControllerBase //derive from ControllerBase
    {
        //Dependency Injection
        private readonly DataContext _context; //prefix _ 

        //Generate UsersController constructor
        public UsersController(DataContext context)
        {
            _context = context;
        }

        //Add 2 endpoints: 1) get all users, 2) get specific user
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers() 
        {
            //await to make it wait while busy
            return await _context.Users.ToListAsync(); //make ToList Asynchronous
        }

        //id route paramater - get individual user (i.e. api/users/3)
        [HttpGet("{id}")] 
        public async Task<ActionResult<AppUser>> GetUser(int id) 
        {
            return await _context.Users.FindAsync(id);
        }
    }
}
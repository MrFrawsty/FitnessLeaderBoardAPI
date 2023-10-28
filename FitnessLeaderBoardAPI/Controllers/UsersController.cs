using FitnessLeaderBoardAPI.Data;
using FitnessLeaderBoardAPI.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Buffers.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text.Encodings.Web;

namespace FitnessLeaderBoardAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly FitnessLeaderboardDbContext _context;

        public UsersController(FitnessLeaderboardDbContext context)
        {
            _context = context;
            _context.Database.EnsureCreated();
        }


      
        // GET: api/<ActivityModelController>
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            return Ok(await _context.Users.ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> GetUserByEmail([FromBody]string email) 
        {
            
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email.ToLower() == email.ToLower());
            if (user != null)
            {
                return Ok(user);
            }

            return NotFound();
        }

        // GET api/<ActivityModelController>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                return Ok(user);
            }

            return NotFound();
        }

        // POST api/<ActivityModelController>
        [HttpPost]
        public async Task<IActionResult> AddUser(UserModel user)
        {
            var email = user.Email;
            var existingUser = await _context.Users.FirstOrDefaultAsync(x => x.Email.ToLower() == email.ToLower());

            if (existingUser != null)
            {
                return Conflict(existingUser);
            }

            if(user != null)
            {
               _context.Add(user);
               await _context.SaveChangesAsync();
               return Ok(user);
            }

            return NotFound();
        }


        // PUT api/<ActivityModelController>/5
        [HttpPut("{id}")]
       // [Route("UpdateUser")]
        public async Task<IActionResult> UpdateUser(string email, UserModel userModel)
        {
            var userToUpdate = await _context.Users.FindAsync(email);
            if (userToUpdate != null)
            {
                userToUpdate.Email = email;
                userToUpdate.Name = userModel.Name;
                await _context.SaveChangesAsync();
                return Ok(userModel);
            }

            return NotFound();
        }

        // DELETE api/<UsersController>/5
        [HttpPut]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                _context.Remove(user);
                _context.SaveChanges();
                return Ok(user);
            }

            return NotFound();

        }

    }
}

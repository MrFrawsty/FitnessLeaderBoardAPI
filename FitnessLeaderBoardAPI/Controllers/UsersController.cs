using FitnessLeaderBoardAPI.Data;
using FitnessLeaderBoardAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text.Encodings.Web;

namespace FitnessLeaderBoardAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly FitnessLeaderboardDbContext _context;

        public UsersController(FitnessLeaderboardDbContext context)
        {
            _context = context;
        }

        // GET: api/<ActivityModelController>
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            return Ok(await _context.Users.ToListAsync());
        }

        // GET api/<ActivityModelController>/5
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
        public async Task<IActionResult> AddUser(UserModel user, string email)
        {            
               _context.Add(user);
               await _context.SaveChangesAsync();
               return Ok(user);
        }


        // PUT api/<ActivityModelController>/5
        [HttpPut("{id}")]
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

        // DELETE api/<ActivityModelController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteActivity(int id)
        {
            var activity = await _context.Activities.FindAsync(id);
            if (activity != null)
            {
                _context.Remove(activity);
                _context.SaveChanges();
                return Ok(activity);
            }

            return NotFound();

        }

    }
}

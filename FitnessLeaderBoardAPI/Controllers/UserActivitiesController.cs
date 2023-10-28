using FitnessLeaderBoardAPI.Data;
using FitnessLeaderBoardAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FitnessLeaderBoardAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserActivitiesController : ControllerBase
    {
        private readonly FitnessLeaderboardDbContext _context;

        public UserActivitiesController(FitnessLeaderboardDbContext context)
        {
            _context = context;
        }
   

        [HttpGet]
        public async Task<IActionResult> GetUserActivities(int id)
        {
            var usersActivities = await _context.Activities.Where(a => a.UserId == id).ToListAsync();

            if(usersActivities.Count > 0)
            {
                return Ok(usersActivities);
            }

            else 
            {
                return NotFound();
            }
        }
 
    }
}

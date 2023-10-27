using FitnessLeaderBoardAPI.Data;
using FitnessLeaderBoardAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FitnessLeaderBoardAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActivitiesController : ControllerBase
    {
        private readonly FitnessLeaderboardDbContext _context;
        public ActivitiesController(FitnessLeaderboardDbContext context)
        {
            _context = context;
            _context.Database.EnsureCreated();
        }



        [HttpGet]
        public async Task<IActionResult> GetActivities()
        {
            var activities = await _context.Activities.ToListAsync();
            if (activities != null)
            {
                return Ok(await _context.Activities.ToListAsync());
            }

            else
            {
                return NotFound();
            }
        }

        // GET api/<ActivityModelController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetActivity(int id)
        {
            var activity = await _context.Activities.FindAsync(id);
            if (activity != null)
            {
                return Ok(activity);
            }

            return NotFound();
        }

        // POST api/<ActivityModelController>
        [HttpPost]
        public async Task<IActionResult> AddActivity(ActivityModel activity)
        {
            activity.CreatedAt = DateTime.UtcNow;
           
            var id = activity.UserId;
            var user = await _context.Users.FindAsync(id);

            if (user != null)
            {
                activity.UserId = user.Id;
                activity.UserName = user.Name;
                _context.Add(activity);
                await _context.SaveChangesAsync();
                return Ok(activity);
            }

                return NotFound();

        }


        // PUT api/<ActivityModelController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateActivity(int? id, ActivityModel activityModel)
        {
            var activityToUpdate = await _context.Activities.FindAsync(id);
            if (activityToUpdate != null)
            {
                activityToUpdate.Name = activityModel.Name;
                activityToUpdate.Description = activityModel.Description;
                activityToUpdate.Minutes = activityModel.Minutes;
                await _context.SaveChangesAsync();
                return Ok(activityToUpdate);
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

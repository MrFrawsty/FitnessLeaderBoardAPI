using System.ComponentModel.DataAnnotations;

namespace FitnessLeaderBoardAPI.Models
{
    public class ActivityModel
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public double Minutes { get; set; }
        public int UserId { get; set; }

    }
}

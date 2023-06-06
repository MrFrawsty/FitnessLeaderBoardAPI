namespace FitnessLeaderBoardAPI.Models
{
    public class UserModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public List<ActivityModel> Activities { get; set; }

    }
}

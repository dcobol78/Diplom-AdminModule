namespace CourseShared.Models
{
    public class AchivementViewModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public float? ExpThreshold { get; set; }

        public string CourseId { get; set; }

        public IFormFile Image { get; set; }
        public string ExistingImageUrl { get; set; }

        public string RewardName { get; set; }
        public string RewardDescription { get; set; }
        public string RewardType { get; set; }
    }

    public class AchivementsViewModel
    {
        public List<Achivement> Achivements { get; set; }
        public string courseId { get; set; }
    }
}

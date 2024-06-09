using CourseShared.Models;

namespace CourseShared.Models
{
    public class HomeworkViewModel
    {
        public Homework Homework { get; set; }

        public List<HomeworkFile> UserFiles { get; set; }
    }
}

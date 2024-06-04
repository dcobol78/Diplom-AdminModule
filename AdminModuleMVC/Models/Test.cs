using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CourseShared.Models
{
    public class Test
    {
        public Test() { }
        public Test(string parentId, string parentType) 
        {
            Name = "Test";
            ParentId = parentId;
            Description = string.Empty;
            AttemptsAlowed = 1;
            ParentType = parentType;
        }

        [ScaffoldColumn(false)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string ParentType {  get; set; }

        public string ParentId { get; set; }

        public virtual DateTime StartTime { get; set; }

        public virtual DateTime CloseTime { get; set; }

        public int Duration { get; set; }

        public int AttemptsAlowed {  get; set; }

        // Стоимость тест
        public int Cost {  get; set; }

        // Количество опыта за тест
        public int Exp { get; set; }

        public virtual List<Question> Questions { get; set; }
    }
}

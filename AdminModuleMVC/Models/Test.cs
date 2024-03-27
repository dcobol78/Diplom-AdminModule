using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AdminModuleMVC.Models
{
    public class Test
    {
        public Test() { }
        public Test(string parentId) 
        {
            Name = "Test";
            ParentId = parentId;
            Description = string.Empty;
            AttemptsAlowed = 1;
        }

        [ScaffoldColumn(false)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string ParentId { get; set; }

        //public DateTime StartTime { get; set; }

        //public DateTime EndTime { get; set; }

        public int Duration { get; set; }

        public int AttemptsAlowed {  get; set; }

        // Стоимость тест
        public int Cost {  get; set; }

        // Количество опыта за тест
        public int Exp { get; set; }

        public virtual List<Question> Questions { get; set; }
    }
}

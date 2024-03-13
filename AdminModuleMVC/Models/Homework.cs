using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AdminModuleMVC.Models
{
    public class Homework
    {
        [ScaffoldColumn(false)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }

        public string Name { get; set; }

        public string ParentId { get; set; }

        //public DateTime StartDate { get; set; }

        //public DateTime EndDate { get; set; }

        public int Duration { get; set; }

        public int Cost {  get; set; }

        public string Description { get; set; }

        // Файлы необходимые для выполнения ДЗ
        public List<CourseFile> HomeWorkFiles { get; set; }

    }
}

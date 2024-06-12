using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CourseShared.Models
{
    public class Item
    {
        [ScaffoldColumn(false)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }

        public string? Type { get; set; }

        public string? AchivementId { get; set; }

        public string? CourseId { get; set; }

        public Image? Image { get; set; }

        public string? Title { get; set; }
    }
}

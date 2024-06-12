using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Drawing;

namespace CourseShared.Models
{
    public class Achivement
    {
        [ScaffoldColumn(false)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }

        public string? Name { get; set; }

        public string? Description {  get; set; }

        public float? ExpThreshold { get; set; }

        public Item? Reward { get; set; }


        public string CourseId { get; set; }

        [ForeignKey("CourseId")]
        public Course Course { get; set; }  

        public Image? Image { get; set; }

    }
}

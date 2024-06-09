using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CourseShared.Models
{
    public class Message
    {
        [ScaffoldColumn(false)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }

        public string? Content { get; set; }

        public DateTime? CreationTime {  get; set; }

        public string? ChatId { get; set; }

        public string? UserId { get; set; }

        public string? UserName { get; set; }

        public string? ImageId { get; set;}
    }
}

using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AdminModuleMVC.Models
{
    public class Notification
    {
        [ScaffoldColumn(false)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string ReciverEmail { get; set; }

        public string CourseId {  get; set; }

        // 1 = учитель; 0 = студент. (Дичь! Сделай Enum-ом)
        public bool NotificationType {  get; set; }

        public Notification() { }
    }
}

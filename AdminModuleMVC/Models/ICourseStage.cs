using System.ComponentModel.DataAnnotations.Schema;

namespace AdminModuleMVC.Models
{
    public interface ICourseStage
    {
        public string Id { get; set; }

        public Homework Homework { get; set; }

        public Test Test { get; set; }
    }
}

using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace AdminModuleMVC.Models
{
    public class Question
    {
        [ScaffoldColumn(false)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }

        public string Content { get; set; }

        public List<string> Answers { get; set; }

        public string Type { get; set; }
        
        public int Number {  get; set; }

        public virtual List<string> Options { get; set; }

        // Стоимость вопроса
        public int Cost { get; set; }

        [NotMapped]
        public List<AnswerHelper> AnswerHelpers { get; set; }
    }
}

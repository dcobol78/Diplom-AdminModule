using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace CourseShared.Models
{
    public class Question
    {
        public Question() { }
        public Question(int number) 
        {
            Number = number;
            Content = string.Empty;
            Type = string.Empty;
        }

        [ScaffoldColumn(false)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }

        public string Content { get; set; }

        public virtual List<Answer> Answers { get; set; }

        public string Type { get; set; }
        
        public int Number {  get; set; }

        // Стоимость вопроса
        public int Cost { get; set; }

    }
}

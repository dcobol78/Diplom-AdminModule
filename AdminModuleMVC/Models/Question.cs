using System.Diagnostics.CodeAnalysis;

namespace AdminModuleMVC.Models
{
    public class Question
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public string Answer { get; set; }

        public string Type { get; set; }

        public int Number {  get; set; }

        [AllowNull]
        public List<string> Options { get; set; }


    }
}

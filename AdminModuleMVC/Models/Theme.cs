﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace CourseShared.Models
{
    public class Theme: ICourseStage
    {

        // Id Темы
        [ScaffoldColumn(false)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }

        // Id Сектора
        public string IdSection { get; set; }

        // Имя Темы
        public string Name { get; set; }

        // Содержание Темы
        public string Content { get; set; }

        // Порядковый номер Темы
        public int Number { get; set; }

        // Продолжительность Темы
        public int? Duration { get; set; }

        public string? HomeworkId { get; set; }

        [ForeignKey("HomeworkId")]
        public virtual Homework Homework { get; set; }

        
        //Опыт получаемый за прохождение курса
        public float? Exp { get; set;}

        public string? TestId { get; set; }

        [ForeignKey("TestId")]
        public Test Test { get; set; }

        // Спсок файлов темы
        public List<CourseFile> ThemeFiles { get; set; }
    }
}

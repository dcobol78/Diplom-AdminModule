﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CourseShared.Models
{
    public class CourseFile
    {
        public CourseFile() { }

        public CourseFile(string parentId, string parentType) 
        { 
            ParentId = parentId;
            Name = string.Empty;
            Description = string.Empty;
            Path = string.Empty;
            ParentType = parentType;
        }

        // Ненужно строить шаблон
        [ScaffoldColumn(false)]
        // Ид будет сгенерировано Бж
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }

        public string? ParentId { get; set; }
        public string? ParentType { get; set; }

        public string? Path { get; set; }

        public Teacher? Author { get; set; }

    }
}

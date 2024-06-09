using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CourseShared.Models
{
    public class Image
    {
        [ScaffoldColumn(false)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string? Id { get; set; }

        public string? ImageTitle { get; set; }

        public string? MimeType { get; set; } // New property to store MIME type

        public virtual byte[] ImageData { get; set; }

        public string GetImageDataUrl()
        {
            if (ImageData != null && ImageData.Length > 0 && !string.IsNullOrEmpty(MimeType))
            {
                string base64String = Convert.ToBase64String(ImageData);
                return $"data:{MimeType};base64,{base64String}";
            }
            return string.Empty;
        }
    }
}

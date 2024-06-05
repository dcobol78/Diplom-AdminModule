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
        public virtual byte[] ImageData { get; set; }

        public string GetImageDataUrl()
        {
            if (ImageData != null && ImageData.Length > 0)
            {
                string base64String = Convert.ToBase64String(ImageData);
                return $"data:image/png;base64,{base64String}"; // Assuming the image is PNG. Adjust the MIME type if necessary.
            }
            return string.Empty;
        }
    }
}

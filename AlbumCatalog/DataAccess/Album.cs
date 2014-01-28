using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace DataAccess
{
    public class Album
    {
        [Required]
        [StringLength(50, ErrorMessage = "Maximum 50 characters")]
        public string Title { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Maximum 50 characters")]
        public string Artist { get; set; }

        [Required]
        [RegularExpression(@"^(0[1-9]|1[012])[- /.](0[1-9]|[12][0-9]|3[01])[- /.](19|20)\d\d$", ErrorMessage = "mm/dd/yyyy")]
        public string ReleaseDate { get; set; }

        public int AlbumId { get; set; }

        public string PictureFileName { get; set; }

        public Byte[] Thumbnail { get; set; }

        [Required]
        [RegularExpression(@"^([1-9]|10)$", ErrorMessage = "Rating must be 1-10")]
        public int Rating { get; set; }

        public bool IsCheckedOut { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class Person
    {
        [Required]
        [StringLength(50, ErrorMessage = "Maximum 50 characters")]
        [RegularExpression(@"^[a-zA-Z]+(([\'\,\.\- ][a-zA-Z])?[a-zA-Z]*)*$", ErrorMessage = "*")]
        public string FullName { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"^\S+@(\S+\.)+\S+$", ErrorMessage = "Not a valid email")]
        public string Email { get; set; }

        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^1?[-\. ]?(\(\d{3}\)?[-\. ]?|\d{3}?[-\. ]?)?\d{3}?[-\. ]?\d{4}$", ErrorMessage = "xxx-xxx-xxxx")]
        public string PhoneNumber { get; set; }

        [Key]
        public int UserID { get; set; }
    }
}

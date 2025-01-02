using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentAppPrj.Models
{
    public class Department
    {

        [Key]
        [RegularExpression(@"^[a-zA-Z0-9]*$")]
        [StringLength(8, MinimumLength = 4)]
        public string MaKhoa { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 3)]
        public string TenKhoa { get; set; }
        public ICollection<Student>? Students { get; set; }
    }
}
using System.ComponentModel.DataAnnotations;

namespace StudentAppPrj.Models
{
    public class Department
    {

        [Key]
        public required string MaKhoa { get; set; }
        public required string TenKhoa { get; set; }
        public ICollection<Student>? Students { get; set; }
    }
}
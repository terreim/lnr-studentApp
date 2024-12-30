using System.ComponentModel.DataAnnotations;

namespace StudentAppPrj.Models
{
    public class Student
    {
        [Key]
        public required int MaSV { get; set; }
        public required string TenSV { get; set; }
        public DateTime? NgaySinh { get; set; }
        public int? GioiTinh { get; set; }
        public required string MaKhoa { get; set; }
        public Department? Department { get; set; }
    }
}
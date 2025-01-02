using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentAppPrj.Models
{

    public class DateRangeAttribute : ValidationAttribute
    {
        private readonly int _minYear;
        private readonly int _maxYear;

        public DateRangeAttribute(int minYear)
        {
            _minYear = minYear;
            _maxYear = DateTime.Now.Year;
        }

        public override string FormatErrorMessage(string name)
        {
            return $"The field {name} must be between {_minYear} and {_maxYear}";
        }

        public override bool IsValid(object value)
        {
            if (value == null) return true;
            
            if (value is DateTime date)
            {
                return date.Year >= _minYear && date.Year <= _maxYear;
            }

            return false;
        }
    }

    public class Student
    {
        [Key]
        [Range(1, int.MaxValue)]
        public int MaSV { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 2)]
        public string TenSV { get; set; }

        [DataType(DataType.Date)]
        [DateRange(1980)]
        public DateTime? NgaySinh { get; set; }
        
        [Range(0, 2)] // 0: Nam, 1: Nu, 2: Khac
        public int? GioiTinh { get; set; }

        [Required]
        [RegularExpression(@"^[a-zA-Z0-9]*$")]
        [StringLength(8, MinimumLength = 4)]
        public string MaKhoa { get; set; }

        public Department? Department { get; set; }
    }
}
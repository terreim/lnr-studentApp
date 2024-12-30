using StudentAppPrj.Models;

namespace StudentAppPrj.Data
{
    public static class DbInitializer
    {
        public static void Initialize(StudentContext context)
        {
            if (context.Departments.Any())
            {
                return;
            }

            var departments = new Department[]
            {
                new Department { MaKhoa = "123KD", TenKhoa = "Kinh Doanh" },
                new Department { MaKhoa = "123D", TenKhoa = "Dược" },
                new Department { MaKhoa = "123IT", TenKhoa = "IT" },
                new Department { MaKhoa = "123TK", TenKhoa = "Thiết Kế" },
                new Department { MaKhoa = "123BDS", TenKhoa = "Bất Động Sản" },
                new Department { MaKhoa = "123M", TenKhoa = "Marketing" },
                new Department { MaKhoa = "123GT", TenKhoa = "Giao Thông" },
                new Department { MaKhoa = "123NN", TenKhoa = "Nông Nghiệp" },
                new Department { MaKhoa = "123KS", TenKhoa = "Khách Sạn" }
            };

            context.Departments.AddRange(departments);
            context.SaveChanges();
            
            var students = new Student[]
            {
                new Student{MaSV=123456,TenSV="Trần Thị Hoa",NgaySinh=DateTime.Parse("1995-03-15"),GioiTinh=1,MaKhoa = "123KD"},
                new Student{MaSV=342451,TenSV="Lê Minh Tuấn",NgaySinh=DateTime.Parse("1998-07-22"),GioiTinh=0,MaKhoa = "123IT"},
                new Student{MaSV=123321,TenSV="Phạm Thu Thảo",NgaySinh=DateTime.Parse("1997-09-30"),GioiTinh=1,MaKhoa = "123TK"},
                new Student{MaSV=657567,TenSV="Hoàng Văn Nam",NgaySinh=DateTime.Parse("1994-05-18"),GioiTinh=0,MaKhoa = "123BDS"},
                new Student{MaSV=545454,TenSV="Nguyễn Mai Linh",NgaySinh=DateTime.Parse("1999-12-25"),GioiTinh=1,MaKhoa = "123D"},
                new Student{MaSV=100000,TenSV="Đỗ Quang Hải",NgaySinh=DateTime.Parse("1996-02-14"),GioiTinh=0,MaKhoa = "123M"},
                new Student{MaSV=000001,TenSV="Vũ Thị Ngọc",NgaySinh=DateTime.Parse("1993-08-07"),GioiTinh=1,MaKhoa = "123GT"},
                new Student{MaSV=999999,TenSV="Bùi Đức Anh",NgaySinh=DateTime.Parse("2000-04-01"),GioiTinh=0,MaKhoa = "123NN"},
                new Student{MaSV=111111,TenSV="Lý Thanh Hương",NgaySinh=DateTime.Parse("1992-11-11"),GioiTinh=1,MaKhoa = "123KD"},
                new Student{MaSV=222222,TenSV="Đinh Công Mạnh",NgaySinh=DateTime.Parse("1991-06-29"),GioiTinh=0,MaKhoa = "123IT"},
                new Student{MaSV=333333,TenSV="Nguyễn Thị Lan",NgaySinh=DateTime.Parse("1996-08-15"),GioiTinh=1,MaKhoa = "123D"},
                new Student{MaSV=444444,TenSV="Trần Văn Hùng",NgaySinh=DateTime.Parse("1997-03-21"),GioiTinh=0,MaKhoa = "123GT"},
                new Student{MaSV=555555,TenSV="Phạm Thị Mai",NgaySinh=DateTime.Parse("1995-12-03"),GioiTinh=1,MaKhoa = "123M"},
                new Student{MaSV=666666,TenSV="Lê Văn Đức",NgaySinh=DateTime.Parse("1994-07-19"),GioiTinh=0,MaKhoa = "123IT"},
                new Student{MaSV=777777,TenSV="Hoàng Thị Hà",NgaySinh=DateTime.Parse("1998-01-25"),GioiTinh=1,MaKhoa = "123TK"},
                new Student{MaSV=888888,TenSV="Vũ Đình Long",NgaySinh=DateTime.Parse("1993-09-08"),GioiTinh=0,MaKhoa = "123BDS"},
                new Student{MaSV=123123,TenSV="Đặng Thu Trang",NgaySinh=DateTime.Parse("1999-04-12"),GioiTinh=1,MaKhoa = "123KS"},
                new Student{MaSV=234234,TenSV="Ngô Quang Minh",NgaySinh=DateTime.Parse("1992-11-30"),GioiTinh=0,MaKhoa = "123NN"},
                new Student{MaSV=345345,TenSV="Bùi Thị Hồng",NgaySinh=DateTime.Parse("1996-05-17"),GioiTinh=1,MaKhoa = "123KD"},
                new Student{MaSV=456456,TenSV="Đỗ Văn Thành",NgaySinh=DateTime.Parse("1997-02-28"),GioiTinh=0,MaKhoa = "123IT"},
                new Student{MaSV=567567,TenSV="Trần Thị Thủy",NgaySinh=DateTime.Parse("1995-10-09"),GioiTinh=1,MaKhoa = "123D"},
                new Student{MaSV=678678,TenSV="Phạm Văn Tùng",NgaySinh=DateTime.Parse("1994-06-14"),GioiTinh=0,MaKhoa = "123M"},
                new Student{MaSV=789789,TenSV="Lê Thị Hương",NgaySinh=DateTime.Parse("1998-12-22"),GioiTinh=1,MaKhoa = "123TK"},
                new Student{MaSV=890890,TenSV="Nguyễn Đức Thắng",NgaySinh=DateTime.Parse("1993-03-05"),GioiTinh=0,MaKhoa = "123GT"},
                new Student{MaSV=901901,TenSV="Hoàng Thị Thảo",NgaySinh=DateTime.Parse("1999-08-11"),GioiTinh=1,MaKhoa = "123KS"},
                new Student{MaSV=112233,TenSV="Vũ Quang Huy",NgaySinh=DateTime.Parse("1992-01-20"),GioiTinh=0,MaKhoa = "123BDS"},
                new Student{MaSV=223344,TenSV="Đinh Thị Loan",NgaySinh=DateTime.Parse("1996-07-16"),GioiTinh=1,MaKhoa = "123NN"},
                new Student{MaSV=334455,TenSV="Đặng Văn Phúc",NgaySinh=DateTime.Parse("1997-04-23"),GioiTinh=0,MaKhoa = "123IT"},
                new Student{MaSV=445566,TenSV="Bùi Thị Ngọc",NgaySinh=DateTime.Parse("1995-11-28"),GioiTinh=1,MaKhoa = "123M"},
                new Student{MaSV=556677,TenSV="Ngô Văn Dũng",NgaySinh=DateTime.Parse("1994-09-02"),GioiTinh=0,MaKhoa = "123KD"},
                new Student{MaSV=667788,TenSV="Phạm Thị Linh",NgaySinh=DateTime.Parse("1998-05-19"),GioiTinh=1,MaKhoa = "123D"},
                new Student{MaSV=778899,TenSV="Trần Đình Quang",NgaySinh=DateTime.Parse("1993-12-07"),GioiTinh=0,MaKhoa = "123TK"},
                new Student{MaSV=889900,TenSV="Lê Thị Thơm",NgaySinh=DateTime.Parse("1999-02-14"),GioiTinh=1,MaKhoa = "123GT"},
                new Student{MaSV=998877,TenSV="Hoàng Văn Trung",NgaySinh=DateTime.Parse("1992-10-31"),GioiTinh=0,MaKhoa = "123KS"},
                new Student{MaSV=887766,TenSV="Vũ Thị Hạnh",NgaySinh=DateTime.Parse("1996-06-26"),GioiTinh=1,MaKhoa = "123NN"},
                new Student{MaSV=776655,TenSV="Nguyễn Văn Tâm",NgaySinh=DateTime.Parse("1997-01-13"),GioiTinh=0,MaKhoa = "123BDS"},
                new Student{MaSV=665544,TenSV="Đỗ Thị Tuyết",NgaySinh=DateTime.Parse("1995-08-08"),GioiTinh=1,MaKhoa = "123IT"},
                new Student{MaSV=554433,TenSV="Đinh Văn Hòa",NgaySinh=DateTime.Parse("1994-04-04"),GioiTinh=0,MaKhoa = "123M"},
                new Student{MaSV=443322,TenSV="Phạm Thị Thúy",NgaySinh=DateTime.Parse("1998-09-29"),GioiTinh=1,MaKhoa = "123KD"},
                new Student{MaSV=332211,TenSV="Bùi Văn Khoa",NgaySinh=DateTime.Parse("1993-05-15"),GioiTinh=0,MaKhoa = "123D"},
                new Student{MaSV=221100,TenSV="Trần Thị Phương",NgaySinh=DateTime.Parse("1999-11-20"),GioiTinh=1,MaKhoa = "123TK"},
                new Student{MaSV=221101,TenSV="Vũ Hải Hà",NgaySinh=DateTime.Parse("1992-11-21"),GioiTinh=1,MaKhoa = "123TK"},
                new Student{MaSV=221102,TenSV="Trần Lệ Khuyên",NgaySinh=DateTime.Parse("2003-01-20"),GioiTinh=1,MaKhoa = "123IT"},
                new Student{MaSV=221103,TenSV="Trần Hà Trung",NgaySinh=DateTime.Parse("1999-12-10"),GioiTinh=0,MaKhoa = "123TK"}
            };

            context.Students.AddRange(students);
            context.SaveChanges();
        }
    }
}
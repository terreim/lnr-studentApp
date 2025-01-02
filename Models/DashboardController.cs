using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentAppPrj.Data;
using StudentAppPrj.Models;

// Dashboard controller to manage the API endpoints
public class DashboardController : Controller
{
    private readonly StudentContext _context;
    public DashboardController (StudentContext context)
    {
        _context = context;
    }

    // GET: /
    // Redirect to the React app
    [HttpGet]
    [Route("/")]
    public IActionResult Index()
    {
        return File("~/index.html", "text/html");
    }

    // GET: api/students
    // Get students with optional filters from Query parameters
    // If no filter is provided, return all students
    [HttpGet]
    [Route("api/students")]
    public async Task<IActionResult> GetStudents(
        [FromQuery] string? search,
        [FromQuery] int? gender,
        [FromQuery] string? department,
        [FromQuery] int? age_range)
    {
        var query = _context.Students
            .Include(s => s.Department)
            .Select(s => new {
                s.MaSV,
                s.TenSV,
                s.NgaySinh,
                s.GioiTinh,
                MaKhoa = s.Department.MaKhoa
            })
            .AsQueryable();

        if (!string.IsNullOrEmpty(search))
        {
            search = search.ToLower();
            query = query.Where(s => 
                s.TenSV.ToLower().Contains(search) ||
                s.MaSV.ToString().Contains(search) ||
                s.MaKhoa.ToLower().Contains(search)
            );
        }

        // Custom filter for students based on Gender, Department, and Age Range 
        if (gender.HasValue) 
        {
            query = query.Where(s => s.GioiTinh == gender.Value);
        }

        if (!string.IsNullOrEmpty(department))
        {
            query = query.Where(s => s.MaKhoa == department);
        }

        if (age_range.HasValue)
        {   
            // Calculate the current year
            var currentYear = DateTime.Now.Year;

            // Filter students by age ranges
            if (age_range == 0) // 18-20
            {
                query = query.Where(s => s.NgaySinh != null && 
                    currentYear - s.NgaySinh.Value.Year >= 18 && 
                    currentYear - s.NgaySinh.Value.Year <= 20);
            }
            else if (age_range == 1) // 21-23
            {
                query = query.Where(s => s.NgaySinh != null && 
                    currentYear - s.NgaySinh.Value.Year >= 21 && 
                    currentYear - s.NgaySinh.Value.Year <= 23);
            }
            else if (age_range == 2) // 24-26
            {
                query = query.Where(s => s.NgaySinh != null && 
                    currentYear - s.NgaySinh.Value.Year >= 24 && 
                    currentYear - s.NgaySinh.Value.Year <= 26);
            }
            else if (age_range == 3) // 27-29
            {
                query = query.Where(s => s.NgaySinh != null && 
                    currentYear - s.NgaySinh.Value.Year >= 27 && 
                    currentYear - s.NgaySinh.Value.Year <= 29);
            }
            else if (age_range == 4) // 30+
            {
                query = query.Where(s => s.NgaySinh != null && 
                    currentYear - s.NgaySinh.Value.Year >= 30);
            }
        }

        var students = await query.ToListAsync();
        return Json(students);
    }

    // POST: api/students
    // Accept student FromBody and add to the database
    // Validation rules: 
    // MaSV: Required, Unique, Range(1, int.MaxValue)
    // TenSV: Required, StringLength(30, MinimumLength = 2)
    // NgaySinh: DateType, DateRange(1980)
    // GioiTinh: Range(0, 2) - 0: Nam, 1: Nu, 2: Khac
    // MaKhoa: Required, RegularExpression(@"^[a-zA-Z0-9]*$"), StringLength(8, MinimumLength = 4)

    [HttpPost]
    [Route("api/students")]
    public async Task<IActionResult> AddStudent([FromBody] Student student)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        if (await _context.Students.AnyAsync(s => s.MaSV == student.MaSV))
            return BadRequest(new { error = "Student ID duplication!" });

        _context.Students.Add(student);
        await _context.SaveChangesAsync();

        var createdStudent = await _context.Students
            .Include(s => s.Department)
            .Select(s => new {
                s.MaSV,
                s.TenSV,
                s.NgaySinh,
                s.GioiTinh,
                MaKhoa = s.Department.MaKhoa
            })
            .FirstOrDefaultAsync(s => s.MaSV == student.MaSV);
        
        return Json(createdStudent);
    }

    // PUT: api/students
    // Update student with the provided student object
    // Validation rules are the same as POST
    [HttpPut]
    [Route("api/students")]
    public async Task<IActionResult> UpdateStudent([FromBody] Student student)
    {
        var department = await _context.Departments
            .FirstOrDefaultAsync(d => d.MaKhoa == student.MaKhoa);

        if (department == null)
            return BadRequest("Invalid department name");
    
        var existingStudent = await _context.Students
            .Include(s => s.Department)
            .FirstOrDefaultAsync(s => s.MaSV == student.MaSV);

        if (existingStudent == null)
            return BadRequest();

        existingStudent.TenSV = student.TenSV;
        existingStudent.NgaySinh = student.NgaySinh;
        existingStudent.GioiTinh = student.GioiTinh;
        existingStudent.MaKhoa = department.MaKhoa;  
        
        try
        {
            await _context.SaveChangesAsync();

            var updatedStudent = await _context.Students
                .Include(s => s.Department)
                .Select(s => new {
                    s.MaSV,
                    s.TenSV,
                    s.NgaySinh,
                    s.GioiTinh,
                    MaKhoa = s.Department.MaKhoa
                })
                .FirstOrDefaultAsync(s => s.MaSV == student.MaSV);

            return Json(updatedStudent);
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!StudentExists(student.MaSV))
                return NotFound();
            throw;
        }
    }

    // DELETE: api/students
    // Delete students with the provided list of IDs
    // Return the remaining students after deletion for React updates
    [HttpDelete]
    [Route("api/students")]
    public async Task<IActionResult> DeleteStudents([FromBody] int[] ids)
    {
        if (ids == null || ids.Length == 0)
            return BadRequest();

        var students = await _context.Students
            .Where(s => ids.Contains(s.MaSV))
            .ToListAsync();

        if (!students.Any())
            return NotFound();

        _context.Students.RemoveRange(students);
        await _context.SaveChangesAsync();

        var remainingStudents = await _context.Students
            .Include(s => s.Department)
            .Select(s => new {
                s.MaSV,
                s.TenSV,
                s.NgaySinh,
                s.GioiTinh,
                MaKhoa = s.Department.MaKhoa
            })
            .ToListAsync();

        return Json(remainingStudents);
    }

    private bool StudentExists(int id)
    {
        return _context.Students.Any(e => e.MaSV == id);
    }

    // GET: api/departments
    // Get all departments for the dropdown list
    [HttpGet]
    [Route("api/departments")]
    public async Task<IActionResult> GetDepartments()
    {
        var departments = await _context.Departments.ToListAsync();
        return Json(departments);
    }
}
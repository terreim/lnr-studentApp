// REFACTOR EVERYTHING TO USE STUDENT MODEL INSTEAD, 
// HAVE THE FRONT-END'S STUDENTFORM USES THE DEPARTMENT GET API

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentAppPrj.Data;
using StudentAppPrj.Models;

public class DashboardController : Controller
{
    private readonly StudentContext _context;
    public DashboardController (StudentContext context)
    {
        _context = context;
    }

    [HttpGet]
    [Route("/")]
    public IActionResult Index()
    {
        return File("~/index.html", "text/html");
    }

    // GET: api/students
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

        if (gender.HasValue) 
        {
            query = query.Where(s => s.GioiTinh == gender.Value);
            Console.WriteLine($"After gender filter: {query.Count()} records");
        }

        if (!string.IsNullOrEmpty(department))
        {
            query = query.Where(s => s.MaKhoa == department);
        }

        if (age_range.HasValue)
        {
            var currentYear = DateTime.Now.Year;
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
    [HttpPost]
    [Route("api/students")]
    public async Task<IActionResult> AddStudent([FromBody] Student student)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

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
    [HttpGet]
    [Route("api/departments")]
    public async Task<IActionResult> GetDepartments()
    {
        var departments = await _context.Departments.ToListAsync();
        return Json(departments);
    }
}
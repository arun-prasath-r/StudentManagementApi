using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentManagementAPI.Data;
using StudentManagementAPI.Models.DTO;
using StudentManagementAPI.Models.Entity;

namespace StudentManagementAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StudentsController : ControllerBase
{
    private readonly StudentDbContext _dbContext;

    public StudentsController(StudentDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Student>>> GetAll()
    {
        var students = await _dbContext.Students
            .AsNoTracking()
            .ToListAsync();

        return Ok(students);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<Student>> GetById(Guid id)
    {
        var student = await _dbContext.Students
            .AsNoTracking()
            .FirstOrDefaultAsync(item => item.StudentId == id);

        return student is null ? NotFound() : Ok(student);
    }

    [HttpPost]
    public async Task<ActionResult<Student>> Create(AddStudentDto dto)
    {
        var student = new Student
        {
            StudentId = Guid.NewGuid(),
            Name = dto.Name,
            Email = dto.Email,
            Phone = dto.Phone,
            Age = dto.Age,
            Course = dto.Course,
            Address = dto.Address
        };

        _dbContext.Students.Add(student);
        await _dbContext.SaveChangesAsync();

        return CreatedAtAction(
            nameof(GetById),
            new { id = student.StudentId },
            student);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, UpdateStudentDto dto)
    {
        var student = await _dbContext.Students
            .FirstOrDefaultAsync(item => item.StudentId == id);

        if (student is null)
        {
            return NotFound();
        }

        student.Name = dto.Name;
        student.Email = dto.Email;
        student.Phone = dto.Phone;
        student.Age = dto.Age;
        student.Course = dto.Course;
        student.Address = dto.Address;

        await _dbContext.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var student = await _dbContext.Students
            .FirstOrDefaultAsync(item => item.StudentId == id);

        if (student is null)
        {
            return NotFound();
        }

        _dbContext.Students.Remove(student);
        await _dbContext.SaveChangesAsync();

        return NoContent();
    }
}

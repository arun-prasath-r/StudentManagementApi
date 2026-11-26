using System.ComponentModel.DataAnnotations;

namespace StudentManagementAPI.Models.DTO;

public class UpdateStudentDto
{
    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    [MaxLength(100)]
    public string Email { get; set; } = string.Empty;

    [Required]
    [Phone]
    [MaxLength(15)]
    public string Phone { get; set; } = string.Empty;

    [Range(1, 120)]
    public int Age { get; set; }

    [Required]
    [MaxLength(100)]
    public string Course { get; set; } = string.Empty;

    [MaxLength(250)]
    public string? Address { get; set; }
}

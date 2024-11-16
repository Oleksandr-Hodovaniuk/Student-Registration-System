using Core.Models;

namespace Application.DTOs;

public class UpdateUserDTO
{
    public string Id { get; set; } = default!;
    public string? Name { get; set; } = default!;
    public string? LastName { get; set; } = default!;
    public int? Age { get; set; }
    public string? Email { get; set; } = default!;
}

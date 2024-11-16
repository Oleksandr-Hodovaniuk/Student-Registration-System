namespace Application.DTOs;

public class CreateUserDTO
{
    public string Name { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public int Age { get; set; }
    public string Email { get; set; } = default!;
}

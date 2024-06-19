using System;

public class UserModelDTO
{
	public int UserId { get; set; }
	public string Name { get; set; } = string.Empty;
	public int Age { get; set; }
	public string Email { get; set; }
	public string Password { get; set; }
	public string Role { get; set; } = "User";
}

namespace Shared.Model;

public class User
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public string Role { get; set; }
    
    public User(string firstName, string lastName, string userName, string password, string email, string role)
    {
        FirstName = firstName;
        LastName = lastName;
        UserName = userName;
        Password = password;
        Email = email;
        Role = role;
    }

   
    private User() { }
}
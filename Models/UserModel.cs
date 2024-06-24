using System.ComponentModel.DataAnnotations;

public class UserModel
{
    [Key]
    public string Username { get; set; }

    public byte[] PasswordHash { get; set; }

}
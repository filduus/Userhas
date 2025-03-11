using System;
using System.Security.Cryptography;
using System.Text;

[Serializable]
public class User
{
    public string Username { get; set; }
    public string PasswordHash { get; set; }

    public User() { }
    public User(string username, string password)
    {
        Username = username;
        PasswordHash = HashPassword(password);
    }

    public static string HashPassword(string password)
    {
        using (SHA256 sha256 = SHA256.Create())
        {
            byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(bytes);
        }
    }

    public bool VerifyPassword(string password)
    {
        return PasswordHash == HashPassword(password);
    }
}

namespace Soly.Utilities.Password;
public static class PasswordUtil
{
    public static string Hash(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password);
    }
    public static bool Check(string password, string hash)
    {
        return BCrypt.Net.BCrypt.Verify(password, hash);
    }
}

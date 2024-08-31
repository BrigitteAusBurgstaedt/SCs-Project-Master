using System.Security.Cryptography;
using System.Text;

namespace SCsProjectMaster.Source.Logic;

/// <summary>
/// Werkzeug für die Erstellung und Überprüfung der Logindaten.
/// </summary>
internal static class LoginTool
{
    private static readonly RandomNumberGenerator randomNumberGenerator = RandomNumberGenerator.Create();

    /// <summary>
    /// Die Größe des Salzes in Byte.
    /// </summary>
    private const int SALT_SIZE = 4;

    private static string ByteArrayToString(byte[] data)
    {
        StringBuilder builder = new();
        for (int i = 0; i < data.Length; i++)
        {
            builder.Append(data[i].ToString("x2"));
        }
        return builder.ToString();
    }

    /// <summary>
    /// Erzeugt eine zufällige Zeichenkette die für des salzen eines Passwortes genutzt werden kann.
    /// </summary>
    /// <returns>Passwort Salz Zeichenkette in der länge <see cref="SALT_SIZE"/>.</returns>
    private static string GenerateSalt()
    {
        var salt = new byte[SALT_SIZE];
        randomNumberGenerator.GetBytes(salt);
        return ByteArrayToString(salt);
    }

    /// <summary>
    /// Erzeugt zu einem gegeben Passwort den Hashwert und Salz.
    /// </summary>
    /// <param name="password">Das Passwort, dass gehasht werden soll.</param>
    /// <param name="salt">Das Salz, dass für das hashen verwendet wurde.</param>
    /// <returns>Der Hashwert als Zeichenkette</returns>
    public static string HashPassword(string password, out string salt)
    {
        salt = GenerateSalt();
        return ByteArrayToString(SHA256.HashData(Encoding.UTF8.GetBytes(password + salt)));
    }

    /// <summary>
    /// Überprüft ob ein gehashtes Passwort zu einem angegebenen Passwort passt.
    /// </summary>
    /// <param name="hashedPassword">Das gehashte Passwort.</param>
    /// <param name="providedPassword">Das angegebene Passwort.</param>
    /// <param name="salt">Das Salz zum angegebenen Passwort.</param>
    /// <returns></returns>
    public static bool VerifyHashedPassword(string hashedPassword, string providedPassword, string salt)
    {
        return hashedPassword == ByteArrayToString(SHA256.HashData(Encoding.UTF8.GetBytes(providedPassword + salt)));
    }
}

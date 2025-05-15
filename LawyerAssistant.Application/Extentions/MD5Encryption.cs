using System.Text;
using System.Security.Cryptography;

namespace LawyerAssistant.Application.Extentions;

public static class MD5Encryption
{
    public static string HashMD5(this string Value)
    {
        byte[] Buffer = Encoding.UTF8.GetBytes(Value);
        MD5 md = MD5.Create();
        return Convert.ToBase64String(md.ComputeHash(Buffer));
    }
}

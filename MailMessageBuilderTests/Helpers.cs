using System.IO;

namespace MailMessageBuilderTests;

public class Helpers
{
    public static byte[] ReadFully(Stream input)
    {
        using var ms = new MemoryStream();
        input.CopyTo(ms);
        return ms.ToArray();
    }
}
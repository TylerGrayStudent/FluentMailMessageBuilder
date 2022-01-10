using System.IO;

namespace FluentMailMessageBuilderTests;

public class Helpers
{
    public static byte[] ReadFully(Stream input)
    {
        using var ms = new MemoryStream();
        input.CopyTo(ms);
        return ms.ToArray();
    }
}
using System.Security.Cryptography;
using System.Text;

namespace NextAmongUsLauncher.Core.Utils;

public static class HashUtils
{
    private static readonly MD5 md5 = MD5.Create();
    private static readonly SHA256 sha256 = SHA256.Create();
    
    public static string GetMD5(string path)
    {
        using var stream = FileUtils.GetStream(path);
        return GetMD5(stream);
    }

    public static string GetMD5(Stream stream)
    {
        var buffers = md5.ComputeHash(stream);
        md5.Clear();
        return BuildString(buffers);
    }
    
    public static string GetSHA256(string path)
    {
        using var stream = FileUtils.GetStream(path);
        return GetSHA256(stream);
    }

    public static string GetSHA256(Stream stream)
    {
        var buffers = sha256.ComputeHash(stream);
        sha256.Clear();
        return BuildString(buffers);
    }
    
    public static string GetHash(this HashAlgorithm algorithm, string path)
    {
        using var stream = FileUtils.GetStream(path);
        return GetHash(algorithm, stream);
    }
    
    public static string GetHash(this HashAlgorithm algorithm, Stream stream)
    {
        var buffers = algorithm.ComputeHash(stream);
        algorithm.Clear();
        return BuildString(buffers);
    }

    public static string BuildString(byte[] buffers)
    {
        var Builder = new StringBuilder();
        foreach (var buffer in buffers)
        {
            Builder.Append(buffer.ToString("x2"));
        }
        return Builder.ToString();
    }
}
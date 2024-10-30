using Microsoft.AspNetCore.Http;

namespace MotorDoctor.Business.Extensions;
public static class FileValidator
{
    public static bool ValidateSize(this IFormFile file, int mb)
    {
        return file.Length <= mb * 1024 * 1024;
    }

    public static bool ValidateType(this IFormFile file, string type = "image")
    {
        return file.ContentType.Contains(type);
    }
}
namespace E_Commerce_Test_Project_MVC.Helpers.Extensions
{
    public static class FileExtensions
    {
        public static bool CheckFileSize(this IFormFile file,int size)
        {
            return file.Length / 1024 > size;
        }

        public static bool CheckFileType(this IFormFile file,string type)
        {
            return file.ContentType.Contains(type);
        }
        public static void Delete(this string path)
        {
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }
    }
}

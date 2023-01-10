namespace Store.Api.Helpers
{
    public class FileManager
    {
        public static string Save(IFormFile file, string rootPath, string folder)
        {
            string fileName = file.FileName;
            string newFileName = Guid.NewGuid().ToString() + (fileName.Length > 64 ? fileName.Substring(fileName.Length - 64) : fileName);
            string path = Path.Combine(rootPath, folder, newFileName);

            using (FileStream fs = new FileStream(path, FileMode.Create))
            {
                file.CopyTo(fs);
            }

            return newFileName;
        }

        public static void Delete(string rootPath, string folder, string filename)
        {
            string path = Path.Combine(rootPath, folder, filename);

            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }
    }
}

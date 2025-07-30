namespace Domain.Storage;

public static class PathHelper
{
    public static string MakeSafeName(string name)
    {
        foreach (var c in Path.GetInvalidFileNameChars())
        {
            name = name.Replace(c, '_');
        }

        return name;
    }

    public static void DeleteFolderRecursively(string path)
    {
        if (!Directory.Exists(path))
            return;
        
        foreach (var file in Directory.GetFiles(path))
        {
            File.Delete(file);
            
        }
        
        foreach (var folder in Directory.GetDirectories(path))
        {
            DeleteFolderRecursively(folder); 
        }
        
        Directory.Delete(path);
    }

    public static void MoveDirectoryContents(string sourceDir, string destDir)
    {
        if (!Directory.Exists(destDir))
            Directory.Move(sourceDir, destDir);

        foreach (var filePath in Directory.GetFiles(sourceDir))
        {
           var fileName = Path.GetFileName(filePath);
           var fileDestPath = Path.Combine(destDir, fileName);
           
           if (File.Exists(filePath))
               throw new Exception("File already exists");
           
           File.Move(filePath, fileDestPath);
        }
    }
}
namespace FinderClone.Storage;

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
}
namespace ManipulateFiles.Helper;

public class FileHelper
{
    /** DIRECTORIES **/
    // List directories
    public void ListDirectories(string path)
    {
        var returnDirectories = Directory.GetDirectories(path, "*", SearchOption.AllDirectories);

        foreach (var eachDirectory in returnDirectories)
        {
            Console.WriteLine(eachDirectory);
        }
    }

    // Create directory
    public void CreateDirectory(string path)
    {
        var directoryInfo = Directory.CreateDirectory(path);
        Console.WriteLine(directoryInfo.FullName);
    }

    // Delete a directory
    public void DeleteDirectory(string path, bool deleteFiles)
    {
        Directory.Delete(path, deleteFiles);
    }

    /** FILES **/
    // List files from directories
    public void ListFilesDirectories(string path)
    {
        var returnFiles = Directory.GetFiles(path, "*", SearchOption.AllDirectories);

        foreach (var eachFile in returnFiles)
        {
            Console.WriteLine(eachFile);
        }
    }

    // Create text file (without stream)
    public void CreateTextFile(string path, string content)
    {
        if (!File.Exists(path))
        {
            File.WriteAllText(path, content);
        }
    }

    // Create text file with stream
    public void CreateTextFileStream(string path, List<string> fileContent)
    {
        using (var stream = File.CreateText(path))
        {
            foreach (var line in fileContent)
            {
                stream.WriteLine(line);
            }
        }
    }

    // Add new lines to a file (without stream)
    public void AddTextToFile(string path, string addContent)
    {
        File.AppendAllText(path, addContent);
    }

    // Add new lines to a file with stream
    public void AddTextToFileStream(string path, List<string> addContent)
    {
        using (var stream = File.AppendText(path))
        {
            foreach (var line in addContent)
            {
                stream.WriteLine(line);
            }
        }
    }

    // Read file (without stream)
    public void ReadFile(string path)
    {
        var fileContent = File.ReadAllLines(path);

        foreach (var line in fileContent)
        {
            Console.WriteLine(line);
        }
    }

    // Read file with stream
    public void ReadFileStream(string path)
    {
        string line = string.Empty;

        using (var stream = File.OpenText(path))
        {
            while ((line = stream.ReadLine()) != null)
            {
                Console.WriteLine(line);
            }
        }
    }

    // Move file
    public void MoveFile(string path, string newPath, bool overwrite)
    {
        File.Move(path, newPath, overwrite);
    }

    // Copy file
    public void CopyFile(string path, string newPath, bool overwrite)
    {
        File.Copy(path, newPath, overwrite);
    }

    // Delete file
    public void DeleteFile(string path)
    {
        File.Delete(path);
    }
}
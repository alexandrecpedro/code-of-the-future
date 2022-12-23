using ManipulateFiles.Helper;

namespace ManipulateFiles;

public class Program
{
    static void Main(string[] args)
    {
        var path = Environment.GetEnvironmentVariable("MANIPULATING_FILES");
        var pathPathCombine = Path.Combine(path!, "Directory Test 3", "Subdirectory Test 3");
        var pathDelete = Path.Combine(path!, "Directory Test 3");
        var pathFile = Path.Combine(path!, "file-test.txt");
        var pathFileCopy = Path.Combine(path!, "file-test-backup.txt");
        var pathFileStream = Path.Combine(path!, "file-test-stream.txt");

        var newPathFile = Path.Combine(path!, "Directory Test 2", "file-test-stream.txt");

        var contentFile = """
            Hello!
            File writing test!
        """;

        var stringList = new List<string>();
        for (int i = 1; i < 4; i++)
        {
            stringList.Add($"Line {i}");
        }

        var stringListContinue = new List<string>();
        for (int i = 4; i < 7; i++)
        {
            stringList.Add($"Line {i}");
        }

        FileHelper helper = new FileHelper();
        // helper.ListDirectories(path!);
        // helper.ListFilesDirectories(path!);
        // helper.CreateDirectory(pathPathCombine);
        // helper.DeleteDirectory(pathDelete, true);

        // helper.CreateTextFile(pathFile, contentFile);
        // helper.CreateTextFileStream(pathFileStream, stringList);
        // helper.AddTextToFileStream(pathFileStream, stringListContinue);
        // helper.ReadFileStream(pathFileStream);
        // helper.MoveFile(pathFile, newPathFile, false);
        // helper.CopyFile(pathFile, pathFileCopy, false);
        helper.DeleteFile(pathFileCopy);
    }
}
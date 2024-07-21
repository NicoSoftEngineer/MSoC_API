using MSoC_API.Models;
using Newtonsoft.Json;
using System.Formats.Tar;
using System.Net.Mime;
using System.Text;
using MSoC_API.Utils;
using Microsoft.AspNetCore.StaticFiles;

namespace MSoC_API.Services;

public class FileService(FileSystemOptions fileSystemOptions)
{
    public int MyProperty { get; set; }
    public string GetAllFilesInFolder()
    {
        var d = new DirectoryInfo(fileSystemOptions.FileSystemPath);//test folder with random .txt files

        var files = d.GetFiles();//get all files in folder

        
        var fileList = new FileListModel
        {
            Directory = d.FullName, // full path to folder
            FileNames = files.Select(f => f.Name).ToArray(), // select only names of folders, and convert to array
        };

        var json = JsonConvert.SerializeObject(fileList); //serialize FileListModel to json

        return json;
    }

    public async Task<FileContent?> GetFile(string fileName)
    {
        var filePath = Path.Combine(fileSystemOptions.FileSystemPath, fileName);
        if (!File.Exists(filePath))
        {
            return null;
        }

        var provider = new FileExtensionContentTypeProvider();
        if (!provider.TryGetContentType(filePath, out var contentType))
        {
            contentType = "text/plain";
        }

        var file = new FileContent
        {
            Name = fileName,
            Content = await File.ReadAllBytesAsync(filePath),
            ContentType = contentType,
        };

        return file;
    }
    //function that returns content of a file
    public async Task<string?> GetFileContent(string fileName)
    {
        var file = await GetFile(fileName);

        if (file == null)
        {
            return null;
        }

        return Encoding.UTF8.GetString(file.Content);
    }
    //function that returns a certain lines of a file, specified by line number. start number and end number
    public async Task<string?> GetFileContentInRange(string fileName, int start, int end)
    {
        var file = await GetFileContent(fileName);

        if (file == null)
        {
            return null;
        }

        var lines = file.Split('\n');

        if (start < 0 || start >= lines.Length || end < 0)
        {
            return null;
        }

        var sb = new StringBuilder();
        for (var i = start; i <= end; i++)
        {
            sb.AppendLine(lines[i]);
        }

        return sb.ToString();
    }
}
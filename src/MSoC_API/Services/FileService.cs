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
}
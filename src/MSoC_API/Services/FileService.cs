using MSoC_API.Models;
using Newtonsoft.Json;

namespace MSoC_API.Services;

public class FileService
{
    public string GetAllFilesInFolder()
    {
        var d = new DirectoryInfo(@"./FileSystem");//test folder with random .txt files

        var files = d.GetFiles();//get all files in folder

        
        var fileList = new FileListModel
        {
            Directory = d.FullName, // full path to folder
            FileNames = files.Select(f => f.Name).ToArray(), // select only names of folders, and convert to array
        };

        var json = JsonConvert.SerializeObject(fileList); //serialize FileListModel to json

        return json;
    }
}
using Application.Contracts.Application;
using Application.Enums;
using LawyerAssistant.Application.Contracts.Common;
using LawyerAssistant.Application.Objects;
using Microsoft.Extensions.Options;

namespace LawyerAssistant.Application.Utilities;

public class FileComponent : IFileComponent, IScoped 
{
    IOptions<AppConfig> appConfigOptions;
    string FileData = string.Empty;
    string FileName = string.Empty;
    string FileExtention = string.Empty;
    FileFolder fileFolder;
    //=====================================================================
    public FileComponent(IOptions<AppConfig> _appConfigOptions)
    {
        appConfigOptions = _appConfigOptions;
    }
    //=====================================================================
    public async Task<string> SaveAsync(FileFolder _fileFolder, string _fileData, string? fileOriginalName = "")
    {
        FileData = _fileData;
        fileFolder = _fileFolder;
        string? originalName = string.IsNullOrWhiteSpace(fileOriginalName) ? " " : fileOriginalName.Trim().ToLower();
        if (!string.IsNullOrEmpty(FileData) && FileData.StartsWith("data:"))
        {
            FileExtention = GetImageExtention();
            FileName = GetFileName(originalName);
        }
        DirectoryInitial(fileFolder);
        if (FileData == "NULL" || string.IsNullOrEmpty(FileData))
            return null;
        if (FileData.StartsWith("data:"))
        {
            await SaveToDisk();
            return GetFileUrlPath();
        }
        else
        {
            return FileData;
        }
    }
    //=====================================================================
    string GetImageExtention()
    {
        string FileType = FileData.Split(';')[0].Split('/')[1].ToString();
        if (FileType.ToUpper() == "JPEG")
        {
            FileType = "jpg";
        }
        return FileType;
    }
    //=====================================================================
    string GetFilePhysicalPath()
    {
        return Path.Combine(appConfigOptions.Value.appFilePath, fileFolder.ToString(), FileName + "." + FileExtention);
    }
    //=====================================================================
    string GetFileUrlPath()
    {
        return "/" + fileFolder + "/" + FileName + "." + FileExtention;
    }
    //=====================================================================
    void DirectoryInitial(FileFolder _fileFolder)
    {
        if (!Directory.Exists(appConfigOptions.Value.appFilePath))
            Directory.CreateDirectory(appConfigOptions.Value.appFilePath);
        if (!Directory.Exists(Path.Combine(appConfigOptions.Value.appFilePath, _fileFolder.ToString())))
            Directory.CreateDirectory(GetImageDirectory());
    }
    //=====================================================================
    string GetImageDirectory()
    {
        return Path.Combine(appConfigOptions.Value.appFilePath, fileFolder.ToString());
    }
    //======================================================================
    string GetFileName(string? name)
    {
        return string.IsNullOrWhiteSpace(name) ? Guid.NewGuid().ToString().Replace("-", "").Replace(' ', '-')  : name + "_" + Guid.NewGuid().ToString().Replace("-", "").Replace(' ', '-') ;
    }
    //======================================================================
    async Task SaveToDisk()
    {
        var buffer = Convert.FromBase64String(FileData.Split(',')[1]);
        using (var fileStream = new FileStream(GetFilePhysicalPath(), FileMode.Create, FileAccess.Write))
        {
            await fileStream.WriteAsync(buffer, 0, buffer.Length);
        }
    }
    //======================================================================
    
}

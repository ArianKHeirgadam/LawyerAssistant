using Application.Enums;

namespace Application.Contracts.Application
{
    public interface IFileComponent
    {
        Task<string> SaveAsync(FileFolder _fileFolder, string _fileData, string? fileOriginalName = "");
    }
}
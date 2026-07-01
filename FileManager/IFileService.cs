using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace QlThietBi.FileManager
{
    public interface IFileService  
    {  
        Task<List<string>> UploadFile(List<IFormFile> files, string subDirectory);  
        (string fileType, byte[] archiveData, string archiveName) DownloadFiles(string subDirectory);  
        string SizeConverter(long bytes);  
    }  
}
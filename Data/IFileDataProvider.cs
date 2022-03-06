using Intuit.VideoApp.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Intuit.VideoApp.Data
{
    public interface IFileDataProvider
    {
        Task<IEnumerable<FileModel>> GetFilesAsync(string path);
    }
}

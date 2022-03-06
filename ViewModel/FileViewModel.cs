using Intuit.VideoApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intuit.VideoApp.ViewModel
{
    public class FileViewModel
    {
        public FileViewModel(FileModel fileModel)
        {
            Name = fileModel.Name;
            Size = fileModel.Size;
            Path = fileModel.Path;
            CanDownload = fileModel.CloudBacked;
            CanUpload = !fileModel.CloudBacked;
        }

        public string? Name { get; set; }
        public int Size { get; set; }
        public string? Path { get; set; }
        public bool CanDownload { get; set; }
        public bool CanUpload { get; set; }
    }
}

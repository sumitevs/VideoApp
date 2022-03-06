using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intuit.VideoApp.Model
{
    public class FileModel
    {
        public FileModel(string? name, int size, string? path, bool cloudBacked)
        {
            Name = name;
            Size = size;
            Path = path;
            CloudBacked = cloudBacked;
        }

        public string? Name { get; set; }
        public int Size { get; set; }
        public string? Path { get; set; }
        public bool CloudBacked { get; set; }
    }
}

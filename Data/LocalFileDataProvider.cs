using Intuit.VideoApp.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Intuit.VideoApp.Data
{
   
    public class LocalFileDataProvider : IFileDataProvider
    {
        //public async Task<IEnumerable<FileModel>> GetFilesAsync(string path)
        //{
        //    await Task.Delay(1000);
        //    List<FileModel> fileModelList = new List<FileModel>();
        //    System.IO.DirectoryInfo directoryInfo = new System.IO.DirectoryInfo(path);
        //    System.IO.FileInfo[] fileInfoList = directoryInfo.GetFiles("*.mp4");
        //    foreach (var fileInfoItem in fileInfoList)
        //    {
        //        fileModelList.Add(new FileModel(fileInfoItem.Name, (int)fileInfoItem.Length));
        //    }
        //    return fileModelList;
        //}
        public async Task<IEnumerable<FileModel>> GetFilesAsync(string path)
        {
            await Task.Delay(1000);
            List<FileModel> fileModelList = new List<FileModel>();
            System.IO.DirectoryInfo directoryInfo = new System.IO.DirectoryInfo(path);
            System.IO.FileInfo[] fileInfoList = directoryInfo.GetFiles("*.mp4");
            foreach (var fileInfoItem in fileInfoList)
            {
                fileModelList.Add(new FileModel(fileInfoItem.Name, (int)fileInfoItem.Length/1000000, fileInfoItem.FullName, false));
            }
            return fileModelList;
        }
    }
}

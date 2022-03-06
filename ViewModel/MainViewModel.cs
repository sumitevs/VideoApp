using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Intuit.VideoApp.Command;
using Intuit.VideoApp.Data;
using Intuit.VideoApp.Model;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;

namespace Intuit.VideoApp.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private readonly IFileDataProvider _localFileDataProvider;
        private readonly IFileDataProvider _cloudFileDataProvider;
        private string _currentLocalFolderPath;
        private string currentCloudFolderPath;
        private FileViewModel? selectedFile;

        public ObservableCollection<FileViewModel> LocalFileCollection { get; set; } = new ObservableCollection<FileViewModel>();
        public ObservableCollection<FileViewModel> CloudFileCollection { get; set; } = new ObservableCollection<FileViewModel>();

        public FileViewModel? SelectedFile
        {
            get => selectedFile;
            set
            {
                selectedFile = value;
                RaisePropertyChanged();
                RaisePropertyChanged(nameof(IsFileSelected));
            }
        }
        public bool IsFileSelected => SelectedFile is not null;

        public string CurrentLocalFolderPath
        {
            get => _currentLocalFolderPath;
            set
            {
                _currentLocalFolderPath = value;
                RaisePropertyChanged();
            }
        }
        public string CurrentCloudFolderPath
        {
            get => currentCloudFolderPath;
            set
            {
                currentCloudFolderPath = value;
                RaisePropertyChanged();
            }
        }
        public MainViewModel(IFileDataProvider localFileDataProvider, IFileDataProvider cloudFileDataProvider)
        {
            _localFileDataProvider = localFileDataProvider;
            _cloudFileDataProvider = cloudFileDataProvider;
            OpenCommand = new DelegateCommand(LoadLocalFiles);
            OpenCloudCommand = new DelegateCommand(LoadCloudFiles);
            DownloadCommand = new DelegateCommand(DownloadFile);
            UploadCommand = new DelegateCommand(UploadFile);
        }

        public DelegateCommand OpenCommand { get; }
        public DelegateCommand OpenCloudCommand { get; }
        public DelegateCommand DownloadCommand { get; }
        public DelegateCommand UploadCommand { get; }
        private async void LoadLocalFiles(object? parameter)
        {
            System.Windows.Forms.FolderBrowserDialog openFileDlg = new System.Windows.Forms.FolderBrowserDialog();
            var result = openFileDlg.ShowDialog();
            if (string.IsNullOrWhiteSpace(result.ToString()))
            {
                return;
            }
            CurrentLocalFolderPath = openFileDlg.SelectedPath;

            if (string.IsNullOrWhiteSpace(CurrentLocalFolderPath))
            {
                return;
            }
            var localFiles = await _localFileDataProvider.GetFilesAsync(CurrentLocalFolderPath);
            LocalFileCollection.Clear();
            if (localFiles is not null)
            {
                foreach (var file in localFiles)
                {
                    LocalFileCollection.Add(new FileViewModel(file));
                }
            }
        }

        private async void LoadCloudFiles(object? parameter)
        {
            if (string.IsNullOrWhiteSpace(CurrentCloudFolderPath))
            {
                return;
            }
            var cloudFiles = await _cloudFileDataProvider.GetFilesAsync(CurrentCloudFolderPath);
            CloudFileCollection.Clear();
            if (cloudFiles is not null)
            {
                foreach (var file in cloudFiles)
                {
                    CloudFileCollection.Add(new FileViewModel(file));
                }
            }
        }

        private async void DownloadFile(object? parameter)
        {
            if (string.IsNullOrWhiteSpace(SelectedFile?.Path))
            {
                return;
            }
            string localFileName = SelectedFile.Name;

            BlobClient blobClient = new BlobClient(new System.Uri(SelectedFile.Path));
            bool exists = await blobClient.ExistsAsync();
            if (exists)
            {
                BlobDownloadInfo blobDownloadInfo = await blobClient.DownloadAsync();

                using FileStream fileStream = File.OpenWrite(localFileName);
                await blobDownloadInfo.Content.CopyToAsync(fileStream);
            }
        }
        private async void UploadFile(object? parameter)
        {
            //todo
        }
    }
}

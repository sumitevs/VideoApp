using Intuit.VideoApp.Data;
using Intuit.VideoApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Windows;

namespace Intuit.VideoApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool isPlaying;
        private readonly MainViewModel _viewModel;

        public MainWindow()
        {
            InitializeComponent();
            _viewModel = new MainViewModel(new LocalFileDataProvider(),new CloudFileDataProvider());
            DataContext = _viewModel;
            Loaded += CloudFiles_Loaded;
            selectedMedia.Volume = 100;
        }

        private void CloudFiles_Loaded(object sender, RoutedEventArgs e)
        {
            //todo
            return;
        }


        private void ButtonPlayPause_Click(object sender, RoutedEventArgs e)
        {
            if (!isPlaying)
            {
                selectedMedia.Play();
                buttonPlayPause.Content = "Pause";
            }
            else
            {
                selectedMedia.Pause();
                buttonPlayPause.Content = "Play";
            }
            isPlaying = !isPlaying;
        }

        private void ButtonMute_Click(object sender, RoutedEventArgs e)
        {
            if (selectedMedia.Volume == 100)
            {
                selectedMedia.Volume = 0;
                buttonMute.Content = "Unmute";
            }
            else
            {
                selectedMedia.Volume = 100;
                buttonMute.Content = "Mute";
            }
        }

        private void ButtonUpload_Click(object sender, RoutedEventArgs e)
        {

        }

      
    }
}


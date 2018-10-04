using System;
using System.Windows;
using System.Windows.Controls;
using System.Collections.ObjectModel;
using Melo.ClientEntities;
using Melo.Service.Interface;
using Melo.Service.Simple;
using Melo.Constants;
using Microsoft.WindowsAPICodePack.Dialogs;

namespace Melo
{
    /// <summary>
    /// Interaction logic for Directories.xaml
    /// </summary>
    public partial class Directories : UserControl
    {

        IDirectyService directoryService;
        IImageService imageService;
        IVideoService videoService;
        IMusicService musicService;

        ObservableCollection<Directory> myDirectories;
                
        public Directories()
        {
            directoryService = new SimpleDirectoryService(new SimpleConnectionCreater());
            imageService = new SimpleImageService(new SimpleConnectionCreater());
            videoService = new SimpleVideoService(new SimpleConnectionCreater());
            musicService = new SimpleMusicService(new SimpleConnectionCreater());

            myDirectories = new ObservableCollection<Directory>(directoryService.GetAll());
            InitializeComponent();
            this.directories.ItemsSource = myDirectories;
            

        }

        private void DeleteDirectory(object sender, RoutedEventArgs e)
        {
            Button b = sender as Button;
            Directory directory = b.CommandParameter as Directory;
            directoryService.DeleteById(directory.Id);
            myDirectories = new ObservableCollection<Directory>(directoryService.GetAll());
            this.directories.ItemsSource = myDirectories;
        }

        private void AddDirectory(object sender, RoutedEventArgs e)
        {
            //Configure File dialog
            if (CommonFileDialog.IsPlatformSupported)
            {
                var folderSelectorDialog = new CommonOpenFileDialog();
                folderSelectorDialog.EnsureReadOnly = true;
                folderSelectorDialog.IsFolderPicker = true;
                folderSelectorDialog.AllowNonFileSystemItems = false;
                folderSelectorDialog.Multiselect = false;
                folderSelectorDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                folderSelectorDialog.Title = "Project Location";

                //If user selected a folder
                if (folderSelectorDialog.ShowDialog() == CommonFileDialogResult.Ok)
                {
                    string path = folderSelectorDialog.FileName;
                    string[] splitted = path.Split( '\\' );
                    string name = splitted[splitted.Length - 1];
                    Directory dir = new Directory(name, path);
                    dir = directoryService.Add(dir);
                    SimpleFileSearcher fileSearcher = new SimpleFileSearcher(dir, new Extensions(), musicService, videoService, imageService);
                    myDirectories = new ObservableCollection<Directory>(directoryService.GetAll());
                    this.directories.ItemsSource = myDirectories;
                }
            }
        }
    }
}

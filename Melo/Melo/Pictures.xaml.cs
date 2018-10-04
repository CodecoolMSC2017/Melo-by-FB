using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Collections.ObjectModel;
using Melo.Service.Interface;
using Melo.Service.Simple;
using Melo.ClientEntities;


namespace Melo
{
    /// <summary>
    /// Interaction logic for Pictures.xaml
    /// </summary>
    public partial class Pictures : UserControl
    {
        IImageService imageService;
        ObservableCollection<ClientEntities.Image> myPictures;
        ClientEntities.Image selectedImage;

        public Pictures()
        {
            imageService = new SimpleImageService(new SimpleConnectionCreater());
            InitializeComponent();
            myPictures = new ObservableCollection<ClientEntities.Image>(imageService.GetAll());
            if(myPictures.ToArray().Length > 0)
            {
                selectedImage = myPictures.ToArray()[0];
            }
            pictures.ItemsSource = myPictures;
            this.DataContext = selectedImage;
        }

        private void SelectImage(object sender, RoutedEventArgs e)
        {
            Button b = sender as Button;
            ClientEntities.Image image = b.CommandParameter as ClientEntities.Image;
            selectedImage = image;
            this.DataContext = selectedImage;
        }
    }
}

using System.Windows.Controls;
using Melo.ClientEntities;
using Melo.ViewModel;

namespace Melo
{
    /// <summary>
    /// Interaction logic for Musics.xaml
    /// </summary>
    public partial class Musics : UserControl
    {
        public static Music selectedMusic;
        public static MusicCategory selectedCategory;
        public static Music editableMusic;
        public Musics()
        {
            InitializeComponent();
            this.DataContext = new MusicViewModel();
        }

        
    }
    
}

using CheckersMAP.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CheckersMAP.Views
{
    /// <summary>
    /// Interaction logic for HelpMenu.xaml
    /// </summary>
    public partial class HelpMenu : Window
    {
        public HelpMenu()
        {
            InitializeComponent();
        }
        private void About_Click(object sender, RoutedEventArgs e)
        {
            // Creare instanță a clasei HelpMenuViewModels
            HelpMenuViewModels viewModel = HelpMenuViewModels.Instance;

            // Apelarea metodei GetAboutInformation din viewModel
            string aboutInformation = viewModel.GetAboutInformation();

            // Afișarea informațiilor într-un MessageBox
            MessageBox.Show(aboutInformation, "About");
        }

    }
}

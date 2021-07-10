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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace JournalsClient.CreatingJournalPages
{
    /// <summary>
    /// Логика взаимодействия для NameChosing_CJP_.xaml
    /// </summary>
    public partial class NameChosing_CJP_ : Page
    {
        private AccessChosing_CJP_ AccessChosingPage;
        public NameChosing_CJP_()
        {
            InitializeComponent();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            
            NavigationService.GoBack();
        }

        private void Next_Click(object sender, RoutedEventArgs e)
        {
            if (AccessChosingPage == null)
            {
                AccessChosingPage = new AccessChosing_CJP_();
                NavigationService.Navigate(AccessChosingPage);
            }
            else
            {
                NavigationService.Navigate(AccessChosingPage);
            }
        }
    }
}

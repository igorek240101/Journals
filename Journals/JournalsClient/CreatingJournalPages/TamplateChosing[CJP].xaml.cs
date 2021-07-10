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
    /// Логика взаимодействия для TamplateChosing_CJP_.xaml
    /// </summary>
    public partial class TamplateChosing_CJP_ : Page
    {
        private NameChosing_CJP_ NameChosingPage;
        public TamplateChosing_CJP_()
        {
            InitializeComponent();
        }

        private void Next_Click(object sender, RoutedEventArgs e)
        {
            if (NameChosingPage == null)
            {
                NameChosingPage = new NameChosing_CJP_();
                NavigationService.Navigate(NameChosingPage);
            }
            else
            {
                NavigationService.Navigate(NameChosingPage);
            }
        }
    }
}

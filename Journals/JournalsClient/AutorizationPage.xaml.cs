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

namespace JournalsClient
{
    /// <summary>
    /// Логика взаимодействия для AutorizationPage.xaml
    /// </summary>
    public partial class AutorizationPage : Page
    {
        private MainWindow startWindow;
        private RegistrationPage RegisterPage;
        public AutorizationPage(MainWindow mW)
        {
            InitializeComponent();
            startWindow = mW;
            
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (RegisterPage == null)
            {
                RegisterPage = new RegistrationPage();
                NavigationService.Navigate(RegisterPage);
            }
            else 
            {
                NavigationService.Navigate(RegisterPage);
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            JournalsWindow journals = new JournalsWindow();
            journals.Show();
            
            startWindow.Close();
        }
    }
}

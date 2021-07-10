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
using JournalsClient.CreatingJournalPages;
using JournalsClient.Account;


namespace JournalsClient
{
    /// <summary>
    /// Логика взаимодействия для JournalsWindow.xaml
    /// </summary>
    public partial class JournalsWindow : Window
    {
        private JournalsList JournalLst;
        private TamplateChosing_CJP_ TamplateChosing;
        private MainAccount MainAccountPage;
        public JournalsWindow()
        {
            InitializeComponent();

        }

        

        private void JournalList_Click(object sender, RoutedEventArgs e)
        {
            if (JournalLst == null)
            {
                JournalLst = new JournalsList(this);
                WorkFrame.Content = JournalLst;
            }
            else
            {
                WorkFrame.Content = JournalLst;
            }
        }

        private void JournalCreating_Click(object sender, RoutedEventArgs e)
        {
            if (JournalLst == null)
            {
                TamplateChosing = new TamplateChosing_CJP_();
                WorkFrame.Content = TamplateChosing;
            }
            else
            {
                WorkFrame.Content = TamplateChosing;
            }
        }

        private void Account_Click(object sender, RoutedEventArgs e)
        {
            if (MainAccountPage == null)
            {
                MainAccountPage = new MainAccount();
                WorkFrame.Content = MainAccountPage;
            }
            else
            {
                WorkFrame.Content = MainAccountPage;
            }
        }
    }
}

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

namespace CryptoApplicationWPF
{
    /// <summary>
    /// Interaction logic for ChoiceWindow.xaml
    /// </summary>
    public partial class ChoiceWindow : Window
    {
        public ChoiceWindow()
        {
            InitializeComponent();
        }

        private void EncryptButton_Click(object sender, RoutedEventArgs e)
        {
            Window encryptWindow = new EncryptWindow();
            encryptWindow.Show();
            this.Close();
        }

        private void DecryptButton_Click(object sender, RoutedEventArgs e)
        {
            Window decryptWindow = new DecryptWindow();
            decryptWindow.Show();
            this.Close();
        }
    }
}

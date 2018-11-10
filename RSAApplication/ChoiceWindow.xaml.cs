using RSA;
using RSA.Models;
using System.Windows;

namespace RSAApplication
{
    /// <summary>
    /// Interaction logic for ChoiceWindow.xaml
    /// </summary>
    public partial class ChoiceWindow : Window
    {
        public FullKey Key { get; set; }

        public ChoiceWindow()
        {
            InitializeComponent();
            DataContext = this;
            Key = RSAAlgorithm.GenerateKey();
            UpdateKeysTextBoxes();
        }

        private void EncryptButton_Click(object sender, RoutedEventArgs e)
        {
            Window encryptWindow = new EncryptWindow(Key);
            encryptWindow.Show();
            this.Close();
        }

        private void DecryptButton_Click(object sender, RoutedEventArgs e)
        {
            Window decryptWindow = new DecryptWindow(Key);
            decryptWindow.Show();
            this.Close();
        }

        private void GenerateKeyButton_OnClick(object sender, RoutedEventArgs e)
        {
            Key = RSAAlgorithm.GenerateKey();
            UpdateKeysTextBoxes();
        }

        private void UpdateKeysTextBoxes()
        {
            TextBoxKeyN.Text = Key.N.ToString();
            TextBoxKeyD.Text = Key.D.ToString();
            TextBoxKeyE.Text = Key.E.ToString();
        }
    }
}

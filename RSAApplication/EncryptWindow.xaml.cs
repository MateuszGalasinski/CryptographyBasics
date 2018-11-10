using Microsoft.Win32;
using RSA;
using RSA.Extensions;
using RSA.Models;
using System.IO;
using System.Text;
using System.Windows;

namespace RSAApplication
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class EncryptWindow : Window
    {
        private readonly FullKey _key;
        private string filePath = string.Empty;
        private BigInteger encryptedValue;

        public EncryptWindow(FullKey key)
        {
            _key = key;
            InitializeComponent();
        }

        private void EncryptButton_Click(object sender, RoutedEventArgs e)
        {
            byte[] textToEncodeBytes;

            if (filePath == string.Empty)
            {
                textToEncodeBytes = Encoding.ASCII.GetBytes(TextToEncryptTextBox.Text);
            }
            else
            {
                textToEncodeBytes = File.ReadAllBytes(filePath);
            }

            textToEncodeBytes = RSA.Padding.AddPadding(textToEncodeBytes, RSAAlgorithm.NumberOfBytes);

            encryptedValue = RSAAlgorithm.Encrypt(new BigInteger(textToEncodeBytes), _key.E, _key.N);

            EncryptedTextBox.Text = encryptedValue.ToString();
        }

        private void ChooseFileButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Title = "Choose file to ecrypt: ";
            if (fileDialog.ShowDialog() == true)
            {
                filePath = fileDialog.FileName;
                TextToEncryptTextBox.Text = filePath;
            }
        }

        private void SaveToFileButton_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog fileDialog = new SaveFileDialog();
            fileDialog.Title = "Save file to: ";
            if (fileDialog.ShowDialog() == true)
            {
                File.WriteAllText(fileDialog.FileName, encryptedValue.GetData().ToTextLines());
                EncryptedTextBox.Text = fileDialog.FileName;
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            Window choiceWindow = new ChoiceWindow();
            choiceWindow.Show();
            this.Close();
        }
    }
}

using Microsoft.Win32;
using System.IO;
using System.Text;
using System.Windows;
using SchnorDigitalSign;
using SchnorDigitalSign.Model;

namespace SchnorrApplication
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class EncryptWindow : Window
    {
        private readonly KeyPair keyPair;
        private readonly UserKeys userKeys;
        private string filePath = string.Empty;
        private byte[] messageToSign;
        private Signature signature;


        public EncryptWindow(KeyPair keyPair, UserKeys userKeys)
        {
            this.keyPair = keyPair;
            this.userKeys = userKeys;
            InitializeComponent();
        }

        private void EncryptButton_Click(object sender, RoutedEventArgs e)
        {
            byte[] messageToSign;

            if (filePath == string.Empty)
            {
                MessageBox.Show("Choose message to sign");
            }
            else
            {
                messageToSign = File.ReadAllBytes(filePath);
                signature = SchnorrAlgorithm.SignMessage(messageToSign, keyPair, userKeys);
            }

            
        }

        private void ChooseFileButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Title = "Choose file to sign: ";
            if (fileDialog.ShowDialog() == true)
            {
                filePath = fileDialog.FileName;
                TextToEncryptTextBox.Text = filePath;
            }
        }

        private void SaveToFileButton_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog fileDialog = new SaveFileDialog();
            fileDialog.Title = "Save signature to: ";
            if (fileDialog.ShowDialog() == true)
            {
                string[] signatureInLines = new string[]
                {
                    this.signature.e.ToString(),
                    this.signature.y.ToString()
                };

                System.IO.File.WriteAllLines(fileDialog.FileName, signatureInLines);
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

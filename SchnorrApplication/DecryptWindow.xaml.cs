using Microsoft.Win32;

using System.IO;
using System.Numerics;
using System.Text;
using System.Windows;
using SchnorDigitalSign;
using SchnorDigitalSign.Model;

namespace SchnorrApplication
{
    /// <summary>
    /// Interaction logic for DecryptWindow.xaml
    /// </summary>
    public partial class DecryptWindow : Window
    {
        private readonly KeyPair keyPair;
        private readonly UserKeys userKeys;
        private string messageFilePath = string.Empty;
        private string signatureFilePath = string.Empty;
        private byte[] messageToSign;
        private BigInteger senderPublicKey;

        public DecryptWindow(KeyPair keyPair, BigInteger senderPublicKey)
        {
            this.keyPair = keyPair;
            this.senderPublicKey = senderPublicKey;

            InitializeComponent();
        }

        private void MessageFileButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Title = "Choose file to verify: ";
            if (fileDialog.ShowDialog() == true)
            {
                messageFilePath = fileDialog.FileName;
                TextToDecryptTextBox.Text = messageFilePath;
            }
        }

        private void SignatureFileButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Title = "Choose signature file: ";
            if (fileDialog.ShowDialog() == true)
            {
                signatureFilePath = fileDialog.FileName;
                //TextToDecryptTextBox.Text = filePath;
            }
        }

        private void DecryptButton_Click(object sender, RoutedEventArgs e)
        {
            byte[] messagetoVerify;

            messagetoVerify = File.ReadAllBytes(messageFilePath);

            string[] signatureLines = System.IO.File.ReadAllLines(signatureFilePath);

            Signature signature = new Signature()
            {
                e = BigInteger.Parse(signatureLines[0]),
                y = BigInteger.Parse(signatureLines[1])
            };

            bool isValid = SchnorrAlgorithm.Verify(messagetoVerify, keyPair, signature, senderPublicKey);

            if (isValid)
            {
                MessageBox.Show("File is valid");
            }
            else
            {
                MessageBox.Show("File is not valid");
;            }

        }


        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            Window choiceWindow = new ChoiceWindow();
            choiceWindow.Show();
            this.Close();
        }
    }
}

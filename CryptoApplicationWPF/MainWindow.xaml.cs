using System.Collections;
using System.IO;
using System.Text;
using System.Windows;
using DES;
using DES.AlgorithmBuilders;
using DESAlgorithm.Extensions;
using Microsoft.Win32;

namespace CryptoApplicationWPF
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string filePath = string.Empty;

        public MainWindow()
        {
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

            BitArray encryptionKey = new BitArray(Encoding.ASCII.GetBytes(EncryptionKeyTextBox.Text));

            BitArray bitArrayToEncode = new BitArray(textToEncodeBytes).RevertEveryByte();

            bool[] bitsToEncode = new bool[bitArrayToEncode.Count];
            bitArrayToEncode.CopyTo(bitsToEncode, 0);

            DESBuilder builder = new DESBuilder();
            builder.AddWholeDES(encryptionKey);
            CryptoAlgorithm des = builder.Build();

            bool[] encrypted = des.Encrypt(bitsToEncode);
            var encryptedBytes = encrypted.ToByteArray();

            EncryptedTextBox.Text = Encoding.ASCII.GetString(encryptedBytes);

            DecryptedTextBox.Text = Encoding.ASCII.GetString(des.Decrypt(encrypted).ToByteArray());
            //byte[] returnMessage = messageWithoutPadding.ToByteArray();

            //File.WriteAllBytes(System.IO.Path.Combine(Directory.GetCurrentDirectory(), "decrypted.bmp"), returnMessage
        }

        private void ChooseFileButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Title = "Choose file to encrypt: ";
            if (fileDialog.ShowDialog() == true)
            {
                filePath = fileDialog.FileName;
            }
        }
    }
}
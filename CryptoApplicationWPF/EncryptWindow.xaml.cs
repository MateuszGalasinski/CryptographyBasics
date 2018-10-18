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
    public partial class EncryptWindow : Window
    {
        private string filePath = string.Empty;
        private byte[] encryptedBytes;

        public EncryptWindow()
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

            if (EncryptionKeyTextBox.Text.Length != 8)
            {
                MessageBox.Show("Key must be an 8byte string", "Error!");
                return;
            }
            BitArray encryptionKey = new BitArray(Encoding.ASCII.GetBytes(EncryptionKeyTextBox.Text));

            if (textToEncodeBytes.Length == 0)
            {
                MessageBox.Show("There is nothing to encode", "Error!");
            }
            BitArray bitArrayToEncode = new BitArray(textToEncodeBytes).RevertEveryByte();

            bool[] bitsToEncode = new bool[bitArrayToEncode.Count];
            bitArrayToEncode.CopyTo(bitsToEncode, 0);

            DESBuilder builder = new DESBuilder();
            builder.AddWholeDES(encryptionKey);
            CryptoAlgorithm des = builder.Build();

            bool[] encrypted = des.Encrypt(bitsToEncode);
            encryptedBytes = encrypted.ToByteArray();

            EncryptedTextBox.Text = Encoding.ASCII.GetString(encryptedBytes);

            filePath = string.Empty;

            //DecryptedTextBox.Text = Encoding.ASCII.GetString(des.Decrypt(encrypted).ToByteArray());
            //byte[] returnMessage = messageWithoutPadding.ToByteArray();

            //File.WriteAllBytes(System.IO.Path.Combine(Directory.GetCurrentDirectory(), "decrypted.bmp"), returnMessage
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
                File.WriteAllBytes(fileDialog.FileName, encryptedBytes);
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
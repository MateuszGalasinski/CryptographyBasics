using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
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
using DES;
using DES.AlgorithmBuilders;
using DESAlgorithm.Extensions;
using Microsoft.Win32;

namespace CryptoApplicationWPF
{
    /// <summary>
    /// Interaction logic for DecryptWindow.xaml
    /// </summary>
    public partial class DecryptWindow : Window
    {
        private  string filePath = string.Empty;
        private byte[] decryptedBytes;

        public DecryptWindow()
        {
            InitializeComponent();
        }

        private void FileButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Title = "Choose file to decrypt: ";
            if (fileDialog.ShowDialog() == true)
            {
                filePath = fileDialog.FileName;
                TextToDecryptTextBox.Text = filePath;
            }
        }

        private void DecryptButton_Click(object sender, RoutedEventArgs e)
        {
            byte[] textToDecodeBytes;

            if (filePath == string.Empty)
            {
                textToDecodeBytes = Encoding.ASCII.GetBytes(TextToDecryptTextBox.Text);
            }
            else
            {
                textToDecodeBytes = File.ReadAllBytes(filePath);
            }

            //BitArray encryptionKey = new BitArray(Encoding.ASCII.GetBytes(DecryptionKeyTextBox.Text));

            if (textToDecodeBytes.Length == 0)
            {
                MessageBox.Show("There is nothing to decode", "Error!");
            }
            BitArray bitArrayToDecode = new BitArray(textToDecodeBytes).RevertEveryByte();

            bool[] bitsToDecode = new bool[bitArrayToDecode.Count];
            bitArrayToDecode.CopyTo(bitsToDecode, 0);

            DecryptedTextBox.Text = Encoding.ASCII.GetString(decryptedBytes);
            filePath = string.Empty;
        }

        private void SaveToFileButton_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog fileDialog = new SaveFileDialog();
            fileDialog.Title = "Save file to: ";
            if (fileDialog.ShowDialog() == true)
            {
                DecryptedTextBox.Text = fileDialog.FileName;
                File.WriteAllBytes(fileDialog.FileName, decryptedBytes);
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

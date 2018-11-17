using Microsoft.Win32;
using RSA;
using RSA.Models;
using RSA.PaddingStrategies;
using System.IO;
using System.Text;
using System.Windows;

namespace RSAApplication
{
    /// <summary>
    /// Interaction logic for DecryptWindow.xaml
    /// </summary>
    public partial class DecryptWindow : Window
    {
        private readonly FullKey _key;
        private  string filePath = string.Empty;
        private BigInteger[] dataBlocks;
        private byte[] encryptedBytes;
        private DataChunker _dataChunker = new DataChunker();
        private IPaddingStrategy _paddingStrategy = new CMSPaddingStrategy();

        public DecryptWindow(FullKey key)
        {
            _key = key;
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

            //if (filePath == string.Empty)
            //{
            //    textToDecodeBytes = Encoding.ASCII.GetBytes(TextToDecryptTextBox.Text);
            //}
            //else
            //{
                dataBlocks = _dataChunker.BytesToBigIntegers(File.ReadAllBytes(filePath), RSAAlgorithm.BlockSize);
                for (int i = 0; i < dataBlocks.Length; i++)
                {
                    dataBlocks[i] = RSAAlgorithm.Decrypt(dataBlocks[i], _key.D, _key.N);
                }
            //}
        }

        private void SaveToFileButton_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog fileDialog = new SaveFileDialog();
            fileDialog.Title = "Save file to: ";
            if (fileDialog.ShowDialog() == true)
            {
                DecryptedTextBox.Text = fileDialog.FileName;

                var blocksWithoutPadding = _dataChunker.MergeDataAndRemovePadding(dataBlocks, RSAAlgorithm.BlockSize);

                File.WriteAllBytes(fileDialog.FileName, blocksWithoutPadding);
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

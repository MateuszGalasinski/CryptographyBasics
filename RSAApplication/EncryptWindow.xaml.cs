using Microsoft.Win32;
using RSA;
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
        private BigInteger[] blocksToEncode;
        private DataChunker _dataChunker = new DataChunker();

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

            blocksToEncode = _dataChunker.ChunkData(textToEncodeBytes, RSAAlgorithm.BlockSize - 8);

            for (int i = 0; i < blocksToEncode.Length; i++)
            {
                blocksToEncode[i] = RSAAlgorithm.Encrypt(blocksToEncode[i], _key.E, _key.N);
            }
            
            MessageBox.Show("Finished encryption.");
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
                var data = _dataChunker.MergeData(blocksToEncode, RSAAlgorithm.BlockSize);
                File.WriteAllBytes(fileDialog.FileName, data);
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

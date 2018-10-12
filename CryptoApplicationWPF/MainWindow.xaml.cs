using System;
using System.Collections;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using DES;
using DES.AlgorithmBuilders;
using DESAlgorithm.Extensions;
using DESAlgorithm.PaddingStrategies;
using Microsoft.Win32;
using System.IO;


namespace CryptoApplicationWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
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
            IPaddingStrategy padding = new CMSPaddingStrategy();

            byte[] textToEncodeBytes;

            if(filePath == string.Empty)
            {
                textToEncodeBytes = Encoding.ASCII.GetBytes(TextToEncryptTextBox.Text);
            }
            else
            {
                textToEncodeBytes = File.ReadAllBytes(filePath);
            }

            byte[] encryptionKeyBytes = Encoding.ASCII.GetBytes(EncryptionKeyTextBox.Text);

            BitArray bitArrayToEncode = new BitArray(textToEncodeBytes).RevertEveryByte();

            bool[] bitsToEncode = new bool[bitArrayToEncode.Count];
            bitArrayToEncode.CopyTo(bitsToEncode, 0);

            bool[] messageWithPadding = padding.AddPadding(bitsToEncode);
            

            bool[] messageWithoutPadding = padding.RemovePadding(messageWithPadding);//.RevertEveryByte();


            byte[] returnMessage = messageWithoutPadding.ToByteArray();

            File.WriteAllBytes(System.IO.Path.Combine(Directory.GetCurrentDirectory(), "decrypted.bmp"), returnMessage);

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

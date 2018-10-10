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


namespace CryptoApplicationWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string filePath;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void EncryptButton_Click(object sender, RoutedEventArgs e)
        {
            IPaddingStrategy padding = new CMSPaddingStrategy();
            

            byte[] textToEncodeBytes = Encoding.ASCII.GetBytes(TextToEncryptTextBox.Text);

            byte[] encryptionKeyBytes = Encoding.ASCII.GetBytes(EncryptionKeyTextBox.Text);

             
                
            BitArray bitsToEncode = new BitArray(textToEncodeBytes).RevertEveryByte();

            BitArray messageWithPadding = padding.AddPadding(bitsToEncode);
            

            //DESBuilder desBuilder = new DESBuilder();

            //desBuilder.AddWholeDES(new BitArray(encryptionKeyBytes).Revert());
            //CryptoAlgorithm cryptoAlgorithm = desBuilder.Build();

            //cryptoAlgorithm.Encrypt();


            BitArray messageWithoutPadding = padding.RemovePadding(messageWithPadding);//.RevertEveryByte();

            string dupa = "";


            for (int i = 0; i < messageWithoutPadding.Length; i++)
            {
               dupa += messageWithoutPadding[i].ToInt().ToString();
            }

            EncryptedTextBox.Text = dupa;


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

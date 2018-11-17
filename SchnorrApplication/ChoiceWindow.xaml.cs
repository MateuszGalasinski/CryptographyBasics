using System.Windows;
using System.Windows.Input;
using SchnorDigitalSign;
using SchnorDigitalSign.Model;

namespace SchnorrApplication
{
    /// <summary>
    /// Interaction logic for ChoiceWindow.xaml
    /// </summary>
    public partial class ChoiceWindow : Window
    {
        public static KeyPair KeyPair { get; set; }
        public static UserKeys UserKeys{ get; set; }

        public ChoiceWindow()
        {
            InitializeComponent();
            DataContext = this;
            UpdateKeysTextBoxes();
        }

        private void EncryptButton_Click(object sender, RoutedEventArgs e)
        {
            if (KeyPair == null)
            {
                return;
            }
            Window encryptWindow = new EncryptWindow(KeyPair, UserKeys);
            encryptWindow.Show();
            this.Close();
        }

        private void DecryptButton_Click(object sender, RoutedEventArgs e)
        {
            if (KeyPair == null)
            {
                return;
            }
            Window decryptWindow = new DecryptWindow(KeyPair, UserKeys.PublicKey);
            decryptWindow.Show();
            this.Close();
        }

        private void GenerateKeyButton_OnClick(object sender, RoutedEventArgs e)
        {
            KeyGenerator keyGen = new KeyGenerator();
            KeyPair = keyGen.Generate(136, 512, 160);
            UserKeys = UserKeyGenerator.Generate(KeyPair);
            UpdateKeysTextBoxes();
        }

        private void UpdateKeysTextBoxes()
        {
            if (KeyPair != null)
            {
                TextBoxKeyP.Text = KeyPair.p.ToString();
                TextBoxKeyQ.Text = KeyPair.q.ToString();
                TextBoxKeyA.Text = KeyPair.a.ToString();
            }
        }
    }
}

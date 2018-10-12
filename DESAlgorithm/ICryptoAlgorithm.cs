namespace DES
{
    interface ICryptoAlgorithm
    {
        bool[] Encrypt(bool[] data);
        bool[] Decrypt(bool[] data);
    }
}

namespace RSAAlgoithm
{
    public interface ICryptoAlgorithm
    {
        bool[] Encrypt(bool[] data);
        bool[] Decrypt(bool[] data);
    }
}
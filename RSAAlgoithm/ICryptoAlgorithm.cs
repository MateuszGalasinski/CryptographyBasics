namespace RSAAlgorithm
{
    public interface ICryptoAlgorithm
    {
        byte[] Encrypt(byte[] data);
        byte[] Decrypt(byte[] data);
    }
}
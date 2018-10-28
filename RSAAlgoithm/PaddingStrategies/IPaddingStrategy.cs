namespace RSAAlgorithm
{
    public interface IPaddingStrategy
    {
        byte[] AddPadding(byte[] message);
        byte[] RemovePadding(byte[] message);
    }
}
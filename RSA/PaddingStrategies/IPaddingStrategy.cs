namespace RSA.PaddingStrategies
{
    public interface IPaddingStrategy
    {
        byte[] AddPadding(byte[] message, int keyLength);
        byte[] RemovePadding(byte[] message, int keyLength);
    }
}

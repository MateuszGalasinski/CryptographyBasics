namespace RSA.PaddingStrategies
{
    public interface IPaddingStrategy
    {
        byte[] AddPadding(byte[] message, int blockLength);
        byte[] RemovePadding(byte[] message, int blockLength);
    }
}

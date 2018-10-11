namespace DESAlgorithm.PaddingStrategies
{
    public interface IPaddingStrategy
    {
        bool[] AddPadding(bool[] message);
        bool[] RemovePadding(bool[] message);
    }
}

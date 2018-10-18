using DESAlgorithm.Models;

namespace DES
{
    public interface IDataTransformation
    {
        DataSet Transform(DataSet data);
    }
}

using RSAAlgoithm.Models;

namespace RSAAlgorithm
{
    public interface IDataTransformation
    {
        DataSet Transform(DataSet data);
    }
}
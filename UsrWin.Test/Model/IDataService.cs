using System.Threading.Tasks;

namespace UsrWin.Test.Model
{
    public interface IDataService
    {
        Task<DataItem> GetData();
    }
}
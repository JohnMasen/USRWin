using System;

namespace UsrWin.UI.WP81.Model
{
    public interface IDataService
    {
        void GetData(Action<DataItem, Exception> callback);
    }
}
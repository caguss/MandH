using MandH.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace MandH.Services
{
    public interface IDataStore<T>
    {
        void GetRefresh();
        DataTable AboutRefresh();
        Task<bool> UpdateItemAsync(T item);
        Task<T> GetItemAsync(string name);
        Task<IEnumerable<T>> GetItemsAsync(bool forceRefresh = false);
     
    }
}

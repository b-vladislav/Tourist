using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tourist.Service
{
    public interface IService
    {
        bustravelEntities bustravel { get; }

        bool LogIn(string login, string pass);

        BindingList<Bus> GetBusList();

        BindingList<Category> GetCategoryList();

        void UpdateBus(Bus newBus);
        void InsertBus(Bus bus);
        void DeleteBus(Bus bus);
    }
}

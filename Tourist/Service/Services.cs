using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tourist.Service
{
    public class Services : IService
    {

        public bustravelEntities bustravel { get; }

        public Services()
        {
            bustravel = new bustravelEntities();
        }

        public bool LogIn(string login, string pass)
        {

            var dbuser = bustravel.User.FirstOrDefault(x => x.Login == login);

            if (dbuser == null)
                return false;
            else if (dbuser.Password != pass)
                return false;
            else
                return true;

        }

        public BindingList<Bus> GetBusList()
        {
            
            bustravel.Bus.Include("Service").Include("Category").Include("Rout").Load();           
            return bustravel.Bus.Local.ToBindingList();

        }

        public BindingList<Category> GetCategoryList()
        {
            bustravel.Category.Load();
            return bustravel.Category.Local.ToBindingList();
        }

        public void UpdateBus(Bus newBus)
        {
            Bus bus = bustravel.Bus.FirstOrDefault(x => x.bus_id == newBus.bus_id);
            if (bus != null)
            {
                bus.Category = newBus.Category;
                bus.number = newBus.number;
                bus.Service = newBus.Service;
                bus.Rout = newBus.Rout;

                bustravel.SaveChanges();
            }
            else
            {
                throw new Exception("Ошибка обновления несуществующего элемента!");
            }

        }

        public void InsertBus(Bus newbus)
        {
            Category cat = bustravel.Category.FirstOrDefault(c => c.category_id == newbus.category_id);

            newbus.Category = cat ?? throw new Exception("Ошибка добавления нового элемента! Не корректная категория");
            
            bustravel.SaveChanges();
            
        }

        public void DeleteBus(Bus bus)
        {
            bustravel.Bus.Remove(bus);

            bustravel.SaveChanges();
        }
    }
}

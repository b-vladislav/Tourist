using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tourist.Service
{
    public class Services : IService
    {

        bustravelEntities bustravel;

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
    }
}

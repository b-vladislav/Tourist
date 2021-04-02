using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tourist.Common;

namespace Tourist.View
{
    public interface IMainForm : IView
    {
        void SetUserName(string UserName);

        event EventHandler ShowBus;
    }
}

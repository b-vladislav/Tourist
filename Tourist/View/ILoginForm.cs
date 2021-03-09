using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tourist.View
{
    public interface ILoginForm
    {
        string Login { get; }
        string Password { get; }

        event Action LoginIn;

        void ShowError(string message);

    }
}

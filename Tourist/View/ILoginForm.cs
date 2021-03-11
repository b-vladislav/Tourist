using System;
using Tourist.Common;

namespace Tourist.View
{
    public interface ILoginForm : IView
    {
        string Login { get; }
        string Password { get; }

        event Action LoginIn;

        void ShowError(string message);

    }
}

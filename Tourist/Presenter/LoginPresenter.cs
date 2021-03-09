using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tourist.Service;
using Tourist.View;

namespace Tourist.Presenter
{
    public class LoginPresenter
    {
        private readonly ILoginForm loginForm;
        private readonly IService service;
        
        public LoginPresenter(ILoginForm loginForm, IService service)
        {
            this.loginForm = loginForm;
            this.service = service;

            loginForm.LoginIn += () => LoginIn();
        }

        private void LoginIn()
        {
            if (!service.LogIn(loginForm.Login, loginForm.Password))
            {
                loginForm.ShowError("Не успешная авторизация");
            }
            else
            {
                loginForm.ShowError("Авторизация прошла успешно");
            }
        }
    }
}

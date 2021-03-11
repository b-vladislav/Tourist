using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tourist.Common;
using Tourist.Service;
using Tourist.View;
using Unity;

namespace Tourist.Presenter
{
    public class LoginPresenter : BasePresener<ILoginForm>
    {
        private readonly IService service;
        
        public LoginPresenter(ApplicationController controller, ILoginForm loginForm, IService service)
            : base(controller, loginForm)
        {
            this.service = service;

            loginForm.LoginIn += () => LoginIn();
        }

        private void LoginIn()
        {
            if (!service.LogIn(view.Login, view.Password))
            {
                view.ShowError("Ошибка авторизации");
            }
            else
            {
                controller.Run<MainPresenter, User>(new User() { Login = view.Login, Password = view.Password });
                view.Close();
            }
        }
    }
}

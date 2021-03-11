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
    public class MainPresenter : BasePresener<IMainForm, User>
    {
        private readonly IService service;
        private User user;

        public MainPresenter(ApplicationController controller, IMainForm mainForm, IService service)
            : base(controller, mainForm)
        {
            this.service = service;
        }

        public override void Run(User argument)
        {
            this.user = argument;
            UpdateUserInfo();
            view.Show();
        }

        private void UpdateUserInfo()
        {
            view.SetUserName(user.Login);
        }
    }
}

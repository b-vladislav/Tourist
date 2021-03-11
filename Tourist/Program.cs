using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tourist.Common;
using Tourist.Presenter;
using Tourist.Service;
using Tourist.View;
using Unity;

namespace Tourist
{
    static class Program
    {
        public static readonly ApplicationContext Context = new ApplicationContext();

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            IUnityContainer container = new UnityContainer();
            ApplicationController controller = new ApplicationController(container);
            controller.RegisterInstance(new ApplicationContext());
            controller.RegisterService<IService, Services>();
            controller.RegisterView<ILoginForm, LoginForm>();
            controller.RegisterView<IMainForm, MainForm>();

            controller.Run<LoginPresenter>();
        }
        
    }
}

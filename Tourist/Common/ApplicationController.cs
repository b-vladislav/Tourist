using Tourist.Service;
using Unity;

namespace Tourist.Common
{
    public class ApplicationController
    {
        private readonly IUnityContainer container;

        public ApplicationController(IUnityContainer container)
        {
            this.container = container;
            this.container.RegisterInstance(this);
        }

        public IUnityContainer RegisterView<TView, TImplementation>()
            where TImplementation : class, TView
            where TView : IView
        {
            container.RegisterType<TView, TImplementation>();
            return container;
        }

        public IUnityContainer RegisterInstance<TInstance>(TInstance instance)
        {
            container.RegisterInstance(instance);
            return container;
        }

        public IUnityContainer RegisterService<TModel, TImplementation>()
            where TImplementation : class, TModel
            where TModel : IService
        {
            container.RegisterType<TModel, TImplementation>();
            return container;
        }

        public void Run<TPresenter>() where TPresenter : class, IPresenter
        {
            if (!container.IsRegistered<TPresenter>())
                container.RegisterType<TPresenter>();

            var presenter = container.Resolve<TPresenter>();
            presenter.Run();
        }

        public void Run<TPresenter, TArgumnent>(TArgumnent argumnent) 
            where TPresenter : class, IPresenter<TArgumnent>
        {
            if (!container.IsRegistered<TPresenter>())
                container.RegisterType<TPresenter>();

            var presenter = container.Resolve<TPresenter>();
            presenter.Run(argumnent);
        }
    }
}

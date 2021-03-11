using Unity;

namespace Tourist.Common
{
    public abstract class BasePresener<TView> : IPresenter
        where TView : IView
    {
        protected TView view { get; private set; }
        protected ApplicationController controller { get; private set; }

        protected BasePresener(ApplicationController controller, TView view)
        {
            this.controller = controller;
            this.view = view;
        }

        public void Run()
        {
            view.Show();
        }        
    }

    public abstract class BasePresener<TView, TArg> : IPresenter<TArg>
        where TView : IView
    {
        protected TView view { get; private set; }
        protected ApplicationController controller { get; private set; }

        protected BasePresener(ApplicationController controller, TView view)
        {
            this.controller = controller;
            this.view = view;
        }

        public abstract void Run(TArg argument);
    }
}

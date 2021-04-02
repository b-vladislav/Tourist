using System;
using Tourist.Common;
using Tourist.Service;
using Tourist.View;

namespace Tourist.Presenter
{
    class BusPresenter : BasePresener<IBusForm>
    {
        private readonly IService service;

        public BusPresenter(ApplicationController controller, IBusForm busForm, IService service)
            : base(controller, busForm)
        {
            this.service = service;
            
            view.UpdateBusList += View_UpdateBusList;
            view.UpdateCategoryList += View_UpdateCategoryList;

            view.UpdateBus += View_UpdateBus;
            view.InsertBus += View_InsertBus;
            view.CreateBus += View_CreateBus;
            view.DeleteBus += View_DeleteBus;
            view.CancelBus += View_CancelBus;

        }

        private void View_DeleteBus(object sender, ChangeBusEventArgs e)
        {
            if (e.bus.Rout.Count > 0)
            {
                view.ShowMessage("Данный автобус используется в маршрутах, удаление запрещено!", true);
                return;
            }

            try
            {
                service.DeleteBus(e.bus);
            }
            catch(Exception exp)
            {
                view.ShowMessage(exp.Message, true);
            }

        }

        private void View_CancelBus(object sender, ChangeBusEventArgs e)
        {
            var entries = service.bustravel.ChangeTracker.Entries();
            foreach(var entry in entries)
            {
                switch (entry.State)
                {
                    case System.Data.Entity.EntityState.Modified:
                        entry.Reload();
                        break;
                    case System.Data.Entity.EntityState.Added:
                        service.bustravel.Bus.Local.Remove((Bus)entry.Entity);
                        break;
                }
            }
        }

        private void View_CreateBus(object sender, ChangeBusEventArgs e)
        {

            e.bus = new Bus();
            e.bus.Rout = new ObservableListSource<Rout>();
            e.bus.Service = new ObservableListSource<Service.Service>();

            service.bustravel.Bus.Local.Add(e.bus);

            view.busBindingSource.MoveLast();

        }

        private void View_InsertBus(object sender, ChangeBusEventArgs e)
        {
            try
            {
                service.InsertBus(e.bus);

                Bus bus = (Bus)view.busBindingSource.Current;
                
                view.busBindingSource.DataSource = service.GetBusList();

                view.busBindingSource.Position
                    = view.busBindingSource.List.IndexOf(bus);

                view.ShowMessage("Данные добавлены", false);

            }
            catch (Exception exp)
            {
                view.ShowMessage(exp.Message, true);
            }
        }

        private void View_UpdateBus(object sender, ChangeBusEventArgs e)
        {
            try
            {                
                service.UpdateBus(e.bus);

                Bus bus = (Bus)view.busBindingSource.Current;

                view.busBindingSource.DataSource = service.GetBusList();

                view.busBindingSource.Position 
                    = view.busBindingSource.List.IndexOf(bus);

                view.ShowMessage("Данные обновлены", false);
            }
            catch(Exception exp)
            {
                view.ShowMessage(exp.Message, true);
            }
        }

        private void View_UpdateBusList(object sender, EventArgs e)
        {
            view.busBindingSource.DataSource = service.GetBusList();
        }

        private void View_UpdateCategoryList(object sender, EventArgs e)
        {
            view.categoryBindingSource.DataSource = service.GetCategoryList();
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tourist.Common;
using Tourist.Service;

namespace Tourist.View
{
    public interface IBusForm : IView
    {        
        // data bindings
        BindingSource busBindingSource { get; set; }
        BindingSource serviceBindingSource { get; set; }
        BindingSource categoryBindingSource { get; set; }

        // get data
        event EventHandler UpdateBusList;
        event EventHandler UpdateCategoryList;
        
        // push data
        event EventHandler<ChangeBusEventArgs> UpdateBus;
        event EventHandler<ChangeBusEventArgs> InsertBus;
        event EventHandler<ChangeBusEventArgs> CreateBus;
        event EventHandler<ChangeBusEventArgs> DeleteBus;
        event EventHandler<ChangeBusEventArgs> CancelBus;

        // send user message
        void ShowMessage(string message, bool isError);
    }

    public class ChangeBusEventArgs : EventArgs
    {
        public Bus bus { get; set; }
                
        public ChangeBusEventArgs(Bus bus)
        {
            this.bus = bus;
        }
    }
}

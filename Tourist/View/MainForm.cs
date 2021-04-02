using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tourist.View;

namespace Tourist
{
    public partial class MainForm : Form, IMainForm
    {
        private readonly ApplicationContext context;
        public event EventHandler ShowBus;

        public MainForm(ApplicationContext context)
        {
            this.context = context;
            InitializeComponent();
        }       

        public void SetUserName(string UserName)
        {
            toolStripStatusLabel1.Text = UserName;
        }

        public new void Show()
        {
            context.MainForm = this;
            base.Show();
        }

        private void busMenuItem_Click(object sender, EventArgs e)
        {
            ShowBus?.Invoke(this, e);
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tourist.View
{
    public partial class LoginForm : Form, IView, ILoginForm
    {
        public new void Show()
        {
            base.Show();
        }
        
        public new void Close()
        {
            base.Close();
        }

        public LoginForm()
        {
            InitializeComponent();
        }

        public string Login { get => textBox1.Text; }
        public string Password { get => textBox2.Text; }

        public event Action LoginIn;

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (LoginIn != null)
                LoginIn.Invoke();
        }

        public void ShowError(string message)
        {
            MessageBox.Show(message);
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tourist.Service;

namespace Tourist.View
{
    public partial class BusForm : Form, IBusForm
    {
        bool editMode = false;
        bool insertMode = false;

        Bus selectedBus;
        Bus newbus;

        public BindingSource busBindingSource { get; set; }
        public BindingSource serviceBindingSource { get; set; }
        public BindingSource categoryBindingSource { get; set; }

        public event EventHandler UpdateCategoryList;
        public event EventHandler UpdateBusList;
        public event EventHandler<ChangeBusEventArgs> UpdateBus;
        public event EventHandler<ChangeBusEventArgs> InsertBus;
        public event EventHandler<ChangeBusEventArgs> CreateBus;
        public event EventHandler<ChangeBusEventArgs> DeleteBus;
        public event EventHandler<ChangeBusEventArgs> CancelBus;

        enum FormState
        {
            Default,
            Edit,
            Insert
        }

        Random rnd;
        public BusForm()
        {
            InitializeComponent();

            rnd = new Random();

            busBindingSource = new BindingSource();
            categoryBindingSource = new BindingSource();
            serviceBindingSource = new BindingSource();
        }

        private void SetFormState(FormState formState)
        {
            switch (formState)
            {
                case FormState.Edit:
                    editMode = true;
                    insertMode = false;

                    button1.Enabled = false;
                    button2.Enabled = true;
                    button3.Enabled = true;

                    button2.Text = "Сохранить";
                    button3.Text = "Отмена";

                    textBox1.ReadOnly = false;
                    comboBox1.Visible = true;
                    textBox3.Visible = false;
                    listBox2.BackColor = Color.FromName("Window");

                    button6.Visible = button7.Visible = true;
                    break;
                case FormState.Insert:

                    editMode = false;
                    insertMode = true;

                    button1.Enabled = true;
                    button2.Enabled = false;
                    button3.Enabled = true;

                    button1.Text = "Сохранить";
                    button3.Text = "Отмена";

                    textBox1.ReadOnly = false;
                    comboBox1.Visible = true;
                    textBox3.Visible = false;
                    listBox2.BackColor = Color.FromName("Window");

                    button6.Visible = button7.Visible = true;

                    break;
                default:

                    editMode = false;
                    insertMode = false;

                    button1.Enabled = true;
                    button2.Enabled = true;
                    button3.Enabled = true;

                    button1.Text = "Добавить";
                    button2.Text = "Изменить";
                    button3.Text = "Удалить";

                    textBox1.ReadOnly = true;
                    comboBox1.Visible = false;
                    textBox3.Visible = true;
                    listBox2.BackColor = Color.FromName("Menu");

                    button6.Visible = button7.Visible = false;

                    break;
            }
        }

        public void ShowMessage(string message, bool isError)
        {
            if (isError)
                MessageBox.Show(message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
                MessageBox.Show(message, "Инфо", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void SetBusListDataSource()
        {

            textBox1.DataBindings.Clear();
            textBox3.DataBindings.Clear();
            listBox2.DataBindings.Clear();
            comboBox1.DataBindings.Clear();

            listBox1.DataSource = null;

            listBox1.DataSource = busBindingSource;
            listBox1.DisplayMember = "number";
                       
            serviceBindingSource.DataSource = busBindingSource;
            serviceBindingSource.DataMember = "Service";

            listBox2.DataSource = serviceBindingSource;
            listBox2.DisplayMember = "serveice_type";

            textBox1.DataBindings.Add("Text", busBindingSource, "number", true, DataSourceUpdateMode.OnPropertyChanged);
            textBox3.DataBindings.Add("Text", busBindingSource, "Category.Name", true, DataSourceUpdateMode.OnPropertyChanged);

            comboBox1.DataSource = categoryBindingSource;
            comboBox1.DisplayMember = "name";
            comboBox1.ValueMember = "category_id";
            comboBox1.DataBindings.Add("SelectedValue", busBindingSource, "category_id", true, DataSourceUpdateMode.OnPropertyChanged);
               
        }

        public new void Show()
        {
            UpdateBusList?.Invoke(null, null);
            UpdateCategoryList?.Invoke(null, null);

            SetBusListDataSource();

            SetFormState(FormState.Default);

            ShowDialog();           
        }



        private void listBox1_SelectedValueChanged(object sender, EventArgs e)
        {            
            if (listBox1.DataSource == null || listBox1.SelectedItem == null)
                return;

            Bus selected = (Bus)listBox1.SelectedItem;
            selectedBus = selected;

        }

        // edit bus
        private void button2_Click(object sender, EventArgs e)
        {
            
            if (!editMode)
            {
                if (selectedBus == null)
                    return;

                SetFormState(FormState.Edit);
            }
            else
            {
                try
                {
                    UpdateBus?.Invoke(null, new ChangeBusEventArgs(selectedBus));
                    
                    busBindingSource.ResetBindings(false);
                }
                catch
                {
                    CancelChanges();
                }

                SetFormState(FormState.Default);
                
            }

        }

        private void BusForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (editMode || insertMode)
            {
                DialogResult result = MessageBox.Show("Сохранить изменения?",
                                      "Внимание", MessageBoxButtons.YesNoCancel);

                switch (result)
                {
                    case DialogResult.Cancel:
                        e.Cancel = true;
                        break;
                    case DialogResult.Yes:
                        if (editMode)
                            button2_Click(this, null);
                        else if (insertMode)
                            button1_Click(this, null);
                        break;
                    case DialogResult.No:
                        e.Cancel = false;
                        break;
                }
            }
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            if (!insertMode)
            {

                ChangeBusEventArgs eventArgs = new ChangeBusEventArgs(null);
                CreateBus?.Invoke(null, eventArgs);
                newbus = eventArgs.bus;

                SetFormState(FormState.Insert);
                
            }
            else
            {
                try
                {
                    InsertBus?.Invoke(null, new ChangeBusEventArgs(newbus));

                    busBindingSource.ResetBindings(false);
                }
                catch
                {
                    CancelChanges();
                }

                SetFormState(FormState.Default);
            }
        }

        private void CancelChanges()
        {
            if (editMode)
            {
                CancelBus?.Invoke(null, new ChangeBusEventArgs(selectedBus));
            }
            else if (insertMode)
            {
                CancelBus?.Invoke(null, new ChangeBusEventArgs(newbus));
            }

            busBindingSource.ResetBindings(false);

            SetFormState(FormState.Default);

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (editMode || insertMode)
            {
                CancelChanges();
            }
            else // удалить объект
            {
                DialogResult dialogResult = 
                    MessageBox.Show("Удалить элемент из справочника?", "Подтверждение", 
                                     MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    DeleteBus?.Invoke(null, new ChangeBusEventArgs(selectedBus));
                }
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            // показать форму для выбора сервисов
        }

        private void button6_Click(object sender, EventArgs e)
        {
            // удалить сервис
        }
    }
}

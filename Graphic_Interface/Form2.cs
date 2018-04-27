using System;
using System.Windows.Forms;
using Towards_Adventures;

namespace Graphic_Interface
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            comboBox1.DataSource = Enum.GetValues(typeof(Sex));
            comboBox2.DataSource = Enum.GetValues(typeof(Document));
        }


        public void OpenFile()
        {
            var openOrder = new OpenFileDialog() { Filter = "Файл заказа|*.train" };
            var result = openOrder.ShowDialog(this);
            if (result == DialogResult.OK)
            {
                var order = Serializer.LoadFromFile(openOrder.FileName);
                SetModelToUI(order);
            }
            MessageBox.Show("Заказ сохранён", "Сохранение", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Close();
        }

        private void SetModelToUI(PurchaseTicketsDto order)
        {
            textBox1.Text = order.Person.FullName.LastName;
            textBox2.Text = order.Person.FullName.FirstName;
            textBox3.Text = order.Person.FullName.Patronymic;
            comboBox1.SelectedItem = order.Person.Sex;
            dateTimePicker1.Value = order.Person.DateBirth;
            comboBox2.SelectedItem = order.Person.DocumentType;
            textBox4.Text = order.Person.Series.ToString();
            textBox5.Text = order.Person.Number.ToString();
            textBox6.Text = order.BeginPoint;
            textBox7.Text = order.EndPoint;
            checkBox1.Checked = order.RestaurantFood;
            checkBox2.Checked = order.Fridge;

            this.ShowDialog();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var saveOrder = new SaveFileDialog()
            {
                Filter = "Файлы заказов|*.train"
            };
            var result = saveOrder.ShowDialog(this);
            if (result == DialogResult.OK)
            {
                var order = GetModelFromUI();
                Serializer.WriteToFile(saveOrder.FileName, order);
            }

            MessageBox.Show("Заказ сохранён", "Сохранение", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Close();
        }

        Towards_Adventures.PurchaseTicketsDto GetModelFromUI()
        {
            return new Towards_Adventures.PurchaseTicketsDto()
            {
                Person = FullName(),
                BeginPoint = textBox6.Text,
                EndPoint = textBox7.Text,
                RestaurantFood = checkBox1.Checked,
                Fridge = checkBox2.Checked,
                FilledTime = DateTime.Now,
            };
        }



        PersonalData FullName()
        {
            return new PersonalData()
            {
               FullName = GetFullName(),
               DateBirth = dateTimePicker1.Value.Date,
               Sex = (Sex)comboBox1.SelectedValue,
               DocumentType = (Document)comboBox2.SelectedValue,
               Series = Convert.ToInt32(textBox4.Text),
               Number = Convert.ToInt32(textBox5.Text),
            };
        }

        NameBuyer GetFullName()
        {
            return new NameBuyer()
            {
                LastName = textBox1.Text,
                FirstName = textBox2.Text,
                Patronymic = textBox3.Text,
            };
        }

        private void checkedListBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }
    }
}

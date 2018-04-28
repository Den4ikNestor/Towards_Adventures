using System;
using System.Windows.Forms;
using Towards_Adventures;

namespace Graphic_Interface
{
    public partial class Form2 : Form
    {
        public double PreviousPRICE = 0;
        public double AdditionalPRICE = 0;

        public Form2()
        {
            InitializeComponent();
            comboBox1.DataSource = Enum.GetValues(typeof(Sex));
            comboBox2.DataSource = Enum.GetValues(typeof(Document));
            comboBox3.DataSource = Enum.GetValues(typeof(BeginPoint));
            comboBox4.DataSource = Enum.GetValues(typeof(EndPoint));
            this.textBox4.KeyPress += new KeyPressEventHandler(textBox4_KeyPress);
            this.textBox5.KeyPress += new KeyPressEventHandler(textBox5_KeyPress);
            this.checkBox1.CheckedChanged += checkBox12_Changed;
            this.checkBox2.CheckedChanged += checkBox12_Changed;
            this.comboBox3.SelectedValueChanged += comboBox34_Changed;
            this.comboBox4.SelectedValueChanged += comboBox34_Changed;
        }

        private void checkBox12_Changed(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true && checkBox2.Checked == true)
            {
                AdditionalPRICE = 900;
                textBox8.Text = (AdditionalPRICE + PreviousPRICE).ToString();
            }
            if (checkBox1.Checked == false && checkBox2.Checked == false)
            {
                AdditionalPRICE = 0;
                textBox8.Text = (AdditionalPRICE + PreviousPRICE).ToString();
            }
            if (checkBox1.Checked == false && checkBox2.Checked == true)
            {
                AdditionalPRICE = 500;
                textBox8.Text = (AdditionalPRICE + PreviousPRICE).ToString();
            }

            if (checkBox1.Checked == true && checkBox2.Checked == false)
            {
                AdditionalPRICE = 400;
                textBox8.Text = (AdditionalPRICE + PreviousPRICE).ToString();
            }
        }

        private void comboBox34_Changed(object sender, EventArgs e)
        {
            if (comboBox3.SelectedIndex == 0 & comboBox4.SelectedIndex == 0)
            {
                if (AdditionalPRICE == 0)
                    PreviousPRICE = 2400;
                else
                    PreviousPRICE = 2400 + AdditionalPRICE;
                textBox8.Text = PreviousPRICE.ToString();
            }
            if (comboBox3.SelectedIndex == 0 & comboBox4.SelectedIndex == 1)
            {
                if (AdditionalPRICE == 0)
                    PreviousPRICE = 5400;
                else
                    PreviousPRICE = 5400 + AdditionalPRICE;
                textBox8.Text = PreviousPRICE.ToString();
            }
            if (comboBox3.SelectedIndex == 1 & comboBox4.SelectedIndex == 0)
            {
                if (AdditionalPRICE == 0)
                    PreviousPRICE = 1800;
                else
                    PreviousPRICE = 1800 + AdditionalPRICE;
                textBox8.Text = PreviousPRICE.ToString();
            }
            if (comboBox3.SelectedIndex == 1 & comboBox4.SelectedIndex == 1)
            {
                if (AdditionalPRICE == 0)
                    PreviousPRICE = 7400;
                else
                    PreviousPRICE = 7400 + AdditionalPRICE;
                textBox8.Text = PreviousPRICE.ToString();
            }
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= '0') && (e.KeyChar <= '9'))
            {
                return;
            }

            if (Char.IsControl(e.KeyChar))
            {
                if (e.KeyChar == (char)Keys.Enter)
                    button1.Focus();
                return;
            }
            e.Handled = true;
        }

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= '0') && (e.KeyChar <= '9'))
            {
                return;
            }

            if (Char.IsControl(e.KeyChar))
            {
                if (e.KeyChar == (char)Keys.Enter)
                    button1.Focus();
                return;
            }
            e.Handled = true;
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
            comboBox1.SelectedItem = order.BeginPoint;
            comboBox2.SelectedItem = order.EndPoint;
            checkBox1.Checked = order.RestaurantFood;
            checkBox2.Checked = order.Fridge;
            textBox8.Text = order.Price.ToString();

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
                BeginPoint = (BeginPoint)comboBox1.SelectedValue,
                EndPoint = (EndPoint)comboBox2.SelectedValue,
                RestaurantFood = checkBox1.Checked,
                Fridge = checkBox2.Checked,
                Price = Convert.ToDouble(textBox8.Text),
                FilledTime = DateTime.Now,
                AdditionalServicePrice = AdditionalPRICE,
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

        private void label13_Click(object sender, EventArgs e)
        {

        }
    }
}

using System;
using System.IO;
using System.Windows.Forms;
using Towards_Adventures;

namespace Graphic_Interface
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CheckLicense();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.OpenFile();
        }
        private void CheckLicense()
        {
            var lv = new LicenceValidator(Directory.GetCurrentDirectory());
            if (!lv.HasLicense)
            {
                MessageBox.Show("You don't have a license");
                Application.Exit();
            }
            else if (!lv.IsValid)
            {
                MessageBox.Show("License expired. Extend or purchase a new one");
                Application.Exit();
            }
        }
    }
}

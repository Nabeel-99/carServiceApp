using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace carServiceApp
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (Worksheet.Count == 0)
            {
                worksheetToolStripMenuItem.Enabled = false;
                paymentToolStripMenuItem.Enabled = false;
            }
        }
        internal static List<Work> Worksheet { get; set; } = new List<Work>(); 
        WorkParser parser = new WorkParser();


        private void openFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFD = new OpenFileDialog())
            {
                openFD.InitialDirectory = @"Downloads";
                openFD.Title = "Browse Text Files";
                openFD.DefaultExt = "txt";
                openFD.Filter = "txt files (*.txt)|*.txt";

                if (openFD.ShowDialog() == DialogResult.OK)
                {
                    Worksheet = parser.ParseFiles<Work>(openFD.FileName);
                    worksheetToolStripMenuItem.Enabled = true;
                    this.Show();
                }
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string title = "About";
            string message = "\nSX42SO\n2022.11.09";
            MessageBox.Show(message, title);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string message = "Do you want to exit?";
            string title = "Close App";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result = MessageBox.Show(message, title, buttons, MessageBoxIcon.Question);
            if(result == DialogResult.Yes)
            {
                this.Close();
            }
            else
            {
                //do something
            }
        }

        private void worksheetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Worksheet worksheet = new Worksheet();

            Label separate = new Label();
            Label services = new Label();
            services.Text = "SERVICES";
            Label materials = new Label();
            materials.Text = "MATERIAL COSTS";
            Label time = new Label();
            time.Text = "TIME";
            Label totalCost = new Label();
            totalCost.Text = "TOTAL COSTS";
            totalCost.Margin = new Padding(10, 10, 10, 10);

            Control[] display = new Control[] { services, materials, time, totalCost };
            worksheet.worksheetPanel.Controls.AddRange(display);
            worksheet.worksheetPanel.SetFlowBreak(display[3], true);

            this.Hide();
            worksheet.ShowDialog();
           
        }

        private void paymentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            Payment payment = new Payment();
            payment.ShowDialog();
        }
    }
}

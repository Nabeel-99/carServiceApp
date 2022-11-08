using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace carServiceApp
{
    public partial class Worksheet : Form
    {
        public Worksheet()
        {
            InitializeComponent();
            
        }
        List <Label> totalCost = new List<Label> ();
        List<CheckBox> checkBox = new List<CheckBox>();
        

        private void Worksheet_Load(object sender, EventArgs e)
        {
            foreach (Work worksheet in Form1.Worksheet)
            {
                Label services = new Label ();
                services.Text = worksheet.Services;
                services.Margin = new Padding(10, 10, 10, 10);
                Label materialCosts = new Label ();
                materialCosts.Text = Convert.ToString(worksheet.MaterialCost) + " Ft";
                materialCosts.Margin = new Padding(10, 10, 10, 10);
                Label time = new Label();
                time.Text = Convert.ToString(worksheet.Hours) + "hr" + Convert.ToString(worksheet.Mins) + "mins";
                CheckBox chkBox = new CheckBox();
                Label total = new Label();
                total.Text = "";

                worksheetPanel.Controls.Add(services);
                worksheetPanel.Controls.Add(materialCosts);
                worksheetPanel.Controls.Add(time);
                worksheetPanel.Controls.Add(chkBox);
                worksheetPanel.Controls.Add(total);

                totalCost.Add(total);
                checkBox.Add(chkBox);

                worksheetPanel.SetFlowBreak(total, true);
                chkBox.Click += new EventHandler(DisplayCost);

                

                

            }
        }

        private void DisplayCost(object sender, EventArgs e)
        {
            // throw new NotImplementedException();

            double totalTime = 0; 
            double materialCost = 0;
            for (int i = 0; i < checkBox.Count; i++)
            {
                double material = 0;
                double time = 0;
                double total = 0;

                if(checkBox[i].Enabled && checkBox[i].Checked)
                {
                    material = Form1.Worksheet[i].MaterialCost;
                    if (Form1.Worksheet[i].Mins <= 30 && Form1.Worksheet[i].Hours == 0)
                    {
                        time = 7500;
                    }
                    else
                    {
                        time = (Form1.Worksheet[i].Hours * 15000) + Form1.Worksheet[i].Mins * 250;
                    }
                    total = material + time;
                    totalCost[i].Text = Convert.ToString(total);

                    materialCost += material;
                    totalTime += time;
                }
                else
                {
                    totalCost[i].Text = "";
                }
                mtCostBox.Text = Convert.ToString(materialCost);
                timeCostBox.Text = Convert.ToString(totalTime);
            }
        }

        private void registerBtn(object sender, EventArgs e)
        {
            int tally = 0;
           
            WorksheetCalc paymentdetails = new WorksheetCalc
            {
                Total = Convert.ToDouble(mtCostBox.Text) + Convert.ToDouble(timeCostBox.Text),
                MaterialCost = Convert.ToDouble(timeCostBox.Text),
                MinsInvoiced = (int)(Convert.ToDouble(timeCostBox.Text) / 15000),
                HoursInvoiced = (int)((Convert.ToDouble(timeCostBox.Text) % 15000) / 250),
                WorkCount = tally,
                WorksheetTime = 1,
                TotalTime = Convert.ToDouble(mtCostBox.Text),


            };
           
            RegisterdWorksheet.Add(paymentdetails);
            string title = "Registration";
            string message = "Do you wish to register \nthe selected Services?";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result = MessageBox.Show(message, title, buttons, MessageBoxIcon.Question);
            if(result == DialogResult.Yes)
            {
                MessageBox.Show("Services Registration successful!", title);
            }
            else
            {
                //
            }

           
            foreach (CheckBox check in checkBox)
            {
                if (check.Checked)
                {
                    tally++;
                }
                else
                {
                    //
                }
            }

            RegisterdWorksheet.Add(paymentdetails);
            

            this.Hide();
            Form1 form = new Form1();
            form.paymentToolStripMenuItem.Enabled = true;
            form.Show();
        }
        internal static List<WorksheetCalc> RegisterdWorksheet = new List<WorksheetCalc>();
        private void Worksheet_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(CloseReason.UserClosing == e.CloseReason)
            {
                string title = "Registration";
                string message = "Do you wish to close without\n registering any services?";
                MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                DialogResult result = MessageBox.Show(message, title, buttons, MessageBoxIcon.Question);
                if(DialogResult.Yes == result)
                {
                    this.Hide();
                    Form1 form1 = new Form1();
                    form1.Show();

                    if(RegisterdWorksheet.Sum(k => k.WorksheetTime)==0)
                    {
                        form1.paymentToolStripMenuItem.Enabled = false;
                    }
                }
                else
                {
                    e.Cancel = true;
                }

            }
        }
    }
}

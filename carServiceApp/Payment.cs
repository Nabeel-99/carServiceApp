using System;
using System.Linq;
using System.Windows.Forms;

namespace carServiceApp
{
    public partial class Payment : Form
    {
        public Payment()
        {
            InitializeComponent();
        }

        private void Payment_Load(object sender, EventArgs e)
        {
            int worksheetCount = Worksheet.RegisterdWorksheet.Sum(k => k.WorksheetTime);
            int workCount = Worksheet.RegisterdWorksheet.Sum(k => k.WorkCount);
            
            int mins = Worksheet.RegisterdWorksheet.Sum(k => k.MinsInvoiced);
            int hours = Worksheet.RegisterdWorksheet.Sum(k => k.HoursInvoiced);
            if (mins >= 60)
            {
                mins %= 60;
                hours += mins / 60;
            }
            double serviceCost = Worksheet.RegisterdWorksheet.Sum(k => k.TotalTime);
            double materialCost = Worksheet.RegisterdWorksheet.Sum(k => k.MaterialCost);
            double total = Worksheet.RegisterdWorksheet.Sum(k => k.Total);

            
            regSheetBox.Text = worksheetCount.ToString() + " db";
            regSheetBox.ReadOnly = true;
            regWorkBox.Text  =workCount.ToString() +" db";
            regWorkBox.ReadOnly = true;
            materialBox.Text =materialCost.ToString() + " Ft";
            materialBox.ReadOnly = true;
            serviceBox.Text = serviceCost.ToString() + " Ft";
            serviceBox.ReadOnly = true;
            invoiceBox.Text = hours.ToString() + " hr" + mins.ToString() + " mins";
            invoiceBox.ReadOnly = true;
            amountBox.Text = total.ToString() + " Ft";
            amountBox.ReadOnly = true;
        }

        

        private void payBtn(object sender, EventArgs e)
        {
            string message = "Confirm Payment";
            string title = "Payment";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result = MessageBox.Show(message, title, buttons, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                MessageBox.Show("Payment Successful!", title);
            }
            else
            {
                //do something
            }
        }

        private void Payment_FormClosing(object sender, FormClosingEventArgs e)
        {
            Form1.Worksheet.Clear();
            Worksheet.RegisterdWorksheet.Clear();
            this.Hide();

            Form1 form = new Form1();
            form.Show();
        }
    }
}

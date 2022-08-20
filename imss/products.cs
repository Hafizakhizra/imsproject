using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace imss
{
    public partial class products : Sample2
    {
        int edit = 0;//This 0 is an indication of save operation and 1 is an indication to update operation
        Int64 prodID;
        public products()
        {
            InitializeComponent();
        }
        retrieval r = new retrieval();
        private void rightPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void products_Load(object sender, EventArgs e)
        {
            MainClass.disable_reset(leftPanel);
            r.getList("st_getCategoriesList", categoryDD, "Category", "ID");
        }
        public override void addBtn_Click(object sender, EventArgs e)
        {
            MainClass.enable_reset(leftPanel);
            edit = 0;
          
        }

        public override void editBtn_Click(object sender, EventArgs e)
        {
            edit = 1;
            MainClass.enable(leftPanel);
        }

        public override void saveBtn_Click(object sender, EventArgs e)
        {
            if (proTxt.Text == "") { proErrorLabel.Visible = true; } else { proErrorLabel.Visible = false; }
            if (barcodeTxt.Text == "") { barcodeErrorLabel.Visible = true; } else { barcodeErrorLabel.Visible = false; }
            if (expiryPicker1.Value < DateTime.Now) { expiryErrorLabel.Visible = true; expiryErrorLabel.Text = "Invalid Date"; } else { expiryErrorLabel.Visible = false; }
            if (expiryPicker1.Value.Date == DateTime.Now.Date) { expiryErrorLabel.Visible = false; }
            if (categoryDD.SelectedIndex == -1 || categoryDD.SelectedIndex == 0) { catErrorLabel.Visible = true; } else { catErrorLabel.Visible = false; }
            if (proErrorLabel.Visible || barcodeErrorLabel.Visible || expiryErrorLabel.Visible|| catErrorLabel.Visible)
            {
                MainClass.ShowMSG("fields with * are mendatory", "stop", "Error");// Error is the type of msg
            }
            else
            {

                if (edit == 0)//code for save operation
                {

                    insertion i = new insertion();
                    if (expiryPicker1.Value.Date == DateTime.Now.Date)
                    {
                        i.insertProduct(proTxt.Text, barcodeTxt.Text,Convert.ToInt32(categoryDD.SelectedValue));
                    }
                    else
                    {
                        i.insertProduct(proTxt.Text, barcodeTxt.Text,Convert.ToInt32(categoryDD.SelectedValue), expiryPicker1.Value);
                    }

                    r.showProducts(dataGridView1, proIDGV, proGV, expiryGV, catGV,barcodeGV, catIDGV);
                    MainClass.disable_reset(leftPanel);
                }
                else if (edit == 1)//code for update operation
                {
                    DialogResult dr = MessageBox.Show("Are you sure ,  you want to update record?", "Question...", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dr == DialogResult.Yes)
                    {
                        updation u = new updation();
                        if (expiryPicker1.Value.Date == DateTime.Now.Date)
                        {
                            u.updateProduct(prodID,proTxt.Text, barcodeTxt.Text,Convert.ToInt32(categoryDD.SelectedValue));
                        }
                        else
                        {
                            u.updateProduct(prodID,proTxt.Text, barcodeTxt.Text,Convert.ToInt32(categoryDD.SelectedValue), expiryPicker1.Value);
                        }

                        r.showProducts(dataGridView1, proIDGV, proGV, expiryGV, catGV, barcodeGV, catIDGV);
                        MainClass.disable_reset(leftPanel);
                    }

                }
            }
        }

        public override void searchTxt_TextChanged(object sender, EventArgs e)
        {
            if (searchTxt.Text != "")
            {
                r.showProducts(dataGridView1, proIDGV, proGV, expiryGV, catGV,barcodeGV, catIDGV);
            }
            else
            {
                  r.showProducts(dataGridView1, proIDGV, proGV, expiryGV, catGV,barcodeGV, catIDGV);
            }
        }
        public override void deleteBtn_Click(object sender, EventArgs e)
        {
            if (edit == 1)
            {
                DialogResult dr = MessageBox.Show("Are you sure , you want to delete record?", "Question...", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    deletion d = new deletion();
                    d.delete(prodID, "s_productDelete", "@proID");
                    r.showProducts(dataGridView1, proIDGV, proGV, expiryGV, catGV,barcodeGV, catIDGV);
                }

            }
        }
        public override void viewBtn_Click(object sender, EventArgs e)
        {
            r.showProducts(dataGridView1, proIDGV, proGV, expiryGV, catGV,barcodeGV, catIDGV);
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void leftPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void categoryDD_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void expiryPicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1 && e.ColumnIndex != -1)
            {
                edit = 1;
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                prodID = Convert.ToInt32(row.Cells["proIDGV"].Value.ToString());
                proTxt.Text = row.Cells["proGV"].Value.ToString();
                barcodeTxt.Text = row.Cells["barcodeGV"].Value.ToString();
                if (row.Cells["expiryGV"].FormattedValue.ToString() == "")
                {
                    expiryPicker1.Value = DateTime.Now;
                }
                else
                {
                    //expiryPicker1.Value = Convert.ToDateTime(row.Cells["expiryGV"].Value.ToString());
                }
                categoryDD.SelectedValue = row.Cells["catIDGV"].Value.ToString();
                MainClass.disable(leftPanel);
            }
        }
    }
}

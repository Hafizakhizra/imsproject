using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace imss
{
    public partial class Sales : Sample2
    {
        public Sales()
        {
            InitializeComponent();
        }
        Regex rg = new Regex(@"^[0-9]*(?:\.[0-9]*)?$");
        private void Sales_Load(object sender, EventArgs e)
        {

        }
        retrieval r = new retrieval();
        string[] prodARR = new string[6];
        float GROSS=0;
        private void barCodeTxt_TextChanged(object sender, EventArgs e)
        {
           
        }
        bool productCheck;
        private void barCodeTxt_Validating(object sender, CancelEventArgs e)
        {
            if (barCodeTxt.Text != "")
            {
                grossTxt.Text = "";
                totDiscountTxt.Text = "";
                amountGivenTxt.Text = "";
                changeGivenTxt.Text = "";
                int qCount = 0, sQuant = 0, nCount = 0;
                prodARR = r.getProductsWRTBarcode(barCodeTxt.Text);
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (prodARR[0] == row.Cells["proIDGV"].Value.ToString())
                    {
                        qCount = qCount + Convert.ToInt32(row.Cells["quantGV"].Value.ToString());
                    }

                }
                sQuant = Convert.ToInt32(r.getProductQuantity(Convert.ToInt64(prodARR[0])));

                nCount = sQuant - qCount;
                if (nCount <= 0)
                {

                }
                else
                {
                    if (dataGridView1.RowCount == 0)
                    {
                        dataGridView1.Rows.Add(Convert.ToInt32(prodARR[0]), prodARR[1], 1, Convert.ToSingle(prodARR[3]), prodARR[4], Convert.ToSingle(prodARR[5]));
                    }
                    else
                    {
                        foreach (DataGridViewRow row in dataGridView1.Rows)
                        {
                            if (row.Cells["proIDGV"].Value.ToString() == prodARR[0])
                            {
                                productCheck = true;
                                break;

                            }
                            else
                            {
                                productCheck = false;
                                //dataGridView1.Rows.Add(Convert.ToInt32(prodARR[0]), prodARR[1], 1, Convert.ToSingle(prodARR[3]), prodARR[4], Convert.ToSingle(prodARR[5]));
                            }

                        }
                        if (productCheck == true)
                        {
                            foreach (DataGridViewRow row in dataGridView1.Rows)
                            {
                                if (row.Cells["proIDGV"].Value.ToString() == prodARR[0])
                                {
                                    float disc = 0;
                                    row.Cells["quantGV"].Value = Convert.ToInt32(row.Cells["quantGV"].Value.ToString()) + 1;
                                    if (row.Cells["discGV"].Value.ToString() != null)
                                    {
                                        disc = Convert.ToSingle(row.Cells["discGV"].Value.ToString()) + Convert.ToSingle(row.Cells["discGV"].Value.ToString());
                                        row.Cells["discGV"].Value = disc;
                                    }

                                    float tot = (Convert.ToSingle(row.Cells["pupGV"].Value.ToString()) * Convert.ToInt32(row.Cells["quantGV"].Value.ToString())) - Convert.ToSingle(row.Cells["discGV"].Value.ToString());
                                    row.Cells["TotGV"].Value = tot;
                                }

                            }

                        }
                        else
                        {
                            dataGridView1.Rows.Add(Convert.ToInt32(prodARR[0]), prodARR[1], 1, Convert.ToSingle(prodARR[3]), prodARR[4], Convert.ToSingle(prodARR[5]));
                        }
                    }
                    foreach (DataGridViewRow item in dataGridView1.Rows)
                    {
                        GROSS += Convert.ToSingle(item.Cells["TotGV"].Value.ToString());

                    }
                    grossLabel.Text = Math.Round(GROSS, 0).ToString();
                    GROSS = 0;
                    barCodeTxt.Focus();
                    barCodeTxt.Text = "";
                }

            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1 && e.ColumnIndex != -1)
            {
                if (e.ColumnIndex == 6)
                {
                    grossTxt.Text = "";
                    totDiscountTxt.Text = "";
                    amountGivenTxt.Text = "";
                    changeGivenTxt.Text = "";
                    float gt,tot,dis;
                    DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                    int q = Convert.ToInt32(row.Cells["quantGV"].Value.ToString());
                    if (q == 1)
                    {
                        gt = Convert.ToSingle(grossLabel.Text);
                        gt = gt - Convert.ToSingle(row.Cells["TotGV"].Value.ToString());
                        grossLabel.Text = gt.ToString();
                        dataGridView1.Rows.Remove(row);
                    }
                    else if (q>1)
                    {
                        q--;
                        row.Cells["quantGV"].Value = q;
                        dis = Convert.ToSingle(row.Cells["discGV"].Value.ToString()) - Convert.ToSingle(prodARR[4]);
                        row.Cells["discGV"].Value = dis;
                        tot = Convert.ToSingle(row.Cells["quantGV"].Value.ToString()) * Convert.ToSingle(row.Cells["pupGV"].Value.ToString());
                        row.Cells["TotGVGV"].Value = tot;
                      
                        foreach (DataGridViewRow item in dataGridView1.Rows)
                        {
                            GROSS += Convert.ToSingle(item.Cells["TotGV"].Value.ToString());

                        }
                        grossLabel.Text = GROSS.ToString();
                        GROSS = 0;
                      

                    }
                }

            }
        }

        private void checkoutBtn_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count>0)
            {
                float dis = 0,gross = 0;
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    dis += Convert.ToSingle(row.Cells["discGV"].Value.ToString());
                    gross += Convert.ToSingle(row.Cells["TotGV"].Value.ToString());
                }
                grossTxt.Text = Math.Round(gross,0).ToString();
                totDiscountTxt.Text = Math.Round(dis,0).ToString();
            }
        }

        private void amountGivenTxt_TextChanged(object sender, EventArgs e)
        {
            if (amountGivenTxt.Text != "")
            {
                if(!rg.Match(amountGivenTxt.Text).Success)
                {
                    amountGivenTxt.Text = "";
                    amountGivenTxt.Focus();
                }
                else
                {
                }
            }
            else
            {
                changeGivenTxt.Text = "";
            }
        }

        private void amountGivenTxt_Validating(object sender, CancelEventArgs e)
        {
            if (amountGivenTxt.Text != "" && grossTxt.Text != "")
            {
                if (!(Convert.ToSingle(grossTxt.Text) <= Convert.ToSingle(amountGivenTxt.Text)))
                {

                    amountGivenTxt.Text = "";
                    changeGivenTxt.Text = "";
                    amountGivenTxt.Focus();
                }
                else
                {

                    float amountGiven = Convert.ToSingle(amountGivenTxt.Text);
                    float amounttoreturn = amountGiven - Convert.ToSingle(grossTxt.Text);
                    changeGivenTxt.Text = Math.Round(amounttoreturn,0).ToString();
                }
            }
           
                  
        }
        insertion i = new insertion();
        private void payBtn_Click(object sender, EventArgs e)
        {
            if (amountGivenTxt.Text != "" && totDiscountTxt.Text != "" && grossTxt.Text != "" && payDD.SelectedIndex!= -1 && changeGivenTxt.Text != "")
            {
                DialogResult dr = MessageBox.Show("\n\tTotal Amount :"+grossTxt.Text+"\n\tTotal Discount : "+totDiscountTxt.Text+"\n\tAmount Given: "+amountGivenTxt.Text+"\n\tAmount Returned : "+changeGivenTxt.Text+"\n\nAre you sure , submit current sales?\n", "Question...",MessageBoxButtons.YesNo,MessageBoxIcon.Question);
               if (dr == DialogResult.Yes)
               {
                  i.insertSales(dataGridView1, "proIDGV", "quantGV", retrieval.USER_ID, DateTime.Now, Convert.ToSingle(grossTxt.Text), Convert.ToSingle(totDiscountTxt.Text), Convert.ToSingle(amountGivenTxt.Text), Convert.ToSingle(changeGivenTxt.Text),payDD.SelectedItem.ToString());
                  MainClass.enable_reset(groupBox2);
                  dataGridView1.Rows.Clear();
                  grossLabel.Text = "0.00";
               }
            }
        }

      
    }
}

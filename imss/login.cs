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
    public partial class login : Sample
    {
        public login()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (usernameTxt.Text == "") { nameErrorLabel.Visible = true; } else { nameErrorLabel.Visible = false; }
            if (passTxt.Text == "") { passErrorLabel.Visible = true; } else { passErrorLabel.Visible = false; }
            if (nameErrorLabel.Visible || passErrorLabel.Visible)
            {
                MainClass.ShowMSG("fields with * are mendatory", "stop", "Error");// Error is the type of msg
            }
            else
            {
               
                    if (retrieval.getUserDetails(usernameTxt.Text, passTxt.Text))
                    {
                        HomeScreen hm = new HomeScreen();
                        MainClass.showWindow(hm, this, MDI.ActiveForm);
                       
                    }
                    else
                    {

                    }
                
            }
           
        }

        private void rightPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void login_Load(object sender, EventArgs e)
        {

        }

        private void usernameTxt_TextChanged(object sender, EventArgs e)
        {
            if (usernameTxt.Text == "") { nameErrorLabel.Visible = true; } else { nameErrorLabel.Visible = false; }
        }

        private void passTxt_TextChanged(object sender, EventArgs e)
        {
            if (passTxt.Text == "") { passErrorLabel.Visible = true; } else { passErrorLabel.Visible = false; }
        }
    }
}

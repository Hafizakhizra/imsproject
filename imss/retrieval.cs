using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace imss
{
    class retrieval
    {
        public void showUsers(DataGridView gv,DataGridViewColumn userIDGV,DataGridViewColumn nameGV,DataGridViewColumn usernameGV,DataGridViewColumn passGV,DataGridViewColumn emailGV,DataGridViewColumn phoneGV,DataGridViewColumn statusGV,string data=null)
        {
            try
            { 
                SqlCommand cmd;
                if (data == null)
                {
                       cmd = new SqlCommand("st_getUsersData",MainClass.con);
                }
                else
                {
                       cmd = new SqlCommand("st_getUsersDataLIKE", MainClass.con);
                    cmd.Parameters.AddWithValue("@data", data);
                }
              
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                userIDGV.DataPropertyName = dt.Columns["ID"].ToString();
                nameGV.DataPropertyName = dt.Columns["Name"].ToString();
                usernameGV.DataPropertyName = dt.Columns["Username"].ToString();
                passGV.DataPropertyName = dt.Columns["Password"].ToString();
                emailGV.DataPropertyName = dt.Columns["Email"].ToString();
                phoneGV.DataPropertyName = dt.Columns["Phone"].ToString();
                statusGV.DataPropertyName = dt.Columns["Status"].ToString();

                gv.DataSource = dt;
                
 

            }
            catch (Exception)
            {
            }
        }
        public void showCategories(DataGridView gv, DataGridViewColumn catIDGV, DataGridViewColumn catnameGV, DataGridViewColumn statusGV)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("st_getCategoriesData", MainClass.con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                catIDGV.DataPropertyName = dt.Columns["ID"].ToString();
                catnameGV.DataPropertyName = dt.Columns["Category"].ToString();
                statusGV.DataPropertyName = dt.Columns["Status"].ToString();
                gv.DataSource = dt;



            }
            catch (Exception)
            {
                MainClass.ShowMSG("Unable to load categories data", "Error", "Error");
            }
        }
     public void getList(string proc, ComboBox cb, string displayMember, string valueMember)
       {
            try
            {
                
                cb.DataSource = null;
               
                SqlCommand cmd = new SqlCommand(proc, MainClass.con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                DataRow dr = dt.NewRow();
                dr.ItemArray = new object[]{0,"Select..."};
                dt.Rows.InsertAt(dr,0);
                cb.DataSource = dt;
                cb.DisplayMember = displayMember;
                cb.ValueMember = valueMember;
            }
            catch (Exception)
            { 

            }
        }

     public void getListWithTwoParameters(string proc, ComboBox cb, string displayMember
         , string valueMember,string param1,object val1,string param2,object val2)
     {
         try
         {
          
             cb.DataSource = null;

             SqlCommand cmd = new SqlCommand(proc, MainClass.con);
             cmd.CommandType = CommandType.StoredProcedure;
             cmd.Parameters.AddWithValue(param1, val1);
             cmd.Parameters.AddWithValue(param2, val2);
             SqlDataAdapter da = new SqlDataAdapter(cmd);
             DataTable dt = new DataTable();
             da.Fill(dt);
             DataRow dr = dt.NewRow();
             dr.ItemArray = new object[] { 0, "Select..." };
             dt.Rows.InsertAt(dr, 0);
             cb.DataSource = dt;
             cb.DisplayMember = displayMember;
             cb.ValueMember = valueMember;
         }
         catch (Exception)
         {

         }
     }
        public void showProducts(DataGridView gv, DataGridViewColumn proIDGV,DataGridViewColumn proNameGV, DataGridViewColumn expiryGV, DataGridViewColumn catGV, DataGridViewColumn barGV, DataGridViewColumn catIDGV)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("st_getProductsData", MainClass.con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                proIDGV.DataPropertyName = dt.Columns["Product ID"].ToString();
                proNameGV.DataPropertyName = dt.Columns["Product"].ToString();
                barGV.DataPropertyName = dt.Columns["Barcode"].ToString();
                expiryGV.DataPropertyName = dt.Columns["Expiry"].ToString();
                catGV.DataPropertyName = dt.Columns["Category"].ToString();
                catIDGV.DataPropertyName = dt.Columns["Category ID"].ToString();
                gv.DataSource = dt;



            }
            catch (Exception)
            {
                MainClass.ShowMSG("Unable to load categories data", "Error", "Error");
            }
        }
        private object productStockcount = 0;
        public object getProductQuantity(Int64 proID)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("st_getProductQuantity",MainClass.con);
                     cmd.CommandType = CommandType.StoredProcedure;
                     cmd.Parameters.AddWithValue("@proID", proID);
                   
                  
                     MainClass.con.Open();
                     productStockcount = cmd.ExecuteScalar();
                     MainClass.con.Close();
            }
            catch (Exception)
            {
            }
            return productStockcount;
    }
        public object getProductQuantityWithoutConnection(Int64 proID)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("st_getProductQuantity", MainClass.con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@proID", proID);
                productStockcount = cmd.ExecuteScalar();
           
            }
            catch (Exception)
            {
            }
            return productStockcount;
        }
        public void showPurchaseInvoiceDetails(Int64 pid,DataGridView gv,DataGridViewColumn mPIDGV, DataGridViewColumn proIDGV, DataGridViewColumn pronameGV, DataGridViewColumn quantGV, DataGridViewColumn pupGV, DataGridViewColumn totGV)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("st_getPurchaseINvoiceDetails", MainClass.con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@pid", pid);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                mPIDGV.DataPropertyName = dt.Columns["mPID"].ToString();
                proIDGV.DataPropertyName = dt.Columns["Product ID"].ToString();
                pronameGV.DataPropertyName = dt.Columns["Product"].ToString();
                quantGV.DataPropertyName = dt.Columns["Quantity"].ToString();
                pupGV.DataPropertyName = dt.Columns["Per Unit Price"].ToString();
                totGV.DataPropertyName = dt.Columns["Total Price"].ToString();
               
                gv.DataSource = dt;



            }
            catch (Exception)
            {
                MainClass.ShowMSG("Unable to load categories data", "Error", "Error");
            }
        }

        public void showProductWRTCategory(int catID, DataGridView gv, DataGridViewColumn proIDGV, DataGridViewColumn proNameGV, DataGridViewColumn bpGV,DataGridViewColumn spGV,
           DataGridViewColumn disGv,DataGridViewColumn profitPerGV)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("st_getProductsWRTCategory", MainClass.con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@catID", catID);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                proIDGV.DataPropertyName = dt.Columns["Product ID"].ToString();
                proNameGV.DataPropertyName = dt.Columns["Product"].ToString();
                bpGV.DataPropertyName = dt.Columns["Buying Price"].ToString();
                spGV.DataPropertyName = dt.Columns["Selling Price"].ToString();
                disGv.DataPropertyName = dt.Columns["Discount"].ToString();
                profitPerGV.DataPropertyName = dt.Columns["Profit Percentage"].ToString();

                gv.DataSource = dt;



            }
            catch (Exception)
            {
                MainClass.ShowMSG("Unable to load products data", "Error", "Error");
            }
        }
        public static int USER_ID
        {
            get;
            private set;
        }
        public static int EMP_NAME
        {
            get;
            private set;
        }
        private string[] productsData = new string[6];
        public string[] getProductsWRTBarcode(string barcode)
        {
            try
            {

                SqlCommand cmd = new SqlCommand("st_getProductByBarcode", MainClass.con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@barcode",barcode);
                MainClass.con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        productsData[0] = dr[0].ToString();//proID
                        productsData[1] = dr[1].ToString();//product
                        productsData[2] = dr[2].ToString();//Barcode
                        productsData[3] = dr[3].ToString();//Selling Price
                        productsData[4] = dr[4].ToString();//Discount
                        productsData[5] = dr[5].ToString();//Final Selling Price
                      
                    }
                }
                else
                {
                   // MainClass.ShowMSG("No product availabel", "Error", "Error");
                    Array.Clear(productsData, 0, productsData.Length);
                }
                MainClass.con.Close();
            }
            catch (Exception)
            {
                MainClass.con.Close();
                throw;

            }
            return productsData;
        }
        private static string user_name=null, pass_word=null;
        private static bool checklogin;
        public static bool getUserDetails(string username,string password)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("st_getUserDetails", MainClass.con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@user",username);
                cmd.Parameters.AddWithValue("@pass",password);
                MainClass.con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    checklogin = true;
                    while (dr.Read())
                    {
                        USER_ID = Convert.ToInt32(dr["ID"].ToString());
                        USER_NAME = dr["Name"].ToString();
                        user_name = dr["Username"].ToString();
                        pass_word = dr["Password"].ToString();
                    }
                
                }
                    else
                    {
                        checklogin = false;
                        if (username != null && password != null)
                        {
                            if (user_name != username && pass_word == password)
                            {
                                MainClass.ShowMSG("Invalid username","Error","Error");
                            }
                            else if (user_name == username && pass_word != password)
                            {
                                MainClass.ShowMSG("Invalid password", "Error", "Error");
                            }
                            if (user_name != username && pass_word != password)
                            {
                                MainClass.ShowMSG("Invalid username and password", "Error", "Error");
                            }
                        }

                    }
                  MainClass.con.Close();
                }
                   
            catch (Exception)
            {
                MainClass.con.Close();
                MainClass.ShowMSG("Unable to login...","Error","Erroor");
             
            }
            return checklogin;
        }

        public static string USER_NAME { get; set; }
        public void showSuppliers(DataGridView gv, DataGridViewColumn suppIDGV, DataGridViewColumn comNameGV,
        DataGridViewColumn personGV, DataGridViewColumn phone1GV, DataGridViewColumn phone2GV, DataGridViewColumn addressGV, DataGridViewColumn ntnGV, DataGridViewColumn statusGV)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("st_getSupplierData", MainClass.con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                suppIDGV.DataPropertyName = dt.Columns["ID"].ToString();
                comNameGV.DataPropertyName = dt.Columns["Company"].ToString();
                personGV.DataPropertyName = dt.Columns["Contact Person"].ToString();
                phone1GV.DataPropertyName = dt.Columns["Phone 1"].ToString();
                phone2GV.DataPropertyName = dt.Columns["Phone 2"].ToString();
                addressGV.DataPropertyName = dt.Columns["Address"].ToString();
                statusGV.DataPropertyName = dt.Columns["Status"].ToString();
                ntnGV.DataPropertyName = dt.Columns["NTN #"].ToString();
                gv.DataSource = dt;



            }
            catch (Exception)
            {
                MainClass.ShowMSG("Unable to load suppliers data", "Error", "Error");
            }
        }
        private bool checkPPExistance;
        public bool checkProductPriceExistance(Int64 proID)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("st_checkProductPriceExist", MainClass.con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@proID", proID);
                MainClass.con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    checkPPExistance = true;
                }
                else
                {
                    checkPPExistance = false;
                }
                MainClass.con.Close();
            }
        catch (Exception)
         {

         }
            return checkPPExistance;
      }
        public void showStockDetails(DataGridView gv, DataGridViewColumn proIDGV, DataGridViewColumn proGV,
     DataGridViewColumn barcodeGV, DataGridViewColumn expiryGV, DataGridViewColumn bpGV, DataGridViewColumn spGV, DataGridViewColumn catGV, DataGridViewColumn availStGV, DataGridViewColumn statusGV,DataGridViewColumn totGV)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("st_getAllStock", MainClass.con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                proIDGV.DataPropertyName = dt.Columns["Product ID"].ToString();
                proGV.DataPropertyName = dt.Columns["Product"].ToString();
                barcodeGV.DataPropertyName = dt.Columns["Barcode"].ToString();
                expiryGV.DataPropertyName = dt.Columns["Expiry Date"].ToString();
                bpGV.DataPropertyName = dt.Columns["Buying Price"].ToString();
                spGV.DataPropertyName = dt.Columns["Selling Price"].ToString();
                catGV.DataPropertyName = dt.Columns["Category"].ToString();
                availStGV.DataPropertyName = dt.Columns["Availabel Stock"].ToString();
                statusGV.DataPropertyName = dt.Columns["Status"].ToString();
                totGV.DataPropertyName = dt.Columns["Total Amount"].ToString();
                gv.DataSource = dt;



            }
            catch (Exception)
            {
                MainClass.ShowMSG("Unable to load stock data", "Error", "Error");
            }
        }

    }
    
}

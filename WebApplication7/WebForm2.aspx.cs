using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.Services;
using Telerik.Web.UI;

namespace WebApplication7
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        private const int ItemsPerRequest = 5;

        public int[] xMoves = { 2, 1, -1, -2, -2, -1, 1, 2 };
        public int[] yMoves = { 1, 2, 2, 1, -1, -2, -2, -1 };
        public int[,] sol = new int[8, 8];
        protected void Page_Load(object sender, EventArgs e)
        {
            PossibleKnightMoves();
            if (!Page.IsPostBack)
            {
                //BindComboBox("", 0);
            }

        }


        public bool PossibleKnightMoves()
        {
            // Intialize sol matrix
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    sol[i, j] = -1;
                }
            }

            // Since the Knight is initially at the first block
            sol[0, 0] = 0;

            if (solveKTUtil(0, 0, 1, sol, xMoves, yMoves) == false)
            {
                Response.Write("Solution does not exist");
                return false;
            }
            else
                printSolution(sol);

            return true;
        }
        /* A utility function to print solution matrix sol[N][N] */
        public void printSolution(int[,] sol)
        {
            for (int x = 0; x < 8; x++)
            {
                for (int y = 0; y < 8; y++)
                {
                    Response.Write(sol[x, y] > 9 ? sol[x, y] + " " : "0" + sol[x, y] + " ");
                }
                Response.Write("<br />");
            }
        }
        public bool solveKTUtil(int x, int y, int movei, int[,] sol, int[] xMove, int[] yMove)
        {
            int k, next_x, next_y;
            if (movei == 8 * 8)
            {
                return true;
            }


            /* Try all next moves from the current coordinate x, y */
            for (k = 0; k < 8; k++)
            {
                next_x = x + xMove[k];
                next_y = y + yMove[k];
                if (isSafe(next_x, next_y, sol))
                {
                    sol[next_x, next_y] = movei;
                    if (solveKTUtil(next_x, next_y, movei + 1, sol,
                                    xMove, yMove) == true)
                        return true;
                    else
                        sol[next_x, next_y] = -1;// backtracking
                }
            }

            return false;
        }
        public bool isSafe(int x, int y, int[,] sol)
        {
            return (x >= 0 && x < 8 && y >= 0 &&
                     y < 8 && sol[x, y] == -1);
        }


        [WebMethod]
        public RadComboBoxItemData[] GetProducts(RadComboBoxContext context)
        {
            IDictionary<string, object> contextDictionary = (IDictionary<string, object>)context;
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["HrmCustomers"].ConnectionString);
            string filterString = ((string)contextDictionary["Text"]).ToLower();
            SqlCommand selectCommand = new SqlCommand(
         @" SELECT * FROM Core_Users WHERE LOWER(UserName) LIKE '" + filterString + "%'", connection);
            SqlDataAdapter adapter = new SqlDataAdapter(selectCommand);
            DataTable products = new DataTable();
            adapter.Fill(products);
            List<RadComboBoxItemData> result = new List<RadComboBoxItemData>(products.Rows.Count);
            foreach (DataRow row in products.Rows)
            {
                RadComboBoxItemData itemData = new RadComboBoxItemData();
                itemData.Text = row["UserName"].ToString();
                itemData.Value = row["Id"].ToString();
                result.Add(itemData);
            }
            return result.ToArray();
        }

        protected void RadComboBox1_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {

        }

        protected void Button4_Click(object sender, EventArgs e)
        {

        }

        protected void RadComboBox1_ItemsRequested1(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            int itemOffset = e.NumberOfItems;
            int endOffset = itemOffset + ItemsPerRequest;


            string sql = string.Empty;
            if (string.IsNullOrEmpty(e.Text))
            {
                sql = @"with temp as (
   select Id,Username,ROW_NUMBER() Over(order by Id) as rownumber from Core_Users
)
select Id,Username from temp where rownumber between " + itemOffset + " and " + endOffset + "";
            }
            else
            {
                sql = @"with temp as (
   select Id,Username,ROW_NUMBER() Over(order by Id) as rownumber from Core_Users WHERE UserName LIKE '" + e.Text + @"%'
)
select Id,Username from temp where rownumber between " + itemOffset + " and " + endOffset + "";

            }
            SqlDataAdapter adapter = new SqlDataAdapter(sql, ConfigurationManager.ConnectionStrings["HrmCustomers"].ConnectionString);
            DataTable data = new DataTable();
            adapter.Fill(data);



            for (int i = 0; i < data.Rows.Count; i++)
            {
                RadComboBox1.Items.Add(new RadComboBoxItem(data.Rows[i]["UserName"].ToString(), data.Rows[i]["Id"].ToString()));
            }

            if (data.Rows.Count > 0)
            {
                e.Message = String.Format("Items <b>1</b>-<b>{0}</b> out of <b>{1}</b>", endOffset.ToString(), endOffset.ToString());
            }
            else { e.Message = "No matches"; }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            //DisplaySelection(RadComboBox1, Label1);
        }

        public void BindComboBox(string text, int numberOfItems)
        {
            string sql = string.Empty;
            if (string.IsNullOrEmpty(text))
            {
                sql = "SELECT  top 3 * from Core_Users";
            }
            else
            {
                sql = "SELECT   * from Core_Users WHERE UserName LIKE '" + text + "%'";
            }
            SqlDataAdapter adapter = new SqlDataAdapter(sql, ConfigurationManager.ConnectionStrings["HrmCustomers"].ConnectionString);
            DataTable data = new DataTable();
            adapter.Fill(data);
            try
            {
                int itemsPerRequest = 3;
                int itemOffset = numberOfItems;
                int endOffset = itemOffset + itemsPerRequest;
                if (endOffset > data.Rows.Count)
                {
                    endOffset = data.Rows.Count;
                }
                for (int i = itemOffset; i < endOffset; i++)
                {
                    RadComboBox1.Items.Add(new RadComboBoxItem(data.Rows[i]["UserName"].ToString(), data.Rows[i]["Id"].ToString() + "'"));
                }
                //if (data.Rows.Count > 0)
                //{
                //    e.Message = String.Format("Items <b>1</b>-<b>{0}</b> out of <b>{1}</b>", endOffset.ToString(), data.Rows.Count.ToString());
                //}
                //else { e.Message = "No matches"; }
            }
            catch
            {
                //e.Message = "No matches";
            }
        }

        protected void RadComboBox1_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {

        }

        protected void RadComboBox1_ItemsRequested2(object sender, RadComboBoxItemsRequestedEventArgs e)
        {

        }
    }
}
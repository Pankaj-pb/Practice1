using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.Services;
using Telerik.Web.UI;

namespace WebApplication7
{
    /// <summary>
    /// Summary description for Product
    /// </summary>
    [System.Web.Script.Services.ScriptService]
    public class Product : WebService
    {

       
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
    }
}

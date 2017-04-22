using System;
using System.Reflection;
using System.Web.UI;

namespace WebApplication7
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        public static int i = 0;
        string str = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            //Method();
            //PrintAllPossibleCombinations("ABC");

            //Fun(1);
            PrintName();
            //Fun(1);

        }

        private void PrintName()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            var instance = assembly.CreateInstance("WebApplication7.Customer");
            Type type = Type.GetType("WebApplication7.Customer");
            var getFullNameMethodInfo = type.GetMethod("GetFullName");
            string[] parameters = new string[2];
            parameters[0] = "Pankaj";
            parameters[1] = "Kakkar";
            Response.Write(getFullNameMethodInfo.Invoke(instance, parameters));

            // -------------------***********------------------------

            var customer = (Customer)assembly.CreateInstance("WebApplication7.Customer");
            Response.Write(customer.GetFullName("Manish", "Kumar"));
        }

        /// <summary>
        /// Result will be like fun(1) to fun(4) --> (3)power(n) where n depends how many time fun(n+1) is called. in other case
        /// if starts from 2 =>fun(2) to fun(4) --> (2)power(n) where n depends how many time fun(n+1) is called.
        /// </summary>
        /// <param name="n"></param>
        public void Fun(int n)
        {


            if (n == 4)
            {
                Response.Write(n);
                return;
            }
            Fun(n + 1);
            Fun(n + 1);
            Fun(n + 1);
        }

        public void Fun1(int n)
        {
            Response.Write(n + ",");
            str += n + ",";
            if (n == 4)
            {
                return;
            }
            for (int i = n; i <= 3; i++)
            {
                Fun1(i + 1);
            }
        }
        private void PrintAllPossibleCombinations(string str)
        {
            GetPer(str.ToCharArray());
        }
        public void GetPer(char[] list)
        {
            int x = list.Length - 1;
            GetPer(list, 0, x);
        }
        private void GetPer(char[] list, int l, int r)
        {
            if (l == r)
            {
                Response.Write(new String(list) + System.Environment.NewLine);

            }
            else
                for (int i = l; i <= r; i++)
                {
                    Swap(ref list[l], ref list[i]);
                    GetPer(list, l + 1, r);
                    Swap(ref list[l], ref list[i]);
                }
        }
        private void Swap(ref char a, ref char b)
        {
            if (a == b) return;

            a ^= b;
            b ^= a;
            a ^= b;
        }
        public void Method()
        {
            var message = " Confirmatkions' message";
            message = message.Replace("'", "/'");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "script", "Confirm('" + message + " ');", true);
            //PlaceHolder_AssociateExistingCandidate.Visible = true;
            //string tmpMsg = "tests";

            //Label_ExistingCandidateWarning.Text = string.Format(tmpMsg);
        }

        protected void btnConfirm_Click(object sender, EventArgs e)
        {
            string confirmValue = Request.Form["confirm_value"];
            if (confirmValue == "Yes")
            {
                //Your logic for OK button
            }
            else
            {
                //Your logic for cancel button
            }
        }

        protected void Button_UseExistingCandidate_Click(object sender, EventArgs e)
        {

        }
    }

    public class Customer
    {

        public string GetFullName(string firstName, string lastName)
        {
            return firstName + " " + lastName;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Arvind_Project_Ado.Models;
using System.Data.SqlClient;
using System.Web.Mvc;

namespace Arvind_Project_Ado.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\727834\source\repos\Arvind_Project_Ado\Arvind_Project_Ado\App_Data\Items.mdf;Integrated Security=True");

            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT * from Items", con);
            cmd.ExecuteNonQuery();
            List<ItemModel> it = new List<ItemModel>();
            ItemModel details = new ItemModel();

            using (SqlDataReader read = cmd.ExecuteReader())
            {
                while (read.Read())
                {
                    details = new ItemModel();
                    details.Id = int.Parse(read["Id"].ToString());
                    details.Name = (read["Name"].ToString());
                    details.Date = DateTime.Parse(read["Date"].ToString());
                    details.DateFormat = details.Date.ToString("MM/dd/yyyy");
                    it.Add(details);
                }
                con.Close();
            }
            return View(it);
        }
        [HttpPost]
        public ActionResult Delete(ItemModel itemModel)
        {
            String query = "Delete from Items where Id=" + itemModel.Id;
            string constr = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\727834\source\repos\Arvind_Project_Ado\Arvind_Project_Ado\App_Data\Items.mdf;Integrated Security=True";
            using (SqlConnection con = new SqlConnection(constr))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Connection = con;
                    cmd.ExecuteNonQuery();
                }
                con.Close();
            }
            return Json("success");
        }
        [HttpPost]
        public ActionResult SearchItem(DateTime d1, DateTime d2)

        {

            SqlConnection con1 = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\727834\source\repos\Arvind_Project_Ado\Arvind_Project_Ado\App_Data\Items.mdf;Integrated Security=True");
            con1.Open();
            string temp = d1.ToShortDateString();
            string temp1 = d2.ToShortDateString();
            d1 = DateTime.Parse(temp);
            string q = "SELECT * from items WHERE Date >= '" + temp + "' AND Date<= '" + temp1 + "'";

            SqlCommand cmd1 = new SqlCommand(q, con1);
            cmd1.ExecuteNonQuery();
            List<ItemModel> li = new List<ItemModel>();

            using (SqlDataReader read = cmd1.ExecuteReader())
            {
                while (read.Read())
                {
                    ItemModel n = new ItemModel();
                    n.Id = int.Parse(read["Id"].ToString());
                    n.Name = read["Name"].ToString();
                    n.Date = DateTime.Parse(read["Date"].ToString());
                    n.DateFormat = n.Date.ToString("MM/dd/yyyy");
                    li.Add(n);
                }


                con1.Close();
            }

            return Json(li);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using Team28Delivery.Models;

namespace Team28Delivery.Controllers
{
    public class CustomerUserController : Controller
    {
        // GET: CustomerUser
        public ActionResult Index()
        {
            string constr = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            string sql = "SELECT * FROM [dbo].[AspNetUsers]";
            var model = new List<CustomerUser>();

            using (SqlConnection conn = new SqlConnection(constr))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    var customeruser = new CustomerUser();
                    customeruser.FirstName = rdr["FirstName"].ToString();
                    customeruser.LastName = rdr["LastName"].ToString();
                    customeruser.Phone = rdr["Phone"].ToString();
                    customeruser.Email = rdr["Email"].ToString();
                    //customeruser.UserType = rdr["UserType"].ToString();

                    //customeruser.StreetAddress = rdr["StreetAddress"].ToString();
                    //customeruser.Suburb = rdr["Suburb"].ToString();
                    //customeruser.PostalCode = rdr["PostalCode"].ToString();
                    //customeruser.State = rdr["State"].ToString();
                    //customeruser.Country = rdr["Country"].ToString();
                    model.Add(customeruser);
                }
            }
            return View(model);
        }

    }
}
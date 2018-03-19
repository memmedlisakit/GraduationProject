using ISM_Final_Lahiye.Areas.Adminpanel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using ISM_Final_Lahiye.Areas.Adminpanel.Filters;

namespace ISM_Final_Lahiye.Areas.Adminpanel.Controllers
{
    [UserIsLoginFilter]
    public class BrandController : Controller
    {
        string cs = LoginLogoutController.cs;
        SqlDataAdapter da = new SqlDataAdapter();
        DataTable dt = new DataTable();

        [UserRoleAuthorizeFilter]
        public ActionResult Index()
        {
            return View(select());
        }

        [UserRoleAuthorizeFilter]
        [HttpPost]
        public ActionResult Insert(FormCollection form)
        {
            string brand_name = form["brand_name"];
            if (brand_name == "") 
            {
                TempData["error_brand"] = "Do not empty";
                return RedirectToAction("Index");
            }
            
            using(SqlConnection conn =new SqlConnection(cs))
            {
                conn.Open();
                string query = "INSERT INTO Brands (brand_name) VALUES ('" + brand_name + "')";
                SqlCommand com = new SqlCommand(query, conn);
                com.ExecuteNonQuery();
                conn.Close();
            }
            return RedirectToAction("Index");
        }

        [UserRoleAuthorizeFilter]
        public ActionResult Delete(int id)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(cs))
                {
                    conn.Open();
                    string query = "DELETE FROM Brands WHERE brand_id = " + id;
                    SqlCommand com = new SqlCommand(query, conn);
                    com.ExecuteNonQuery();
                    conn.Close();
                }
            }
            catch (Exception)
            {
                TempData["delete_error_message"] = "You can not delete this Brand because this Brand connected to some Products";
            }
         
            return RedirectToAction("Index");
        }

        [UserRoleAuthorizeFilter]
        public ActionResult Update(int id)
        {
            return View(select(id));
        }

        [UserRoleAuthorizeFilter]
        [HttpPost]
        public ActionResult Update(int id, Brand brand)
        {
            if (brand.brand_name == null) 
            {
                TempData["error_brand"] = "Do not empty";
                return View(select(id));
            }
            using(SqlConnection conn =new SqlConnection(cs))
            {
                conn.Open();
                string query = "UPDATE Brands SET brand_name = '" + brand.brand_name + "' WHERE brand_id = " + id;
                SqlCommand com = new SqlCommand(query, conn);
                com.ExecuteNonQuery();
                conn.Close();
            } 
            return RedirectToAction("Index");
        }


        List<Brand> select()
        {
            List<Brand> brads = new List<Brand>();
            using(SqlConnection conn =new SqlConnection(cs))
            {
                string query = "SELECT * FROM Brands";
                SqlCommand com = new SqlCommand(query, conn);
                da.SelectCommand = com;
                da.Fill(dt);
            }
            foreach (DataRow brand in dt.Rows)
            {
                brads.Add(new Brand()
                {
                    brand_id = Convert.ToInt32(brand["brand_id"]),
                    brand_name = brand["brand_name"].ToString()
                });
            }
            return brads;
        }

        Brand select(int id)
        {
            Brand brand = new Brand();
            using(SqlConnection conn =new SqlConnection(cs))
            {
                string query = "SELECT * FROM Brands WHERE brand_id = " + id;
                SqlCommand com = new SqlCommand(query, conn);
                da.SelectCommand = com;
                da.Fill(dt);
            }
            brand.brand_id = Convert.ToInt32(dt.Rows[0]["brand_id"]);
            brand.brand_name = dt.Rows[0]["brand_name"].ToString();
            return brand;
        }
    }
}
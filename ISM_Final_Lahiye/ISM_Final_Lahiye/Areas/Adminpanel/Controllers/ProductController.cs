using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using ISM_Final_Lahiye.Areas.Adminpanel.Models;
using System.IO;
using ISM_Final_Lahiye.Areas.Adminpanel.Filters;

namespace ISM_Final_Lahiye.Areas.Adminpanel.Controllers
{
    [UserIsLoginFilter]
    public class ProductController : Controller
    {
        string cs = LoginLogoutController.cs;
        SqlDataAdapter da = new SqlDataAdapter();
        DataTable dt = new DataTable();
        
        [UserRoleAuthorizeFilter]
        public ActionResult Index()
        { 
            ViewBag.barnds = select_brands();
            return View(select(false));
        }


        [UserRoleAuthorizeFilter]
        public ActionResult Insert()
        {
            List<Brand> brands = new List<Brand>();
            using (SqlConnection conn = new SqlConnection(cs))
            {
                string query = "SELECT * FROM Brands";
                SqlCommand com = new SqlCommand(query, conn);
                da.SelectCommand = com;
                da.Fill(dt);
            }
            foreach (DataRow brand in dt.Rows)
            {
                brands.Add(new Brand()
                {
                    brand_id = Convert.ToInt32(brand["brand_id"]),
                    brand_name = brand["brand_name"].ToString()
                });
            }
            ViewBag.brands = brands;
            return View();
        }
         

        [UserRoleAuthorizeFilter]
        [HttpPost]
        public ActionResult Insert(Product product, HttpPostedFileBase pro_img)
        {
            string file_name = null;
            bool pro_status = product.pro_status == "on" ? true : false;
            if (pro_img != null) 
            {
                file_name = DateTime.Now.ToString("MMddyyyyHHssmmttfff") + pro_img.FileName;
                string file_path = Path.Combine(Server.MapPath("/Uploads/"), file_name);
                pro_img.SaveAs(file_path);
            }
            using(SqlConnection conn =new SqlConnection(cs))
            {
                conn.Open();
                string query = "INSERT INTO Products (pro_name, pro_price, pro_quantity, pro_img, pro_color, pro_status, pro_brand_id) VALUES ('" + product.pro_name + "', " + product.pro_price + ", " + product.pro_quantity + ", '" + file_name + "', '" + product.pro_color + "', '" + pro_status + "', " + product.pro_brand_id + ")";
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
                    string query = "DELETE Products WHERE pro_id = " + id;
                    SqlCommand com = new SqlCommand(query, conn);
                    com.ExecuteNonQuery();
                    conn.Close();
                }
            }
            catch (Exception)
            {
                TempData["delete_error_message"] = "You can not delete this Product because this Product  added to cart or ordered some users";

            }
           
            return RedirectToAction("Index");
        }


        [UserRoleAuthorizeFilter]
        public  ActionResult Update(int id)
        {
            ViewBag.update_brands = select_brands();
            return View(select(id));
        }


        [UserRoleAuthorizeFilter]
        [HttpPost]
        public ActionResult Update(int id, Product product, HttpPostedFileBase pro_img)
        {
            string file_name = null;
            bool pro_status = product.pro_status == "on" ? true : false;
            string query = "UPDATE Products SET pro_name = '" + product.pro_name + "', pro_price = " + product.pro_price + ", pro_quantity = " + product.pro_quantity + ", pro_status = '" + pro_status + "', pro_brand_id = " + product.pro_brand_id;

            if (pro_img != null)
            {
                file_name = DateTime.Now.ToString("MMddyyyyHHssmmttfff") + pro_img.FileName;
                string file_path = Path.Combine(Server.MapPath("/Uploads/"), file_name);
                pro_img.SaveAs(file_path);
                query += ", pro_img = '" + file_name+"'";
            }
            if (product.pro_color != null) 
            {
                query += ", pro_color = '" + product.pro_color+"'";
            } 
            using (SqlConnection conn = new SqlConnection(cs))
            {
                conn.Open();
                query += " WHERE pro_id = " + id; 
                SqlCommand com = new SqlCommand(query, conn);
                com.ExecuteNonQuery();
                conn.Close();
            }
            return RedirectToAction("Index");
        }


        [UserRoleAuthorizeFilter]
        public ActionResult Customer_index()
        {
            ViewBag.brands = select_brands();
            return View(select(true)); 
        }
           
        [UserRoleAuthorizeFilter]
        [HttpPost]   
        public void Add_to_cart(string product_quantity, int product_id)
        {
            int user_id = LoginLogoutController.Logined_user.user_id;
            using (SqlConnection conn =new SqlConnection(cs))
            {
                conn.Open();
                string query = "INSERT INTO Cart (cart_user_id, cart_product_id, cart_product_quantity) VALUES (" + user_id + ", " + product_id + ", " + product_quantity + ")";
                SqlCommand com = new SqlCommand(query, conn);
                com.ExecuteNonQuery();
                conn.Close();
            }
        }

        [UserRoleAuthorizeFilter]
        public ActionResult Cart()
        { 
            return View(select_cart());
        }

        [UserRoleAuthorizeFilter]
        public ActionResult Delete_cart(int id)
        {
            using(SqlConnection conn =new SqlConnection(cs))
            {
                conn.Open();
                string query = "DELETE FROM Cart WHERE cart_id = " + id;
                SqlCommand com = new SqlCommand(query, conn);
                com.ExecuteNonQuery();
                conn.Close();
            }
            return RedirectToAction("Cart");
        }


        // product order with ajax
        [UserRoleAuthorizeFilter]
        [HttpPost]
        public void Order(int user_id, int product_id, int product_quantity, double total_price, int cart_id)
        {
            using (SqlConnection conn = new SqlConnection(cs))
            {
                conn.Open();
                string query = "INSERT INTO Orders (order_user_id, order_product_id, order_product_quantity, order_total_price, order_date) VALUES (" + user_id + ", " + product_id + ", " + product_quantity + ", " + total_price + ", '" + DateTime.Now + "')";
                SqlCommand com = new SqlCommand(query, conn);
                com.ExecuteNonQuery();

                query = "DELETE FROM Cart WHERE cart_id  = " + cart_id;
                com.CommandText = query;
                com.Connection = conn;
                com.ExecuteNonQuery();
                conn.Close();
            }
        }






        List<Product> select(bool status)
        {
            string query = "SELECT * FROM Products";
            if (status)
            {
                query += " WHERE pro_status = " +  "'"+status+"'"+" AND pro_quantity > 0";
            }
            dt.Clear();
            List<Product> products = new List<Product>();
            using(SqlConnection conn =new SqlConnection(cs))
            { 
                SqlCommand com = new SqlCommand(query, conn);
                da.SelectCommand = com;
                da.Fill(dt);
            }
            foreach (DataRow pro in dt.Rows)
            {
                products.Add(new Product()
                {
                    pro_id = Convert.ToInt32(pro["pro_id"]),
                    pro_name = pro["pro_name"].ToString(),
                    pro_quantity = Convert.ToInt32(pro["pro_quantity"]),
                    pro_price = Convert.ToDecimal(pro["pro_price"]),
                    pro_img = pro["pro_img"].ToString(),
                    pro_color = pro["pro_color"].ToString(),
                    pro_status = pro["pro_status"].ToString(),
                    pro_brand_id = Convert.ToInt32(pro["pro_brand_id"])
                });

            }
            return products;
        }

        Product select(int id)
        {
            dt.Clear();
            Product product = new Product();
            using(SqlConnection conn =new SqlConnection(cs))
            {
                string query = "SELECT * FROM Products WHERE pro_id = " + id;
                SqlCommand com = new SqlCommand(query, conn);
                da.SelectCommand = com;
                da.Fill(dt);
            }
            product.pro_id = Convert.ToInt32(dt.Rows[0]["pro_id"]);
            product.pro_name = dt.Rows[0]["pro_name"].ToString();
            product.pro_price = Convert.ToDecimal(dt.Rows[0]["pro_price"]);
            product.pro_quantity = Convert.ToInt32(dt.Rows[0]["pro_quantity"]);
            product.pro_status = dt.Rows[0]["pro_status"].ToString();
            product.pro_img = dt.Rows[0]["pro_img"].ToString();
            product.pro_color = dt.Rows[0]["pro_color"].ToString();
            product.pro_brand_id = Convert.ToInt32(dt.Rows[0]["pro_brand_id"]);
            return product;
        }

        List<Brand> select_brands()
        {
            List<Brand> brands = new List<Brand>();
            using (SqlConnection conn = new SqlConnection(cs))
            {
                string query = "SELECT * FROM Brands";
                SqlCommand com = new SqlCommand(query, conn);
                da.SelectCommand = com;
                da.Fill(dt);
            }
            foreach (DataRow brand in dt.Rows)
            {
                brands.Add(new Brand()
                {
                    brand_id = Convert.ToInt32(brand["brand_id"]),
                    brand_name = brand["brand_name"].ToString()
                });
            }
            return brands;
        }

        List<Cart> select_cart()
        {
            dt.Clear();
            List<Cart> carts = new List<Models.Cart>();
            using(SqlConnection conn =new SqlConnection(cs))
            {
                string query = "SELECT cart_id, cart_user_id,pro_id, pro_name, brand_name, pro_price, cart_product_quantity, pro_color, pro_img FROM Cart  INNER JOIN Users ON user_id = cart_user_id INNER JOIN Products ON pro_id = cart_product_id  INNER JOIN Brands ON brand_id = pro_brand_id WHERE cart_user_id = " + LoginLogoutController.Logined_user.user_id;
                SqlCommand com = new SqlCommand(query, conn);
                da.SelectCommand = com;
                da.Fill(dt);
            }

            foreach (DataRow row in dt.Rows)
            {
                carts.Add(new Cart()
                {
                    cart_id = Convert.ToInt32(row["cart_id"]),
                    cart_user_id = Convert.ToInt32(row["cart_user_id"]),
                    pro_id = Convert.ToInt32(row["pro_id"]),
                    pro_name = row["pro_name"].ToString(),
                    brand_name = row["brand_name"].ToString(),
                    pro_price = Convert.ToDouble(row["pro_price"]) * Convert.ToInt32(row["cart_product_quantity"]),
                    cart_product_quantity = Convert.ToInt32(row["cart_product_quantity"]),
                    pro_color = row["pro_color"].ToString(),
                    pro_img = row["pro_img"].ToString(),
                    
                });
            }
            return carts;
        }

     }
}
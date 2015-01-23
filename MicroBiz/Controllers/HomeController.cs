using MicroBiz.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace MicroBiz.Controllers
{
    public class HomeController : Controller
    {
        private List<Product> myCart = new List<Product>();
        private ProductDbContext db = new ProductDbContext();
        public byte[] ConvertToBytes(HttpPostedFileBase image)
        {
            byte[] imageBytes = null;
            BinaryReader reader = new BinaryReader(image.InputStream);
            imageBytes = reader.ReadBytes((int)image.ContentLength);
            return imageBytes;
        }
        public FileContentResult GetImg(int id)
        {
            Product product = db.Products.Find(id);

            var image = product.ImageUrl;

            return image != null ? new FileContentResult(image, "image/png") : null;
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        // GET: Product
        public ActionResult Product()
        {
            List<Product> ProductViewModels = new List<Product>();
            var products = db.Products;
            foreach (Product product in products)
            {
                /*
                var model = new Product
                {
                    Id = product.Id,
                    Name = product.Name,
                    Description = product.Description,
                    Price = product.Price
                };
                */

                ProductViewModels.Add(product);
            }

            if (TempData["myCart"] != null)
            {
                decimal price = 0M;
                var tempCard = TempData["myCart"] as List<Product>;
                foreach (Product p in tempCard)
                {
                    myCart.Add(p);
                    price += Convert.ToDecimal(p.Price);
                }
                TempData["myCart"] = myCart;
                ViewData["qty"] = myCart.Count;
                ViewData["price"] = Convert.ToDecimal(price).ToString("c");
            }
            else
            {
                ViewData["qty"] = "0";
                ViewData["price"] = "$0.00";
            }

            return View(ProductViewModels.ToList());
        }
        public ActionResult AddToCart(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            if (TempData["myCart"] != null)
            {
                var tempCard = TempData["myCart"] as List<Product>;
                foreach (Product p in tempCard)
                {
                    myCart.Add(p);
                }
            }
            myCart.Add(product);
            TempData["myCart"] = myCart;

            return RedirectToAction("Products", "Home");
        }
        public ActionResult Cart()
        {
            string _item_number = string.Empty;
            string _item_name = string.Empty;

            decimal _Total = 0M;
            List<Cart> _cart = new List<Cart>();
            if (TempData["myCart"] != null)
            {
                var tempCard = TempData["myCart"] as List<Product>;
                foreach (Product p in tempCard)
                {
                    var c = new Cart
                    {
                        ProductId = p.Id,
                        Name = p.Name,
                        Price = p.Price,
                        Qty = 1
                    };

                    _item_number += c.ProductId + Environment.NewLine;
                    _item_name += c.Name + Environment.NewLine;

                    _Total += Convert.ToDecimal(c.Qty * c.Price);


                    _cart.Add(c);
                }
            }
            ViewData["Qty"] = _cart.Count;
            ViewData["Total"] = _Total.ToString("c");
            ViewData["item_number"] = _item_number;
            ViewData["item_name"] = _item_name;

            return View(_cart.ToList());
        }
        #region shopping cart
        public ActionResult IPN()
        {

            var order = new Order(); // this is something I have defined in order to save the order in the database

            // Receive IPN request from PayPal and parse all the variables returned
            var formVals = new Dictionary<string, string>();
            formVals.Add("cmd", "_notify-synch"); //notify-synch_notify-validate
            formVals.Add("at", "this is a long token found in Buyers account"); // this has to be adjusted
            formVals.Add("tx", Request["tx"]);

            // if you want to use the PayPal sandbox change this from false to true
            string response = GetPayPalResponse(formVals, false);

            if (response.Contains("SUCCESS"))
            {
                string transactionID = GetPDTValue(response, "txn_id"); // txn_id //d
                string sAmountPaid = GetPDTValue(response, "mc_gross"); // d
                string deviceID = GetPDTValue(response, "custom"); // d
                string payerEmail = GetPDTValue(response, "payer_email"); // d
                string Item = GetPDTValue(response, "item_name");

                //validate the order
                Decimal amountPaid = 0;
                Decimal.TryParse(sAmountPaid, System.Globalization.NumberStyles.Number, System.Globalization.CultureInfo.InvariantCulture, out amountPaid);

                //if (amountPaid == 9)  // you might want to have a bigger than or equal to sign here!
                //{
                //    if (orders.Count(d => d.PayPalOrderRef == transactionID) < 1)
                //    {
                //        //if the transactionID is not found in the database, add it
                //        //then, add the additional features to the user account
                //    }
                //    else
                //    {
                //        //if we are here, the user must have already used the transaction ID for an account
                //        //you might want to show the details of the order, but do not upgrade it!
                //    }
                //    // take the information returned and store this into a subscription table
                //    // this is where you would update your database with the details of the tran

                //    //return View();

                //}
                //else
                //{
                //    // let fail - this is the IPN so there is no viewer
                //    // you may want to log something here
                //    order.Comments = "User did not pay the right ammount.";

                //    // since the user did not pay the right amount, we still want to log that for future reference.

                //    _db.Orders.Add(order); // order is your new Order
                //    _db.SaveChanges();
                //}

            }
            else
            {
                //error
            }
            return View();
        }

        string GetPayPalResponse(Dictionary<string, string> formVals, bool useSandbox)
        {

            string paypalUrl = useSandbox ? "https://www.sandbox.paypal.com/cgi-bin/webscr"
                : "https://www.paypal.com/cgi-bin/webscr";

            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(paypalUrl);

            // Set values for the request back
            req.Method = "POST";
            req.ContentType = "application/x-www-form-urlencoded";

            byte[] param = Request.BinaryRead(Request.ContentLength);
            string strRequest = Encoding.ASCII.GetString(param);

            StringBuilder sb = new StringBuilder();
            sb.Append(strRequest);

            foreach (string key in formVals.Keys)
            {
                sb.AppendFormat("&{0}={1}", key, formVals[key]);
            }
            strRequest += sb.ToString();
            req.ContentLength = strRequest.Length;

            //for proxy
            //WebProxy proxy = new WebProxy(new Uri("http://urlort#");
            //req.Proxy = proxy;
            //Send the request to PayPal and get the response
            string response = "";
            using (StreamWriter streamOut = new StreamWriter(req.GetRequestStream(), System.Text.Encoding.ASCII))
            {

                streamOut.Write(strRequest);
                streamOut.Close();
                using (StreamReader streamIn = new StreamReader(req.GetResponse().GetResponseStream()))
                {
                    response = streamIn.ReadToEnd();
                }
            }

            return response;
        }
        string GetPDTValue(string pdt, string key)
        {

            string[] keys = pdt.Split('\n');
            string thisVal = "";
            string thisKey = "";
            foreach (string s in keys)
            {
                string[] bits = s.Split('=');
                if (bits.Length > 1)
                {
                    thisVal = bits[1];
                    thisKey = bits[0];
                    if (thisKey.Equals(key, StringComparison.InvariantCultureIgnoreCase))
                        break;
                }
            }
            return thisVal;

        }
        #endregion
    }
}
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Umbraco.Web.Mvc;
using Umbraco7.Model;
using Umbraco7.Services;

namespace Umbraco7.Controllers
{
    public class CartController : SurfaceController
    {
        private List<Cart> carts { get; set; }
        IProducts _products;
        public CartController()
        {

            _products = new ProductService();
        }

        private bool setCookieCart(List<Cart> lstCart)
        {
            try
            {
                lstCart = lstCart.Where(x => x.Numbers > 0).ToList();
                var jsonCarts = JsonConvert.SerializeObject(lstCart);
                HttpCookie cartsCookie = new HttpCookie("carts");
                cartsCookie.Value = jsonCarts;
                cartsCookie.Expires = DateTime.Now.AddDays(30);
                cartsCookie.Domain = Request.Url.Host;
                Response.SetCookie(cartsCookie);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public List<Cart> lstCart
        {
            get
            {
                if (Request.Cookies["carts"] == null)
                    this.carts = new List<Cart>();
                else
                    this.carts = JsonConvert.DeserializeObject<List<Cart>>(Request.Cookies["carts"].Value);
                lstCart = this.carts;
                return carts;
            }
            set
            {
                this.carts = value;
            }
        }

        // GET: Cart
        [HttpGet]
        public JsonResult Cart()
        {
            return Json(new
            {
                success = true,
                data = lstCart,
                count=lstCart.Count
            });
        }

        [HttpPost]
        public JsonResult Cart(int id, int Numbers=1)
        {
            var listCart = lstCart;
                var Product = _products.GetProduct(id);
            if (Product != null)
            {
                //Find and update cart item if exits
                var cartOld = listCart.FirstOrDefault(x => x.Id == id);
                if (cartOld == null)
                {
                    listCart.Add(new Cart
                    {
                        FromDate = Product.FromDate,
                        Id = id,
                        Name = Product.Title,
                        Numbers = Numbers,
                        price = Product.Price,
                        toDate = Product.ToDate
                    });
                }
                else
                {
                    cartOld.price = Product.Price;
                    cartOld.Name = Product.Title;
                    cartOld.FromDate = Product.FromDate;
                    cartOld.toDate = Product.ToDate;
                    cartOld.Numbers += Numbers;
                }
               
                setCookieCart(listCart);
                return Json(new
                {
                    success = true,
                    message = "Add cart success",
                    data = listCart,
                    count = listCart.Count
                });
            }
            return Json(new
            {
                success = false,
                message = "Add cart Failed",
            });

        }

        [HttpDelete]
        public JsonResult Cart(int id)
        {
            var listCart = lstCart;
            var cart = listCart.FirstOrDefault(x => x.Id == id);
            if (cart != null)
            {
                listCart.Remove(cart);
                setCookieCart(listCart);
                return Json(new
                {
                    success = true,
                    message = "Delete successfull",
                    data = cart,
                    count = listCart.Count
                });

            }
            return Json(new
            {
                success = false,
                message = "Not found"
            });
        }

        [HttpPut]
        public JsonResult Cart(int id, Cart cart)
        {
            var listCart = lstCart;
            if (id != cart.Id)
                return Json(new
                {
                    success = false,
                    message = "Not match ID"
                });

            var cartOld = listCart.FirstOrDefault(x => x.Id == id);
            if (cartOld != null)
            {
                cartOld.Numbers=cart.Numbers;
                setCookieCart(listCart);
                return Json(new
                {
                    success = true,
                    message = "Update success",
                    data = cartOld,
                    count = cart.Numbers==0?listCart.Count-1:lstCart.Count
                });
            }
            return Json(new
            {
                success = false,
                message = "Not Found Cart item"
            });
        }
    }
}
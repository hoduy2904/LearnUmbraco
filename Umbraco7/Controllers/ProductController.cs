using System.Web.Http;
using System.Web.Mvc;
using Umbraco.Web.Mvc;
using Umbraco7.Services;

namespace Umbraco7.Controllers
{
    public class ProductController : SurfaceController
    {
        IProducts products;
        public ProductController()
        {
            products = new ProductService();
        }

        public JsonResult getProducts([FromUri] int[] ids)
        {
            return Json(new
            {
                success = true,
                data = products.GetProducts(ids)
            },JsonRequestBehavior.AllowGet);
        }

    }
}




using System.Collections.Generic;
using umbraco.cms.businesslogic;
using umbraco.cms.businesslogic.web;
using Umbraco.Core.Models;
using Umbraco.Core.Models.EntityBase;
using Umbraco.Core.Models.PublishedContent;
using Umbraco7.Model;

namespace Umbraco7.Services
{
    public interface IProducts
    {
        List<Product> getAllProduct();
        Product GetProduct(int id);
        List<Product> GetProducts(int[] ids);
    }
}

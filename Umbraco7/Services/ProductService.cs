
using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using umbraco.cms.businesslogic.web;
using Umbraco.Core;
using Umbraco.Core.Models;
using Umbraco7.Model;

namespace Umbraco7.Services
{
    public class ProductService : IProducts
    {
        public List<Product> getAllProduct()
        {
            int idByAlias = DocumentType.GetByAlias("productPage").Id;
          var lstValue= ApplicationContext.Current.Services.ContentService.GetChildren(idByAlias);
            List<Product> products = new List<Product>();
            foreach(var item in lstValue)
            {
                Product product = new Product
                {
                    Id = item.Id,
                    FromDate = item.GetValue<DateTime>("fromDate"),
                    ToDate = item.GetValue<DateTime>("toDate"),
                    Price = item.GetValue<Double>("price"),
                    Title = item.GetValue<string>("title")
                };
                if (product != null)
                {
                    products.Add(product);
                }
            }
            return products;
        }

        public Product GetProduct(int id)
        {
           var value= ApplicationContext.Current.Services.ContentService.GetById(id);
            if (value != null)
            {
                Product product = new Product
                {
                    Id = id,
                    FromDate = value.GetValue<DateTime>("fromDate"),
                    ToDate = value.GetValue<DateTime>("toDate"),
                    Price = value.GetValue<Double>("price"),
                    Title = value.GetValue<string>("title")
                };
                return product;
            }
            return null;
        }

        public List<Product> GetProducts(int[] ids)
        {
            List<Product> lstProduct = new List<Product>();
            if (ids.Length == 0)
                return lstProduct;
            foreach (var id in ids)
            {
                var value = ApplicationContext.Current.Services.ContentService.GetById(id);
                if (value != null)
                {
                    Product product = new Product
                    {
                        Id = id,
                        FromDate = value.GetValue<DateTime>("fromDate"),
                        ToDate = value.GetValue<DateTime>("toDate"),
                        Price = value.GetValue<Double>("price"),
                        Title = value.GetValue<string>("title")
                    };
                    lstProduct.Add(product);
                }
            }
            return lstProduct;
        }

    }
}
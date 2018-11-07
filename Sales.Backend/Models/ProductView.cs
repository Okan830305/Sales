namespace Sales.Backend.Models
{
    using Sales.Common.Models;
    using System.Web;
    public class ProductView : Products
    {

        public HttpPostedFileBase ImageFile { get; set; }
    }
}
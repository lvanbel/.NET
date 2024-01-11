using System.ComponentModel;
using Examen.Models;

namespace Examen.ViewModels
{
    public class ProductModel
    {
        
        private readonly Product _monProduct;

        public Product MonProduct
        {
            get { return _monProduct; }
        }

        public ProductModel(Product current)
        {
            this._monProduct = current;
        }

        public String ProductId
        {
            get { return _monProduct.ProductId.ToString(); }
        }

        public String ProductName
        {
            get { return _monProduct.ProductName; }
        }

        public String CategoryName
        {
            get { return _monProduct.Category.CategoryName; }
        }

        public String SupplierName
        {
            get { return _monProduct.Supplier.CompanyName; }
        }
    }
}

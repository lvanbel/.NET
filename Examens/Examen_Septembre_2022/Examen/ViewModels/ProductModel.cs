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
            set { _monProduct.ProductId = _monProduct.ProductId; }
        }

        public String ProductName
        {
            get { return _monProduct.ProductName; }
            set { _monProduct.ProductName = value; }
        }

        public String SupplierContactName
        {
            get { return _monProduct.Supplier.ContactName; }
            set { _monProduct.Supplier.ContactName = _monProduct.Supplier.ContactName; }
        }

        public String QuantityPerUnit
        {
            get { return _monProduct.QuantityPerUnit; }
            set { _monProduct.QuantityPerUnit = value; }
        }
    }
}

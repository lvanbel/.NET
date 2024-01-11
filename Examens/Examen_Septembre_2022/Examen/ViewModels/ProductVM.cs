using System.Collections.ObjectModel;
using System.ComponentModel;
using Examen.Models;

namespace Examen.ViewModels
{
    public class ProductVM : INotifyPropertyChanged
    {
        private NorthwindContext dc = new NorthwindContext();
    
        // Property changed standard handling
        public event PropertyChangedEventHandler PropertyChanged; // La view s'enregistera automatiquement sur cet event
        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName)); // On notifie que la propriété a changé
            }
        }

        private ProductModel _selectedProduct;
        private ObservableCollection<ProductModel> _ProductsList;

        private List<String> _ProductsSalesList;

        private DelegateCommand _majCommand;

        public ObservableCollection<ProductModel> ProductsList
        {
            get
            {
                if (_ProductsList == null)
                {
                    _ProductsList = loadProducts();
                }

                return _ProductsList;
            }
        }

        public List<String> ProductsSalesList
        {
            get
            {
                if (_ProductsSalesList == null)
                {
                    _ProductsSalesList = loadProductsSalesList();
                }

                return _ProductsSalesList;
            }
        }
  
        private ObservableCollection<ProductModel> loadProducts()
        {
            ObservableCollection<ProductModel> localCollection = new ObservableCollection<ProductModel>();
            foreach (var item in dc.Products)
            {
                localCollection.Add(new ProductModel(item));
            }
            
            return localCollection;
        }

        private List<string> loadProductsSalesList()
        {
            List<string> localCollection = new List<string>();

            var productSales = dc.OrderDetails
                .GroupBy(od => od.ProductId)
                .Select(g => new
                {
                    ProductId = g.Key,
                    TotalSales = g.Sum(od => od.UnitPrice * od.Quantity)
                })
                .OrderBy(item => item.ProductId); // Tri par l'ID du produit

            foreach (var item in productSales)
            {
                string salesInfo = $"ProductID: {item.ProductId},        Total Sales: {item.TotalSales:C}";
                localCollection.Add(salesInfo);
            }

            return localCollection;
        }

        public ProductModel SelectedProduct
        {
            get { return _selectedProduct; }
            set { _selectedProduct = value; } 
        }

        public DelegateCommand MajCommand
        {
            get { return _majCommand = _majCommand ?? new DelegateCommand(MajProduct); }
        }

        private void MajProduct()
        {
            dc.Products.Find(SelectedProduct.MonProduct.ProductId).ProductName = SelectedProduct.ProductName;
            dc.Products.Find(SelectedProduct.MonProduct.ProductId).QuantityPerUnit = SelectedProduct.QuantityPerUnit;
            dc.SaveChanges();

            _ProductsList = loadProducts();
        }
    }
}

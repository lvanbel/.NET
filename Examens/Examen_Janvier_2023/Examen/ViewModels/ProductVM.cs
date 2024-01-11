using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
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

        private List<dynamic> _ProductsSoldByCountry;

        private DelegateCommand _discontinueCommand;

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

        public List<dynamic> ProductsSoldByCountry
        {
            get
            {
                if (_ProductsSoldByCountry == null)
                {
                    _ProductsSoldByCountry = loadProductsSoldByCountry();
                }

                return _ProductsSoldByCountry;
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

        private List<dynamic> loadProductsSoldByCountry()
        {
            List<dynamic> localCollection = dc.OrderDetails
                .Where(od => od.Quantity > 0) // Filtrer les lignes de commande avec au moins une vente
                .Select(od => new { od.Product.Supplier.Country, od.Product.ProductName })
                .GroupBy(p => p.Country)
                .Select(g => new
                {
                    Country = g.Key,
                    ProductCount = g.Select(p => p.ProductName).Distinct().Count()
                })
                .OrderByDescending(p => p.ProductCount)
                .ToList<dynamic>();

            return localCollection;
        }


        public ProductModel SelectedProduct
        {
            get { return _selectedProduct; }
            set { _selectedProduct = value; } 
        }

        public DelegateCommand DiscontinueCommand
        {
            get { return _discontinueCommand = _discontinueCommand ?? new DelegateCommand(DiscontinueProduct); }
        }

        private void DiscontinueProduct()
        {      
            dc.Products.Find(SelectedProduct.MonProduct.ProductId).Discontinued = true;
            dc.SaveChanges();
            ProductsList.Remove(SelectedProduct);
        }
    }
}

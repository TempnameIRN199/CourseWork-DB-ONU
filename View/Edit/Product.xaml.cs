namespace CourseWork_DB_ONU.View.Edit
{
    using CourseWork_DB_ONU.Services.Product;
    using System.Windows;
    using ProductModel = Data.Models.Product;
    public partial class Product : Window
    {
        private ProductModel _product;
        private readonly IProductService _productService;
        public Product(ProductModel product, IProductService productService)
        {
            InitializeComponent();
            _product = product;
            _productService = productService;
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (_productService == null)
            {
                MessageBox.Show("Product service is not initialized.");
                return;
            }
            try
            {
                _txtName.Text = _product.Name;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading product: {ex.Message}");
            }
        }

        private void _btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (_txtName.Text == null)
            {
                MessageBox.Show("Product is not initialized.");
                return;
            }
            if (_productService == null)
            {
                MessageBox.Show("Product service is not initialized.");
                return;
            }
            try
            {
                _product.Name = _txtName.Text.Trim();
                if (string.IsNullOrEmpty(_product.Name))
                {
                    MessageBox.Show("Product name cannot be empty.");
                    return;
                }
                _productService.Edit(_product);
                MessageBox.Show("Product added successfully.");
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding product: {ex.Message}");
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Close();
        }

    }
}

namespace CourseWork_DB_ONU.View.Add
{
    using CourseWork_DB_ONU.Services.Product;
    using System.Windows;
    public partial class Product : Window
    {
        private readonly IProductService _productService;
        public Product(IProductService productService)
        {
            InitializeComponent();
            _productService = productService;
        }
        private void _btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (_txtName.Text == null)
            {
                MessageBox.Show("Product name cannot be empty.");
                return;
            }
            string productName = _txtName.Text.Trim();
            _productService.Add(new Data.Models.Product
            {
                Name = productName
            });
            MessageBox.Show("Product added successfully.");
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Close();
        }

    }
}

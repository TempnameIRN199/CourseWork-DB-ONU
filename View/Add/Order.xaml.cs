namespace CourseWork_DB_ONU.View.Add
{
    using CourseWork_DB_ONU.Services.Order;
    using CourseWork_DB_ONU.Services.Outlet;
    using CourseWork_DB_ONU.Services.Product;
    using CourseWork_DB_ONU.Services.Supplier;
    using System.Windows;
    using System.Windows.Controls;
    public partial class Order : Window
    {
        private readonly IOrderService _orderService;
        private readonly IOutletService _outletService;
        private readonly ISupplierService _supplierService;
        private readonly IProductService _productService;
        public Order(IOrderService orderService,
            IOutletService outletService, ISupplierService supplierService,
            IProductService productService)
        {
            InitializeComponent();
            _orderService = orderService;
            _outletService = outletService;
            _supplierService = supplierService;
            _productService = productService;
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _cbOutlet.ItemsSource = _outletService.GetAll();
            _cbOutlet.DisplayMemberPath = "Name";
            _cbOutlet.SelectedValuePath = "Id";
            _cbSupplier.ItemsSource = _supplierService.GetAll();
            _cbSupplier.DisplayMemberPath = "Name";
            _cbSupplier.SelectedValuePath = "Id";
            _cbProduct.ItemsSource = _productService.GetAll();
            _cbProduct.DisplayMemberPath = "Name";
            _cbProduct.SelectedValuePath = "Id";
        }
        private void _txtOutlet_TextChanged(object sender, TextChangedEventArgs e)
        {
            _cbOutlet.DisplayMemberPath = "Name";
            _cbOutlet.SelectedValuePath = "Id";
            string? filter = _txtOutlet.Text?.Trim();
            _cbOutlet.ItemsSource =
                _outletService.SearchByName(filter);
        }
        private void _txtSupplier_TextChanged(object sender, TextChangedEventArgs e)
        {
            _cbSupplier.DisplayMemberPath = "Name";
            _cbSupplier.SelectedValuePath = "Id";
            string? filter = _txtSupplier.Text?.Trim();
            _cbSupplier.ItemsSource =
                _supplierService.SearchByName(filter);
        }
        private void _txtProduct_TextChanged(object sender, TextChangedEventArgs e)
        {
            _cbProduct.DisplayMemberPath = "Name";
            _cbProduct.SelectedValuePath = "Id";
            string? filter = _txtProduct.Text?.Trim();
            _cbProduct.ItemsSource =
                _productService.SearchByName(filter);
        }
        private void _btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (_dpDate.SelectedDate == null ||
                _cbOutlet.SelectedValue == null ||
                _cbSupplier.SelectedValue == null ||
                _cbProduct.SelectedValue == null ||
                string.IsNullOrWhiteSpace(_txtAmount.Text) ||
                string.IsNullOrWhiteSpace(_txtPrice.Text))
            {
                MessageBox.Show("Please fill in all fields.");
                return;
            }
            try
            {
                Data.Models.Order order = new Data.Models.Order
                {
                    Date = DateOnly.FromDateTime(_dpDate.SelectedDate.Value),
                    OutletId = (int)_cbOutlet.SelectedValue,
                    SupplierId = (int)_cbSupplier.SelectedValue,
                    ProductId = (int)_cbProduct.SelectedValue,
                    Amount = int.Parse(_txtAmount.Text),
                    Price = double.Parse(_txtPrice.Text)
                };
                _orderService.Add(order);
                MessageBox.Show("Order added successfully.");
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding order: {ex.Message}");
            }
        }
        private void Window_Closed(object sender, EventArgs e)
        {
            Close();
        }
    }
}

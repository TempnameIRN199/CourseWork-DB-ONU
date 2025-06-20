namespace CourseWork_DB_ONU
{
    using CourseWork_DB_ONU.Data.Models;
    using CourseWork_DB_ONU.Services.EnumService;
    using CourseWork_DB_ONU.Services.Order;
    using CourseWork_DB_ONU.Services.Organization;
    using CourseWork_DB_ONU.Services.Outlet;
    using CourseWork_DB_ONU.Services.Product;
    using CourseWork_DB_ONU.Services.Seller;
    using CourseWork_DB_ONU.Services.Supplier;
    using CourseWork_DB_ONU.ViewModels;
    using System.Windows;
    using System.Windows.Controls;
    using AddOrderView = View.Add.Order;
    using AddOrganizationView = View.Add.Organization;
    using AddOutletView = View.Add.Outlet;
    using AddProductView = View.Add.Product;
    using AddSellerView = View.Add.Seller;
    using AddSupplierView = View.Add.Supplier;
    using EditOrderView = View.Edit.Order;
    using EditOrganizationView = View.Edit.Organization;
    using EditOutletView = View.Edit.Outlet;
    using EditProductView = View.Edit.Product;
    using EditSellerView = View.Edit.Seller;
    using EditSupplierView = View.Edit.Supplier;

    public partial class MainWindow : Window
    {
        private readonly IProductService _productService;
        private readonly ISupplierService _supplierService;
        private readonly IOrganizationService _organizationService;
        private readonly IOutletService _outletService;
        private readonly ISellerService _sellerService;
        private readonly IOrderService _orderService;
        private readonly IEnumService _enumService;

        public MainWindow(IProductService productService, ISupplierService supplierService,
            IOrganizationService organizationService, IOutletService outletService,
            ISellerService sellerService, IOrderService orderService, IEnumService enumService)
        {
            InitializeComponent();

            _productService = productService;
            _supplierService = supplierService;
            _organizationService = organizationService;
            _outletService = outletService;
            _sellerService = sellerService;
            _orderService = orderService;
            _enumService = enumService;
        }

        #region Loaded
        private void _ListViewProducts_Loaded(object sender, RoutedEventArgs e)
        {
            if (_productService == null)
            {
                MessageBox.Show("Product service is not initialized.");
                return;
            }
            try
            {
                _ListViewProducts.ItemsSource = null;
                List<Product> products = _productService.GetAll();
                _ListViewProducts.ItemsSource = products;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading products: {ex.Message}");
            }
        }
        private void _ListViewSupplier_Loaded(object sender, RoutedEventArgs e)
        {
            if (_supplierService == null)
            {
                MessageBox.Show("Supplier service is not initialized.");
                return;
            }
            try
            {
                _ListViewSupplier.ItemsSource = null;
                List<Supplier> suppliers = _supplierService.GetAll();
                List<SupplierViewModel> supplierVM = _supplierService.ConvertToViewModel(suppliers);
                _ListViewSupplier.ItemsSource = supplierVM;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading suppliers: {ex.Message}");
            }
        }
        private void _ListViewOrganization_Loaded(object sender, RoutedEventArgs e)
        {
            if (_organizationService == null)
            {
                MessageBox.Show("Organizator service is not initialized.");
                return;
            }
            try
            {
                _ListViewOrganization.ItemsSource = null;
                List<Organization> organizations = _organizationService.GetAll();
                _ListViewOrganization.ItemsSource = organizations;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading suppliers: {ex.Message}");
            }
        }
        private void _ListViewOutlet_Loaded(object sender, RoutedEventArgs e)
        {
            if (_outletService == null)
            {
                MessageBox.Show("Outlet service is not initialized.");
                return;
            }
            try
            {
                _ListViewOutlet.ItemsSource = null;
                List<Outlet> outlets = _outletService.GetAll();
                List<OutletViewModel> outletVM = _outletService.ConvertToViewModel(outlets);
                _ListViewOutlet.ItemsSource = outletVM;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading outlets: {ex.Message}");
            }
        }
        private void _ListViewSeller_Loaded(object sender, RoutedEventArgs e)
        {
            if (_sellerService == null)
            {
                MessageBox.Show("Seller service is not initialized.");
                return;
            }
            try
            {
                _ListViewSeller.ItemsSource = null;
                List<Seller> sellers = _sellerService.GetAll();
                List<SellerViewModel> sellerVM = _sellerService.ConvertToViewModel(sellers);
                _ListViewSeller.ItemsSource = sellerVM;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading outlets: {ex.Message}");
            }
        }
        private void _ListViewOrder_Loaded(object sender, RoutedEventArgs e)
        {
            if (_orderService == null)
            {
                MessageBox.Show("Order service is not initialized.");
                return;
            }
            try
            {
                _ListViewOrder.ItemsSource = null;
                List<Order> orders = _orderService.GetAll();
                List<OrderViewModel> orderVM = _orderService.ConvertToViewModel(orders);
                _ListViewOrder.ItemsSource = orderVM;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading orders: {ex.Message}");
            }
        }
        #endregion

        #region Button Add
        private void _ButtonAddProduct_Click(object sender, RoutedEventArgs e)
        {
            AddProductView productView = new AddProductView(_productService);
            productView.ShowDialog();
            _ListViewProducts_Loaded(sender, e);
        }
        private void _ButtonAddSupplier_Click(object sender, RoutedEventArgs e)
        {
            AddSupplierView supplierView = new AddSupplierView(_supplierService, _enumService);
            supplierView.ShowDialog();
            _ListViewSupplier_Loaded(sender, e);
        }
        private void _ButtonAddOrganization_Click(object sender, RoutedEventArgs e)
        {
            AddOrganizationView organizationView = new AddOrganizationView(_organizationService);
            organizationView.ShowDialog();
            _ListViewOrganization_Loaded(sender, e);
        }
        private void _ButtonAddOutlet_Click(object sender, RoutedEventArgs e)
        {
            AddOutletView outletView = new AddOutletView(_outletService, _organizationService, _enumService);
            outletView.ShowDialog();
            _ListViewOutlet_Loaded(sender, e);
        }
        private void _ButtonAddSeller_Click(object sender, RoutedEventArgs e)
        {
            AddSellerView sellerView = new AddSellerView(_sellerService, _outletService, _enumService);
            sellerView.ShowDialog();
            _ListViewSeller_Loaded(sender, e);
        }
        private void _ButtonAddOrder_Click(object sender, RoutedEventArgs e)
        {
            AddOrderView orderView = new AddOrderView(_orderService, _outletService,
                _supplierService, _productService);
            orderView.ShowDialog();
            _ListViewOrder_Loaded(sender, e);
        }
        #endregion

        #region Button Edit
        private void _ButtonEditProduct_Click(object sender, RoutedEventArgs e)
        {
            if (_ListViewProducts.SelectedItem is Product selectedProduct)
            {
                EditProductView productView = new EditProductView(selectedProduct, _productService);
                productView.ShowDialog();
                _ListViewProducts_Loaded(sender, e);
            }
            else
            {
                MessageBox.Show("Please select a product to edit.");
            }
        }
        private void _ButtonEditSupplier_Click(object sender, RoutedEventArgs e)
        {
            if (_ListViewSupplier.SelectedItem is SupplierViewModel selectedSupplier)
            {
                EditSupplierView supplierView = new EditSupplierView(selectedSupplier, _supplierService, _enumService);
                supplierView.ShowDialog();
                _ListViewSupplier_Loaded(sender, e);
            }
            else
            {
                MessageBox.Show("Please select a supplier to edit.");
            }
        }
        private void _ButtonEditOrganization_Click(object sender, RoutedEventArgs e)
        {
            if (_ListViewOrganization.SelectedItem is Organization selectedOrganization)
            {
                EditOrganizationView organizationView = new EditOrganizationView(selectedOrganization, _organizationService);
                organizationView.ShowDialog();
                _ListViewOrganization_Loaded(sender, e);
            }
            else
            {
                MessageBox.Show("Please select an organization to edit.");
            }
        }
        private void _ButtonEditOutlet_Click(object sender, RoutedEventArgs e)
        {
            if (_ListViewOutlet.SelectedItem is OutletViewModel selectedOutlet)
            {
                EditOutletView outletView = new EditOutletView(selectedOutlet, _outletService, _organizationService, _enumService);
                outletView.ShowDialog();
                _ListViewOutlet_Loaded(sender, e);
            }
            else
            {
                MessageBox.Show("Please select an outlet to edit.");
            }
        }
        private void _ButtonEditSeller_Click(object sender, RoutedEventArgs e)
        {
            if (_ListViewSeller.SelectedItem is SellerViewModel selectedSeller)
            {
                EditSellerView sellerView = new EditSellerView(selectedSeller, _sellerService,
                    _outletService, _enumService);
                sellerView.ShowDialog();
                _ListViewSeller_Loaded(sender, e);
            }
            else
            {
                MessageBox.Show("Please select a seller to edit.");
            }
        }
        private void _ButtonEditOrder_Click(object sender, RoutedEventArgs e)
        {
            if (_ListViewOrder.SelectedItem is OrderViewModel selectedOrder)
            {
                EditOrderView orderView = new EditOrderView(selectedOrder,
                    _orderService, _outletService, _supplierService,
                    _productService);
                orderView.ShowDialog();
                _ListViewOrder_Loaded(sender, e);
            }
            else
            {
                MessageBox.Show("Please select an order to edit.");
            }
        }
        #endregion

        #region Button Delete
        private void _ButtonDeleteProduct_Click(object sender, RoutedEventArgs e)
        {
            if (_ListViewProducts.SelectedItem is Product selectedProduct)
            {
                try
                {
                    _productService.Delete(selectedProduct.Id);
                    MessageBox.Show("Product deleted successfully.");
                    _ListViewProducts_Loaded(sender, e);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting product: {ex.Message}");
                }
            }
            else
            {
                MessageBox.Show("Please select a product to delete.");
            }
        }
        private void _ButtonDeleteSupplier_Click(object sender, RoutedEventArgs e)
        {
            if (_ListViewSupplier.SelectedItem is SupplierViewModel selectedSupplier)
            {
                try
                {
                    _supplierService.Delete(selectedSupplier.Id);
                    MessageBox.Show("Supplier deleted successfully.");
                    _ListViewSupplier_Loaded(sender, e);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting supplier: {ex.Message}");
                }
            }
            else
            {
                MessageBox.Show("Please select a supplier to delete.");
            }
        }
        private void _ButtonDeleteOrganization_Click(object sender, RoutedEventArgs e)
        {
            if (_ListViewOrganization.SelectedItem is Organization selectedOrganization)
            {
                try
                {
                    _organizationService.Delete(selectedOrganization.Id);
                    MessageBox.Show("Organization deleted successfully.");
                    _ListViewOrganization_Loaded(sender, e);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting organization: {ex.Message}");
                }
            }
            else
            {
                MessageBox.Show("Please select an organization to delete.");
            }
        }
        private void _ButtonDeleteOutlet_Click(object sender, RoutedEventArgs e)
        {
            if (_ListViewOutlet.SelectedItem is OutletViewModel selectedOutlet)
            {
                try
                {
                    _outletService.Delete(selectedOutlet.Id);
                    MessageBox.Show("Outlet deleted successfully.");
                    _ListViewOutlet_Loaded(sender, e);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting outlet: {ex.Message}");
                }
            }
            else
            {
                MessageBox.Show("Please select an outlet to delete.");
            }
        }
        private void _ButtonDeleteSeller_Click(object sender, RoutedEventArgs e)
        {
            if (_ListViewSeller.SelectedItem is SellerViewModel selectedSeller)
            {
                try
                {
                    _sellerService.Delete(selectedSeller.Id);
                    MessageBox.Show("Seller deleted successfully.");
                    _ListViewSeller_Loaded(sender, e);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting seller: {ex.Message}");
                }
            }
            else
            {
                MessageBox.Show("Please select a seller to delete.");
            }
        }
        private void _ButtonDeleteOrder_Click(object sender, RoutedEventArgs e)
        {
            if (_ListViewOrder.SelectedItem is OrderViewModel selectedOrder)
            {
                try
                {
                    _orderService.Delete(selectedOrder.Id);
                    MessageBox.Show("Order deleted successfully.");
                    _ListViewOrder_Loaded(sender, e);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting order: {ex.Message}");
                }
            }
            else
            {
                MessageBox.Show("Please select an order to delete.");
            }
        }
        #endregion

        #region Button Update
        private void _ButtonUpdateProduct_Click(object sender, RoutedEventArgs e)
        {
            _ListViewProducts_Loaded(sender, e);
        }
        private void _ButtonUpdateSupplier_Click(object sender, RoutedEventArgs e)
        {
            _ListViewSupplier_Loaded(sender, e);
        }
        private void _ButtonUpdateOrganization_Click(object sender, RoutedEventArgs e)
        {
            _ListViewOrganization_Loaded(sender, e);
        }
        private void _ButtonUpdateOutlet_Click(object sender, RoutedEventArgs e)
        {
            _ListViewOutlet_Loaded(sender, e);
        }
        private void _ButtonUpdateSeller_Click(object sender, RoutedEventArgs e)
        {
            _ListViewSeller_Loaded(sender, e);
        }
        private void _ButtonUpdateOrder_Click(object sender, RoutedEventArgs e)
        {
            _ListViewOrder_Loaded(sender, e);
        }
        #endregion

        #region TextBox Search
        private void _TextBoxSearchProduct_TextChanged(object sender, TextChangedEventArgs e)
        {
            _TextBoxSearchProduct.Focus();
            string searchText = _TextBoxSearchProduct.Text.Trim();
            if (string.IsNullOrWhiteSpace(searchText))
            {
                _ListViewProducts_Loaded(sender, e);
            }
            else
            {
                try
                {
                    List<Product> products = _productService.SearchByName(searchText);
                    _ListViewProducts.ItemsSource = products;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error searching products: {ex.Message}");
                }
            }
        }
        private void _TextBoxSearchSupplier_TextChanged(object sender, TextChangedEventArgs e)
        {
            string? name = _TextBoxSearchSupplierByName.Text?.Trim();
            string? type = _TextBoxSearchSupplierByType.Text?.Trim();
            string? country = _TextBoxSearchSupplierByCountry.Text?.Trim();
            string? city = _TextBoxSearchSupplierByCity.Text?.Trim();
            string? address = _TextBoxSearchSupplierByAddress.Text?.Trim();

            var filtered = _supplierService.SearchSuppliers(name, type, country, city, address);
            List<SupplierViewModel> supplierVM = _supplierService.ConvertToViewModel(filtered);
            _ListViewSupplier.ItemsSource = supplierVM;
        }
        private void _TextBoxSearchOrganization_TextChanged(object sender, TextChangedEventArgs e)
        {
            string? name = _TextBoxSearchOrganizationByName.Text?.Trim();
            string? address = _TextBoxSearchOrganizationByAddress.Text?.Trim();
            string? director = _TextBoxSearchOrganizationByFullNameDirector.Text?.Trim();
            string? taxNumber = _TextBoxSearchOrganizationByTaxNumber.Text?.Trim();

            var filtered = _organizationService.SearchOrganizations(name, address, director, taxNumber);
            _ListViewOrganization.ItemsSource = filtered;
        }
        private void _TextBoxSearchOutlet_TextChanged(object sender, TextChangedEventArgs e)
        {
            string? name = _TextBoxSearchOutletByName.Text?.Trim();
            string? type = _TextBoxSearchOutletByType.Text?.Trim();
            string? organization = _TextBoxSearchOutletByOrganization.Text?.Trim();
            string? address = _TextBoxSearchOutletByAddress.Text?.Trim();
            string? head = _TextBoxSearchOutletByFullNameHeadOf.Text?.Trim();

            var filtered = _outletService.SearchOutlets(name, type, organization,
                address, head);
            var viewModels = _outletService.ConvertToViewModel(filtered);
            _ListViewOutlet.ItemsSource = viewModels;
        }
        private void _TextBoxSearchSeller_TextChanged(object sender, TextChangedEventArgs e)
        {
            string? surname = _TextBoxSearchSellerBySurname.Text?.Trim();
            string? name = _TextBoxSearchSellerByName.Text?.Trim();
            string? patronymic = _TextBoxSearchSellerByPatronymic.Text?.Trim();
            string? outletName = _TextBoxSearchSellerByOutlet.Text?.Trim();
            string? position = _TextBoxSearchSellerByPosition.Text?.Trim();
            string? sex = _TextBoxSearchSellerBySex.Text?.Trim();
            string? address = _TextBoxSearchSellerByAddress.Text?.Trim();
            string? residence = _TextBoxSearchSellerByResidence.Text?.Trim();

            var filtered = _sellerService.SearchSellers(
                surname, name, patronymic, outletName, position, sex, address, residence);

            var viewModels = _sellerService.ConvertToViewModel(filtered);
            _ListViewSeller.ItemsSource = viewModels;
        }
        private void _TextBoxSearchOrder_TextChanged(object sender, TextChangedEventArgs e)
        {
            string? date = _TextBoxSearchOrderByDate.Text?.Trim();
            string? outlet = _TextBoxSearchOrderByOutlet.Text?.Trim();
            string? supplier = _TextBoxSearchOrderBySupplier.Text?.Trim();
            string? product = _TextBoxSearchOrderByProduct.Text?.Trim();
            string? amount = _TextBoxSearchOrderByAmount.Text?.Trim();
            string? price = _TextBoxSearchOrderByPrice.Text?.Trim();

            var filtered = _orderService.SearchOrders(date, outlet, supplier, product, amount, price);
            var viewModels = _orderService.ConvertToViewModel(filtered);
            _ListViewOrder.ItemsSource = viewModels;
        }
        #endregion

    }
}
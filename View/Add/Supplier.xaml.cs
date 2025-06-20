namespace CourseWork_DB_ONU.View.Add
{
    using CourseWork_DB_ONU.Data.Models.Enum;
    using CourseWork_DB_ONU.Services.EnumService;
    using CourseWork_DB_ONU.Services.Supplier;
    using System.Windows;
    using System.Windows.Controls;
    public partial class Supplier : Window
    {
        private readonly ISupplierService _supplierService;
        private readonly IEnumService _enumService;
        public Supplier(ISupplierService supplierService,
            IEnumService enumService)
        {
            InitializeComponent();
            _supplierService = supplierService;
            _enumService = enumService;
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _cbType.DisplayMemberPath = "Description";
            _cbType.SelectedValuePath = "Value";
            _cbType.ItemsSource = _enumService.GetEnumOptions<SupplierType>();
        }
        private void _txtType_TextChanged(object sender, TextChangedEventArgs e)
        {
            _cbType.DisplayMemberPath = "Description";
            _cbType.SelectedValuePath = "Value";

            string? filter = _txtType.Text?.Trim();

            _cbType.ItemsSource = _enumService.GetEnumOptions<SupplierType>(filter);
        }
        private void _btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(_txtName.Text) ||
                string.IsNullOrWhiteSpace(_txtCountry.Text) ||
                string.IsNullOrWhiteSpace(_txtCity.Text) ||
                string.IsNullOrWhiteSpace(_txtAddress.Text))
            {
                MessageBox.Show("Please fill in all fields.");
                return;
            }
            if (_supplierService == null)
            {
                MessageBox.Show("Supplier service is not initialized.");
                return;
            }
            try
            {
                SupplierType? selectedType = (SupplierType?)_cbType.SelectedValue;
                if (selectedType == null)
                {
                    MessageBox.Show("Please select a supplier type.");
                    return;
                }
                Data.Models.Supplier supplier = new Data.Models.Supplier
                {
                    Name = _txtName.Text.Trim(),
                    Type = selectedType.Value,
                    Country = _txtCountry.Text.Trim(),
                    City = _txtCity.Text.Trim(),
                    Address = _txtAddress.Text.Trim()
                };
                _supplierService.Add(supplier);
                MessageBox.Show("Supplier added successfully.");
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding supplier: {ex.Message}");
            }
        }
        private void Window_Closed(object sender, EventArgs e)
        {
            Close();
        }
    }
}

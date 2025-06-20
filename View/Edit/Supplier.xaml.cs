using CourseWork_DB_ONU.Data.Models.Enum;
using CourseWork_DB_ONU.Services.EnumService;
using CourseWork_DB_ONU.Services.Supplier;
using CourseWork_DB_ONU.ViewModels;
using System.Windows;
using SupplierModel = CourseWork_DB_ONU.Data.Models.Supplier;

namespace CourseWork_DB_ONU.View.Edit
{
    public partial class Supplier : Window
    {
        private SupplierViewModel _supplier;
        private readonly ISupplierService _supplierService;
        private readonly IEnumService _enumService;
        public Supplier(SupplierViewModel supplier, ISupplierService supplierService,
            IEnumService enumService)
        {
            InitializeComponent();
            _supplier = supplier;
            _supplierService = supplierService;
            _enumService = enumService;
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (_supplier == null)
            {
                MessageBox.Show("Supplier data is not available.");
                return;
            }
            try
            {
                _cbType.DisplayMemberPath = "Description";
                _cbType.SelectedValuePath = "Value";
                _cbType.ItemsSource = _enumService.GetEnumOptions<SupplierType>();

                _txtName.Text = _supplier.Name;
                _cbType.SelectedValue = _supplier.Type;
                _txtCountry.Text = _supplier.Country;
                _txtCity.Text = _supplier.City;
                _txtAddress.Text = _supplier.Address;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading supplier: {ex.Message}");
            }
        }
        private void _txtType_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            _cbType.DisplayMemberPath = "Description";
            _cbType.SelectedValuePath = "Value";

            string? filter = _txtType.Text?.Trim();

            _cbType.ItemsSource = _enumService.GetEnumOptions<SupplierType>(filter);
        }
        private void _btnEdit_Click(object sender, RoutedEventArgs e)
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
                _supplier.Name = _txtName.Text.Trim();
                _supplier.Type = (SupplierType)_cbType.SelectedValue;
                _supplier.Country = _txtCountry.Text.Trim();
                _supplier.City = _txtCity.Text.Trim();
                _supplier.Address = _txtAddress.Text.Trim();
                _supplierService.Edit(new SupplierModel
                {
                    Id = _supplier.Id,
                    Name = _supplier.Name,
                    Type = _supplier.Type,
                    Country = _supplier.Country,
                    City = _supplier.City,
                    Address = _supplier.Address
                });
                MessageBox.Show("Supplier updated successfully.");
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating supplier: {ex.Message}");
            }
        }
        private void Window_Closed(object sender, EventArgs e)
        {
            Close();
        }
    }
}

namespace CourseWork_DB_ONU.View.Edit
{
    using Data.Models.Enum;
    using CourseWork_DB_ONU.Services.EnumService;
    using CourseWork_DB_ONU.Services.Organization;
    using CourseWork_DB_ONU.Services.Outlet;
    using CourseWork_DB_ONU.ViewModels;
    using System.Windows;
    using System.Windows.Controls;
    using OutletModel = Data.Models.Outlet;
    public partial class Outlet : Window
    {
        private OutletViewModel _outlet;
        private readonly IOutletService _outletService;
        private readonly IOrganizationService _organizationService;
        private readonly IEnumService _enumService;
        public Outlet(OutletViewModel outlet,
            IOutletService outletService,
            IOrganizationService organizationService,
            IEnumService enumService)
        {
            InitializeComponent();
            _outlet = outlet;
            _outletService = outletService;
            _organizationService = organizationService;
            _enumService = enumService;
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _cbType.DisplayMemberPath = "Description";
            _cbType.SelectedValuePath = "Value";
            _cbType.ItemsSource = _enumService.GetEnumOptions<OutletType>();

            _cbOrganization.DisplayMemberPath = "Name";
            _cbOrganization.SelectedValuePath = "Id";
            _cbOrganization.ItemsSource = _organizationService.GetAll();

            if (_outlet == null)
            {
                MessageBox.Show("Supplier data is not available.");
                return;
            }
            try
            {
                _txtName.Text = _outlet.Name;
                _cbType.SelectedValue = _outlet.Type;
                _cbOrganization.SelectedValue = _organizationService.GetOrganizationIdByName(_outlet.OrganizationName);
                _txtAddress.Text = _outlet.Address;
                _txtFullName.Text = _outlet.FullNameHeadOf;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading outlet: {ex.Message}");
            }
        }
        private void _txtType_TextChanged(object sender, TextChangedEventArgs e)
        {
            _cbType.DisplayMemberPath = "Description";
            _cbType.SelectedValuePath = "Value";
            string? filter = _txtType.Text?.Trim();
            _cbType.ItemsSource = _enumService.GetEnumOptions<OutletType>(filter);
        }
        private void _txtOrganization_TextChanged(object sender, TextChangedEventArgs e)
        {
            _cbOrganization.DisplayMemberPath = "Name";
            _cbOrganization.SelectedValuePath = "Id";
            string? filter = _txtOrganization.Text?.Trim();
            _cbOrganization.ItemsSource = _organizationService.SearchByName(_txtOrganization.Text?.Trim());
        }
        private void _btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(_txtName.Text) ||
                _cbType.SelectedValue == null ||
                _cbOrganization.SelectedValue == null ||
                string.IsNullOrWhiteSpace(_txtAddress.Text) ||
                string.IsNullOrWhiteSpace(_txtFullName.Text))
            {
                MessageBox.Show("Please fill in all fields.");
                return;
            }
            if (_organizationService == null)
            {
                MessageBox.Show("Organization service is not available.");
                return;
            }
            try
            {
                _outlet.Name = _txtName.Text.Trim();
                _outlet.Type = (OutletType)_cbType.SelectedValue;
                _outlet.OrganizationName = _organizationService.GetOrganizationNameById((int)_cbOrganization.SelectedValue);
                _outlet.Address = _txtAddress.Text.Trim();
                _outlet.FullNameHeadOf = _txtFullName.Text.Trim();
                _outletService.Edit(new OutletModel
                {
                    Id = _outlet.Id,
                    Name = _outlet.Name,
                    Type = _outlet.Type,
                    OrganizationId = _organizationService.GetOrganizationIdByName(_outlet.OrganizationName),
                    Address = _outlet.Address,
                    FullNameHeadOf = _outlet.FullNameHeadOf
                });
                MessageBox.Show("Outlet updated successfully.");
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

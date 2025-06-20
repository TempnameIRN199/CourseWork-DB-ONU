namespace CourseWork_DB_ONU.View.Add
{
    using CourseWork_DB_ONU.Data.Models.Enum;
    using CourseWork_DB_ONU.Services.EnumService;
    using CourseWork_DB_ONU.Services.Organization;
    using CourseWork_DB_ONU.Services.Outlet;
    using System.Windows;
    using System.Windows.Controls;
    public partial class Outlet : Window
    {
        private readonly IOutletService _outletService;
        private readonly IOrganizationService _organizationService;
        private readonly IEnumService _enumService;
        public Outlet(IOutletService outletService,
            IOrganizationService organizationService,
            IEnumService enumService)
        {
            InitializeComponent();
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
        private void _btnAdd_Click(object sender, RoutedEventArgs e)
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
                OutletType outletType = (OutletType)_cbType.SelectedValue;
                int organizationId = (int)_cbOrganization.SelectedValue;
                Data.Models.Outlet outler = new Data.Models.Outlet
                {
                    Name = _txtName.Text.Trim(),
                    Type = outletType,
                    OrganizationId = organizationId,
                    Address = _txtAddress.Text.Trim(),
                    FullNameHeadOf = _txtFullName.Text.Trim()
                };
                _outletService.Add(outler);
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

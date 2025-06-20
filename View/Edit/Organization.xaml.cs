namespace CourseWork_DB_ONU.View.Edit
{
    using CourseWork_DB_ONU.Services.Organization;
    using System.Windows;
    using OrganizationModel = Data.Models.Organization;
    public partial class Organization : Window
    {
        private OrganizationModel _organization;
        private readonly IOrganizationService _organizationService;
        public Organization(OrganizationModel organization,
            IOrganizationService organizationService)
        {
            InitializeComponent();
            _organization = organization;
            _organizationService = organizationService;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (_organizationService == null)
            {
                MessageBox.Show("Organization service is not initialized.");
                return;
            }
            if (_organization == null)
            {
                MessageBox.Show("Organization data is not provided.");
                return;
            }
            try
            {
                _txtName.Text = _organization.Name;
                _txtAddress.Text = _organization.Address;
                _txtFullNameDirector.Text = _organization.FullNameDirector;
                _txtTaxNumber.Text = _organization.TaxNumber;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading organization: {ex.Message}");
            }
        }

        private void _btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(_txtName.Text) ||
                string.IsNullOrWhiteSpace(_txtAddress.Text) ||
                string.IsNullOrWhiteSpace(_txtFullNameDirector.Text) ||
                string.IsNullOrWhiteSpace(_txtTaxNumber.Text))
            {
                MessageBox.Show("All fields must be filled out.");
                return;
            }
            if (_organization == null)
            {
                MessageBox.Show("Organization data is not provided.");
                return;
            }
            try
            {
                _organization.Name = _txtName.Text;
                _organization.Address = _txtAddress.Text;
                _organization.FullNameDirector = _txtFullNameDirector.Text;
                _organization.TaxNumber = _txtTaxNumber.Text;
                _organizationService.Edit(_organization);
                MessageBox.Show("Organization updated successfully.");
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating organization: {ex.Message}");
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Close();
        }
    }
}

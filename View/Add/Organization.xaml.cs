namespace CourseWork_DB_ONU.View.Add
{
    using CourseWork_DB_ONU.Services.Organization;
    using System.Windows;
    public partial class Organization : Window
    {
        private readonly IOrganizationService _organizationService;
        public Organization(IOrganizationService organizationService)
        {
            InitializeComponent();
            _organizationService = organizationService;
        }
        private void _btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(_txtName.Text) ||
                string.IsNullOrWhiteSpace(_txtAddress.Text) ||
                string.IsNullOrWhiteSpace(_txtFullNameDirector.Text) ||
                string.IsNullOrWhiteSpace(_txtTaxNumber.Text))
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
                var organization = new Data.Models.Organization
                {
                    Name = _txtName.Text,
                    Address = _txtAddress.Text,
                    FullNameDirector = _txtFullNameDirector.Text,
                    TaxNumber = _txtTaxNumber.Text
                };
                _organizationService.Add(organization);
                MessageBox.Show("Organization added successfully.");
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding organization: {ex.Message}");
            }
        }
        private void Window_Closed(object sender, EventArgs e)
        {
            Close();
        }
    }
}

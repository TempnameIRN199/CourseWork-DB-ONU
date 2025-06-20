namespace CourseWork_DB_ONU.View.Add
{
    using CourseWork_DB_ONU.Data.Models.Enum;
    using CourseWork_DB_ONU.Services.EnumService;
    using CourseWork_DB_ONU.Services.Outlet;
    using CourseWork_DB_ONU.Services.Seller;
    using System.Windows;
    public partial class Seller : Window
    {
        private readonly ISellerService _sellerService;
        private readonly IOutletService _outletService;
        private readonly IEnumService _enumService;

        public Seller(ISellerService sellerService,
            IOutletService outletService, IEnumService enumService)
        {
            InitializeComponent();
            _sellerService = sellerService;
            _outletService = outletService;
            _enumService = enumService;
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _cbOutlet.ItemsSource = _outletService.GetAll();
            _cbOutlet.DisplayMemberPath = "Name";
            _cbOutlet.SelectedValuePath = "Id";
            _cbGender.ItemsSource = _enumService.GetEnumOptions<SexType>();
            _cbGender.DisplayMemberPath = "Description";
            _cbGender.SelectedValuePath = "Value";
        }
        private void _txtOutlet_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            _cbOutlet.DisplayMemberPath = "Name";
            _cbOutlet.SelectedValuePath = "Id";
            string? filter = _txtOutlet.Text?.Trim();
            _cbOutlet.ItemsSource =
                _outletService.SearchByName(filter);
        }
        private void _btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(_txtName.Text) ||
                string.IsNullOrWhiteSpace(_txtSurname.Text) ||
                string.IsNullOrWhiteSpace(_txtPatronymic.Text) ||
                _cbOutlet.SelectedItem == null ||
                _cbGender.SelectedItem == null ||
                string.IsNullOrWhiteSpace(_txtPosition.Text) ||
                !_dpBirthDate.SelectedDate.HasValue)
            {
                MessageBox.Show("Please fill in all fields.");
                return;
            }
            try
            {
                Data.Models.Seller newSeller = new Data.Models.Seller
                {
                    Name = _txtName.Text.Trim(),
                    Surname = _txtSurname.Text.Trim(),
                    Patronymic = _txtPatronymic.Text.Trim(),
                    OutletId = (int)_cbOutlet.SelectedValue,
                    Position = _txtPosition.Text.Trim(),
                    BirthDate = DateOnly.FromDateTime(_dpBirthDate.SelectedDate.Value),
                    Sex = (SexType)_cbGender.SelectedValue,
                    Address = _txtAddress.Text.Trim(),
                    Residence = _txtResidence.Text.Trim()
                };
                _sellerService.Add(newSeller);
                MessageBox.Show("Seller added successfully.");
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding seller: {ex.Message}");
            }
        }
        private void Window_Closed(object sender, EventArgs e)
        {
            Close();
        }
    }
}

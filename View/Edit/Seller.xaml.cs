namespace CourseWork_DB_ONU.View.Edit
{
    using CourseWork_DB_ONU.Data.Models.Enum;
    using CourseWork_DB_ONU.Services.EnumService;
    using CourseWork_DB_ONU.Services.Outlet;
    using CourseWork_DB_ONU.Services.Seller;
    using CourseWork_DB_ONU.ViewModels;
    using System.Windows;
    using SellerModel = Data.Models.Seller;
    public partial class Seller : Window
    {
        private readonly SellerViewModel _seller;
        private readonly ISellerService _sellerService;
        private readonly IOutletService _outletService;
        private readonly IEnumService _enumService;

        public Seller(SellerViewModel sellerViewModel,
            ISellerService sellerService,
            IOutletService outletService, IEnumService enumService)
        {
            InitializeComponent();
            _seller = sellerViewModel;
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

            if (_seller == null)
            {
                MessageBox.Show("Seller data is not provided.");
                return;
            }
            try
            {
                _txtSurname.Text = _seller.Surname;
                _txtName.Text = _seller.Name;
                _txtPatronymic.Text = _seller.Patronymic;
                _cbOutlet.SelectedValue = _outletService.GetOutletIdByName(_seller.OutletName);
                _txtPosition.Text = _seller.Position;
                _dpBirthDate.SelectedDate = _seller.BirthDate.ToDateTime(new TimeOnly(0, 0));
                _cbGender.SelectedValue = _seller.Sex;
                _txtAddress.Text = _seller.Address;
                _txtResidence.Text = _seller.Residence;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading seller data: {ex.Message}");
            }
        }
        private void _txtOutlet_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            _cbOutlet.DisplayMemberPath = "Name";
            _cbOutlet.SelectedValuePath = "Id";
            string? filter = _txtOutlet.Text?.Trim();
            _cbOutlet.ItemsSource =
                _outletService.SearchByName(filter);
        }
        private void _btnEdit_Click(object sender, RoutedEventArgs e)
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
                _seller.Surname = _txtSurname.Text.Trim();
                _seller.Name = _txtName.Text.Trim();
                _seller.Patronymic = _txtPatronymic.Text.Trim();
                _seller.OutletName = _outletService.GetOutletNameById((int)_cbOutlet.SelectedValue);
                _seller.Position = _txtPosition.Text.Trim();
                _seller.BirthDate = DateOnly.FromDateTime(_dpBirthDate.SelectedDate.Value);
                _seller.Sex = (SexType)_cbGender.SelectedValue;
                _seller.Address = _txtAddress.Text.Trim();
                _seller.Residence = _txtResidence.Text.Trim();
                _sellerService.Edit(new SellerModel()
                {
                    Id = _seller.Id,
                    Surname = _seller.Surname,
                    Name = _seller.Name,
                    Patronymic = _seller.Patronymic,
                    OutletId = _outletService.GetOutletIdByName(_seller.OutletName),
                    Position = _seller.Position,
                    BirthDate = _seller.BirthDate,
                    Sex = _seller.Sex,
                    Address = _seller.Address,
                    Residence = _seller.Residence
                });
                MessageBox.Show("Seller details updated successfully.");
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

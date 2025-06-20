namespace CourseWork_DB_ONU.Services.Seller
{
    using CourseWork_DB_ONU.Data;
    using CourseWork_DB_ONU.Data.Models;
    using CourseWork_DB_ONU.Services.Outlet;
    using CourseWork_DB_ONU.ViewModels;
    using Microsoft.EntityFrameworkCore;

    public class SellerService : ISellerService
    {
        private readonly DatabaseContext _context;
        private readonly IOutletService _outletService;

        public SellerService(DatabaseContext context, IOutletService outletService)
        {
            _context = context;
            _outletService = outletService;
        }
        public List<Seller> GetAll()
        {
            return _context.Sellers.ToList();
        }
        public List<SellerViewModel> ConvertToViewModel(List<Seller> sellers)
        {
            return sellers.Select(s => new SellerViewModel
            {
                Id = s.Id,
                Surname = s.Surname,
                Name = s.Name,
                Patronymic = s.Patronymic,
                OutletName = _outletService.GetOutletNameById(s.OutletId),
                Position = s.Position,
                BirthDate = s.BirthDate,
                Sex = s.Sex,
                Address = s.Address,
                Residence = s.Residence
            }).ToList();
        }
        public void Add(Seller seller)
        {
            if (seller == null)
            {
                throw new ArgumentNullException(nameof(seller), "Seller cannot be null");
            }
            _context.Sellers.Add(seller);
            _context.SaveChanges();
        }
        public void Delete(int id)
        {
            var seller = _context.Sellers
                .FirstOrDefault(s => s.Id == id);
            if (seller == null)
            {
                throw new KeyNotFoundException($"Seller with ID {id} not found");
            }
            _context.Sellers.Remove(seller);
            _context.SaveChanges();
        }
        public void Edit(Seller seller)
        {
            if (seller == null)
            {
                throw new ArgumentNullException(nameof(seller), "Seller cannot be null");
            }
            var existingSeller = _context.Sellers
                .FirstOrDefault(s => s.Id == seller.Id);
            if (existingSeller == null)
            {
                throw new KeyNotFoundException($"Seller with ID {seller.Id} not found");
            }
            existingSeller.Surname = seller.Surname;
            existingSeller.Name = seller.Name;
            existingSeller.Patronymic = seller.Patronymic;
            existingSeller.OutletId = seller.OutletId;
            existingSeller.Position = seller.Position;
            existingSeller.BirthDate = seller.BirthDate;
            existingSeller.Sex = seller.Sex;
            existingSeller.Address = seller.Address;
            existingSeller.Residence = seller.Residence;
            _context.SaveChanges();
        }
        public List<Seller> SearchSellers(string? surname, string? name,
            string? patronymic, string? outletName, string? position,
            string? sexDescription, string? address, string? residence)
        {
            var sellers = _context.Sellers
                .Include(s => s.Outlet)
                .AsEnumerable();

            if (!string.IsNullOrWhiteSpace(surname))
                sellers = sellers.Where(s => s.Surname.Contains(surname, StringComparison.OrdinalIgnoreCase));

            if (!string.IsNullOrWhiteSpace(name))
                sellers = sellers.Where(s => s.Name.Contains(name, StringComparison.OrdinalIgnoreCase));

            if (!string.IsNullOrWhiteSpace(patronymic))
                sellers = sellers.Where(s => s.Patronymic.Contains(patronymic, StringComparison.OrdinalIgnoreCase));

            if (!string.IsNullOrWhiteSpace(outletName))
                sellers = sellers.Where(s => s.Outlet?.Name.Contains(outletName, StringComparison.OrdinalIgnoreCase) == true);

            if (!string.IsNullOrWhiteSpace(position))
                sellers = sellers.Where(s => s.Position.Contains(position, StringComparison.OrdinalIgnoreCase));

            if (!string.IsNullOrWhiteSpace(sexDescription))
                sellers = sellers.Where(s => s.Sex.GetDescription().Contains(sexDescription, StringComparison.OrdinalIgnoreCase));

            if (!string.IsNullOrWhiteSpace(address))
                sellers = sellers.Where(s => s.Address.Contains(address, StringComparison.OrdinalIgnoreCase));

            if (!string.IsNullOrWhiteSpace(residence))
                sellers = sellers.Where(s => s.Residence.Contains(residence, StringComparison.OrdinalIgnoreCase));

            return sellers.ToList();
        }
    }

}

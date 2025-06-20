namespace CourseWork_DB_ONU.Data.Models.Enum
{
    public enum SupplierType
    {
        [System.ComponentModel.Description("Виробник")]
        Manufacturer = 1,
        [System.ComponentModel.Description("Офіційний дистриб'ютор")]
        OfficialDistributor = 2,
        [System.ComponentModel.Description("Імпортер")]
        Importer = 3,
        [System.ComponentModel.Description("Оптовий постачальник")]
        Wholesaler = 4,
        [System.ComponentModel.Description("Роздрібний постачальник")]
        Retailer = 5,
        [System.ComponentModel.Description("Приватний підприємець")]
        PrivateEntrepreneur = 6,
        [System.ComponentModel.Description("Дропшипінг постачальник")]
        DropshipSupplier = 7,
        [System.ComponentModel.Description("Аграрний виробник")]
        AgriculturalProducer = 8,
        [System.ComponentModel.Description("Постачальник послуг")]
        ServiceProvider = 9,
        [System.ComponentModel.Description("Інший")]
        Other = 10
    }
}

namespace CourseWork_DB_ONU.Data.Models.Enum
{
    public enum OutletType
    {
        [System.ComponentModel.Description("Стаціонарний магазин")]
        Store = 1,
        [System.ComponentModel.Description("Супермаркет / Гіпермаркет")]
        Supermarket = 2,
        [System.ComponentModel.Description("Бутик")]
        Boutique = 3,
        [System.ComponentModel.Description("Кіоск / Павільйон")]
        Kiosk = 4,
        [System.ComponentModel.Description("Ринок")]
        Market = 5,
        [System.ComponentModel.Description("Ярмарок")]
        Fair = 6,
        [System.ComponentModel.Description("Торговий автомат")]
        VendingMachine = 7,
        [System.ComponentModel.Description("Інтернет-магазин")]
        OnlineStore = 8,
        [System.ComponentModel.Description("Мобільна торгова точка")]
        MobileOutlet = 9,
        [System.ComponentModel.Description("Острівець у торговому центрі")]
        MallIsland = 10,
        [System.ComponentModel.Description("Шоурум")]
        Showroom = 11,
        [System.ComponentModel.Description("Аптечний пункт")]
        PharmacyPoint = 12,
        [System.ComponentModel.Description("Склад-магазин (Cash & Carry)")]
        WarehouseStore = 13
    }

}

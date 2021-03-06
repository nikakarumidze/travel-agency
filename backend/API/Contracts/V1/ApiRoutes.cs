namespace API.Contracts.V1;

public class ApiRoutes
{
    private const string Root = "api";
    private const string Version = "v1";
    private const string Base = Root + "/" + Version;

    public static class User
    {
         private const string UserBase = Base + "/User";
         public const string SignIn = UserBase + "/SignIn";
         public const string Register = UserBase + "/Register";
         public const string ChangePassword = UserBase + "/ChangePassword";
         public const string UpdateUserInfo = UserBase + "/UpdateInfo";
         public const string GetInfo = UserBase + "/GetInfo";
         public const string Refresh = UserBase + "/Refresh";
    }

    public static class Apartment
    {
        private const string ApartmentBase = Base + "/Apartment";
        public const string GetAll = ApartmentBase + "/GetAll";
        public const string GetAllByCity = ApartmentBase + "/GetAllByCity";
        public const string GetAllByAddress = ApartmentBase + "/GetAllByAddress";
        public const string GetMine = ApartmentBase + "/GetMine";
        public const string Create = ApartmentBase + "/CreateMine";
        public const string Update = ApartmentBase + "/UpdateMine";
        public const string Delete = ApartmentBase + "/DeleteMine";
        public const string GetAllWithBusyDates = ApartmentBase + "/GetAllWithBusyDates";
        public const string GetWithBusyDates = ApartmentBase + "/GetWithBusyDates";
        public const string Search = ApartmentBase + "/Search";
    }

    public static class City
    {
        private const string CityBase = Base + "/City";
        public const string GetAll = CityBase + "/GetAll";
    }

    public static class Order
    {
        private const string OrderBase = Base + "/Order";
        public const string Book = OrderBase + "/Book";
        public const string GetWhereIHost = OrderBase + "/GetWhereIHost";
        public const string GetWhereITravel = OrderBase + "/GetWhereITravel";
        public const string GetPendingWhereIHost = OrderBase + "/GetPendingWhereIHost";
        public const string GetPendingWhereITravel = OrderBase + "/GetPendingWhereITravel";
        public const string ChangeOrderStatus = OrderBase + "/ChangeOrderStatus";
    }
    
}
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
    }

    public static class City
    {
        private const string CityBase = Base + "/City";
        public const string GetAll = CityBase + "/GetAll";
        public const string Create = CityBase + "/Create";
    }
    
}
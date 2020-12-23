namespace Web_BTL_Backend.Models
{
    public class UserModel
    {
        public string UserName { set; get; }

        public int IdUser { set; get; }
        public string Password { set; get; }
        public string EmailAddress { set; get; }

        public string Role { set; get; }

        public UserModel()
        {
            EmailAddress = "";
            UserName = "";
            IdUser = 0;
            Password = "";
            Role = "";
        }
    }
}

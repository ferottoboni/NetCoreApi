namespace junto_test_api.Domain
{
    public class UserViewModel : BaseDomain
    {
        public int? AccountId { get; set; }

        public string Name { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        private string password;

        public string Password
        {
            get { return password; }
            set { password = DomainHelper.GetHashString(value); }
        }
    }
}

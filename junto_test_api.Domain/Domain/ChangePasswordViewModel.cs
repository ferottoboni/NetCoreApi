namespace junto_test_api.Domain
{
    public class ChangePasswordViewModel : BaseDomain
    {
        public string Email { get; set; }

        public string OldPassword { get; set; }

        public string NewPassword { get; set; }

    }
}

using CustomerRegisterationFlow.Domain.Common;
namespace CustomerRegisterationFlow.Domain.Entities
{

    public class Customer :IAuditEntity
    {
        public string Name { get; set; }
        public int ICNumber { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        //public bool? IsPhoneVerified { get; set; }
        //public bool? IsEmailVerified { get; set; }
        public bool? HasAcceptedTerms { get; set; }
        public string PasswordHash { get; set; }
        public string Salt { get; set; }
        public bool? IsBiometricLoginEnabled { get; set; }
    }
}

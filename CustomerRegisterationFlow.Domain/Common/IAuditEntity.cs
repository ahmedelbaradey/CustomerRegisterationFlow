namespace CustomerRegisterationFlow.Domain.Common
{
    public abstract class IAuditEntity :IBaseEntity
    {
         
        public DateTime DateCreated { get; set; }
        public string CreatedBy { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public string LastModifiedBy { get; set; }
    }
}

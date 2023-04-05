using System;

namespace LotoMate.Lottery.Infrastructure.Models
{
    public class BaseModel
    {
        public BaseModel() { }
        public string CreatedBy { get; set; }
        public DateTimeOffset? CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public DateTimeOffset? ModifiedOn { get; set; }

        public void AuditAdd(int? userId)
        {
            CreatedOn = DateTime.UtcNow;
            CreatedBy = userId.ToString();
            ModifiedOn = DateTime.UtcNow;
            ModifiedBy = userId.ToString();
        }
        public void AuditUpdate(int? userId)
        {
            ModifiedOn = DateTime.UtcNow;
            ModifiedBy = userId.ToString();
        }
    }
}

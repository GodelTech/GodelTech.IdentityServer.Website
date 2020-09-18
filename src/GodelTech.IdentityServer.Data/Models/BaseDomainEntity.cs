using System;

namespace GodelTech.IdentityServer.Data.Models
{
    public abstract class BaseDomainEntity : IEquatable<BaseDomainEntity>
    {
        public int Id { get; protected set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public string CreatedBy { get; set; }

        public string ModifiedBy { get; set; }

        public bool Equals(BaseDomainEntity other)
        {
            return other?.Id == Id;
        }

        public override bool Equals(object obj)
        {
            return !ReferenceEquals(null, obj)
                   && (ReferenceEquals(this, obj)
                       || obj.GetType() == GetType() && Equals((BaseDomainEntity)obj));
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}

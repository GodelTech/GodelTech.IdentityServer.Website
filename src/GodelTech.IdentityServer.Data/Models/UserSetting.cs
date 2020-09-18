namespace GodelTech.IdentityServer.Data.Models
{
    public class UserSetting : BaseDomainEntity
    {
        public string Name { get; set; }
        
        public string Value { get; set; }
        
        public string Type { get; set; }
        
        public User SettingOwner { get; set; }
    }
}

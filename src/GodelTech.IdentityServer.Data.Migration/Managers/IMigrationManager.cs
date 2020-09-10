namespace GodelTech.IdentityServer.Data.Migration.Managers
{
    public interface IMigrationManager
    {
        void Migrate(string[] args, bool withDataSeed = false);
    }
}

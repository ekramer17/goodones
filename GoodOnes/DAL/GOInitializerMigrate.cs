using System.Data.Entity;
using GoodOnes.Migrations;

namespace GoodOnes.DAL
{
    internal class GOInitializerMigrate : MigrateDatabaseToLatestVersion<GOContext, Configuration>
    {
    }

    //internal class GOInitializer : DropCreateDatabaseAlways<GOContext>
    //{
    //}
}

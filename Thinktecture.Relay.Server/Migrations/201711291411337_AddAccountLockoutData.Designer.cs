// <auto-generated />
namespace Thinktecture.Relay.Server.Migrations
{
    using System.CodeDom.Compiler;
    using System.Data.Entity.Migrations;
    using System.Data.Entity.Migrations.Infrastructure;
    using System.Resources;
    
    [GeneratedCode("EntityFramework.Migrations", "6.2.0-61023")]
    public sealed partial class AddAccountLockoutData : IMigrationMetadata
    {
        private readonly ResourceManager Resources = new ResourceManager(typeof(AddAccountLockoutData));
        
        string IMigrationMetadata.Id
        {
            get { return "201711291411337_AddAccountLockoutData"; }
        }
        
        string IMigrationMetadata.Source
        {
            get { return null; }
        }
        
        string IMigrationMetadata.Target
        {
            get { return Resources.GetString("Target"); }
        }
    }
}
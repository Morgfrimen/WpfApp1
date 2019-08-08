namespace WpfApp1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCompanyMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "Comn", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "Comn");
        }
    }
}

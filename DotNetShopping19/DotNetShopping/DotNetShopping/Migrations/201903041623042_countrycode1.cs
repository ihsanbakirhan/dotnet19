namespace DotNetShopping.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class countrycode1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Countries", "Code", c => c.String(nullable: false));
            AlterColumn("dbo.States", "Code", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.States", "Code", c => c.String());
            AlterColumn("dbo.Countries", "Code", c => c.String());
        }
    }
}

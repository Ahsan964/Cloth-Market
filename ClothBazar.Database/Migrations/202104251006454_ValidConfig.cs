namespace ClothBazar.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ValidConfig : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Configs", "Value", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Configs", "Value", c => c.String());
        }
    }
}

namespace PD.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addavatar : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Avatars",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Image = c.Binary(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UserDetails", t => t.Id)
                .Index(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Avatars", "Id", "dbo.UserDetails");
            DropIndex("dbo.Avatars", new[] { "Id" });
            DropTable("dbo.Avatars");
        }
    }
}

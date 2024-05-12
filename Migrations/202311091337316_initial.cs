namespace Library.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BookDetail",
                c => new
                    {
                        BookDetailId = c.Int(nullable: false, identity: true),
                        AuthorName = c.String(),
                        Price = c.String(),
                        BookId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.BookDetailId)
                .ForeignKey("dbo.BookMaster", t => t.BookId, cascadeDelete: true)
                .Index(t => t.BookId);
            
            CreateTable(
                "dbo.BookMaster",
                c => new
                    {
                        BookId = c.Int(nullable: false, identity: true),
                        BookName = c.String(),
                        PublishDate = c.DateTime(nullable: false, storeType: "date"),
                        Available = c.String(),
                        Image = c.String(),
                    })
                .PrimaryKey(t => t.BookId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BookDetail", "BookId", "dbo.BookMaster");
            DropIndex("dbo.BookDetail", new[] { "BookId" });
            DropTable("dbo.BookMaster");
            DropTable("dbo.BookDetail");
        }
    }
}

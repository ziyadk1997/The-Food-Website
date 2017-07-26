namespace FoodWebsite.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedRestaurantDataModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RestaurantDataModels",
                c => new
                    {
                        RestaurantID = c.Guid(nullable: false),
                        Name = c.String(),
                        SerializedItems = c.String(),
                    })
                .PrimaryKey(t => t.RestaurantID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.RestaurantDataModels");
        }
    }
}

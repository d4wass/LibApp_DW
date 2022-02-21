using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibApp.Data.Migrations
{
    public partial class SetNameOfCustomer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("UPDATE Customers SET NAME = 'Roman' WHERE Id = 1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}

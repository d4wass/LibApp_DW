using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibApp.Data.Migrations
{
    public partial class InitialSetBooks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("SET IDENTITY_INSERT Books ON");
            migrationBuilder.Sql("INSERT INTO Books (Id, Name, AuthorName, GenreId, DateAdded, ReleaseDate, NumberInStock, NumberAvailable) VALUES (1, 'Harry Potter and Half-Blood Prince', 'J.K. Rowling', 6, '10/10/20', '10/10/00', 10, 5)");
            migrationBuilder.Sql("INSERT INTO Books (Id, Name, AuthorName, GenreId, DateAdded, ReleaseDate, NumberInStock, NumberAvailable) VALUES (2, 'The Lord of the Rings', 'J.R.R Tolkien', 6, '10/10/20', '11/05/99', 15, 0)");
            migrationBuilder.Sql("INSERT INTO Books (Id, Name, AuthorName, GenreId, DateAdded, ReleaseDate, NumberInStock, NumberAvailable) VALUES (3, 'Don Quixote', 'Miguel de Cervantes', 1, '10/10/20', '12/16/70', 20, 15)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
    
        }
    }
}

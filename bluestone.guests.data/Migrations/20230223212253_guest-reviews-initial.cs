using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace bluestone.guests.data.Migrations
  {
  /// <inheritdoc />
  public partial class Guestreviewsinitial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "guests",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    ForeNames = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    SurName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_guests", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "reviews",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GuestID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Body = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Score = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_reviews", x => x.ID);
                    table.ForeignKey(
                        name: "FK_reviews_guests_GuestID",
                        column: x => x.GuestID,
                        principalTable: "guests",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_reviews_GuestID",
                table: "reviews",
                column: "GuestID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "reviews");

            migrationBuilder.DropTable(
                name: "guests");
        }
    }
}

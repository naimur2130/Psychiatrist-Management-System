using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Psychiatrist_Management_System.Migrations
{
    /// <inheritdoc />
    public partial class AddPsychiatristProfileToDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PsychiatristProfile",
                columns: table => new
                {
                    PyschoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PsychoName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PsychoEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PsychoAge = table.Column<int>(type: "int", nullable: false),
                    PsychoGender = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PsychoPhone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Specialization = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PsychiatristProfile", x => x.PyschoId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PsychiatristProfile");
        }
    }
}

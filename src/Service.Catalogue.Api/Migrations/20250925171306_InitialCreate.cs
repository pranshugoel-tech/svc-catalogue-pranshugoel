using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Service.Catalogue.Api.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Services",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    OwnerTeam = table.Column<string>(type: "TEXT", nullable: false),
                    Tier = table.Column<string>(type: "TEXT", nullable: false),
                    Lifecycle = table.Column<string>(type: "TEXT", nullable: false),
                    Endpoints = table.Column<string>(type: "TEXT", nullable: false),
                    Tags = table.Column<string>(type: "TEXT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Services", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Services",
                columns: new[] { "Id", "CreatedAt", "Endpoints", "Lifecycle", "Name", "OwnerTeam", "Tags", "Tier", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("20dc5834-b9a9-4cee-86ba-16ee2b268f28"), new DateTime(2025, 9, 25, 17, 13, 5, 937, DateTimeKind.Utc).AddTicks(6638), "[\"https://api.example.com/users\"]", "production", "User Management", "Identity", "[\"auth\",\"users\"]", "gold", new DateTime(2025, 9, 25, 17, 13, 5, 937, DateTimeKind.Utc).AddTicks(6721) },
                    { new Guid("d36021ab-2982-4de1-834c-bb969382d526"), new DateTime(2025, 9, 25, 17, 13, 5, 937, DateTimeKind.Utc).AddTicks(6837), "[\"https://api.example.com/payments\"]", "preprod", "Payments API", "Finance", "[\"payments\",\"finance\"]", "silver", new DateTime(2025, 9, 25, 17, 13, 5, 937, DateTimeKind.Utc).AddTicks(6837) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Services");
        }
    }
}

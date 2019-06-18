using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace manageContacts.Migrations
{
    public partial class createDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "contactTag",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    tag = table.Column<string>(maxLength: 128, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_contactTag", x => x.id);
                    table.UniqueConstraint("AK_contactTag_tag", x => x.tag);
                });

            migrationBuilder.CreateTable(
                name: "simContacts",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    guid = table.Column<Guid>(nullable: false),
                    name = table.Column<string>(nullable: true),
                    surname = table.Column<string>(nullable: true),
                    Discriminator = table.Column<string>(nullable: false),
                    address = table.Column<string>(nullable: true),
                    email = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_simContacts", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "mobileNumber",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    guid = table.Column<Guid>(nullable: false),
                    number = table.Column<string>(maxLength: 64, nullable: false),
                    contactid = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mobileNumber", x => x.id);
                    table.UniqueConstraint("AK_mobileNumber_number", x => x.number);
                    table.ForeignKey(
                        name: "FK_mobileNumber_simContacts_contactid",
                        column: x => x.contactid,
                        principalTable: "simContacts",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "phoneContactTags",
                columns: table => new
                {
                    phoneContactId = table.Column<int>(nullable: false),
                    tagId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_phoneContactTags", x => new { x.phoneContactId, x.tagId });
                    table.ForeignKey(
                        name: "FK_phoneContactTags_simContacts_phoneContactId",
                        column: x => x.phoneContactId,
                        principalTable: "simContacts",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_phoneContactTags_contactTag_tagId",
                        column: x => x.tagId,
                        principalTable: "contactTag",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_mobileNumber_contactid",
                table: "mobileNumber",
                column: "contactid");

            migrationBuilder.CreateIndex(
                name: "IX_phoneContactTags_tagId",
                table: "phoneContactTags",
                column: "tagId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "mobileNumber");

            migrationBuilder.DropTable(
                name: "phoneContactTags");

            migrationBuilder.DropTable(
                name: "simContacts");

            migrationBuilder.DropTable(
                name: "contactTag");
        }
    }
}

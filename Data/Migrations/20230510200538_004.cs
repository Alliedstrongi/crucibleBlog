using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace crucibleBlog.Data.Migrations
{
    /// <inheritdoc />
    public partial class _004 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "isPublished",
                table: "BlogPost",
                newName: "IsPublished");

            migrationBuilder.RenameColumn(
                name: "isDeleted",
                table: "BlogPost",
                newName: "IsDeleted");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Updated",
                table: "Comments",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Categories",
                type: "character varying(200)",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "Abstract",
                table: "BlogPost",
                type: "character varying(600)",
                maxLength: 600,
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer",
                oldMaxLength: 600);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsPublished",
                table: "BlogPost",
                newName: "isPublished");

            migrationBuilder.RenameColumn(
                name: "IsDeleted",
                table: "BlogPost",
                newName: "isDeleted");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Updated",
                table: "Comments",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Categories",
                type: "character varying(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(200)",
                oldMaxLength: 200,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Abstract",
                table: "BlogPost",
                type: "integer",
                maxLength: 600,
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "character varying(600)",
                oldMaxLength: 600,
                oldNullable: true);
        }
    }
}

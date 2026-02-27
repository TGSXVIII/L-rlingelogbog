using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class Tasks_standarts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EducationalStandarts_Educations_educationIdId",
                table: "EducationalStandarts");

            migrationBuilder.DropForeignKey(
                name: "FK_PicturesAndVideos_Tasks_taskIdId",
                table: "PicturesAndVideos");

            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_EducationalStandarts_educationStandartsIdId",
                table: "Tasks");

            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Users_userIdId",
                table: "Tasks");

            migrationBuilder.RenameColumn(
                name: "userIdId",
                table: "Tasks",
                newName: "createdById");

            migrationBuilder.RenameColumn(
                name: "educationStandartsIdId",
                table: "Tasks",
                newName: "assignedToId");

            migrationBuilder.RenameIndex(
                name: "IX_Tasks_userIdId",
                table: "Tasks",
                newName: "IX_Tasks_createdById");

            migrationBuilder.RenameIndex(
                name: "IX_Tasks_educationStandartsIdId",
                table: "Tasks",
                newName: "IX_Tasks_assignedToId");

            migrationBuilder.RenameColumn(
                name: "taskIdId",
                table: "PicturesAndVideos",
                newName: "TaskId");

            migrationBuilder.RenameColumn(
                name: "Path",
                table: "PicturesAndVideos",
                newName: "Name");

            migrationBuilder.RenameIndex(
                name: "IX_PicturesAndVideos_taskIdId",
                table: "PicturesAndVideos",
                newName: "IX_PicturesAndVideos_TaskId");

            migrationBuilder.RenameColumn(
                name: "educationIdId",
                table: "EducationalStandarts",
                newName: "EducationId");

            migrationBuilder.RenameIndex(
                name: "IX_EducationalStandarts_educationIdId",
                table: "EducationalStandarts",
                newName: "IX_EducationalStandarts_EducationId");

            migrationBuilder.AddColumn<int>(
                name: "TaskStatus",
                table: "Tasks",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "Type",
                table: "PicturesAndVideos",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "RefreshTokens",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Token = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Expires = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    IsRevoked = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshTokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RefreshTokens_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Tasks_EducationalStandarts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    TaskId = table.Column<int>(type: "int", nullable: false),
                    EducationalStandartId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tasks_EducationalStandarts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tasks_EducationalStandarts_EducationalStandarts_EducationalS~",
                        column: x => x.EducationalStandartId,
                        principalTable: "EducationalStandarts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tasks_EducationalStandarts_Tasks_TaskId",
                        column: x => x.TaskId,
                        principalTable: "Tasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Users_Education",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Grade = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    EducationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users_Education", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Education_Educations_EducationId",
                        column: x => x.EducationId,
                        principalTable: "Educations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Users_Education_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokens_UserId",
                table: "RefreshTokens",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_EducationalStandarts_EducationalStandartId",
                table: "Tasks_EducationalStandarts",
                column: "EducationalStandartId");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_EducationalStandarts_TaskId",
                table: "Tasks_EducationalStandarts",
                column: "TaskId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Education_EducationId",
                table: "Users_Education",
                column: "EducationId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Education_UserId",
                table: "Users_Education",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_EducationalStandarts_Educations_EducationId",
                table: "EducationalStandarts",
                column: "EducationId",
                principalTable: "Educations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PicturesAndVideos_Tasks_TaskId",
                table: "PicturesAndVideos",
                column: "TaskId",
                principalTable: "Tasks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Users_assignedToId",
                table: "Tasks",
                column: "assignedToId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Users_createdById",
                table: "Tasks",
                column: "createdById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EducationalStandarts_Educations_EducationId",
                table: "EducationalStandarts");

            migrationBuilder.DropForeignKey(
                name: "FK_PicturesAndVideos_Tasks_TaskId",
                table: "PicturesAndVideos");

            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Users_assignedToId",
                table: "Tasks");

            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Users_createdById",
                table: "Tasks");

            migrationBuilder.DropTable(
                name: "RefreshTokens");

            migrationBuilder.DropTable(
                name: "Tasks_EducationalStandarts");

            migrationBuilder.DropTable(
                name: "Users_Education");

            migrationBuilder.DropColumn(
                name: "TaskStatus",
                table: "Tasks");

            migrationBuilder.RenameColumn(
                name: "createdById",
                table: "Tasks",
                newName: "userIdId");

            migrationBuilder.RenameColumn(
                name: "assignedToId",
                table: "Tasks",
                newName: "educationStandartsIdId");

            migrationBuilder.RenameIndex(
                name: "IX_Tasks_createdById",
                table: "Tasks",
                newName: "IX_Tasks_userIdId");

            migrationBuilder.RenameIndex(
                name: "IX_Tasks_assignedToId",
                table: "Tasks",
                newName: "IX_Tasks_educationStandartsIdId");

            migrationBuilder.RenameColumn(
                name: "TaskId",
                table: "PicturesAndVideos",
                newName: "taskIdId");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "PicturesAndVideos",
                newName: "Path");

            migrationBuilder.RenameIndex(
                name: "IX_PicturesAndVideos_TaskId",
                table: "PicturesAndVideos",
                newName: "IX_PicturesAndVideos_taskIdId");

            migrationBuilder.RenameColumn(
                name: "EducationId",
                table: "EducationalStandarts",
                newName: "educationIdId");

            migrationBuilder.RenameIndex(
                name: "IX_EducationalStandarts_EducationId",
                table: "EducationalStandarts",
                newName: "IX_EducationalStandarts_educationIdId");

            migrationBuilder.AlterColumn<string>(
                name: "Type",
                table: "PicturesAndVideos",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddForeignKey(
                name: "FK_EducationalStandarts_Educations_educationIdId",
                table: "EducationalStandarts",
                column: "educationIdId",
                principalTable: "Educations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PicturesAndVideos_Tasks_taskIdId",
                table: "PicturesAndVideos",
                column: "taskIdId",
                principalTable: "Tasks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_EducationalStandarts_educationStandartsIdId",
                table: "Tasks",
                column: "educationStandartsIdId",
                principalTable: "EducationalStandarts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Users_userIdId",
                table: "Tasks",
                column: "userIdId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

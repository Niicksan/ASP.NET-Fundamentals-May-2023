﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskBoard.Data.Migrations
{
    public partial class AddTasksAndBoardsAndSeedDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Boards",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Boards", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tasks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BoardId = table.Column<int>(type: "int", nullable: false),
                    OwnerId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tasks_AspNetUsers_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tasks_Boards_BoardId",
                        column: x => x.BoardId,
                        principalTable: "Boards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Boards",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1, "Open" });

            migrationBuilder.InsertData(
                table: "Boards",
                columns: new[] { "Id", "Name" },
                values: new object[] { 2, "In Progress" });

            migrationBuilder.InsertData(
                table: "Boards",
                columns: new[] { "Id", "Name" },
                values: new object[] { 3, "Done" });

            migrationBuilder.InsertData(
                table: "Tasks",
                columns: new[] { "Id", "BoardId", "CreatedOn", "Description", "OwnerId", "Title" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2022, 11, 27, 19, 44, 9, 411, DateTimeKind.Utc).AddTicks(7865), "Implement better styling for all public pages", "5279d3fe-35a2-4ebc-bbc0-ccc83350fd98", "Improve CSS styles" },
                    { 2, 1, new DateTime(2023, 1, 15, 19, 44, 9, 411, DateTimeKind.Utc).AddTicks(7908), "Create Android client App for the RESTful TaskBoard service", "9602aa4c-1c5e-48f0-b45d-1699c43569bd", "Android Client App" },
                    { 3, 2, new DateTime(2023, 5, 15, 19, 44, 9, 411, DateTimeKind.Utc).AddTicks(7913), "Create Desktop client App for the RESTful TaskBoard service", "9602aa4c-1c5e-48f0-b45d-1699c43569bd", "Desktop Client App" },
                    { 4, 3, new DateTime(2022, 6, 15, 19, 44, 9, 411, DateTimeKind.Utc).AddTicks(7916), "Implement [Create Task] page for adding tasks", "5279d3fe-35a2-4ebc-bbc0-ccc83350fd98", "Create Tasks" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_BoardId",
                table: "Tasks",
                column: "BoardId");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_OwnerId",
                table: "Tasks",
                column: "OwnerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tasks");

            migrationBuilder.DropTable(
                name: "Boards");
        }
    }
}

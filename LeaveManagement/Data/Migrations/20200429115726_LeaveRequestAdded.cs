﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LeaveManagement.Data.Migrations
{
    public partial class LeaveRequestAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EditLeaveAllocationVM");

            migrationBuilder.DropTable(
                name: "LeaveAllocationVM");

            migrationBuilder.DropTable(
                name: "LeaveHistories");

            migrationBuilder.DropTable(
                name: "EmployeeVM");

            migrationBuilder.DropTable(
                name: "DetailsLeaveTypeVM");

            migrationBuilder.CreateTable(
                name: "LeaveRequests",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RequestingEmployeeId = table.Column<string>(nullable: true),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false),
                    LeaveTypeId = table.Column<int>(nullable: false),
                    DateRequested = table.Column<DateTime>(nullable: false),
                    DateActioned = table.Column<DateTime>(nullable: false),
                    Approved = table.Column<bool>(nullable: true),
                    ApprovedById = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeaveRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LeaveRequests_AspNetUsers_ApprovedById",
                        column: x => x.ApprovedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LeaveRequests_LeaveTypes_LeaveTypeId",
                        column: x => x.LeaveTypeId,
                        principalTable: "LeaveTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LeaveRequests_AspNetUsers_RequestingEmployeeId",
                        column: x => x.RequestingEmployeeId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LeaveRequests_ApprovedById",
                table: "LeaveRequests",
                column: "ApprovedById");

            migrationBuilder.CreateIndex(
                name: "IX_LeaveRequests_LeaveTypeId",
                table: "LeaveRequests",
                column: "LeaveTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_LeaveRequests_RequestingEmployeeId",
                table: "LeaveRequests",
                column: "RequestingEmployeeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LeaveRequests");

            migrationBuilder.CreateTable(
                name: "DetailsLeaveTypeVM",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DefaultDays = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetailsLeaveTypeVM", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeVM",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DateJoined = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Firstname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lastname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TaxId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeVM", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LeaveHistories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Approved = table.Column<bool>(type: "bit", nullable: true),
                    ApprovedById = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DateActioned = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateRequested = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LeaveTypeId = table.Column<int>(type: "int", nullable: false),
                    RequestingEmployeeId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeaveHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LeaveHistories_AspNetUsers_ApprovedById",
                        column: x => x.ApprovedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LeaveHistories_LeaveTypes_LeaveTypeId",
                        column: x => x.LeaveTypeId,
                        principalTable: "LeaveTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LeaveHistories_AspNetUsers_RequestingEmployeeId",
                        column: x => x.RequestingEmployeeId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EditLeaveAllocationVM",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    EmployeeId1 = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    LeaveTypeId = table.Column<int>(type: "int", nullable: true),
                    NumberOfDays = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EditLeaveAllocationVM", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EditLeaveAllocationVM_EmployeeVM_EmployeeId1",
                        column: x => x.EmployeeId1,
                        principalTable: "EmployeeVM",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EditLeaveAllocationVM_DetailsLeaveTypeVM_LeaveTypeId",
                        column: x => x.LeaveTypeId,
                        principalTable: "DetailsLeaveTypeVM",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LeaveAllocationVM",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EmployeeId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    LeaveTypeId = table.Column<int>(type: "int", nullable: false),
                    NumberOfDays = table.Column<int>(type: "int", nullable: false),
                    Period = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeaveAllocationVM", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LeaveAllocationVM_EmployeeVM_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "EmployeeVM",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LeaveAllocationVM_DetailsLeaveTypeVM_LeaveTypeId",
                        column: x => x.LeaveTypeId,
                        principalTable: "DetailsLeaveTypeVM",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EditLeaveAllocationVM_EmployeeId1",
                table: "EditLeaveAllocationVM",
                column: "EmployeeId1");

            migrationBuilder.CreateIndex(
                name: "IX_EditLeaveAllocationVM_LeaveTypeId",
                table: "EditLeaveAllocationVM",
                column: "LeaveTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_LeaveAllocationVM_EmployeeId",
                table: "LeaveAllocationVM",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_LeaveAllocationVM_LeaveTypeId",
                table: "LeaveAllocationVM",
                column: "LeaveTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_LeaveHistories_ApprovedById",
                table: "LeaveHistories",
                column: "ApprovedById");

            migrationBuilder.CreateIndex(
                name: "IX_LeaveHistories_LeaveTypeId",
                table: "LeaveHistories",
                column: "LeaveTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_LeaveHistories_RequestingEmployeeId",
                table: "LeaveHistories",
                column: "RequestingEmployeeId");
        }
    }
}

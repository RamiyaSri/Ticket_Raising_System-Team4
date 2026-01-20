using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TicketClassLibrary.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Employee",
                columns: table => new
                {
                    EmpId = table.Column<string>(type: "char(5)", maxLength: 5, nullable: false),
                    FirstName = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    Password = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    Role = table.Column<string>(type: "varchar(20)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "char(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employee", x => x.EmpId);
                });

            migrationBuilder.CreateTable(
                name: "TicketPriority",
                columns: table => new
                {
                    PriorityId = table.Column<string>(type: "char(5)", maxLength: 5, nullable: false),
                    PriorityLevel = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false),
                    PriorityDescription = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    ResponseTime = table.Column<int>(type: "int", nullable: false),
                    ResolutionTime = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketPriority", x => x.PriorityId);
                });

            migrationBuilder.CreateTable(
                name: "TicketType",
                columns: table => new
                {
                    TicketTypeId = table.Column<string>(type: "char(5)", maxLength: 5, nullable: false),
                    TypeName = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    PriorityId = table.Column<string>(type: "char(5)", maxLength: 5, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketType", x => x.TicketTypeId);
                    table.ForeignKey(
                        name: "FK_TicketType_TicketPriority_PriorityId",
                        column: x => x.PriorityId,
                        principalTable: "TicketPriority",
                        principalColumn: "PriorityId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Ticket",
                columns: table => new
                {
                    TicketId = table.Column<string>(type: "char(5)", maxLength: 5, nullable: false),
                    EmpId = table.Column<string>(type: "char(5)", maxLength: 5, nullable: false),
                    Subject = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false),
                    Status = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false),
                    TicketTypeId = table.Column<string>(type: "char(5)", maxLength: 5, nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ResolutionDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ticket", x => x.TicketId);
                    table.ForeignKey(
                        name: "FK_Ticket_Employee_EmpId",
                        column: x => x.EmpId,
                        principalTable: "Employee",
                        principalColumn: "EmpId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Ticket_TicketType_TicketTypeId",
                        column: x => x.TicketTypeId,
                        principalTable: "TicketType",
                        principalColumn: "TicketTypeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TicketAssignment",
                columns: table => new
                {
                    AssignmentId = table.Column<string>(type: "char(3)", maxLength: 3, nullable: false),
                    TicketId = table.Column<string>(type: "char(5)", maxLength: 5, nullable: false),
                    Support_Emp_Id = table.Column<string>(type: "char(5)", maxLength: 5, nullable: false),
                    AssignmentDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketAssignment", x => x.AssignmentId);
                    table.ForeignKey(
                        name: "FK_TicketAssignment_Employee_Support_Emp_Id",
                        column: x => x.Support_Emp_Id,
                        principalTable: "Employee",
                        principalColumn: "EmpId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TicketAssignment_Ticket_TicketId",
                        column: x => x.TicketId,
                        principalTable: "Ticket",
                        principalColumn: "TicketId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TicketComment",
                columns: table => new
                {
                    CommentId = table.Column<string>(type: "char(5)", maxLength: 5, nullable: false),
                    TicketId = table.Column<string>(type: "char(5)", maxLength: 5, nullable: false),
                    EmpId = table.Column<string>(type: "char(5)", maxLength: 5, nullable: false),
                    Support_Emp_Id = table.Column<string>(type: "char(5)", maxLength: 5, nullable: true),
                    CommentText = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false),
                    CommentDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketComment", x => x.CommentId);
                    table.ForeignKey(
                        name: "FK_TicketComment_Employee_EmpId",
                        column: x => x.EmpId,
                        principalTable: "Employee",
                        principalColumn: "EmpId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TicketComment_Employee_Support_Emp_Id",
                        column: x => x.Support_Emp_Id,
                        principalTable: "Employee",
                        principalColumn: "EmpId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TicketComment_Ticket_TicketId",
                        column: x => x.TicketId,
                        principalTable: "Ticket",
                        principalColumn: "TicketId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Employee_Email",
                table: "Employee",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employee_PhoneNumber",
                table: "Employee",
                column: "PhoneNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_EmpId",
                table: "Ticket",
                column: "EmpId");

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_TicketTypeId",
                table: "Ticket",
                column: "TicketTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketAssignment_Support_Emp_Id",
                table: "TicketAssignment",
                column: "Support_Emp_Id");

            migrationBuilder.CreateIndex(
                name: "IX_TicketAssignment_TicketId",
                table: "TicketAssignment",
                column: "TicketId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketComment_EmpId",
                table: "TicketComment",
                column: "EmpId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketComment_Support_Emp_Id",
                table: "TicketComment",
                column: "Support_Emp_Id");

            migrationBuilder.CreateIndex(
                name: "IX_TicketComment_TicketId",
                table: "TicketComment",
                column: "TicketId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketType_PriorityId",
                table: "TicketType",
                column: "PriorityId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TicketAssignment");

            migrationBuilder.DropTable(
                name: "TicketComment");

            migrationBuilder.DropTable(
                name: "Ticket");

            migrationBuilder.DropTable(
                name: "Employee");

            migrationBuilder.DropTable(
                name: "TicketType");

            migrationBuilder.DropTable(
                name: "TicketPriority");
        }
    }
}

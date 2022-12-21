using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AtmAPI.Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table =>
                    new
                    {
                        Id = table
                            .Column<int>(type: "int", nullable: false)
                            .Annotation("SqlServer:Identity", "1, 1"),
                        Name = table.Column<string>(
                            type: "nvarchar(100)",
                            maxLength: 100,
                            nullable: false
                        ),
                        Description = table.Column<string>(
                            type: "nvarchar(1500)",
                            maxLength: 1500,
                            nullable: true
                        ),
                        CreatedBy = table.Column<string>(
                            type: "nvarchar(500)",
                            maxLength: 500,
                            nullable: false
                        ),
                        UpdatedBy = table.Column<string>(
                            type: "nvarchar(500)",
                            maxLength: 500,
                            nullable: false
                        ),
                        CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                        UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                    },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                }
            );

            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table =>
                    new
                    {
                        Id = table
                            .Column<int>(type: "int", nullable: false)
                            .Annotation("SqlServer:Identity", "1, 1"),
                        Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                        Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                        CreatedBy = table.Column<string>(
                            type: "nvarchar(500)",
                            maxLength: 500,
                            nullable: false
                        ),
                        UpdatedBy = table.Column<string>(
                            type: "nvarchar(500)",
                            maxLength: 500,
                            nullable: false
                        ),
                        CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                        UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                    },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.Id);
                }
            );

            migrationBuilder.CreateTable(
                name: "Operators",
                columns: table =>
                    new
                    {
                        Id = table
                            .Column<int>(type: "int", nullable: false)
                            .Annotation("SqlServer:Identity", "1, 1"),
                        CreatedBy = table.Column<string>(
                            type: "nvarchar(500)",
                            maxLength: 500,
                            nullable: false
                        ),
                        UpdatedBy = table.Column<string>(
                            type: "nvarchar(500)",
                            maxLength: 500,
                            nullable: false
                        ),
                        CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                        UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                        UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                        Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                        FamilyName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                        GivenName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                        BirthDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                        Picture = table.Column<string>(
                            type: "nvarchar(500)",
                            maxLength: 500,
                            nullable: false
                        ),
                        Code = table.Column<string>(
                            type: "nvarchar(100)",
                            maxLength: 100,
                            nullable: false,
                            computedColumnSql: "CONCAT(\r\n                'ATO', \r\n                CAST(\r\n                    SUBSTRING(\r\n                        CONVERT(VARCHAR, [CreatedAt], 111), \r\n                        0, \r\n                        CHARINDEX('/', CONVERT(VARCHAR, [CreatedAt], 111)\r\n                    )\r\n                ) AS INTEGER) % 1000, \r\n                '.', \r\n                RIGHT('000000' + CAST([Id] % 1000000 AS NVARCHAR), 6)\r\n            )",
                            stored: true
                        ),
                        IdentityReference = table.Column<string>(
                            type: "nvarchar(max)",
                            nullable: false
                        )
                    },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Operators", x => x.Id);
                }
            );

            migrationBuilder.CreateTable(
                name: "Skills",
                columns: table =>
                    new
                    {
                        Id = table
                            .Column<int>(type: "int", nullable: false)
                            .Annotation("SqlServer:Identity", "1, 1"),
                        Name = table.Column<string>(
                            type: "nvarchar(500)",
                            maxLength: 500,
                            nullable: false
                        ),
                        Description = table.Column<string>(
                            type: "nvarchar(1500)",
                            maxLength: 1500,
                            nullable: true
                        ),
                        CreatedBy = table.Column<string>(
                            type: "nvarchar(500)",
                            maxLength: 500,
                            nullable: false
                        ),
                        UpdatedBy = table.Column<string>(
                            type: "nvarchar(500)",
                            maxLength: 500,
                            nullable: false
                        ),
                        CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                        UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                    },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Skills", x => x.Id);
                }
            );

            migrationBuilder.CreateTable(
                name: "Slots",
                columns: table =>
                    new
                    {
                        Id = table
                            .Column<int>(type: "int", nullable: false)
                            .Annotation("SqlServer:Identity", "1, 1"),
                        DayOfWeek = table.Column<int>(type: "int", nullable: false),
                        StartAt = table.Column<TimeSpan>(type: "time", nullable: false),
                        EndAt = table.Column<TimeSpan>(type: "time", nullable: false),
                        Index = table.Column<int>(type: "int", nullable: false),
                        CreatedBy = table.Column<string>(
                            type: "nvarchar(500)",
                            maxLength: 500,
                            nullable: false
                        ),
                        UpdatedBy = table.Column<string>(
                            type: "nvarchar(500)",
                            maxLength: 500,
                            nullable: false
                        ),
                        CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                        UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                    },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Slots", x => x.Id);
                }
            );

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table =>
                    new
                    {
                        Id = table
                            .Column<int>(type: "int", nullable: false)
                            .Annotation("SqlServer:Identity", "1, 1"),
                        CreatedBy = table.Column<string>(
                            type: "nvarchar(500)",
                            maxLength: 500,
                            nullable: false
                        ),
                        UpdatedBy = table.Column<string>(
                            type: "nvarchar(500)",
                            maxLength: 500,
                            nullable: false
                        ),
                        CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                        UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                        UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                        Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                        FamilyName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                        GivenName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                        BirthDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                        Picture = table.Column<string>(
                            type: "nvarchar(500)",
                            maxLength: 500,
                            nullable: false
                        ),
                        Code = table.Column<string>(
                            type: "nvarchar(100)",
                            maxLength: 100,
                            nullable: false,
                            computedColumnSql: "CONCAT(\r\n                'ATS', \r\n                CAST(\r\n                    SUBSTRING(\r\n                        CONVERT(VARCHAR, [CreatedAt], 111), \r\n                        0, \r\n                        CHARINDEX('/', CONVERT(VARCHAR, [CreatedAt], 111)\r\n                    )\r\n                ) AS INTEGER) % 1000, \r\n                '.', \r\n                RIGHT('000000' + CAST([Id] % 1000000 AS NVARCHAR), 6)\r\n            )",
                            stored: true
                        ),
                        IdentityReference = table.Column<string>(
                            type: "nvarchar(max)",
                            nullable: false
                        )
                    },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.Id);
                }
            );

            migrationBuilder.CreateTable(
                name: "Lecturers",
                columns: table =>
                    new
                    {
                        Id = table
                            .Column<int>(type: "int", nullable: false)
                            .Annotation("SqlServer:Identity", "1, 1"),
                        DepartmentId = table.Column<int>(type: "int", nullable: true),
                        CreatedBy = table.Column<string>(
                            type: "nvarchar(500)",
                            maxLength: 500,
                            nullable: false
                        ),
                        UpdatedBy = table.Column<string>(
                            type: "nvarchar(500)",
                            maxLength: 500,
                            nullable: false
                        ),
                        CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                        UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                        UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                        Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                        FamilyName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                        GivenName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                        BirthDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                        Picture = table.Column<string>(
                            type: "nvarchar(500)",
                            maxLength: 500,
                            nullable: false
                        ),
                        Code = table.Column<string>(
                            type: "nvarchar(100)",
                            maxLength: 100,
                            nullable: false,
                            computedColumnSql: "CONCAT(\r\n                'ATL', \r\n                CAST(\r\n                    SUBSTRING(\r\n                        CONVERT(VARCHAR, [CreatedAt], 111), \r\n                        0, \r\n                        CHARINDEX('/', CONVERT(VARCHAR, [CreatedAt], 111)\r\n                    )\r\n                ) AS INTEGER) % 1000, \r\n                '.', \r\n                RIGHT('000000' + CAST([Id] % 1000000 AS NVARCHAR), 6)\r\n            )",
                            stored: true
                        ),
                        IdentityReference = table.Column<string>(
                            type: "nvarchar(max)",
                            nullable: false
                        )
                    },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lecturers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Lecturers_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id"
                    );
                }
            );

            migrationBuilder.CreateTable(
                name: "Classes",
                columns: table =>
                    new
                    {
                        Id = table
                            .Column<int>(type: "int", nullable: false)
                            .Annotation("SqlServer:Identity", "1, 1"),
                        Name = table.Column<string>(
                            type: "nvarchar(500)",
                            maxLength: 500,
                            nullable: false
                        ),
                        Description = table.Column<string>(
                            type: "nvarchar(1500)",
                            maxLength: 1500,
                            nullable: true
                        ),
                        Placement = table.Column<string>(
                            type: "nvarchar(500)",
                            maxLength: 500,
                            nullable: false
                        ),
                        StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                        EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                        Available = table.Column<bool>(type: "bit", nullable: true),
                        EnableAutomation = table.Column<bool>(type: "bit", nullable: false),
                        MaxLearner = table.Column<int>(type: "int", nullable: true),
                        SkillId = table.Column<int>(type: "int", nullable: true),
                        CreatedBy = table.Column<string>(
                            type: "nvarchar(500)",
                            maxLength: 500,
                            nullable: false
                        ),
                        UpdatedBy = table.Column<string>(
                            type: "nvarchar(500)",
                            maxLength: 500,
                            nullable: false
                        ),
                        CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                        UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                    },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Classes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Classes_Skills_SkillId",
                        column: x => x.SkillId,
                        principalTable: "Skills",
                        principalColumn: "Id"
                    );
                }
            );

            migrationBuilder.CreateTable(
                name: "SkillCategories",
                columns: table =>
                    new
                    {
                        SkillId = table.Column<int>(type: "int", nullable: false),
                        CategoryId = table.Column<int>(type: "int", nullable: false),
                        CreatedBy = table.Column<string>(
                            type: "nvarchar(500)",
                            maxLength: 500,
                            nullable: false
                        ),
                        UpdatedBy = table.Column<string>(
                            type: "nvarchar(500)",
                            maxLength: 500,
                            nullable: false
                        ),
                        CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                        UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                    },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SkillCategories", x => new { x.SkillId, x.CategoryId });
                    table.ForeignKey(
                        name: "FK_SkillCategories_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                    table.ForeignKey(
                        name: "FK_SkillCategories_Skills_SkillId",
                        column: x => x.SkillId,
                        principalTable: "Skills",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateTable(
                name: "ClassLecturers",
                columns: table =>
                    new
                    {
                        ClassId = table.Column<int>(type: "int", nullable: false),
                        LecturerId = table.Column<int>(type: "int", nullable: false),
                        CreatedBy = table.Column<string>(
                            type: "nvarchar(500)",
                            maxLength: 500,
                            nullable: false
                        ),
                        UpdatedBy = table.Column<string>(
                            type: "nvarchar(500)",
                            maxLength: 500,
                            nullable: false
                        ),
                        CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                        UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                    },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassLecturers", x => new { x.ClassId, x.LecturerId });
                    table.ForeignKey(
                        name: "FK_ClassLecturers_Classes_ClassId",
                        column: x => x.ClassId,
                        principalTable: "Classes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                    table.ForeignKey(
                        name: "FK_ClassLecturers_Lecturers_LecturerId",
                        column: x => x.LecturerId,
                        principalTable: "Lecturers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateTable(
                name: "Schedules",
                columns: table =>
                    new
                    {
                        Id = table
                            .Column<int>(type: "int", nullable: false)
                            .Annotation("SqlServer:Identity", "1, 1"),
                        InitialSlotId = table.Column<int>(type: "int", nullable: true),
                        SlotId = table.Column<int>(type: "int", nullable: false),
                        ClassId = table.Column<int>(type: "int", nullable: false),
                        CreatedBy = table.Column<string>(
                            type: "nvarchar(500)",
                            maxLength: 500,
                            nullable: false
                        ),
                        UpdatedBy = table.Column<string>(
                            type: "nvarchar(500)",
                            maxLength: 500,
                            nullable: false
                        ),
                        CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                        UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                    },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schedules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Schedules_Classes_ClassId",
                        column: x => x.ClassId,
                        principalTable: "Classes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                    table.ForeignKey(
                        name: "FK_Schedules_Slots_SlotId",
                        column: x => x.SlotId,
                        principalTable: "Slots",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateTable(
                name: "SkillReports",
                columns: table =>
                    new
                    {
                        Id = table
                            .Column<int>(type: "int", nullable: false)
                            .Annotation("SqlServer:Identity", "1, 1"),
                        Level = table.Column<int>(type: "int", nullable: true),
                        Status = table.Column<int>(type: "int", nullable: false),
                        Editable = table.Column<bool>(type: "bit", nullable: false),
                        Metadata = table.Column<string>(type: "nvarchar(max)", nullable: true),
                        ClassId = table.Column<int>(type: "int", nullable: true),
                        StudentId = table.Column<int>(type: "int", nullable: false),
                        CreatedBy = table.Column<string>(
                            type: "nvarchar(500)",
                            maxLength: 500,
                            nullable: false
                        ),
                        UpdatedBy = table.Column<string>(
                            type: "nvarchar(500)",
                            maxLength: 500,
                            nullable: false
                        ),
                        CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                        UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                    },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SkillReports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SkillReports_Classes_ClassId",
                        column: x => x.ClassId,
                        principalTable: "Classes",
                        principalColumn: "Id"
                    );
                    table.ForeignKey(
                        name: "FK_SkillReports_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateTable(
                name: "Attendances",
                columns: table =>
                    new
                    {
                        Id = table
                            .Column<int>(type: "int", nullable: false)
                            .Annotation("SqlServer:Identity", "1, 1"),
                        AttendDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                        Status = table.Column<int>(type: "int", nullable: false),
                        ScheduleId = table.Column<int>(type: "int", nullable: false),
                        StudentId = table.Column<int>(type: "int", nullable: false),
                        CreatedBy = table.Column<string>(
                            type: "nvarchar(500)",
                            maxLength: 500,
                            nullable: false
                        ),
                        UpdatedBy = table.Column<string>(
                            type: "nvarchar(500)",
                            maxLength: 500,
                            nullable: false
                        ),
                        CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                        UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                    },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attendances", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Attendances_Schedules_ScheduleId",
                        column: x => x.ScheduleId,
                        principalTable: "Schedules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                    table.ForeignKey(
                        name: "FK_Attendances_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateIndex(
                name: "IX_Attendances_ScheduleId",
                table: "Attendances",
                column: "ScheduleId"
            );

            migrationBuilder.CreateIndex(
                name: "IX_Attendances_StudentId",
                table: "Attendances",
                column: "StudentId"
            );

            migrationBuilder.CreateIndex(
                name: "IX_Categories_Name",
                table: "Categories",
                column: "Name",
                unique: true
            );

            migrationBuilder.CreateIndex(
                name: "IX_Classes_SkillId",
                table: "Classes",
                column: "SkillId"
            );

            migrationBuilder.CreateIndex(
                name: "IX_ClassLecturers_LecturerId",
                table: "ClassLecturers",
                column: "LecturerId"
            );

            migrationBuilder.CreateIndex(
                name: "IX_Lecturers_Code",
                table: "Lecturers",
                column: "Code",
                unique: true
            );

            migrationBuilder.CreateIndex(
                name: "IX_Lecturers_DepartmentId",
                table: "Lecturers",
                column: "DepartmentId"
            );

            migrationBuilder.CreateIndex(
                name: "IX_Operators_Code",
                table: "Operators",
                column: "Code",
                unique: true
            );

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_ClassId",
                table: "Schedules",
                column: "ClassId"
            );

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_SlotId",
                table: "Schedules",
                column: "SlotId"
            );

            migrationBuilder.CreateIndex(
                name: "IX_SkillCategories_CategoryId",
                table: "SkillCategories",
                column: "CategoryId"
            );

            migrationBuilder.CreateIndex(
                name: "IX_SkillReports_ClassId",
                table: "SkillReports",
                column: "ClassId"
            );

            migrationBuilder.CreateIndex(
                name: "IX_SkillReports_StudentId",
                table: "SkillReports",
                column: "StudentId"
            );

            migrationBuilder.CreateIndex(
                name: "IX_Skills_Name",
                table: "Skills",
                column: "Name",
                unique: true
            );

            migrationBuilder.CreateIndex(
                name: "IX_Slots_DayOfWeek_StartAt_EndAt",
                table: "Slots",
                columns: new[] { "DayOfWeek", "StartAt", "EndAt" },
                unique: true
            );

            migrationBuilder.CreateIndex(
                name: "IX_Students_Code",
                table: "Students",
                column: "Code",
                unique: true
            );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "Attendances");

            migrationBuilder.DropTable(name: "ClassLecturers");

            migrationBuilder.DropTable(name: "Operators");

            migrationBuilder.DropTable(name: "SkillCategories");

            migrationBuilder.DropTable(name: "SkillReports");

            migrationBuilder.DropTable(name: "Schedules");

            migrationBuilder.DropTable(name: "Lecturers");

            migrationBuilder.DropTable(name: "Categories");

            migrationBuilder.DropTable(name: "Students");

            migrationBuilder.DropTable(name: "Classes");

            migrationBuilder.DropTable(name: "Slots");

            migrationBuilder.DropTable(name: "Departments");

            migrationBuilder.DropTable(name: "Skills");
        }
    }
}

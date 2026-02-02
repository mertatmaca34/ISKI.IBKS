using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ISKI.IBKS.Persistence.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AlarmDefinitions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    SensorName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    MinThreshold = table.Column<double>(type: "float", nullable: true),
                    MaxThreshold = table.Column<double>(type: "float", nullable: true),
                    ExpectedDigitalValue = table.Column<bool>(type: "bit", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Priority = table.Column<int>(type: "int", nullable: false),
                    CreatedAtUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAtUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedAtUtc = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlarmDefinitions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AlarmEvents",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AlarmDefinitionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OccurredAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ResolvedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TriggerValue = table.Column<double>(type: "float", nullable: true),
                    SensorName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Message = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    NotificationSent = table.Column<bool>(type: "bit", nullable: false),
                    NotificationSentAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Priority = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlarmEvents", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AlarmUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Department = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    ReceiveEmailNotifications = table.Column<bool>(type: "bit", nullable: false),
                    MinimumPriorityLevel = table.Column<int>(type: "int", nullable: false),
                    CreatedAtUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAtUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedAtUtc = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlarmUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AlarmUserSubscriptions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AlarmDefinitionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AlarmUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlarmUserSubscriptions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Calibrations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DbColumnName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CalibrationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ZeroRef = table.Column<double>(type: "float", nullable: false),
                    ZeroMeas = table.Column<double>(type: "float", nullable: false),
                    ZeroDiff = table.Column<double>(type: "float", nullable: false),
                    ZeroStd = table.Column<double>(type: "float", nullable: false),
                    SpanRef = table.Column<double>(type: "float", nullable: false),
                    SpanMeas = table.Column<double>(type: "float", nullable: false),
                    SpanDiff = table.Column<double>(type: "float", nullable: false),
                    SpanStd = table.Column<double>(type: "float", nullable: false),
                    ResultFactor = table.Column<double>(type: "float", nullable: false),
                    ResultZero = table.Column<bool>(type: "bit", nullable: false),
                    ResultSpan = table.Column<bool>(type: "bit", nullable: false),
                    Result = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAtUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAtUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedAtUtc = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Calibrations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ChannelInformations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Brand = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    BrandModel = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    FullName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Parameter = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ParameterText = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UnitId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UnitText = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    ChannelMinValue = table.Column<double>(type: "float", nullable: false),
                    ChannelMaxValue = table.Column<double>(type: "float", nullable: false),
                    ChannelNumber = table.Column<short>(type: "smallint", nullable: false),
                    CalibrationFormulaA = table.Column<double>(type: "float", nullable: false),
                    CalibrationFormulaB = table.Column<double>(type: "float", nullable: false),
                    SerialNumber = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChannelInformations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DiagnosticTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DiagnosticTypeNo = table.Column<int>(type: "int", nullable: false),
                    DiagnosticTypeName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DiagnosticLevel = table.Column<int>(type: "int", nullable: false),
                    DiagnosticLevelTitle = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiagnosticTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LogEntries",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LogTitle = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    LogDescription = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Level = table.Column<int>(type: "int", nullable: false),
                    Category = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogEntries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PowerOffTimes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsSentToSais = table.Column<bool>(type: "bit", nullable: false),
                    SentToSaisAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedAtUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAtUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedAtUtc = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PowerOffTimes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SampleRequests",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SampleCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TriggerParameter = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    StartedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CompletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TriggerType = table.Column<int>(type: "int", nullable: false),
                    ErrorMessage = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CreatedAtUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAtUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedAtUtc = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SampleRequests", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SensorDatas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReadTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Period = table.Column<int>(type: "int", nullable: false),
                    SoftwareVersion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TesisDebi = table.Column<double>(type: "float", nullable: true),
                    TesisDebi_Status = table.Column<int>(type: "int", nullable: true),
                    AkisHizi = table.Column<double>(type: "float", nullable: true),
                    AkisHizi_Status = table.Column<int>(type: "int", nullable: true),
                    Ph = table.Column<double>(type: "float", nullable: true),
                    Ph_Status = table.Column<int>(type: "int", nullable: true),
                    Iletkenlik = table.Column<double>(type: "float", nullable: true),
                    Iletkenlik_Status = table.Column<int>(type: "int", nullable: true),
                    CozunmusOksijen = table.Column<double>(type: "float", nullable: true),
                    CozunmusOksijen_Status = table.Column<int>(type: "int", nullable: true),
                    Koi = table.Column<double>(type: "float", nullable: true),
                    Koi_Status = table.Column<int>(type: "int", nullable: true),
                    Akm = table.Column<double>(type: "float", nullable: true),
                    Akm_Status = table.Column<int>(type: "int", nullable: true),
                    Sicaklik = table.Column<double>(type: "float", nullable: true),
                    Sicaklik_Status = table.Column<int>(type: "int", nullable: true),
                    DesarjDebi = table.Column<double>(type: "float", nullable: true),
                    DesarjDebi_Status = table.Column<int>(type: "int", nullable: true),
                    HariciDebi = table.Column<double>(type: "float", nullable: true),
                    HariciDebi_Status = table.Column<int>(type: "int", nullable: true),
                    HariciDebi2 = table.Column<double>(type: "float", nullable: true),
                    HariciDebi2_Status = table.Column<int>(type: "int", nullable: true),
                    IsSentToSais = table.Column<bool>(type: "bit", nullable: false),
                    SentToSaisAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedAtUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAtUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedAtUtc = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SensorDatas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StationSettings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    DataPeriodMinute = table.Column<int>(type: "int", nullable: false),
                    LastDataDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ConnectionDomainAddress = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    ConnectionPort = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    ConnectionUser = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ConnectionPassword = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Company = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SetupDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Software = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    PlcIpAddress = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PlcRack = table.Column<int>(type: "int", nullable: false),
                    PlcSlot = table.Column<int>(type: "int", nullable: false),
                    PhZeroDuration = table.Column<int>(type: "int", nullable: false),
                    PhZeroReference = table.Column<double>(type: "float", nullable: false),
                    PhSpanDuration = table.Column<int>(type: "int", nullable: false),
                    PhSpanReference = table.Column<double>(type: "float", nullable: false),
                    ConductivityZeroDuration = table.Column<int>(type: "int", nullable: false),
                    ConductivityZeroReference = table.Column<double>(type: "float", nullable: false),
                    ConductivitySpanDuration = table.Column<int>(type: "int", nullable: false),
                    ConductivitySpanReference = table.Column<double>(type: "float", nullable: false),
                    AkmZeroDuration = table.Column<int>(type: "int", nullable: false),
                    AkmZeroReference = table.Column<double>(type: "float", nullable: false),
                    KoiZeroDuration = table.Column<int>(type: "int", nullable: false),
                    KoiZeroReference = table.Column<double>(type: "float", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StationSettings", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AlarmDefinitions_IsActive",
                table: "AlarmDefinitions",
                column: "IsActive");

            migrationBuilder.CreateIndex(
                name: "IX_AlarmDefinitions_SensorName",
                table: "AlarmDefinitions",
                column: "SensorName");

            migrationBuilder.CreateIndex(
                name: "IX_AlarmEvents_NotificationSent",
                table: "AlarmEvents",
                column: "NotificationSent");

            migrationBuilder.CreateIndex(
                name: "IX_AlarmEvents_ResolvedAt",
                table: "AlarmEvents",
                column: "ResolvedAt");

            migrationBuilder.CreateIndex(
                name: "IX_AlarmEvents_StationId_OccurredAt",
                table: "AlarmEvents",
                columns: new[] { "StationId", "OccurredAt" });

            migrationBuilder.CreateIndex(
                name: "IX_AlarmUsers_Email",
                table: "AlarmUsers",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AlarmUsers_IsActive",
                table: "AlarmUsers",
                column: "IsActive");

            migrationBuilder.CreateIndex(
                name: "IX_AlarmUserSubscriptions_AlarmDefinitionId_AlarmUserId",
                table: "AlarmUserSubscriptions",
                columns: new[] { "AlarmDefinitionId", "AlarmUserId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AlarmUserSubscriptions_IsActive",
                table: "AlarmUserSubscriptions",
                column: "IsActive");

            migrationBuilder.CreateIndex(
                name: "IX_ChannelInformations_StationId",
                table: "ChannelInformations",
                column: "StationId");

            migrationBuilder.CreateIndex(
                name: "IX_ChannelInformations_StationId_Parameter",
                table: "ChannelInformations",
                columns: new[] { "StationId", "Parameter" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DiagnosticTypes_DiagnosticTypeNo",
                table: "DiagnosticTypes",
                column: "DiagnosticTypeNo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_LogEntries_Category",
                table: "LogEntries",
                column: "Category");

            migrationBuilder.CreateIndex(
                name: "IX_LogEntries_Level",
                table: "LogEntries",
                column: "Level");

            migrationBuilder.CreateIndex(
                name: "IX_LogEntries_StationId_CreatedAt",
                table: "LogEntries",
                columns: new[] { "StationId", "CreatedAt" });

            migrationBuilder.CreateIndex(
                name: "IX_PowerOffTimes_IsSentToSais",
                table: "PowerOffTimes",
                column: "IsSentToSais");

            migrationBuilder.CreateIndex(
                name: "IX_PowerOffTimes_StationId_StartDate",
                table: "PowerOffTimes",
                columns: new[] { "StationId", "StartDate" });

            migrationBuilder.CreateIndex(
                name: "IX_SampleRequests_SampleCode",
                table: "SampleRequests",
                column: "SampleCode");

            migrationBuilder.CreateIndex(
                name: "IX_SampleRequests_StationId_StartedAt",
                table: "SampleRequests",
                columns: new[] { "StationId", "StartedAt" });

            migrationBuilder.CreateIndex(
                name: "IX_SampleRequests_Status",
                table: "SampleRequests",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_SensorDatas_IsSentToSais",
                table: "SensorDatas",
                column: "IsSentToSais");

            migrationBuilder.CreateIndex(
                name: "IX_SensorDatas_StationId_ReadTime",
                table: "SensorDatas",
                columns: new[] { "StationId", "ReadTime" });

            migrationBuilder.CreateIndex(
                name: "IX_StationSettings_StationId",
                table: "StationSettings",
                column: "StationId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AlarmDefinitions");

            migrationBuilder.DropTable(
                name: "AlarmEvents");

            migrationBuilder.DropTable(
                name: "AlarmUsers");

            migrationBuilder.DropTable(
                name: "AlarmUserSubscriptions");

            migrationBuilder.DropTable(
                name: "Calibrations");

            migrationBuilder.DropTable(
                name: "ChannelInformations");

            migrationBuilder.DropTable(
                name: "DiagnosticTypes");

            migrationBuilder.DropTable(
                name: "LogEntries");

            migrationBuilder.DropTable(
                name: "PowerOffTimes");

            migrationBuilder.DropTable(
                name: "SampleRequests");

            migrationBuilder.DropTable(
                name: "SensorDatas");

            migrationBuilder.DropTable(
                name: "StationSettings");
        }
    }
}


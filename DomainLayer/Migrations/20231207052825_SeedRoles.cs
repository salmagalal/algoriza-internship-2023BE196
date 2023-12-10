using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DomainLayer.Migrations
{
    public partial class SeedRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"INSERT INTO AspNetRoles (Id, Name, ConcurrencyStamp, NormalizedName)
                                   VALUES (1, 'Admin', 1, 'ADMIN');
                                    
                                   INSERT INTO AspNetRoles (Id, Name, ConcurrencyStamp, NormalizedName)
                                   VALUES (2, 'Patient', 2, 'PATIENT')
    
                                    INSERT INTO AspNetRoles (Id, Name, ConcurrencyStamp, NormalizedName)
                                   VALUES (3, 'Doctor', 3, 'DOCTOR')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM AspNetRoles;");
        }
    }
}

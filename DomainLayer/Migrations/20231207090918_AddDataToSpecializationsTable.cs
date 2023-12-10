using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DomainLayer.Migrations
{
    public partial class AddDataToSpecializationsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
            INSERT INTO Specializations (Name) VALUES
            ('Cardiology'),
            ('Dermatology'),
            ('Endocrinology'),
            ('Gastroenterology'),
            ('Hematology'),
            ('Infectious Disease'),
            ('Internal Medicine'),
            ('Neurology'),
            ('Obstetrics and Gynecology'),
            ('Oncology'),
            ('Ophthalmology'),
            ('Orthopedics'),
            ('Otolaryngology'),
            ('Pediatrics'),
            ('Physical Medicine and Rehabilitation'),
            ('Psychiatry'),
            ('Pulmonology'),
            ('Radiology'),
            ('Rheumatology'),
            ('Urology'),
            ('Allergy and Immunology'),
            ('Anesthesiology'),
            ('Cardiothoracic Surgery'),
            ('Colorectal Surgery'),
            ('Critical Care Medicine'),
            ('Dental Medicine'),
            ('Emergency Medicine'),
            ('Family Medicine'),
            ('Genetics'),
            ('Geriatrics'),
            ('Nephrology'),
            ('Pain Medicine'),
            ('Pathology'),
            ('Physical Therapy'),
            ('Plastic Surgery'),
            ('Podiatry'),
            ('Preventive Medicine'),
            ('Sleep Medicine'),
            ('Sports Medicine'),
            ('Transplant Surgery');
        ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Specializations;");
        }
    }
}

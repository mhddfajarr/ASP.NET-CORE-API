using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Latihan.Migrations
{
    /// <inheritdoc />
    public partial class fixtypofirstnameatEmployee : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_accounts_employees_NIK",
                table: "accounts");

            migrationBuilder.DropForeignKey(
                name: "FK_education_Universities_University_Id",
                table: "education");

            migrationBuilder.DropForeignKey(
                name: "FK_profilings_accounts_NIK",
                table: "profilings");

            migrationBuilder.DropForeignKey(
                name: "FK_profilings_education_Education_Id",
                table: "profilings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_profilings",
                table: "profilings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_employees",
                table: "employees");

            migrationBuilder.DropPrimaryKey(
                name: "PK_education",
                table: "education");

            migrationBuilder.DropPrimaryKey(
                name: "PK_accounts",
                table: "accounts");

            migrationBuilder.RenameTable(
                name: "profilings",
                newName: "Profilings");

            migrationBuilder.RenameTable(
                name: "employees",
                newName: "Employees");

            migrationBuilder.RenameTable(
                name: "education",
                newName: "Education");

            migrationBuilder.RenameTable(
                name: "accounts",
                newName: "Accounts");

            migrationBuilder.RenameIndex(
                name: "IX_profilings_Education_Id",
                table: "Profilings",
                newName: "IX_Profilings_Education_Id");

            migrationBuilder.RenameColumn(
                name: "FisrtName",
                table: "Employees",
                newName: "FirstName");

            migrationBuilder.RenameIndex(
                name: "IX_education_University_Id",
                table: "Education",
                newName: "IX_Education_University_Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Profilings",
                table: "Profilings",
                column: "NIK");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Employees",
                table: "Employees",
                column: "NIK");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Education",
                table: "Education",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Accounts",
                table: "Accounts",
                column: "NIK");

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_Employees_NIK",
                table: "Accounts",
                column: "NIK",
                principalTable: "Employees",
                principalColumn: "NIK",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Education_Universities_University_Id",
                table: "Education",
                column: "University_Id",
                principalTable: "Universities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Profilings_Accounts_NIK",
                table: "Profilings",
                column: "NIK",
                principalTable: "Accounts",
                principalColumn: "NIK",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Profilings_Education_Education_Id",
                table: "Profilings",
                column: "Education_Id",
                principalTable: "Education",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_Employees_NIK",
                table: "Accounts");

            migrationBuilder.DropForeignKey(
                name: "FK_Education_Universities_University_Id",
                table: "Education");

            migrationBuilder.DropForeignKey(
                name: "FK_Profilings_Accounts_NIK",
                table: "Profilings");

            migrationBuilder.DropForeignKey(
                name: "FK_Profilings_Education_Education_Id",
                table: "Profilings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Profilings",
                table: "Profilings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Employees",
                table: "Employees");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Education",
                table: "Education");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Accounts",
                table: "Accounts");

            migrationBuilder.RenameTable(
                name: "Profilings",
                newName: "profilings");

            migrationBuilder.RenameTable(
                name: "Employees",
                newName: "employees");

            migrationBuilder.RenameTable(
                name: "Education",
                newName: "education");

            migrationBuilder.RenameTable(
                name: "Accounts",
                newName: "accounts");

            migrationBuilder.RenameIndex(
                name: "IX_Profilings_Education_Id",
                table: "profilings",
                newName: "IX_profilings_Education_Id");

            migrationBuilder.RenameColumn(
                name: "FirstName",
                table: "employees",
                newName: "FisrtName");

            migrationBuilder.RenameIndex(
                name: "IX_Education_University_Id",
                table: "education",
                newName: "IX_education_University_Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_profilings",
                table: "profilings",
                column: "NIK");

            migrationBuilder.AddPrimaryKey(
                name: "PK_employees",
                table: "employees",
                column: "NIK");

            migrationBuilder.AddPrimaryKey(
                name: "PK_education",
                table: "education",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_accounts",
                table: "accounts",
                column: "NIK");

            migrationBuilder.AddForeignKey(
                name: "FK_accounts_employees_NIK",
                table: "accounts",
                column: "NIK",
                principalTable: "employees",
                principalColumn: "NIK",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_education_Universities_University_Id",
                table: "education",
                column: "University_Id",
                principalTable: "Universities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_profilings_accounts_NIK",
                table: "profilings",
                column: "NIK",
                principalTable: "accounts",
                principalColumn: "NIK",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_profilings_education_Education_Id",
                table: "profilings",
                column: "Education_Id",
                principalTable: "education",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

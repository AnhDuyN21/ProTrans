using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructures.Migrations
{
    /// <inheritdoc />
    public partial class fix_TranslatorSkill : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TranslatorSkill_Account_AccountId",
                table: "TranslatorSkill");

            migrationBuilder.DropIndex(
                name: "IX_TranslatorSkill_AccountId",
                table: "TranslatorSkill");

            migrationBuilder.DeleteData(
                table: "DocumentType",
                keyColumn: "Id",
                keyValue: new Guid("315aec26-cb3e-4014-b7f2-5cef95b0381d"));

            migrationBuilder.DeleteData(
                table: "DocumentType",
                keyColumn: "Id",
                keyValue: new Guid("4141db79-970b-401d-a198-1393cef76b0a"));

            migrationBuilder.DeleteData(
                table: "DocumentType",
                keyColumn: "Id",
                keyValue: new Guid("98786af5-3618-4ec4-a134-050cab0c0024"));

            migrationBuilder.DeleteData(
                table: "Notarization",
                keyColumn: "Id",
                keyValue: new Guid("0fb48686-7545-4550-b2df-778732eb1853"));

            migrationBuilder.DeleteData(
                table: "Notarization",
                keyColumn: "Id",
                keyValue: new Guid("25c71041-98fa-45c4-a9cd-a873ace333d9"));

            migrationBuilder.DeleteData(
                table: "Notarization",
                keyColumn: "Id",
                keyValue: new Guid("2fe260a1-fbba-44cd-a47f-01dc74f1bffc"));

            migrationBuilder.DeleteData(
                table: "Notarization",
                keyColumn: "Id",
                keyValue: new Guid("7742571d-c2d0-46f5-ad7b-d68158b5cedb"));

            migrationBuilder.DeleteData(
                table: "Notarization",
                keyColumn: "Id",
                keyValue: new Guid("b58bd0eb-72a7-4124-b1e1-d69d5257e071"));

            migrationBuilder.DeleteData(
                table: "Notarization",
                keyColumn: "Id",
                keyValue: new Guid("e33e4892-336a-403e-9602-88eca01cef21"));

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("51c65112-eef3-4c70-92f5-18e1d4de3729"));

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("6e89ef27-2b78-4c87-9a6a-1053ffa18624"));

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("83af6c44-9176-450b-810d-d7582279ec99"));

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("845dbac8-e8ae-4830-8b04-a431133df643"));

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("dac0aea3-7102-43cc-9666-7b6d8006fb0e"));

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("e6042a33-9fad-4183-9426-9ba115adc0f5"));

            migrationBuilder.DropColumn(
                name: "AccountId",
                table: "TranslatorSkill");

            migrationBuilder.AlterColumn<string>(
                name: "ImageUrl",
                table: "Shipping",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.InsertData(
                table: "DocumentType",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "DeletedBy", "DeletedDate", "IsDeleted", "ModifiedBy", "ModifiedDate", "Name", "PriceFactor" },
                values: new object[,]
                {
                    { new Guid("7ceaff8b-18cf-4b6f-bb19-daf7ea8b9ef4"), null, null, null, null, false, null, null, "Khoa học", 200000m },
                    { new Guid("7fb21488-2b92-490c-aceb-b43fa44805a2"), null, null, null, null, false, null, null, "Hộ chiếu", 200000m },
                    { new Guid("f9c3988e-47ba-4763-88a6-c473f5013a0e"), null, null, null, null, false, null, null, "Trường học", 200000m }
                });

            migrationBuilder.InsertData(
                table: "Language",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "DeletedBy", "DeletedDate", "IsDeleted", "ModifiedBy", "ModifiedDate", "Name" },
                values: new object[,]
                {
                    { new Guid("0ef1b705-25cf-4d05-baa2-0391b3889d58"), null, null, null, null, false, null, null, "Tiếng Tây Ban Nha" },
                    { new Guid("13e36df4-8f4b-4c98-a952-7ab6641f0c6d"), null, null, null, null, false, null, null, "Tiếng Pháp" },
                    { new Guid("3cb55048-8c4a-4231-bd71-60831c09085c"), null, null, null, null, false, null, null, "Tiếng Anh" },
                    { new Guid("46d73931-feab-497b-af9d-d51539a57a2e"), null, null, null, null, false, null, null, "Tiếng Việt" },
                    { new Guid("a6ce089c-acab-41ce-9a98-989aa6c2834a"), null, null, null, null, false, null, null, "Tiếng Đức" },
                    { new Guid("ee56ec5c-81d0-4757-a4ea-3a74c9f1aebd"), null, null, null, null, false, null, null, "Tiếng Trung" }
                });

            migrationBuilder.InsertData(
                table: "Notarization",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "DeletedBy", "DeletedDate", "IsDeleted", "ModifiedBy", "ModifiedDate", "Name", "Price" },
                values: new object[,]
                {
                    { new Guid("19464660-1eaa-4835-a6b0-f9109ae3fed6"), null, null, null, null, false, null, null, "Công chứng xác nhận chữ ký", 500000m },
                    { new Guid("1bf6de62-6459-4b90-a11c-8294ba4dc85b"), null, null, null, null, false, null, null, "Công chứng bản dịch", 500000m },
                    { new Guid("a117c13f-e755-42b2-addf-5574dabb6b6a"), null, null, null, null, false, null, null, "Công chứng di chúc và thừa kế", 500000m },
                    { new Guid("cbbf64b6-facf-4f21-b9ee-dead85a65c67"), null, null, null, null, false, null, null, "Công chứng hợp đồng", 500000m },
                    { new Guid("d61b9ae8-966f-4aaa-951f-c1f816e0ed41"), null, null, null, null, false, null, null, "Công chứng bản sao", 500000m },
                    { new Guid("f71609e5-cf32-4b43-9d87-8cb61fe5f96c"), null, null, null, null, false, null, null, "Công chứng giấy ủy quyền", 500000m }
                });

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("196f8a40-6205-4067-a3a0-28764cfa7409"), "Admin" },
                    { new Guid("241abe41-ab00-4ca1-9f79-62e48971310f"), "Staff" },
                    { new Guid("25645374-3529-4a08-8e3f-6286216093e1"), "Translator" },
                    { new Guid("583207e5-2de0-4f0b-9eb3-a2e4c9cb004f"), "Manager" },
                    { new Guid("63a8f533-2315-4778-b5db-43e72f8f817c"), "Customer" },
                    { new Guid("acb5a241-649a-4ede-ae7f-4912ff70b9a2"), "Shipper" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_TranslatorSkill_TranslatorId",
                table: "TranslatorSkill",
                column: "TranslatorId");

            migrationBuilder.AddForeignKey(
                name: "FK_TranslatorSkill_Account_TranslatorId",
                table: "TranslatorSkill",
                column: "TranslatorId",
                principalTable: "Account",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TranslatorSkill_Account_TranslatorId",
                table: "TranslatorSkill");

            migrationBuilder.DropIndex(
                name: "IX_TranslatorSkill_TranslatorId",
                table: "TranslatorSkill");

            migrationBuilder.DeleteData(
                table: "DocumentType",
                keyColumn: "Id",
                keyValue: new Guid("7ceaff8b-18cf-4b6f-bb19-daf7ea8b9ef4"));

            migrationBuilder.DeleteData(
                table: "DocumentType",
                keyColumn: "Id",
                keyValue: new Guid("7fb21488-2b92-490c-aceb-b43fa44805a2"));

            migrationBuilder.DeleteData(
                table: "DocumentType",
                keyColumn: "Id",
                keyValue: new Guid("f9c3988e-47ba-4763-88a6-c473f5013a0e"));

            migrationBuilder.DeleteData(
                table: "Language",
                keyColumn: "Id",
                keyValue: new Guid("0ef1b705-25cf-4d05-baa2-0391b3889d58"));

            migrationBuilder.DeleteData(
                table: "Language",
                keyColumn: "Id",
                keyValue: new Guid("13e36df4-8f4b-4c98-a952-7ab6641f0c6d"));

            migrationBuilder.DeleteData(
                table: "Language",
                keyColumn: "Id",
                keyValue: new Guid("3cb55048-8c4a-4231-bd71-60831c09085c"));

            migrationBuilder.DeleteData(
                table: "Language",
                keyColumn: "Id",
                keyValue: new Guid("46d73931-feab-497b-af9d-d51539a57a2e"));

            migrationBuilder.DeleteData(
                table: "Language",
                keyColumn: "Id",
                keyValue: new Guid("a6ce089c-acab-41ce-9a98-989aa6c2834a"));

            migrationBuilder.DeleteData(
                table: "Language",
                keyColumn: "Id",
                keyValue: new Guid("ee56ec5c-81d0-4757-a4ea-3a74c9f1aebd"));

            migrationBuilder.DeleteData(
                table: "Notarization",
                keyColumn: "Id",
                keyValue: new Guid("19464660-1eaa-4835-a6b0-f9109ae3fed6"));

            migrationBuilder.DeleteData(
                table: "Notarization",
                keyColumn: "Id",
                keyValue: new Guid("1bf6de62-6459-4b90-a11c-8294ba4dc85b"));

            migrationBuilder.DeleteData(
                table: "Notarization",
                keyColumn: "Id",
                keyValue: new Guid("a117c13f-e755-42b2-addf-5574dabb6b6a"));

            migrationBuilder.DeleteData(
                table: "Notarization",
                keyColumn: "Id",
                keyValue: new Guid("cbbf64b6-facf-4f21-b9ee-dead85a65c67"));

            migrationBuilder.DeleteData(
                table: "Notarization",
                keyColumn: "Id",
                keyValue: new Guid("d61b9ae8-966f-4aaa-951f-c1f816e0ed41"));

            migrationBuilder.DeleteData(
                table: "Notarization",
                keyColumn: "Id",
                keyValue: new Guid("f71609e5-cf32-4b43-9d87-8cb61fe5f96c"));

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("196f8a40-6205-4067-a3a0-28764cfa7409"));

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("241abe41-ab00-4ca1-9f79-62e48971310f"));

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("25645374-3529-4a08-8e3f-6286216093e1"));

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("583207e5-2de0-4f0b-9eb3-a2e4c9cb004f"));

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("63a8f533-2315-4778-b5db-43e72f8f817c"));

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("acb5a241-649a-4ede-ae7f-4912ff70b9a2"));

            migrationBuilder.AddColumn<Guid>(
                name: "AccountId",
                table: "TranslatorSkill",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ImageUrl",
                table: "Shipping",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "DocumentType",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "DeletedBy", "DeletedDate", "IsDeleted", "ModifiedBy", "ModifiedDate", "Name", "PriceFactor" },
                values: new object[,]
                {
                    { new Guid("315aec26-cb3e-4014-b7f2-5cef95b0381d"), null, null, null, null, false, null, null, "Hộ chiếu", 200000m },
                    { new Guid("4141db79-970b-401d-a198-1393cef76b0a"), null, null, null, null, false, null, null, "Khoa học", 200000m },
                    { new Guid("98786af5-3618-4ec4-a134-050cab0c0024"), null, null, null, null, false, null, null, "Trường học", 200000m }
                });

            migrationBuilder.InsertData(
                table: "Notarization",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "DeletedBy", "DeletedDate", "IsDeleted", "ModifiedBy", "ModifiedDate", "Name", "Price" },
                values: new object[,]
                {
                    { new Guid("0fb48686-7545-4550-b2df-778732eb1853"), null, null, null, null, false, null, null, "Công chứng bản sao", 500000m },
                    { new Guid("25c71041-98fa-45c4-a9cd-a873ace333d9"), null, null, null, null, false, null, null, "Công chứng xác nhận chữ ký", 500000m },
                    { new Guid("2fe260a1-fbba-44cd-a47f-01dc74f1bffc"), null, null, null, null, false, null, null, "Công chứng giấy ủy quyền", 500000m },
                    { new Guid("7742571d-c2d0-46f5-ad7b-d68158b5cedb"), null, null, null, null, false, null, null, "Công chứng hợp đồng", 500000m },
                    { new Guid("b58bd0eb-72a7-4124-b1e1-d69d5257e071"), null, null, null, null, false, null, null, "Công chứng di chúc và thừa kế", 500000m },
                    { new Guid("e33e4892-336a-403e-9602-88eca01cef21"), null, null, null, null, false, null, null, "Công chứng bản dịch", 500000m }
                });

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("51c65112-eef3-4c70-92f5-18e1d4de3729"), "Shipper" },
                    { new Guid("6e89ef27-2b78-4c87-9a6a-1053ffa18624"), "Staff" },
                    { new Guid("83af6c44-9176-450b-810d-d7582279ec99"), "Translator" },
                    { new Guid("845dbac8-e8ae-4830-8b04-a431133df643"), "Admin" },
                    { new Guid("dac0aea3-7102-43cc-9666-7b6d8006fb0e"), "Manager" },
                    { new Guid("e6042a33-9fad-4183-9426-9ba115adc0f5"), "Customer" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_TranslatorSkill_AccountId",
                table: "TranslatorSkill",
                column: "AccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_TranslatorSkill_Account_AccountId",
                table: "TranslatorSkill",
                column: "AccountId",
                principalTable: "Account",
                principalColumn: "Id");
        }
    }
}

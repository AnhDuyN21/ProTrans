using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructures.Migrations
{
    /// <inheritdoc />
    public partial class fix_db : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "DocumentType",
                keyColumn: "Id",
                keyValue: new Guid("7bca2768-c67f-4d3f-87b7-e27e0c7b8bf2"));

            migrationBuilder.DeleteData(
                table: "DocumentType",
                keyColumn: "Id",
                keyValue: new Guid("9db1a475-5476-40b4-9466-d862496e9a9c"));

            migrationBuilder.DeleteData(
                table: "DocumentType",
                keyColumn: "Id",
                keyValue: new Guid("b8702a1e-1173-4c5a-afbd-033b2062e0a6"));

            migrationBuilder.DeleteData(
                table: "Notarization",
                keyColumn: "Id",
                keyValue: new Guid("07153cb3-fd82-4ce1-a4bc-3603c7188d10"));

            migrationBuilder.DeleteData(
                table: "Notarization",
                keyColumn: "Id",
                keyValue: new Guid("28e45ecc-303b-40c8-8453-381811155da7"));

            migrationBuilder.DeleteData(
                table: "Notarization",
                keyColumn: "Id",
                keyValue: new Guid("79c1e3d1-979c-48c2-8ba2-bef80de5b63a"));

            migrationBuilder.DeleteData(
                table: "Notarization",
                keyColumn: "Id",
                keyValue: new Guid("f7e82201-3151-49c6-a3e8-514268665921"));

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("2d640b79-ccb3-4129-9b6c-4c95029e4563"));

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("3c10b9c5-7867-4149-8550-19e82685fb57"));

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("440ea7fe-38e2-4a72-a7e6-021e2b2f70e6"));

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("53109532-6e9a-428b-b10f-97bebec1b74d"));

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("788c75fb-2955-4b4f-a05f-1dab678141af"));

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("c22dac42-992b-402a-b678-608e62bfb584"));

            migrationBuilder.RenameColumn(
                name: "NumberOfNotarizatedCopies",
                table: "Document",
                newName: "NumberOfNotarizedCopies");

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.RenameColumn(
                name: "NumberOfNotarizedCopies",
                table: "Document",
                newName: "NumberOfNotarizatedCopies");

            migrationBuilder.InsertData(
                table: "DocumentType",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "DeletedBy", "DeletedDate", "IsDeleted", "ModifiedBy", "ModifiedDate", "Name", "PriceFactor" },
                values: new object[,]
                {
                    { new Guid("7bca2768-c67f-4d3f-87b7-e27e0c7b8bf2"), null, null, null, null, false, null, null, "Khoa học", 200000m },
                    { new Guid("9db1a475-5476-40b4-9466-d862496e9a9c"), null, null, null, null, false, null, null, "Hộ chiếu", 200000m },
                    { new Guid("b8702a1e-1173-4c5a-afbd-033b2062e0a6"), null, null, null, null, false, null, null, "Trường học", 200000m }
                });

            migrationBuilder.InsertData(
                table: "Notarization",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "DeletedBy", "DeletedDate", "IsDeleted", "ModifiedBy", "ModifiedDate", "Name", "Price" },
                values: new object[,]
                {
                    { new Guid("07153cb3-fd82-4ce1-a4bc-3603c7188d10"), null, null, null, null, false, null, null, "Công chứng bản dịch tiếng Trung", 500000m },
                    { new Guid("28e45ecc-303b-40c8-8453-381811155da7"), null, null, null, null, false, null, null, "Công chứng bản dịch tiếng Nhật", 500000m },
                    { new Guid("79c1e3d1-979c-48c2-8ba2-bef80de5b63a"), null, null, null, null, false, null, null, "Công chứng bản dịch tiếng Anh", 500000m },
                    { new Guid("f7e82201-3151-49c6-a3e8-514268665921"), null, null, null, null, false, null, null, "Công chứng bản dịch tiếng Pháp", 500000m }
                });

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("2d640b79-ccb3-4129-9b6c-4c95029e4563"), "Shipper" },
                    { new Guid("3c10b9c5-7867-4149-8550-19e82685fb57"), "Customer" },
                    { new Guid("440ea7fe-38e2-4a72-a7e6-021e2b2f70e6"), "Manager" },
                    { new Guid("53109532-6e9a-428b-b10f-97bebec1b74d"), "Admin" },
                    { new Guid("788c75fb-2955-4b4f-a05f-1dab678141af"), "Staff" },
                    { new Guid("c22dac42-992b-402a-b678-608e62bfb584"), "Translator" }
                });
        }
    }
}

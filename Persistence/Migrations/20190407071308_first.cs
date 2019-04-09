using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class first : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Commands",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Price = table.Column<double>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Commands", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "InvoiceProvider",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    IdProvider = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceProvider", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "InvoiceProviderIngredient",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 500, nullable: false),
                    Quantity = table.Column<int>(nullable: false),
                    Price = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceProviderIngredient", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Invoices",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    Price = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoices", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MenuIngredient",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 500, nullable: false),
                    Quantity = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuIngredient", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Menus",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 500, nullable: false),
                    Price = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Menus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProviderIngredient",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 500, nullable: false),
                    Quantity = table.Column<int>(nullable: false),
                    Price = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProviderIngredient", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Providers",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Providers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Stores",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 500, nullable: false),
                    Quantity = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stores", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "InvoiceProviderIngredients",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    IdProvider = table.Column<Guid>(nullable: false),
                    ProviderId = table.Column<Guid>(nullable: true),
                    IdIngredient = table.Column<Guid>(nullable: false),
                    IngredientId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceProviderIngredients", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InvoiceProviderIngredients_InvoiceProviderIngredient_IngredientId",
                        column: x => x.IngredientId,
                        principalTable: "InvoiceProviderIngredient",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InvoiceProviderIngredients_InvoiceProvider_ProviderId",
                        column: x => x.ProviderId,
                        principalTable: "InvoiceProvider",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "InvoiceProviders",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    IdInvoice = table.Column<Guid>(nullable: false),
                    InvoiceId = table.Column<Guid>(nullable: true),
                    IdProvider = table.Column<Guid>(nullable: false),
                    ProviderId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceProviders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InvoiceProviders_Invoices_InvoiceId",
                        column: x => x.InvoiceId,
                        principalTable: "Invoices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InvoiceProviders_InvoiceProvider_ProviderId",
                        column: x => x.ProviderId,
                        principalTable: "InvoiceProvider",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CommandMenus",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    IdMenu = table.Column<Guid>(nullable: false),
                    MenuId = table.Column<Guid>(nullable: true),
                    IdCommand = table.Column<Guid>(nullable: false),
                    CommandId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommandMenus", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CommandMenus_Commands_CommandId",
                        column: x => x.CommandId,
                        principalTable: "Commands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CommandMenus_Menus_MenuId",
                        column: x => x.MenuId,
                        principalTable: "Menus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MenuIngredients",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    IdMenu = table.Column<Guid>(nullable: false),
                    MenuId = table.Column<Guid>(nullable: true),
                    IdIngredient = table.Column<Guid>(nullable: false),
                    IngredientId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuIngredients", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MenuIngredients_MenuIngredient_IngredientId",
                        column: x => x.IngredientId,
                        principalTable: "MenuIngredient",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MenuIngredients_Menus_MenuId",
                        column: x => x.MenuId,
                        principalTable: "Menus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProviderIngredients",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    IdProvider = table.Column<Guid>(nullable: false),
                    ProviderId = table.Column<Guid>(nullable: true),
                    IdIngredient = table.Column<Guid>(nullable: false),
                    IngredientId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProviderIngredients", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProviderIngredients_ProviderIngredient_IngredientId",
                        column: x => x.IngredientId,
                        principalTable: "ProviderIngredient",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProviderIngredients_Providers_ProviderId",
                        column: x => x.ProviderId,
                        principalTable: "Providers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CommandMenus_CommandId",
                table: "CommandMenus",
                column: "CommandId");

            migrationBuilder.CreateIndex(
                name: "IX_CommandMenus_MenuId",
                table: "CommandMenus",
                column: "MenuId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceProviderIngredients_IngredientId",
                table: "InvoiceProviderIngredients",
                column: "IngredientId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceProviderIngredients_ProviderId",
                table: "InvoiceProviderIngredients",
                column: "ProviderId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceProviders_InvoiceId",
                table: "InvoiceProviders",
                column: "InvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceProviders_ProviderId",
                table: "InvoiceProviders",
                column: "ProviderId");

            migrationBuilder.CreateIndex(
                name: "IX_MenuIngredients_IngredientId",
                table: "MenuIngredients",
                column: "IngredientId");

            migrationBuilder.CreateIndex(
                name: "IX_MenuIngredients_MenuId",
                table: "MenuIngredients",
                column: "MenuId");

            migrationBuilder.CreateIndex(
                name: "IX_ProviderIngredients_IngredientId",
                table: "ProviderIngredients",
                column: "IngredientId");

            migrationBuilder.CreateIndex(
                name: "IX_ProviderIngredients_ProviderId",
                table: "ProviderIngredients",
                column: "ProviderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CommandMenus");

            migrationBuilder.DropTable(
                name: "InvoiceProviderIngredients");

            migrationBuilder.DropTable(
                name: "InvoiceProviders");

            migrationBuilder.DropTable(
                name: "MenuIngredients");

            migrationBuilder.DropTable(
                name: "ProviderIngredients");

            migrationBuilder.DropTable(
                name: "Stores");

            migrationBuilder.DropTable(
                name: "Commands");

            migrationBuilder.DropTable(
                name: "InvoiceProviderIngredient");

            migrationBuilder.DropTable(
                name: "Invoices");

            migrationBuilder.DropTable(
                name: "InvoiceProvider");

            migrationBuilder.DropTable(
                name: "MenuIngredient");

            migrationBuilder.DropTable(
                name: "Menus");

            migrationBuilder.DropTable(
                name: "ProviderIngredient");

            migrationBuilder.DropTable(
                name: "Providers");
        }
    }
}

using Catalog.Host.Data.Entities;

namespace Catalog.Host.Data;

public static class DbInitializer
{
    public static async Task Initialize(ApplicationDbContext context)
    {
        await context.Database.EnsureCreatedAsync();

        if (!context.CatalogBrands.Any())
        {
            await context.CatalogBrands.AddRangeAsync(GetPreconfiguredCatalogBrands());

            await context.SaveChangesAsync();
        }

        if (!context.CatalogTypes.Any())
        {
            await context.CatalogTypes.AddRangeAsync(GetPreconfiguredCatalogTypes());

            await context.SaveChangesAsync();
        }

        if (!context.CatalogItems.Any())
        {
            await context.CatalogItems.AddRangeAsync(GetPreconfiguredItems());

            await context.SaveChangesAsync();
        }
    }

	private static IEnumerable<CatalogBrand> GetPreconfiguredCatalogBrands()
	{
		return new List<CatalogBrand>()
		{
			new CatalogBrand() { Brand = "Roshen" },
			new CatalogBrand() { Brand = "Lukas" },
			new CatalogBrand() { Brand = "LaPasta" },
			new CatalogBrand() { Brand = "Fanta" },
			new CatalogBrand() { Brand = "Coca-Cola" },
			new CatalogBrand() { Brand = "Rollton" },
			new CatalogBrand() { Brand = "Lays" },
			new CatalogBrand() { Brand = "Pringles" },
		};
	}

	private static IEnumerable<CatalogType> GetPreconfiguredCatalogTypes()
	{
		return new List<CatalogType>()
		{
			new CatalogType() { Type = "Chocolate" },
			new CatalogType() { Type = "Milk" },
			new CatalogType() { Type = "Candy" },
			new CatalogType() { Type = "Cake" },
			new CatalogType() { Type = "Pasta" },
			new CatalogType() { Type = "Drink" },
			new CatalogType() { Type = "Chips" },
		};
	}

	private static IEnumerable<CatalogItem> GetPreconfiguredItems()
	{
		return new List<CatalogItem>()
		{
			new CatalogItem { CatalogTypeId = 1, CatalogBrandId = 1, AvailableStock = 45, Description = "Chocolate with nuts and milk. Weight: 200g", Name = "Lacmi", Price = 65, PictureFileName = "Lacmi_chocolate.png" },
			new CatalogItem { CatalogTypeId = 1, CatalogBrandId = 1, AvailableStock = 325, Description = "Chocolate Roshen Dark with whole Hazelnut 90g", Name = "Chocolate Roshen Dark", Price = 65, PictureFileName = "chocolate_roshen_dark_90g.png" },
			new CatalogItem { CatalogTypeId = 2, CatalogBrandId = 1, AvailableStock = 24, Description = "The volume is 200 ml.", Name = "Pasteurized milk", Price = 55, PictureFileName = "pasteurized_milk.png" },
			new CatalogItem { CatalogTypeId = 6, CatalogBrandId = 5, AvailableStock = 142, Description = "Common the Coca-Cola. The volume is 2 L.", Name = "Coca-Cola", Price = 50, PictureFileName = "Coca-Cola.png" },
			new CatalogItem { CatalogTypeId = 6, CatalogBrandId = 5, AvailableStock = 43, Description = "Coca-Cola which hasn't sugar. The volume is 500 ml.", Name = "Coca-Cola zero", Price = 23, PictureFileName = "Coca-Cola zero.png" },
			new CatalogItem { CatalogTypeId = 6, CatalogBrandId = 4, AvailableStock = 10, Description = "Fanta which hasn't sugar. The volume is 500 ml.", Name = "Fanta zero", Price = 30, PictureFileName = "fanta_zero.png" },
			new CatalogItem { CatalogTypeId = 6, CatalogBrandId = 4, AvailableStock = 76, Description = "Common a Fanta. The volume is 1 L.", Name = "Fanta", Price = 21, PictureFileName = "common_fanta.png" },
			new CatalogItem { CatalogTypeId = 5, CatalogBrandId = 3, AvailableStock = 123, Description = "Common a pasta. Weight: 500g", Name = "La Past", Price = 71, PictureFileName = "la_pasta.png" },
			new CatalogItem { CatalogTypeId = 5, CatalogBrandId = 6, AvailableStock = 187, Description = "Rollton with taste a chicken. Weight: 300g", Name = "Rollton hot chicken", Price = 20, PictureFileName = "rollton_chicken.png" },
			new CatalogItem { CatalogTypeId = 7, CatalogBrandId = 7, AvailableStock = 98, Description = "Grilled Cheese & Tomato Soup", Name = "Grilled Cheese & Tomato Soup", Price = 64, PictureFileName = "grilled_cheese_&_tomato_soup.png" },
			new CatalogItem { CatalogTypeId = 7, CatalogBrandId = 8, AvailableStock = 139, Description = "Grilled Cheese & Tomato Soup", Name = "Grilled Cheese & Tomato Soup", Price = 64, PictureFileName = "grilled_cheese_&_tomato_soup.png" },
		};
	}
}
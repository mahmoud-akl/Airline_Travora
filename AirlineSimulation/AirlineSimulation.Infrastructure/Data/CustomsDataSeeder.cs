using AirlineSimulation.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AirlineSimulation.Infrastructure.Data;

public static class CustomsDataSeeder
{
    public static async Task SeedCustomsDataAsync(AirlineDbContext context)
    {
        if (await context.Categories.AnyAsync()) return; // Already seeded

        // ==================== ELECTRONICS ====================
        var electronics = new Category { Name = "Electronics" };
        context.Categories.Add(electronics);
        await context.SaveChangesAsync();

        var mobilePhones = new SubCategory { CategoryId = electronics.CategoryId, Name = "Mobile Phones" };
        var laptops = new SubCategory { CategoryId = electronics.CategoryId, Name = "Laptops" };
        var tablets = new SubCategory { CategoryId = electronics.CategoryId, Name = "Tablets" };
        var headphones = new SubCategory { CategoryId = electronics.CategoryId, Name = "Headphones" };
        var smartWatches = new SubCategory { CategoryId = electronics.CategoryId, Name = "Smart Watches" };
        var chargersAndCables = new SubCategory { CategoryId = electronics.CategoryId, Name = "Chargers and Cables" };
        var tvs = new SubCategory { CategoryId = electronics.CategoryId, Name = "TVs" };
        var cameras = new SubCategory { CategoryId = electronics.CategoryId, Name = "Cameras" };
        
        context.SubCategories.AddRange(mobilePhones, laptops, tablets, headphones, smartWatches, chargersAndCables, tvs, cameras);
        await context.SaveChangesAsync();

        context.Products.AddRange(
            // Mobile Phones
            new Product { SubCategoryId = mobilePhones.SubCategoryId, Name = "iPhone", CustomsRate = 0.03m },
            new Product { SubCategoryId = mobilePhones.SubCategoryId, Name = "Samsung Galaxy", CustomsRate = 0.025m },
            new Product { SubCategoryId = mobilePhones.SubCategoryId, Name = "Xiaomi", CustomsRate = 0.02m },
            new Product { SubCategoryId = mobilePhones.SubCategoryId, Name = "Huawei", CustomsRate = 0.02m },
            new Product { SubCategoryId = mobilePhones.SubCategoryId, Name = "Nokia", CustomsRate = 0.015m },
            new Product { SubCategoryId = mobilePhones.SubCategoryId, Name = "Oppo", CustomsRate = 0.018m },
            new Product { SubCategoryId = mobilePhones.SubCategoryId, Name = "Realme", CustomsRate = 0.018m },
            
            // Laptops
            new Product { SubCategoryId = laptops.SubCategoryId, Name = "MacBook", CustomsRate = 0.05m },
            new Product { SubCategoryId = laptops.SubCategoryId, Name = "Dell", CustomsRate = 0.04m },
            new Product { SubCategoryId = laptops.SubCategoryId, Name = "HP", CustomsRate = 0.04m },
            new Product { SubCategoryId = laptops.SubCategoryId, Name = "Lenovo", CustomsRate = 0.035m },
            new Product { SubCategoryId = laptops.SubCategoryId, Name = "Asus", CustomsRate = 0.038m },
            new Product { SubCategoryId = laptops.SubCategoryId, Name = "Acer", CustomsRate = 0.033m },
            new Product { SubCategoryId = laptops.SubCategoryId, Name = "MSI", CustomsRate = 0.045m },
            
            // Tablets
            new Product { SubCategoryId = tablets.SubCategoryId, Name = "iPad", CustomsRate = 0.04m },
            new Product { SubCategoryId = tablets.SubCategoryId, Name = "Samsung Galaxy Tab", CustomsRate = 0.035m },
            new Product { SubCategoryId = tablets.SubCategoryId, Name = "Huawei MatePad", CustomsRate = 0.03m },
            new Product { SubCategoryId = tablets.SubCategoryId, Name = "Lenovo Tab", CustomsRate = 0.028m },
            new Product { SubCategoryId = tablets.SubCategoryId, Name = "Microsoft Surface", CustomsRate = 0.045m },
            
            // Headphones
            new Product { SubCategoryId = headphones.SubCategoryId, Name = "AirPods", CustomsRate = 0.02m },
            new Product { SubCategoryId = headphones.SubCategoryId, Name = "Sony Headphones", CustomsRate = 0.018m },
            new Product { SubCategoryId = headphones.SubCategoryId, Name = "JBL Headphones", CustomsRate = 0.015m },
            new Product { SubCategoryId = headphones.SubCategoryId, Name = "Bose Headphones", CustomsRate = 0.02m },
            new Product { SubCategoryId = headphones.SubCategoryId, Name = "Beats", CustomsRate = 0.02m },
            new Product { SubCategoryId = headphones.SubCategoryId, Name = "Anker Soundcore", CustomsRate = 0.014m },
            
            // Smart Watches
            new Product { SubCategoryId = smartWatches.SubCategoryId, Name = "Apple Watch", CustomsRate = 0.03m },
            new Product { SubCategoryId = smartWatches.SubCategoryId, Name = "Samsung Galaxy Watch", CustomsRate = 0.028m },
            new Product { SubCategoryId = smartWatches.SubCategoryId, Name = "Huawei Watch", CustomsRate = 0.025m },
            new Product { SubCategoryId = smartWatches.SubCategoryId, Name = "Xiaomi Mi Band", CustomsRate = 0.02m },
            new Product { SubCategoryId = smartWatches.SubCategoryId, Name = "Garmin", CustomsRate = 0.03m },
            
            // Chargers and Cables
            new Product { SubCategoryId = chargersAndCables.SubCategoryId, Name = "USB-C Charger", CustomsRate = 0.01m },
            new Product { SubCategoryId = chargersAndCables.SubCategoryId, Name = "Lightning Cable", CustomsRate = 0.01m },
            new Product { SubCategoryId = chargersAndCables.SubCategoryId, Name = "USB-C Cable", CustomsRate = 0.008m },
            new Product { SubCategoryId = chargersAndCables.SubCategoryId, Name = "Wireless Charger", CustomsRate = 0.012m },
            new Product { SubCategoryId = chargersAndCables.SubCategoryId, Name = "Power Bank", CustomsRate = 0.015m },
            new Product { SubCategoryId = chargersAndCables.SubCategoryId, Name = "Car Charger", CustomsRate = 0.01m },
            
            // TVs
            new Product { SubCategoryId = tvs.SubCategoryId, Name = "Samsung TV", CustomsRate = 0.06m },
            new Product { SubCategoryId = tvs.SubCategoryId, Name = "LG TV", CustomsRate = 0.055m },
            new Product { SubCategoryId = tvs.SubCategoryId, Name = "Sony TV", CustomsRate = 0.06m },
            new Product { SubCategoryId = tvs.SubCategoryId, Name = "TCL TV", CustomsRate = 0.05m },
            new Product { SubCategoryId = tvs.SubCategoryId, Name = "Hisense TV", CustomsRate = 0.05m },
            new Product { SubCategoryId = tvs.SubCategoryId, Name = "Panasonic TV", CustomsRate = 0.055m },
            
            // Cameras
            new Product { SubCategoryId = cameras.SubCategoryId, Name = "Canon Camera", CustomsRate = 0.05m },
            new Product { SubCategoryId = cameras.SubCategoryId, Name = "Nikon Camera", CustomsRate = 0.05m },
            new Product { SubCategoryId = cameras.SubCategoryId, Name = "Sony Camera", CustomsRate = 0.05m },
            new Product { SubCategoryId = cameras.SubCategoryId, Name = "Fujifilm Camera", CustomsRate = 0.045m },
            new Product { SubCategoryId = cameras.SubCategoryId, Name = "GoPro", CustomsRate = 0.04m }
        );

        // ==================== CLOTHING ====================
        var clothing = new Category { Name = "Clothing" };
        context.Categories.Add(clothing);
        await context.SaveChangesAsync();

        var menClothing = new SubCategory { CategoryId = clothing.CategoryId, Name = "Men Clothing" };
        var womenClothing = new SubCategory { CategoryId = clothing.CategoryId, Name = "Women Clothing" };
        var kidsClothing = new SubCategory { CategoryId = clothing.CategoryId, Name = "Kids Clothing" };
        var jacketsAndCoats = new SubCategory { CategoryId = clothing.CategoryId, Name = "Jackets and Coats" };
        var sportswear = new SubCategory { CategoryId = clothing.CategoryId, Name = "Sportswear" };
        var shoes = new SubCategory { CategoryId = clothing.CategoryId, Name = "Shoes" };
        var accessoriesBeltsHats = new SubCategory { CategoryId = clothing.CategoryId, Name = "Accessories Belts Hats" };
        
        context.SubCategories.AddRange(menClothing, womenClothing, kidsClothing, jacketsAndCoats, sportswear, shoes, accessoriesBeltsHats);
        await context.SaveChangesAsync();

        context.Products.AddRange(
            // Men Clothing
            new Product { SubCategoryId = menClothing.SubCategoryId, Name = "T-Shirts", CustomsRate = 0.1m },
            new Product { SubCategoryId = menClothing.SubCategoryId, Name = "Shirts", CustomsRate = 0.11m },
            new Product { SubCategoryId = menClothing.SubCategoryId, Name = "Jeans", CustomsRate = 0.12m },
            new Product { SubCategoryId = menClothing.SubCategoryId, Name = "Suits", CustomsRate = 0.15m },
            new Product { SubCategoryId = menClothing.SubCategoryId, Name = "Casual Pants", CustomsRate = 0.12m },
            
            // Women Clothing
            new Product { SubCategoryId = womenClothing.SubCategoryId, Name = "Dresses", CustomsRate = 0.14m },
            new Product { SubCategoryId = womenClothing.SubCategoryId, Name = "Tops", CustomsRate = 0.12m },
            new Product { SubCategoryId = womenClothing.SubCategoryId, Name = "Skirts", CustomsRate = 0.13m },
            new Product { SubCategoryId = womenClothing.SubCategoryId, Name = "Blouses", CustomsRate = 0.12m },
            new Product { SubCategoryId = womenClothing.SubCategoryId, Name = "Abayas", CustomsRate = 0.1m },
            
            // Kids Clothing
            new Product { SubCategoryId = kidsClothing.SubCategoryId, Name = "Baby Sets", CustomsRate = 0.08m },
            new Product { SubCategoryId = kidsClothing.SubCategoryId, Name = "Boys Clothing", CustomsRate = 0.09m },
            new Product { SubCategoryId = kidsClothing.SubCategoryId, Name = "Girls Clothing", CustomsRate = 0.09m },
            new Product { SubCategoryId = kidsClothing.SubCategoryId, Name = "School Uniforms", CustomsRate = 0.07m },
            
            // Jackets and Coats
            new Product { SubCategoryId = jacketsAndCoats.SubCategoryId, Name = "Leather Jackets", CustomsRate = 0.18m },
            new Product { SubCategoryId = jacketsAndCoats.SubCategoryId, Name = "Winter Coats", CustomsRate = 0.16m },
            new Product { SubCategoryId = jacketsAndCoats.SubCategoryId, Name = "Hoodies", CustomsRate = 0.12m },
            new Product { SubCategoryId = jacketsAndCoats.SubCategoryId, Name = "Blazers", CustomsRate = 0.15m },
            
            // Sportswear
            new Product { SubCategoryId = sportswear.SubCategoryId, Name = "Tracksuits", CustomsRate = 0.12m },
            new Product { SubCategoryId = sportswear.SubCategoryId, Name = "Gym Wear", CustomsRate = 0.12m },
            new Product { SubCategoryId = sportswear.SubCategoryId, Name = "Swimwear", CustomsRate = 0.14m },
            new Product { SubCategoryId = sportswear.SubCategoryId, Name = "Yoga Wear", CustomsRate = 0.12m },
            
            // Shoes
            new Product { SubCategoryId = shoes.SubCategoryId, Name = "Sneakers", CustomsRate = 0.18m },
            new Product { SubCategoryId = shoes.SubCategoryId, Name = "Formal Shoes", CustomsRate = 0.2m },
            new Product { SubCategoryId = shoes.SubCategoryId, Name = "Sandals", CustomsRate = 0.16m },
            new Product { SubCategoryId = shoes.SubCategoryId, Name = "Boots", CustomsRate = 0.22m },
            new Product { SubCategoryId = shoes.SubCategoryId, Name = "Slippers", CustomsRate = 0.1m },
            
            // Accessories Belts Hats
            new Product { SubCategoryId = accessoriesBeltsHats.SubCategoryId, Name = "Belts", CustomsRate = 0.12m },
            new Product { SubCategoryId = accessoriesBeltsHats.SubCategoryId, Name = "Hats", CustomsRate = 0.1m },
            new Product { SubCategoryId = accessoriesBeltsHats.SubCategoryId, Name = "Caps", CustomsRate = 0.1m },
            new Product { SubCategoryId = accessoriesBeltsHats.SubCategoryId, Name = "Scarves", CustomsRate = 0.12m },
            new Product { SubCategoryId = accessoriesBeltsHats.SubCategoryId, Name = "Sunglasses", CustomsRate = 0.15m }
        );

        // ==================== FOOD ====================
        var food = new Category { Name = "Food" };
        context.Categories.Add(food);
        await context.SaveChangesAsync();

        var freshFood = new SubCategory { CategoryId = food.CategoryId, Name = "Fresh Food" };
        var frozenFood = new SubCategory { CategoryId = food.CategoryId, Name = "Frozen Food" };
        var snacks = new SubCategory { CategoryId = food.CategoryId, Name = "Snacks" };
        var beverages = new SubCategory { CategoryId = food.CategoryId, Name = "Beverages" };
        var dairyProducts = new SubCategory { CategoryId = food.CategoryId, Name = "Dairy Products" };
        var bakery = new SubCategory { CategoryId = food.CategoryId, Name = "Bakery" };
        var cannedFood = new SubCategory { CategoryId = food.CategoryId, Name = "Canned Food" };
        
        context.SubCategories.AddRange(freshFood, frozenFood, snacks, beverages, dairyProducts, bakery, cannedFood);
        await context.SaveChangesAsync();

        context.Products.AddRange(
            // Fresh Food
            new Product { SubCategoryId = freshFood.SubCategoryId, Name = "Fruits", CustomsRate = 0.0m },
            new Product { SubCategoryId = freshFood.SubCategoryId, Name = "Vegetables", CustomsRate = 0.0m },
            new Product { SubCategoryId = freshFood.SubCategoryId, Name = "Meat", CustomsRate = 0.02m },
            new Product { SubCategoryId = freshFood.SubCategoryId, Name = "Chicken", CustomsRate = 0.02m },
            new Product { SubCategoryId = freshFood.SubCategoryId, Name = "Fish", CustomsRate = 0.02m },
            
            // Frozen Food
            new Product { SubCategoryId = frozenFood.SubCategoryId, Name = "Frozen Vegetables", CustomsRate = 0.01m },
            new Product { SubCategoryId = frozenFood.SubCategoryId, Name = "Frozen Meat", CustomsRate = 0.02m },
            new Product { SubCategoryId = frozenFood.SubCategoryId, Name = "Ice Cream", CustomsRate = 0.05m },
            new Product { SubCategoryId = frozenFood.SubCategoryId, Name = "Ready Meals", CustomsRate = 0.04m },
            
            // Snacks
            new Product { SubCategoryId = snacks.SubCategoryId, Name = "Chips", CustomsRate = 0.08m },
            new Product { SubCategoryId = snacks.SubCategoryId, Name = "Biscuits", CustomsRate = 0.07m },
            new Product { SubCategoryId = snacks.SubCategoryId, Name = "Chocolate", CustomsRate = 0.12m },
            new Product { SubCategoryId = snacks.SubCategoryId, Name = "Nuts", CustomsRate = 0.06m },
            
            // Beverages
            new Product { SubCategoryId = beverages.SubCategoryId, Name = "Soft Drinks", CustomsRate = 0.1m },
            new Product { SubCategoryId = beverages.SubCategoryId, Name = "Juices", CustomsRate = 0.08m },
            new Product { SubCategoryId = beverages.SubCategoryId, Name = "Energy Drinks", CustomsRate = 0.12m },
            new Product { SubCategoryId = beverages.SubCategoryId, Name = "Tea", CustomsRate = 0.04m },
            new Product { SubCategoryId = beverages.SubCategoryId, Name = "Coffee", CustomsRate = 0.05m },
            
            // Dairy Products
            new Product { SubCategoryId = dairyProducts.SubCategoryId, Name = "Milk", CustomsRate = 0.0m },
            new Product { SubCategoryId = dairyProducts.SubCategoryId, Name = "Cheese", CustomsRate = 0.03m },
            new Product { SubCategoryId = dairyProducts.SubCategoryId, Name = "Yogurt", CustomsRate = 0.01m },
            new Product { SubCategoryId = dairyProducts.SubCategoryId, Name = "Butter", CustomsRate = 0.02m },
            
            // Bakery
            new Product { SubCategoryId = bakery.SubCategoryId, Name = "Bread", CustomsRate = 0.0m },
            new Product { SubCategoryId = bakery.SubCategoryId, Name = "Cakes", CustomsRate = 0.06m },
            new Product { SubCategoryId = bakery.SubCategoryId, Name = "Pastries", CustomsRate = 0.06m },
            new Product { SubCategoryId = bakery.SubCategoryId, Name = "Croissants", CustomsRate = 0.06m },
            
            // Canned Food
            new Product { SubCategoryId = cannedFood.SubCategoryId, Name = "Canned Beans", CustomsRate = 0.02m },
            new Product { SubCategoryId = cannedFood.SubCategoryId, Name = "Tuna", CustomsRate = 0.03m },
            new Product { SubCategoryId = cannedFood.SubCategoryId, Name = "Corn", CustomsRate = 0.02m },
            new Product { SubCategoryId = cannedFood.SubCategoryId, Name = "Tomato Paste", CustomsRate = 0.02m }
        );

        // ==================== JEWELRY ====================
        var jewelry = new Category { Name = "Jewelry" };
        context.Categories.Add(jewelry);
        await context.SaveChangesAsync();

        var rings = new SubCategory { CategoryId = jewelry.CategoryId, Name = "Rings" };
        var necklaces = new SubCategory { CategoryId = jewelry.CategoryId, Name = "Necklaces" };
        var bracelets = new SubCategory { CategoryId = jewelry.CategoryId, Name = "Bracelets" };
        var earrings = new SubCategory { CategoryId = jewelry.CategoryId, Name = "Earrings" };
        var watches = new SubCategory { CategoryId = jewelry.CategoryId, Name = "Watches" };
        var goldJewelry = new SubCategory { CategoryId = jewelry.CategoryId, Name = "Gold Jewelry" };
        var silverJewelry = new SubCategory { CategoryId = jewelry.CategoryId, Name = "Silver Jewelry" };
        
        context.SubCategories.AddRange(rings, necklaces, bracelets, earrings, watches, goldJewelry, silverJewelry);
        await context.SaveChangesAsync();

        context.Products.AddRange(
            // Rings
            new Product { SubCategoryId = rings.SubCategoryId, Name = "Gold Ring", CustomsRate = 0.05m },
            new Product { SubCategoryId = rings.SubCategoryId, Name = "Silver Ring", CustomsRate = 0.04m },
            new Product { SubCategoryId = rings.SubCategoryId, Name = "Diamond Ring", CustomsRate = 0.06m },
            
            // Necklaces
            new Product { SubCategoryId = necklaces.SubCategoryId, Name = "Gold Necklace", CustomsRate = 0.05m },
            new Product { SubCategoryId = necklaces.SubCategoryId, Name = "Silver Necklace", CustomsRate = 0.04m },
            new Product { SubCategoryId = necklaces.SubCategoryId, Name = "Pearl Necklace", CustomsRate = 0.05m },
            
            // Bracelets
            new Product { SubCategoryId = bracelets.SubCategoryId, Name = "Charm Bracelet", CustomsRate = 0.04m },
            new Product { SubCategoryId = bracelets.SubCategoryId, Name = "Gold Bracelet", CustomsRate = 0.05m },
            new Product { SubCategoryId = bracelets.SubCategoryId, Name = "Silver Bracelet", CustomsRate = 0.04m },
            
            // Earrings
            new Product { SubCategoryId = earrings.SubCategoryId, Name = "Stud Earrings", CustomsRate = 0.04m },
            new Product { SubCategoryId = earrings.SubCategoryId, Name = "Hoop Earrings", CustomsRate = 0.04m },
            new Product { SubCategoryId = earrings.SubCategoryId, Name = "Drop Earrings", CustomsRate = 0.045m },
            
            // Watches
            new Product { SubCategoryId = watches.SubCategoryId, Name = "Rolex", CustomsRate = 0.08m },
            new Product { SubCategoryId = watches.SubCategoryId, Name = "Casio", CustomsRate = 0.05m },
            new Product { SubCategoryId = watches.SubCategoryId, Name = "Fossil", CustomsRate = 0.06m },
            new Product { SubCategoryId = watches.SubCategoryId, Name = "Tissot", CustomsRate = 0.07m },
            
            // Gold Jewelry
            new Product { SubCategoryId = goldJewelry.SubCategoryId, Name = "18K Gold Set", CustomsRate = 0.06m },
            new Product { SubCategoryId = goldJewelry.SubCategoryId, Name = "21K Gold Set", CustomsRate = 0.07m },
            new Product { SubCategoryId = goldJewelry.SubCategoryId, Name = "24K Gold Bar", CustomsRate = 0.08m },
            
            // Silver Jewelry
            new Product { SubCategoryId = silverJewelry.SubCategoryId, Name = "Sterling Silver Set", CustomsRate = 0.05m },
            new Product { SubCategoryId = silverJewelry.SubCategoryId, Name = "Handmade Silver Piece", CustomsRate = 0.05m }
        );

        // ==================== COSMETICS ====================
        var cosmetics = new Category { Name = "Cosmetics" };
        context.Categories.Add(cosmetics);
        await context.SaveChangesAsync();

        var makeup = new SubCategory { CategoryId = cosmetics.CategoryId, Name = "Makeup" };
        var skincare = new SubCategory { CategoryId = cosmetics.CategoryId, Name = "Skincare" };
        var haircare = new SubCategory { CategoryId = cosmetics.CategoryId, Name = "Haircare" };
        var perfumes = new SubCategory { CategoryId = cosmetics.CategoryId, Name = "Perfumes" };
        var personalCare = new SubCategory { CategoryId = cosmetics.CategoryId, Name = "Personal Care" };
        var beautyTools = new SubCategory { CategoryId = cosmetics.CategoryId, Name = "Beauty Tools" };
        
        context.SubCategories.AddRange(makeup, skincare, haircare, perfumes, personalCare, beautyTools);
        await context.SaveChangesAsync();

        context.Products.AddRange(
            // Makeup
            new Product { SubCategoryId = makeup.SubCategoryId, Name = "Lipstick", CustomsRate = 0.12m },
            new Product { SubCategoryId = makeup.SubCategoryId, Name = "Foundation", CustomsRate = 0.12m },
            new Product { SubCategoryId = makeup.SubCategoryId, Name = "Mascara", CustomsRate = 0.12m },
            new Product { SubCategoryId = makeup.SubCategoryId, Name = "Eyeliner", CustomsRate = 0.12m },
            
            // Skincare
            new Product { SubCategoryId = skincare.SubCategoryId, Name = "Face Wash", CustomsRate = 0.08m },
            new Product { SubCategoryId = skincare.SubCategoryId, Name = "Moisturizer", CustomsRate = 0.08m },
            new Product { SubCategoryId = skincare.SubCategoryId, Name = "Sunscreen", CustomsRate = 0.08m },
            new Product { SubCategoryId = skincare.SubCategoryId, Name = "Serum", CustomsRate = 0.1m },
            
            // Haircare
            new Product { SubCategoryId = haircare.SubCategoryId, Name = "Shampoo", CustomsRate = 0.08m },
            new Product { SubCategoryId = haircare.SubCategoryId, Name = "Conditioner", CustomsRate = 0.08m },
            new Product { SubCategoryId = haircare.SubCategoryId, Name = "Hair Oil", CustomsRate = 0.1m },
            new Product { SubCategoryId = haircare.SubCategoryId, Name = "Hair Mask", CustomsRate = 0.1m },
            
            // Perfumes
            new Product { SubCategoryId = perfumes.SubCategoryId, Name = "Men Perfume", CustomsRate = 0.15m },
            new Product { SubCategoryId = perfumes.SubCategoryId, Name = "Women Perfume", CustomsRate = 0.15m },
            new Product { SubCategoryId = perfumes.SubCategoryId, Name = "Unisex Perfume", CustomsRate = 0.15m },
            
            // Personal Care
            new Product { SubCategoryId = personalCare.SubCategoryId, Name = "Body Lotion", CustomsRate = 0.08m },
            new Product { SubCategoryId = personalCare.SubCategoryId, Name = "Deodorant", CustomsRate = 0.1m },
            new Product { SubCategoryId = personalCare.SubCategoryId, Name = "Soap", CustomsRate = 0.05m },
            
            // Beauty Tools
            new Product { SubCategoryId = beautyTools.SubCategoryId, Name = "Makeup Brushes", CustomsRate = 0.06m },
            new Product { SubCategoryId = beautyTools.SubCategoryId, Name = "Hair Dryer", CustomsRate = 0.07m },
            new Product { SubCategoryId = beautyTools.SubCategoryId, Name = "Hair Straightener", CustomsRate = 0.07m }
        );

        // ==================== BOOKS ====================
        var books = new Category { Name = "Books" };
        context.Categories.Add(books);
        await context.SaveChangesAsync();

        var educationalBooks = new SubCategory { CategoryId = books.CategoryId, Name = "Educational Books" };
        var novels = new SubCategory { CategoryId = books.CategoryId, Name = "Novels" };
        var religiousBooks = new SubCategory { CategoryId = books.CategoryId, Name = "Religious Books" };
        var childrenBooks = new SubCategory { CategoryId = books.CategoryId, Name = "Children Books" };
        var scientificBooks = new SubCategory { CategoryId = books.CategoryId, Name = "Scientific Books" };
        var comics = new SubCategory { CategoryId = books.CategoryId, Name = "Comics" };
        var magazines = new SubCategory { CategoryId = books.CategoryId, Name = "Magazines" };
        
        context.SubCategories.AddRange(educationalBooks, novels, religiousBooks, childrenBooks, scientificBooks, comics, magazines);
        await context.SaveChangesAsync();

        context.Products.AddRange(
            // Educational Books
            new Product { SubCategoryId = educationalBooks.SubCategoryId, Name = "School Books", CustomsRate = 0.0m },
            new Product { SubCategoryId = educationalBooks.SubCategoryId, Name = "University Books", CustomsRate = 0.0m },
            new Product { SubCategoryId = educationalBooks.SubCategoryId, Name = "Language Learning", CustomsRate = 0.0m },
            
            // Novels
            new Product { SubCategoryId = novels.SubCategoryId, Name = "Romance Novel", CustomsRate = 0.0m },
            new Product { SubCategoryId = novels.SubCategoryId, Name = "Mystery Novel", CustomsRate = 0.0m },
            new Product { SubCategoryId = novels.SubCategoryId, Name = "Fantasy Novel", CustomsRate = 0.0m },
            
            // Religious Books
            new Product { SubCategoryId = religiousBooks.SubCategoryId, Name = "Quran", CustomsRate = 0.0m },
            new Product { SubCategoryId = religiousBooks.SubCategoryId, Name = "Bible", CustomsRate = 0.0m },
            new Product { SubCategoryId = religiousBooks.SubCategoryId, Name = "Hadith Books", CustomsRate = 0.0m },
            
            // Children Books
            new Product { SubCategoryId = childrenBooks.SubCategoryId, Name = "Story Books", CustomsRate = 0.0m },
            new Product { SubCategoryId = childrenBooks.SubCategoryId, Name = "Coloring Books", CustomsRate = 0.0m },
            
            // Scientific Books
            new Product { SubCategoryId = scientificBooks.SubCategoryId, Name = "Computer Science Book", CustomsRate = 0.0m },
            new Product { SubCategoryId = scientificBooks.SubCategoryId, Name = "Engineering Book", CustomsRate = 0.0m },
            new Product { SubCategoryId = scientificBooks.SubCategoryId, Name = "Medical Book", CustomsRate = 0.0m },
            
            // Comics
            new Product { SubCategoryId = comics.SubCategoryId, Name = "Marvel Comics", CustomsRate = 0.0m },
            new Product { SubCategoryId = comics.SubCategoryId, Name = "DC Comics", CustomsRate = 0.0m },
            new Product { SubCategoryId = comics.SubCategoryId, Name = "Manga", CustomsRate = 0.0m },
            
            // Magazines
            new Product { SubCategoryId = magazines.SubCategoryId, Name = "Fashion Magazine", CustomsRate = 0.0m },
            new Product { SubCategoryId = magazines.SubCategoryId, Name = "Technology Magazine", CustomsRate = 0.0m },
            new Product { SubCategoryId = magazines.SubCategoryId, Name = "Sports Magazine", CustomsRate = 0.0m }
        );

        // ==================== TOYS ====================
        var toys = new Category { Name = "Toys" };
        context.Categories.Add(toys);
        await context.SaveChangesAsync();

        var kidsToys = new SubCategory { CategoryId = toys.CategoryId, Name = "Kids Toys" };
        var educationalToys = new SubCategory { CategoryId = toys.CategoryId, Name = "Educational Toys" };
        var actionFigures = new SubCategory { CategoryId = toys.CategoryId, Name = "Action Figures" };
        var dolls = new SubCategory { CategoryId = toys.CategoryId, Name = "Dolls" };
        var boardGames = new SubCategory { CategoryId = toys.CategoryId, Name = "Board Games" };
        var outdoorToys = new SubCategory { CategoryId = toys.CategoryId, Name = "Outdoor Toys" };
        
        context.SubCategories.AddRange(kidsToys, educationalToys, actionFigures, dolls, boardGames, outdoorToys);
        await context.SaveChangesAsync();

        context.Products.AddRange(
            // Kids Toys
            new Product { SubCategoryId = kidsToys.SubCategoryId, Name = "Plush Toys", CustomsRate = 0.1m },
            new Product { SubCategoryId = kidsToys.SubCategoryId, Name = "Toy Cars", CustomsRate = 0.1m },
            new Product { SubCategoryId = kidsToys.SubCategoryId, Name = "Building Blocks", CustomsRate = 0.1m },
            
            // Educational Toys
            new Product { SubCategoryId = educationalToys.SubCategoryId, Name = "Puzzles", CustomsRate = 0.08m },
            new Product { SubCategoryId = educationalToys.SubCategoryId, Name = "Learning Boards", CustomsRate = 0.08m },
            new Product { SubCategoryId = educationalToys.SubCategoryId, Name = "STEM Toys", CustomsRate = 0.09m },
            
            // Action Figures
            new Product { SubCategoryId = actionFigures.SubCategoryId, Name = "Superhero Figures", CustomsRate = 0.12m },
            new Product { SubCategoryId = actionFigures.SubCategoryId, Name = "Anime Figures", CustomsRate = 0.12m },
            
            // Dolls
            new Product { SubCategoryId = dolls.SubCategoryId, Name = "Barbie", CustomsRate = 0.12m },
            new Product { SubCategoryId = dolls.SubCategoryId, Name = "Baby Dolls", CustomsRate = 0.12m },
            
            // Board Games
            new Product { SubCategoryId = boardGames.SubCategoryId, Name = "Chess", CustomsRate = 0.06m },
            new Product { SubCategoryId = boardGames.SubCategoryId, Name = "Monopoly", CustomsRate = 0.06m },
            new Product { SubCategoryId = boardGames.SubCategoryId, Name = "Ludo", CustomsRate = 0.06m },
            
            // Outdoor Toys
            new Product { SubCategoryId = outdoorToys.SubCategoryId, Name = "Bicycles", CustomsRate = 0.08m },
            new Product { SubCategoryId = outdoorToys.SubCategoryId, Name = "Scooters", CustomsRate = 0.08m },
            new Product { SubCategoryId = outdoorToys.SubCategoryId, Name = "Balls", CustomsRate = 0.05m }
        );

        // ==================== MEDICINE ====================
        var medicine = new Category { Name = "Medicine" };
        context.Categories.Add(medicine);
        await context.SaveChangesAsync();

        var prescriptionMedicine = new SubCategory { CategoryId = medicine.CategoryId, Name = "Prescription Medicine" };
        var otc = new SubCategory { CategoryId = medicine.CategoryId, Name = "OTC" };
        var vitaminsAndSupplements = new SubCategory { CategoryId = medicine.CategoryId, Name = "Vitamins and Supplements" };
        var medicalDevices = new SubCategory { CategoryId = medicine.CategoryId, Name = "Medical Devices" };
        var firstAid = new SubCategory { CategoryId = medicine.CategoryId, Name = "First Aid" };
        var personalHealthCare = new SubCategory { CategoryId = medicine.CategoryId, Name = "Personal Health Care" };
        
        context.SubCategories.AddRange(prescriptionMedicine, otc, vitaminsAndSupplements, medicalDevices, firstAid, personalHealthCare);
        await context.SaveChangesAsync();

        context.Products.AddRange(
            // Prescription Medicine
            new Product { SubCategoryId = prescriptionMedicine.SubCategoryId, Name = "Antibiotics", CustomsRate = 0.0m },
            new Product { SubCategoryId = prescriptionMedicine.SubCategoryId, Name = "Pain Killers", CustomsRate = 0.0m },
            new Product { SubCategoryId = prescriptionMedicine.SubCategoryId, Name = "Blood Pressure Medicine", CustomsRate = 0.0m },
            
            // OTC
            new Product { SubCategoryId = otc.SubCategoryId, Name = "Cold & Flu", CustomsRate = 0.0m },
            new Product { SubCategoryId = otc.SubCategoryId, Name = "Headache Relief", CustomsRate = 0.0m },
            new Product { SubCategoryId = otc.SubCategoryId, Name = "Antacid", CustomsRate = 0.0m },
            
            // Vitamins and Supplements
            new Product { SubCategoryId = vitaminsAndSupplements.SubCategoryId, Name = "Vitamin C", CustomsRate = 0.05m },
            new Product { SubCategoryId = vitaminsAndSupplements.SubCategoryId, Name = "Omega 3", CustomsRate = 0.05m },
            new Product { SubCategoryId = vitaminsAndSupplements.SubCategoryId, Name = "Protein Powder", CustomsRate = 0.06m },
            
            // Medical Devices
            new Product { SubCategoryId = medicalDevices.SubCategoryId, Name = "Blood Pressure Monitor", CustomsRate = 0.04m },
            new Product { SubCategoryId = medicalDevices.SubCategoryId, Name = "Glucometer", CustomsRate = 0.04m },
            new Product { SubCategoryId = medicalDevices.SubCategoryId, Name = "Nebulizer", CustomsRate = 0.04m },
            
            // First Aid
            new Product { SubCategoryId = firstAid.SubCategoryId, Name = "Bandages", CustomsRate = 0.0m },
            new Product { SubCategoryId = firstAid.SubCategoryId, Name = "Antiseptics", CustomsRate = 0.0m },
            new Product { SubCategoryId = firstAid.SubCategoryId, Name = "First Aid Kit", CustomsRate = 0.02m },
            
            // Personal Health Care
            new Product { SubCategoryId = personalHealthCare.SubCategoryId, Name = "Thermometer", CustomsRate = 0.02m },
            new Product { SubCategoryId = personalHealthCare.SubCategoryId, Name = "Face Masks", CustomsRate = 0.01m },
            new Product { SubCategoryId = personalHealthCare.SubCategoryId, Name = "Hand Sanitizer", CustomsRate = 0.03m }
        );

        // ==================== FURNITURE ====================
        var furniture = new Category { Name = "Furniture" };
        context.Categories.Add(furniture);
        await context.SaveChangesAsync();

        var livingRoomFurniture = new SubCategory { CategoryId = furniture.CategoryId, Name = "Living Room Furniture" };
        var bedroomFurniture = new SubCategory { CategoryId = furniture.CategoryId, Name = "Bedroom Furniture" };
        var officeFurniture = new SubCategory { CategoryId = furniture.CategoryId, Name = "Office Furniture" };
        var kitchenFurniture = new SubCategory { CategoryId = furniture.CategoryId, Name = "Kitchen Furniture" };
        var outdoorFurniture = new SubCategory { CategoryId = furniture.CategoryId, Name = "Outdoor Furniture" };
        var homeDecor = new SubCategory { CategoryId = furniture.CategoryId, Name = "Home Decor" };
        
        context.SubCategories.AddRange(livingRoomFurniture, bedroomFurniture, officeFurniture, kitchenFurniture, outdoorFurniture, homeDecor);
        await context.SaveChangesAsync();

        context.Products.AddRange(
            // Living Room Furniture
            new Product { SubCategoryId = livingRoomFurniture.SubCategoryId, Name = "Sofas", CustomsRate = 0.08m },
            new Product { SubCategoryId = livingRoomFurniture.SubCategoryId, Name = "Coffee Tables", CustomsRate = 0.06m },
            new Product { SubCategoryId = livingRoomFurniture.SubCategoryId, Name = "TV Units", CustomsRate = 0.07m },
            
            // Bedroom Furniture
            new Product { SubCategoryId = bedroomFurniture.SubCategoryId, Name = "Beds", CustomsRate = 0.08m },
            new Product { SubCategoryId = bedroomFurniture.SubCategoryId, Name = "Wardrobes", CustomsRate = 0.07m },
            new Product { SubCategoryId = bedroomFurniture.SubCategoryId, Name = "Nightstands", CustomsRate = 0.06m },
            
            // Office Furniture
            new Product { SubCategoryId = officeFurniture.SubCategoryId, Name = "Office Chairs", CustomsRate = 0.07m },
            new Product { SubCategoryId = officeFurniture.SubCategoryId, Name = "Desks", CustomsRate = 0.07m },
            new Product { SubCategoryId = officeFurniture.SubCategoryId, Name = "Book Shelves", CustomsRate = 0.06m },
            
            // Kitchen Furniture
            new Product { SubCategoryId = kitchenFurniture.SubCategoryId, Name = "Cabinets", CustomsRate = 0.06m },
            new Product { SubCategoryId = kitchenFurniture.SubCategoryId, Name = "Dining Tables", CustomsRate = 0.06m },
            
            // Outdoor Furniture
            new Product { SubCategoryId = outdoorFurniture.SubCategoryId, Name = "Garden Chairs", CustomsRate = 0.06m },
            new Product { SubCategoryId = outdoorFurniture.SubCategoryId, Name = "Outdoor Tables", CustomsRate = 0.06m },
            new Product { SubCategoryId = outdoorFurniture.SubCategoryId, Name = "Umbrellas", CustomsRate = 0.05m },
            
            // Home Decor
            new Product { SubCategoryId = homeDecor.SubCategoryId, Name = "Curtains", CustomsRate = 0.05m },
            new Product { SubCategoryId = homeDecor.SubCategoryId, Name = "Carpets", CustomsRate = 0.06m },
            new Product { SubCategoryId = homeDecor.SubCategoryId, Name = "Wall Art", CustomsRate = 0.04m }
        );

        // ==================== ART ====================
        var art = new Category { Name = "Art" };
        context.Categories.Add(art);
        await context.SaveChangesAsync();

        var paintings = new SubCategory { CategoryId = art.CategoryId, Name = "Paintings" };
        var sculptures = new SubCategory { CategoryId = art.CategoryId, Name = "Sculptures" };
        var handicrafts = new SubCategory { CategoryId = art.CategoryId, Name = "Handicrafts" };
        var digitalArt = new SubCategory { CategoryId = art.CategoryId, Name = "Digital Art" };
        var artSupplies = new SubCategory { CategoryId = art.CategoryId, Name = "Art Supplies" };
        var calligraphy = new SubCategory { CategoryId = art.CategoryId, Name = "Calligraphy" };
        
        context.SubCategories.AddRange(paintings, sculptures, handicrafts, digitalArt, artSupplies, calligraphy);
        await context.SaveChangesAsync();

        context.Products.AddRange(
            // Paintings
            new Product { SubCategoryId = paintings.SubCategoryId, Name = "Oil Painting", CustomsRate = 0.04m },
            new Product { SubCategoryId = paintings.SubCategoryId, Name = "Acrylic Painting", CustomsRate = 0.04m },
            new Product { SubCategoryId = paintings.SubCategoryId, Name = "Watercolor Painting", CustomsRate = 0.03m },
            
            // Sculptures
            new Product { SubCategoryId = sculptures.SubCategoryId, Name = "Stone Sculpture", CustomsRate = 0.05m },
            new Product { SubCategoryId = sculptures.SubCategoryId, Name = "Metal Sculpture", CustomsRate = 0.05m },
            new Product { SubCategoryId = sculptures.SubCategoryId, Name = "Wood Sculpture", CustomsRate = 0.045m },
            
            // Handicrafts
            new Product { SubCategoryId = handicrafts.SubCategoryId, Name = "Handmade Pottery", CustomsRate = 0.04m },
            new Product { SubCategoryId = handicrafts.SubCategoryId, Name = "Wooden Crafts", CustomsRate = 0.04m },
            new Product { SubCategoryId = handicrafts.SubCategoryId, Name = "Handmade Textile Art", CustomsRate = 0.04m },
            
            // Digital Art
            new Product { SubCategoryId = digitalArt.SubCategoryId, Name = "Digital Illustration", CustomsRate = 0.02m },
            new Product { SubCategoryId = digitalArt.SubCategoryId, Name = "3D Art", CustomsRate = 0.02m },
            new Product { SubCategoryId = digitalArt.SubCategoryId, Name = "Digital Prints", CustomsRate = 0.02m },
            
            // Art Supplies
            new Product { SubCategoryId = artSupplies.SubCategoryId, Name = "Paint Brushes", CustomsRate = 0.03m },
            new Product { SubCategoryId = artSupplies.SubCategoryId, Name = "Canvas", CustomsRate = 0.03m },
            new Product { SubCategoryId = artSupplies.SubCategoryId, Name = "Acrylic Paint Set", CustomsRate = 0.03m },
            
            // Calligraphy
            new Product { SubCategoryId = calligraphy.SubCategoryId, Name = "Arabic Calligraphy Piece", CustomsRate = 0.04m },
            new Product { SubCategoryId = calligraphy.SubCategoryId, Name = "English Calligraphy Piece", CustomsRate = 0.04m },
            new Product { SubCategoryId = calligraphy.SubCategoryId, Name = "Calligraphy Tools", CustomsRate = 0.03m }
        );

        await context.SaveChangesAsync();
    }
}

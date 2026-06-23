BEGIN TRANSACTION;
INSERT INTO [Categories] ([Name], [CreatedAt]) VALUES ('Electronics', GETUTCDATE());

INSERT INTO [SubCategories] ([CategoryId], [Name], [CreatedAt]) VALUES ((SELECT [CategoryId] FROM [Categories] WHERE [Name] = 'Electronics'), 'Mobile Phones', GETUTCDATE());

INSERT INTO [Products] ([SubCategoryId], [Name], [CustomsRate], [CreatedAt]) VALUES ((SELECT [SubCategoryId] FROM [SubCategories] WHERE [Name] = 'Electronics'), 'iPhone', 0.03, GETUTCDATE());

INSERT INTO [Products] ([SubCategoryId], [Name], [CustomsRate], [CreatedAt]) VALUES ((SELECT [SubCategoryId] FROM [SubCategories] WHERE [Name] = 'Electronics'), 'Samsung Galaxy', 0.025, GETUTCDATE());

INSERT INTO [Products] ([SubCategoryId], [Name], [CustomsRate], [CreatedAt]) VALUES ((SELECT [SubCategoryId] FROM [SubCategories] WHERE [Name] = 'Electronics'), 'Xiaomi', 0.02, GETUTCDATE());

INSERT INTO [Products] ([SubCategoryId], [Name], [CustomsRate], [CreatedAt]) VALUES ((SELECT [SubCategoryId] FROM [SubCategories] WHERE [Name] = 'Electronics'), 'Huawei', 0.02, GETUTCDATE());

INSERT INTO [Products] ([SubCategoryId], [Name], [CustomsRate], [CreatedAt]) VALUES ((SELECT [SubCategoryId] FROM [SubCategories] WHERE [Name] = 'Electronics'), 'Nokia', 0.015, GETUTCDATE());

INSERT INTO [Products] ([SubCategoryId], [Name], [CustomsRate], [CreatedAt]) VALUES ((SELECT [SubCategoryId] FROM [SubCategories] WHERE [Name] = 'Electronics'), 'Oppo', 0.018, GETUTCDATE());

INSERT INTO [Products] ([SubCategoryId], [Name], [CustomsRate], [CreatedAt]) VALUES ((SELECT [SubCategoryId] FROM [SubCategories] WHERE [Name] = 'Electronics'), 'Realme', 0.018, GETUTCDATE());

INSERT INTO [Products] ([SubCategoryId], [Name], [CustomsRate], [CreatedAt]) VALUES ((SELECT [SubCategoryId] FROM [SubCategories] WHERE [Name] = 'Electronics'), 'MacBook', 0.05, GETUTCDATE());

INSERT INTO [Products] ([SubCategoryId], [Name], [CustomsRate], [CreatedAt]) VALUES ((SELECT [SubCategoryId] FROM [SubCategories] WHERE [Name] = 'Electronics'), 'Dell', 0.04, GETUTCDATE());

INSERT INTO [Products] ([SubCategoryId], [Name], [CustomsRate], [CreatedAt]) VALUES ((SELECT [SubCategoryId] FROM [SubCategories] WHERE [Name] = 'Electronics'), 'HP', 0.04, GETUTCDATE());

INSERT INTO [Products] ([SubCategoryId], [Name], [CustomsRate], [CreatedAt]) VALUES ((SELECT [SubCategoryId] FROM [SubCategories] WHERE [Name] = 'Electronics'), 'Lenovo', 0.035, GETUTCDATE());

INSERT INTO [Products] ([SubCategoryId], [Name], [CustomsRate], [CreatedAt]) VALUES ((SELECT [SubCategoryId] FROM [SubCategories] WHERE [Name] = 'Electronics'), 'Asus', 0.038, GETUTCDATE());

INSERT INTO [Products] ([SubCategoryId], [Name], [CustomsRate], [CreatedAt]) VALUES ((SELECT [SubCategoryId] FROM [SubCategories] WHERE [Name] = 'Electronics'), 'Acer', 0.033, GETUTCDATE());

INSERT INTO [Products] ([SubCategoryId], [Name], [CustomsRate], [CreatedAt]) VALUES ((SELECT [SubCategoryId] FROM [SubCategories] WHERE [Name] = 'Electronics'), 'MSI', 0.045, GETUTCDATE());

INSERT INTO [Products] ([SubCategoryId], [Name], [CustomsRate], [CreatedAt]) VALUES ((SELECT [SubCategoryId] FROM [SubCategories] WHERE [Name] = 'Electronics'), 'iPad', 0.04, GETUTCDATE());

INSERT INTO [Products] ([SubCategoryId], [Name], [CustomsRate], [CreatedAt]) VALUES ((SELECT [SubCategoryId] FROM [SubCategories] WHERE [Name] = 'Electronics'), 'Samsung Galaxy Tab', 0.035, GETUTCDATE());

INSERT INTO [Products] ([SubCategoryId], [Name], [CustomsRate], [CreatedAt]) VALUES ((SELECT [SubCategoryId] FROM [SubCategories] WHERE [Name] = 'Electronics'), 'Huawei MatePad', 0.03, GETUTCDATE());

INSERT INTO [Products] ([SubCategoryId], [Name], [CustomsRate], [CreatedAt]) VALUES ((SELECT [SubCategoryId] FROM [SubCategories] WHERE [Name] = 'Electronics'), 'Lenovo Tab', 0.028, GETUTCDATE());

INSERT INTO [Products] ([SubCategoryId], [Name], [CustomsRate], [CreatedAt]) VALUES ((SELECT [SubCategoryId] FROM [SubCategories] WHERE [Name] = 'Electronics'), 'Microsoft Surface', 0.045, GETUTCDATE());

INSERT INTO [Products] ([SubCategoryId], [Name], [CustomsRate], [CreatedAt]) VALUES ((SELECT [SubCategoryId] FROM [SubCategories] WHERE [Name] = 'Electronics'), 'AirPods', 0.02, GETUTCDATE());

INSERT INTO [Products] ([SubCategoryId], [Name], [CustomsRate], [CreatedAt]) VALUES ((SELECT [SubCategoryId] FROM [SubCategories] WHERE [Name] = 'Electronics'), 'Sony Headphones', 0.018, GETUTCDATE());

INSERT INTO [Products] ([SubCategoryId], [Name], [CustomsRate], [CreatedAt]) VALUES ((SELECT [SubCategoryId] FROM [SubCategories] WHERE [Name] = 'Electronics'), 'JBL Headphones', 0.015, GETUTCDATE());

INSERT INTO [Products] ([SubCategoryId], [Name], [CustomsRate], [CreatedAt]) VALUES ((SELECT [SubCategoryId] FROM [SubCategories] WHERE [Name] = 'Electronics'), 'Bose Headphones', 0.02, GETUTCDATE());

INSERT INTO [Products] ([SubCategoryId], [Name], [CustomsRate], [CreatedAt]) VALUES ((SELECT [SubCategoryId] FROM [SubCategories] WHERE [Name] = 'Electronics'), 'Beats', 0.02, GETUTCDATE());

INSERT INTO [Products] ([SubCategoryId], [Name], [CustomsRate], [CreatedAt]) VALUES ((SELECT [SubCategoryId] FROM [SubCategories] WHERE [Name] = 'Electronics'), 'Anker Soundcore', 0.014, GETUTCDATE());

INSERT INTO [Products] ([SubCategoryId], [Name], [CustomsRate], [CreatedAt]) VALUES ((SELECT [SubCategoryId] FROM [SubCategories] WHERE [Name] = 'Electronics'), 'Apple Watch', 0.03, GETUTCDATE());

INSERT INTO [Products] ([SubCategoryId], [Name], [CustomsRate], [CreatedAt]) VALUES ((SELECT [SubCategoryId] FROM [SubCategories] WHERE [Name] = 'Electronics'), 'Samsung Galaxy Watch', 0.028, GETUTCDATE());

INSERT INTO [Products] ([SubCategoryId], [Name], [CustomsRate], [CreatedAt]) VALUES ((SELECT [SubCategoryId] FROM [SubCategories] WHERE [Name] = 'Electronics'), 'Huawei Watch', 0.025, GETUTCDATE());

INSERT INTO [Products] ([SubCategoryId], [Name], [CustomsRate], [CreatedAt]) VALUES ((SELECT [SubCategoryId] FROM [SubCategories] WHERE [Name] = 'Electronics'), 'Xiaomi Mi Band', 0.02, GETUTCDATE());

INSERT INTO [Products] ([SubCategoryId], [Name], [CustomsRate], [CreatedAt]) VALUES ((SELECT [SubCategoryId] FROM [SubCategories] WHERE [Name] = 'Electronics'), 'Garmin', 0.03, GETUTCDATE());

INSERT INTO [Products] ([SubCategoryId], [Name], [CustomsRate], [CreatedAt]) VALUES ((SELECT [SubCategoryId] FROM [SubCategories] WHERE [Name] = 'Electronics'), 'USB-C Charger', 0.01, GETUTCDATE());

INSERT INTO [Products] ([SubCategoryId], [Name], [CustomsRate], [CreatedAt]) VALUES ((SELECT [SubCategoryId] FROM [SubCategories] WHERE [Name] = 'Electronics'), 'Lightning Cable', 0.01, GETUTCDATE());

INSERT INTO [Products] ([SubCategoryId], [Name], [CustomsRate], [CreatedAt]) VALUES ((SELECT [SubCategoryId] FROM [SubCategories] WHERE [Name] = 'Electronics'), 'USB-C Cable', 0.008, GETUTCDATE());

INSERT INTO [Products] ([SubCategoryId], [Name], [CustomsRate], [CreatedAt]) VALUES ((SELECT [SubCategoryId] FROM [SubCategories] WHERE [Name] = 'Electronics'), 'Wireless Charger', 0.012, GETUTCDATE());

INSERT INTO [Products] ([SubCategoryId], [Name], [CustomsRate], [CreatedAt]) VALUES ((SELECT [SubCategoryId] FROM [SubCategories] WHERE [Name] = 'Electronics'), 'Power Bank', 0.015, GETUTCDATE());

INSERT INTO [Products] ([SubCategoryId], [Name], [CustomsRate], [CreatedAt]) VALUES ((SELECT [SubCategoryId] FROM [SubCategories] WHERE [Name] = 'Electronics'), 'Car Charger', 0.01, GETUTCDATE());

INSERT INTO [Products] ([SubCategoryId], [Name], [CustomsRate], [CreatedAt]) VALUES ((SELECT [SubCategoryId] FROM [SubCategories] WHERE [Name] = 'Electronics'), 'Samsung TV', 0.06, GETUTCDATE());

INSERT INTO [Products] ([SubCategoryId], [Name], [CustomsRate], [CreatedAt]) VALUES ((SELECT [SubCategoryId] FROM [SubCategories] WHERE [Name] = 'Electronics'), 'LG TV', 0.055, GETUTCDATE());

INSERT INTO [Products] ([SubCategoryId], [Name], [CustomsRate], [CreatedAt]) VALUES ((SELECT [SubCategoryId] FROM [SubCategories] WHERE [Name] = 'Electronics'), 'Sony TV', 0.06, GETUTCDATE());

INSERT INTO [Products] ([SubCategoryId], [Name], [CustomsRate], [CreatedAt]) VALUES ((SELECT [SubCategoryId] FROM [SubCategories] WHERE [Name] = 'Electronics'), 'TCL TV', 0.05, GETUTCDATE());

INSERT INTO [Products] ([SubCategoryId], [Name], [CustomsRate], [CreatedAt]) VALUES ((SELECT [SubCategoryId] FROM [SubCategories] WHERE [Name] = 'Electronics'), 'Hisense TV', 0.05, GETUTCDATE());

INSERT INTO [Products] ([SubCategoryId], [Name], [CustomsRate], [CreatedAt]) VALUES ((SELECT [SubCategoryId] FROM [SubCategories] WHERE [Name] = 'Electronics'), 'Panasonic TV', 0.055, GETUTCDATE());

INSERT INTO [Products] ([SubCategoryId], [Name], [CustomsRate], [CreatedAt]) VALUES ((SELECT [SubCategoryId] FROM [SubCategories] WHERE [Name] = 'Electronics'), 'Canon Camera', 0.05, GETUTCDATE());

INSERT INTO [Products] ([SubCategoryId], [Name], [CustomsRate], [CreatedAt]) VALUES ((SELECT [SubCategoryId] FROM [SubCategories] WHERE [Name] = 'Electronics'), 'Nikon Camera', 0.05, GETUTCDATE());

INSERT INTO [Products] ([SubCategoryId], [Name], [CustomsRate], [CreatedAt]) VALUES ((SELECT [SubCategoryId] FROM [SubCategories] WHERE [Name] = 'Electronics'), 'Sony Camera', 0.05, GETUTCDATE());

INSERT INTO [Products] ([SubCategoryId], [Name], [CustomsRate], [CreatedAt]) VALUES ((SELECT [SubCategoryId] FROM [SubCategories] WHERE [Name] = 'Electronics'), 'Fujifilm Camera', 0.045, GETUTCDATE());

INSERT INTO [Products] ([SubCategoryId], [Name], [CustomsRate], [CreatedAt]) VALUES ((SELECT [SubCategoryId] FROM [SubCategories] WHERE [Name] = 'Electronics'), 'GoPro', 0.04, GETUTCDATE());

INSERT INTO [Products] ([SubCategoryId], [Name], [CustomsRate], [CreatedAt]) VALUES ((SELECT [SubCategoryId] FROM [SubCategories] WHERE [Name] = 'Electronics'), 'T-Shirts', 0.1, GETUTCDATE());

INSERT INTO [Products] ([SubCategoryId], [Name], [CustomsRate], [CreatedAt]) VALUES ((SELECT [SubCategoryId] FROM [SubCategories] WHERE [Name] = 'Electronics'), 'Shirts', 0.11, GETUTCDATE());

INSERT INTO [Products] ([SubCategoryId], [Name], [CustomsRate], [CreatedAt]) VALUES ((SELECT [SubCategoryId] FROM [SubCategories] WHERE [Name] = 'Electronics'), 'Jeans', 0.12, GETUTCDATE());

INSERT INTO [Products] ([SubCategoryId], [Name], [CustomsRate], [CreatedAt]) VALUES ((SELECT [SubCategoryId] FROM [SubCategories] WHERE [Name] = 'Electronics'), 'Suits', 0.15, GETUTCDATE());

INSERT INTO [Products] ([SubCategoryId], [Name], [CustomsRate], [CreatedAt]) VALUES ((SELECT [SubCategoryId] FROM [SubCategories] WHERE [Name] = 'Electronics'), 'Casual Pants', 0.12, GETUTCDATE());

INSERT INTO [Products] ([SubCategoryId], [Name], [CustomsRate], [CreatedAt]) VALUES ((SELECT [SubCategoryId] FROM [SubCategories] WHERE [Name] = 'Electronics'), 'Dresses', 0.14, GETUTCDATE());

INSERT INTO [Products] ([SubCategoryId], [Name], [CustomsRate], [CreatedAt]) VALUES ((SELECT [SubCategoryId] FROM [SubCategories] WHERE [Name] = 'Electronics'), 'Tops', 0.12, GETUTCDATE());

INSERT INTO [Products] ([SubCategoryId], [Name], [CustomsRate], [CreatedAt]) VALUES ((SELECT [SubCategoryId] FROM [SubCategories] WHERE [Name] = 'Electronics'), 'Skirts', 0.13, GETUTCDATE());

INSERT INTO [Products] ([SubCategoryId], [Name], [CustomsRate], [CreatedAt]) VALUES ((SELECT [SubCategoryId] FROM [SubCategories] WHERE [Name] = 'Electronics'), 'Blouses', 0.12, GETUTCDATE());

INSERT INTO [Products] ([SubCategoryId], [Name], [CustomsRate], [CreatedAt]) VALUES ((SELECT [SubCategoryId] FROM [SubCategories] WHERE [Name] = 'Electronics'), 'Abayas', 0.1, GETUTCDATE());

INSERT INTO [Products] ([SubCategoryId], [Name], [CustomsRate], [CreatedAt]) VALUES ((SELECT [SubCategoryId] FROM [SubCategories] WHERE [Name] = 'Electronics'), 'Baby Sets', 0.08, GETUTCDATE());

INSERT INTO [Products] ([SubCategoryId], [Name], [CustomsRate], [CreatedAt]) VALUES ((SELECT [SubCategoryId] FROM [SubCategories] WHERE [Name] = 'Electronics'), 'Boys Clothing', 0.09, GETUTCDATE());

INSERT INTO [Products] ([SubCategoryId], [Name], [CustomsRate], [CreatedAt]) VALUES ((SELECT [SubCategoryId] FROM [SubCategories] WHERE [Name] = 'Electronics'), 'Girls Clothing', 0.09, GETUTCDATE());

INSERT INTO [Products] ([SubCategoryId], [Name], [CustomsRate], [CreatedAt]) VALUES ((SELECT [SubCategoryId] FROM [SubCategories] WHERE [Name] = 'Electronics'), 'School Uniforms', 0.07, GETUTCDATE());

INSERT INTO [Products] ([SubCategoryId], [Name], [CustomsRate], [CreatedAt]) VALUES ((SELECT [SubCategoryId] FROM [SubCategories] WHERE [Name] = 'Electronics'), 'Leather Jackets', 0.18, GETUTCDATE());

INSERT INTO [Products] ([SubCategoryId], [Name], [CustomsRate], [CreatedAt]) VALUES ((SELECT [SubCategoryId] FROM [SubCategories] WHERE [Name] = 'Electronics'), 'Winter Coats', 0.16, GETUTCDATE());

INSERT INTO [Products] ([SubCategoryId], [Name], [CustomsRate], [CreatedAt]) VALUES ((SELECT [SubCategoryId] FROM [SubCategories] WHERE [Name] = 'Electronics'), 'Hoodies', 0.12, GETUTCDATE());

INSERT INTO [Products] ([SubCategoryId], [Name], [CustomsRate], [CreatedAt]) VALUES ((SELECT [SubCategoryId] FROM [SubCategories] WHERE [Name] = 'Electronics'), 'Blazers', 0.15, GETUTCDATE());

INSERT INTO [Products] ([SubCategoryId], [Name], [CustomsRate], [CreatedAt]) VALUES ((SELECT [SubCategoryId] FROM [SubCategories] WHERE [Name] = 'Electronics'), 'Tracksuits', 0.12, GETUTCDATE());

INSERT INTO [Products] ([SubCategoryId], [Name], [CustomsRate], [CreatedAt]) VALUES ((SELECT [SubCategoryId] FROM [SubCategories] WHERE [Name] = 'Electronics'), 'Gym Wear', 0.12, GETUTCDATE());

INSERT INTO [Products] ([SubCategoryId], [Name], [CustomsRate], [CreatedAt]) VALUES ((SELECT [SubCategoryId] FROM [SubCategories] WHERE [Name] = 'Electronics'), 'Swimwear', 0.14, GETUTCDATE());

INSERT INTO [Products] ([SubCategoryId], [Name], [CustomsRate], [CreatedAt]) VALUES ((SELECT [SubCategoryId] FROM [SubCategories] WHERE [Name] = 'Electronics'), 'Yoga Wear', 0.12, GETUTCDATE());

INSERT INTO [Products] ([SubCategoryId], [Name], [CustomsRate], [CreatedAt]) VALUES ((SELECT [SubCategoryId] FROM [SubCategories] WHERE [Name] = 'Electronics'), 'Sneakers', 0.18, GETUTCDATE());

INSERT INTO [Products] ([SubCategoryId], [Name], [CustomsRate], [CreatedAt]) VALUES ((SELECT [SubCategoryId] FROM [SubCategories] WHERE [Name] = 'Electronics'), 'Formal Shoes', 0.2, GETUTCDATE());

INSERT INTO [Products] ([SubCategoryId], [Name], [CustomsRate], [CreatedAt]) VALUES ((SELECT [SubCategoryId] FROM [SubCategories] WHERE [Name] = 'Electronics'), 'Sandals', 0.16, GETUTCDATE());

INSERT INTO [Products] ([SubCategoryId], [Name], [CustomsRate], [CreatedAt]) VALUES ((SELECT [SubCategoryId] FROM [SubCategories] WHERE [Name] = 'Electronics'), 'Boots', 0.22, GETUTCDATE());

INSERT INTO [Products] ([SubCategoryId], [Name], [CustomsRate], [CreatedAt]) VALUES ((SELECT [SubCategoryId] FROM [SubCategories] WHERE [Name] = 'Electronics'), 'Slippers', 0.1, GETUTCDATE());

INSERT INTO [Products] ([SubCategoryId], [Name], [CustomsRate], [CreatedAt]) VALUES ((SELECT [SubCategoryId] FROM [SubCategories] WHERE [Name] = 'Electronics'), 'Belts', 0.12, GETUTCDATE());

INSERT INTO [Products] ([SubCategoryId], [Name], [CustomsRate], [CreatedAt]) VALUES ((SELECT [SubCategoryId] FROM [SubCategories] WHERE [Name] = 'Electronics'), 'Hats', 0.1, GETUTCDATE());

INSERT INTO [Products] ([SubCategoryId], [Name], [CustomsRate], [CreatedAt]) VALUES ((SELECT [SubCategoryId] FROM [SubCategories] WHERE [Name] = 'Electronics'), 'Caps', 0.1, GETUTCDATE());

INSERT INTO [Products] ([SubCategoryId], [Name], [CustomsRate], [CreatedAt]) VALUES ((SELECT [SubCategoryId] FROM [SubCategories] WHERE [Name] = 'Electronics'), 'Scarves', 0.12, GETUTCDATE());

INSERT INTO [Products] ([SubCategoryId], [Name], [CustomsRate], [CreatedAt]) VALUES ((SELECT [SubCategoryId] FROM [SubCategories] WHERE [Name] = 'Electronics'), 'Sunglasses', 0.15, GETUTCDATE());

INSERT INTO [Products] ([SubCategoryId], [Name], [CustomsRate], [CreatedAt]) VALUES ((SELECT [SubCategoryId] FROM [SubCategories] WHERE [Name] = 'Electronics'), 'Fruits', 0.0, GETUTCDATE());

INSERT INTO [Products] ([SubCategoryId], [Name], [CustomsRate], [CreatedAt]) VALUES ((SELECT [SubCategoryId] FROM [SubCategories] WHERE [Name] = 'Electronics'), 'Vegetables', 0.0, GETUTCDATE());

INSERT INTO [Products] ([SubCategoryId], [Name], [CustomsRate], [CreatedAt]) VALUES ((SELECT [SubCategoryId] FROM [SubCategories] WHERE [Name] = 'Electronics'), 'Meat', 0.02, GETUTCDATE());

INSERT INTO [Products] ([SubCategoryId], [Name], [CustomsRate], [CreatedAt]) VALUES ((SELECT [SubCategoryId] FROM [SubCategories] WHERE [Name] = 'Electronics'), 'Chicken', 0.02, GETUTCDATE());

INSERT INTO [Products] ([SubCategoryId], [Name], [CustomsRate], [CreatedAt]) VALUES ((SELECT [SubCategoryId] FROM [SubCategories] WHERE [Name] = 'Electronics'), 'Fish', 0.02, GETUTCDATE());

INSERT INTO [Products] ([SubCategoryId], [Name], [CustomsRate], [CreatedAt]) VALUES ((SELECT [SubCategoryId] FROM [SubCategories] WHERE [Name] = 'Electronics'), 'Frozen Vegetables', 0.01, GETUTCDATE());

INSERT INTO [Products] ([SubCategoryId], [Name], [CustomsRate], [CreatedAt]) VALUES ((SELECT [SubCategoryId] FROM [SubCategories] WHERE [Name] = 'Electronics'), 'Frozen Meat', 0.02, GETUTCDATE());

INSERT INTO [Products] ([SubCategoryId], [Name], [CustomsRate], [CreatedAt]) VALUES ((SELECT [SubCategoryId] FROM [SubCategories] WHERE [Name] = 'Electronics'), 'Ice Cream', 0.05, GETUTCDATE());

INSERT INTO [Products] ([SubCategoryId], [Name], [CustomsRate], [CreatedAt]) VALUES ((SELECT [SubCategoryId] FROM [SubCategories] WHERE [Name] = 'Electronics'), 'Ready Meals', 0.04, GETUTCDATE());

INSERT INTO [Products] ([SubCategoryId], [Name], [CustomsRate], [CreatedAt]) VALUES ((SELECT [SubCategoryId] FROM [SubCategories] WHERE [Name] = 'Electronics'), 'Chips', 0.08, GETUTCDATE());

INSERT INTO [Products] ([SubCategoryId], [Name], [CustomsRate], [CreatedAt]) VALUES ((SELECT [SubCategoryId] FROM [SubCategories] WHERE [Name] = 'Electronics'), 'Biscuits', 0.07, GETUTCDATE());

INSERT INTO [Products] ([SubCategoryId], [Name], [CustomsRate], [CreatedAt]) VALUES ((SELECT [SubCategoryId] FROM [SubCategories] WHERE [Name] = 'Electronics'), 'Chocolate', 0.12, GETUTCDATE());

INSERT INTO [Products] ([SubCategoryId], [Name], [CustomsRate], [CreatedAt]) VALUES ((SELECT [SubCategoryId] FROM [SubCategories] WHERE [Name] = 'Electronics'), 'Nuts', 0.06, GETUTCDATE());

INSERT INTO [Products] ([SubCategoryId], [Name], [CustomsRate], [CreatedAt]) VALUES ((SELECT [SubCategoryId] FROM [SubCategories] WHERE [Name] = 'Electronics'), 'Soft Drinks', 0.1, GETUTCDATE());

INSERT INTO [Products] ([SubCategoryId], [Name], [CustomsRate], [CreatedAt]) VALUES ((SELECT [SubCategoryId] FROM [SubCategories] WHERE [Name] = 'Electronics'), 'Juices', 0.08, GETUTCDATE());

INSERT INTO [Products] ([SubCategoryId], [Name], [CustomsRate], [CreatedAt]) VALUES ((SELECT [SubCategoryId] FROM [SubCategories] WHERE [Name] = 'Electronics'), 'Energy Drinks', 0.12, GETUTCDATE());

INSERT INTO [Products] ([SubCategoryId], [Name], [CustomsRate], [CreatedAt]) VALUES ((SELECT [SubCategoryId] FROM [SubCategories] WHERE [Name] = 'Electronics'), 'Tea', 0.04, GETUTCDATE());

INSERT INTO [Products] ([SubCategoryId], [Name], [CustomsRate], [CreatedAt]) VALUES ((SELECT [SubCategoryId] FROM [SubCategories] WHERE [Name] = 'Electronics'), 'Coffee', 0.05, GETUTCDATE());

INSERT INTO [Products] ([SubCategoryId], [Name], [CustomsRate], [CreatedAt]) VALUES ((SELECT [SubCategoryId] FROM [SubCategories] WHERE [Name] = 'Electronics'), 'Milk', 0.0, GETUTCDATE());

INSERT INTO [Products] ([SubCategoryId], [Name], [CustomsRate], [CreatedAt]) VALUES ((SELECT [SubCategoryId] FROM [SubCategories] WHERE [Name] = 'Electronics'), 'Cheese', 0.03, GETUTCDATE());

INSERT INTO [Products] ([SubCategoryId], [Name], [CustomsRate], [CreatedAt]) VALUES ((SELECT [SubCategoryId] FROM [SubCategories] WHERE [Name] = 'Electronics'), 'Yogurt', 0.01, GETUTCDATE());

INSERT INTO [Products] ([SubCategoryId], [Name], [CustomsRate], [CreatedAt]) VALUES ((SELECT [SubCategoryId] FROM [SubCategories] WHERE [Name] = 'Electronics'), 'Butter', 0.02, GETUTCDATE());

INSERT INTO [Products] ([SubCategoryId], [Name], [CustomsRate], [CreatedAt]) VALUES ((SELECT [SubCategoryId] FROM [SubCategories] WHERE [Name] = 'Electronics'), 'Bread', 0.0, GETUTCDATE());

INSERT INTO [Products] ([SubCategoryId], [Name], [CustomsRate], [CreatedAt]) VALUES ((SELECT [SubCategoryId] FROM [SubCategories] WHERE [Name] = 'Electronics'), 'Cakes', 0.06, GETUTCDATE());

INSERT INTO [Products] ([SubCategoryId], [Name], [CustomsRate], [CreatedAt]) VALUES ((SELECT [SubCategoryId] FROM [SubCategories] WHERE [Name] = 'Electronics'), 'Pastries', 0.06, GETUTCDATE());

INSERT INTO [Products] ([SubCategoryId], [Name], [CustomsRate], [CreatedAt]) VALUES ((SELECT [SubCategoryId] FROM [SubCategories] WHERE [Name] = 'Electronics'), 'Croissants', 0.06, GETUTCDATE());

INSERT INTO [Products] ([SubCategoryId], [Name], [CustomsRate], [CreatedAt]) VALUES ((SELECT [SubCategoryId] FROM [SubCategories] WHERE [Name] = 'Electronics'), 'Canned Beans', 0.02, GETUTCDATE());

INSERT INTO [Products] ([SubCategoryId], [Name], [CustomsRate], [CreatedAt]) VALUES ((SELECT [SubCategoryId] FROM [SubCategories] WHERE [Name] = 'Electronics'), 'Tuna', 0.03, GETUTCDATE());

INSERT INTO [Products] ([SubCategoryId], [Name], [CustomsRate], [CreatedAt]) VALUES ((SELECT [SubCategoryId] FROM [SubCategories] WHERE [Name] = 'Electronics'), 'Corn', 0.02, GETUTCDATE());

INSERT INTO [Products] ([SubCategoryId], [Name], [CustomsRate], [CreatedAt]) VALUES ((SELECT [SubCategoryId] FROM [SubCategories] WHERE [Name] = 'Electronics'), 'Tomato Paste', 0.02, GETUTCDATE());

INSERT INTO [Products] ([SubCategoryId], [Name], [CustomsRate], [CreatedAt]) VALUES ((SELECT [SubCategoryId] FROM [SubCategories] WHERE [Name] = 'Electronics'), 'Gold Ring', 0.05, GETUTCDATE());

INSERT INTO [Products] ([SubCategoryId], [Name], [CustomsRate], [CreatedAt]) VALUES ((SELECT [SubCategoryId] FROM [SubCategories] WHERE [Name] = 'Electronics'), 'Silver Ring', 0.04, GETUTCDATE());

INSERT INTO [Products] ([SubCategoryId], [Name], [CustomsRate], [CreatedAt]) VALUES ((SELECT [SubCategoryId] FROM [SubCategories] WHERE [Name] = 'Electronics'), 'Diamond Ring', 0.06, GETUTCDATE());

INSERT INTO [Products] ([SubCategoryId], [Name], [CustomsRate], [CreatedAt]) VALUES ((SELECT [SubCategoryId] FROM [SubCategories] WHERE [Name] = 'Electronics'), 'Gold Necklace', 0.05, GETUTCDATE());

INSERT INTO [Products] ([SubCategoryId], [Name], [CustomsRate], [CreatedAt]) VALUES ((SELECT [SubCategoryId] FROM [SubCategories] WHERE [Name] = 'Electronics'), 'Silver Necklace', 0.04, GETUTCDATE());

INSERT INTO [Products] ([SubCategoryId], [Name], [CustomsRate], [CreatedAt]) VALUES ((SELECT [SubCategoryId] FROM [SubCategories] WHERE [Name] = 'Electronics'), 'Pearl Necklace', 0.05, GETUTCDATE());

INSERT INTO [Products] ([SubCategoryId], [Name], [CustomsRate], [CreatedAt]) VALUES ((SELECT [SubCategoryId] FROM [SubCategories] WHERE [Name] = 'Electronics'), 'Charm Bracelet', 0.04, GETUTCDATE());

INSERT INTO [Products] ([SubCategoryId], [Name], [CustomsRate], [CreatedAt]) VALUES ((SELECT [SubCategoryId] FROM [SubCategories] WHERE [Name] = 'Electronics'), 'Gold Bracelet', 0.05, GETUTCDATE());

INSERT INTO [Products] ([SubCategoryId], [Name], [CustomsRate], [CreatedAt]) VALUES ((SELECT [SubCategoryId] FROM [SubCategories] WHERE [Name] = 'Electronics'), 'Silver Bracelet', 0.04, GETUTCDATE());

INSERT INTO [Products] ([SubCategoryId], [Name], [CustomsRate], [CreatedAt]) VALUES ((SELECT [SubCategoryId] FROM [SubCategories] WHERE [Name] = 'Electronics'), 'Stud Earrings', 0.04, GETUTCDATE());

INSERT INTO [Products] ([SubCategoryId], [Name], [CustomsRate], [CreatedAt]) VALUES ((SELECT [SubCategoryId] FROM [SubCategories] WHERE [Name] = 'Electronics'), 'Hoop Earrings', 0.04, GETUTCDATE());

INSERT INTO [Products] ([SubCategoryId], [Name], [CustomsRate], [CreatedAt]) VALUES ((SELECT [SubCategoryId] FROM [SubCategories] WHERE [Name] = 'Electronics'), 'Drop Earrings', 0.045, GETUTCDATE());

INSERT INTO [Products] ([SubCategoryId], [Name], [CustomsRate], [CreatedAt]) VALUES ((SELECT [SubCategoryId] FROM [SubCategories] WHERE [Name] = 'Electronics'), 'Rolex', 0.08, GETUTCDATE());

INSERT INTO [Products] ([SubCategoryId], [Name], [CustomsRate], [CreatedAt]) VALUES ((SELECT [SubCategoryId] FROM [SubCategories] WHERE [Name] = 'Electronics'), 'Casio', 0.05, GETUTCDATE());

INSERT INTO [Products] ([SubCategoryId], [Name], [CustomsRate], [CreatedAt]) VALUES ((SELECT [SubCategoryId] FROM [SubCategories] WHERE [Name] = 'Electronics'), 'Fossil', 0.06, GETUTCDATE());

INSERT INTO [Products] ([SubCategoryId], [Name], [CustomsRate], [CreatedAt]) VALUES ((SELECT [SubCategoryId] FROM [SubCategories] WHERE [Name] = 'Electronics'), 'Tissot', 0.07, GETUTCDATE());

INSERT INTO [Products] ([SubCategoryId], [Name], [CustomsRate], [CreatedAt]) VALUES ((SELECT [SubCategoryId] FROM [SubCategories] WHERE [Name] = 'Electronics'), '18K Gold Set', 0.06, GETUTCDATE());

INSERT INTO [Products] ([SubCategoryId], [Name], [CustomsRate], [CreatedAt]) VALUES ((SELECT [SubCategoryId] FROM [SubCategories] WHERE [Name] = 'Electronics'), '21K Gold Set', 0.07, GETUTCDATE());

INSERT INTO [Products] ([SubCategoryId], [Name], [CustomsRate], [CreatedAt]) VALUES ((SELECT [SubCategoryId] FROM [SubCategories] WHERE [Name] = 'Electronics'), '24K Gold Bar', 0.08, GETUTCDATE());

INSERT INTO [Products] ([SubCategoryId], [Name], [CustomsRate], [CreatedAt]) VALUES ((SELECT [SubCategoryId] FROM [SubCategories] WHERE [Name] = 'Electronics'), 'Sterling Silver Set', 0.05, GETUTCDATE());

INSERT INTO [Products] ([SubCategoryId], [Name], [CustomsRate], [CreatedAt]) VALUES ((SELECT [SubCategoryId] FROM [SubCategories] WHERE [Name] = 'Electronics'), 'Handmade Silver Piece', 0.05, GETUTCDATE());

INSERT INTO [Products] ([SubCategoryId], [Name], [CustomsRate], [CreatedAt]) VALUES ((SELECT [SubCategoryId] FROM [SubCategories] WHERE [Name] = 'Electronics'), 'Lipstick', 0.12, GETUTCDATE());

INSERT INTO [Products] ([SubCategoryId], [Name], [CustomsRate], [CreatedAt]) VALUES ((SELECT [SubCategoryId] FROM [SubCategories] WHERE [Name] = 'Electronics'), 'Foundation', 0.12, GETUTCDATE());

INSERT INTO [Products] ([SubCategoryId], [Name], [CustomsRate], [CreatedAt]) VALUES ((SELECT [SubCategoryId] FROM [SubCategories] WHERE [Name] = 'Electronics'), 'Mascara', 0.12, GETUTCDATE());

INSERT INTO [Products] ([SubCategoryId], [Name], [CustomsRate], [CreatedAt]) VALUES ((SELECT [SubCategoryId] FROM [SubCategories] WHERE [Name] = 'Electronics'), 'Eyeliner', 0.12, GETUTCDATE());

INSERT INTO [Products] ([SubCategoryId], [Name], [CustomsRate], [CreatedAt]) VALUES ((SELECT [SubCategoryId] FROM [SubCategories] WHERE [Name] = 'Electronics'), 'Face Wash', 0.08, GETUTCDATE());

INSERT INTO [Products] ([SubCategoryId], [Name], [CustomsRate], [CreatedAt]) VALUES ((SELECT [SubCategoryId] FROM [SubCategories] WHERE [Name] = 'Electronics'), 'Moisturizer', 0.08, GETUTCDATE());

INSERT INTO [Products] ([SubCategoryId], [Name], [CustomsRate], [CreatedAt]) VALUES ((SELECT [SubCategoryId] FROM [SubCategories] WHERE [Name] = 'Electronics'), 'Sunscreen', 0.08, GETUTCDATE());

INSERT INTO [Products] ([SubCategoryId], [Name], [CustomsRate], [CreatedAt]) VALUES ((SELECT [SubCategoryId] FROM [SubCategories] WHERE [Name] = 'Electronics'), 'Serum', 0.1, GETUTCDATE());

INSERT INTO [Products] ([SubCategoryId], [Name], [CustomsRate], [CreatedAt]) VALUES ((SELECT [SubCategoryId] FROM [SubCategories] WHERE [Name] = 'Electronics'), 'Shampoo', 0.08, GETUTCDATE());

INSERT INTO [Products] ([SubCategoryId], [Name], [CustomsRate], [CreatedAt]) VALUES ((SELECT [SubCategoryId] FROM [SubCategories] WHERE [Name] = 'Electronics'), 'Conditioner', 0.08, GETUTCDATE());

INSERT INTO [Products] ([SubCategoryId], [Name], [CustomsRate], [CreatedAt]) VALUES ((SELECT [SubCategoryId] FROM [SubCategories] WHERE [Name] = 'Electronics'), 'Hair Oil', 0.1, GETUTCDATE());

INSERT INTO [Products] ([SubCategoryId], [Name], [CustomsRate], [CreatedAt]) VALUES ((SELECT [SubCategoryId] FROM [SubCategories] WHERE [Name] = 'Electronics'), 'Hair Mask', 0.1, GETUTCDATE());

INSERT INTO [Products] ([SubCategoryId], [Name], [CustomsRate], [CreatedAt]) VALUES ((SELECT [SubCategoryId] FROM [SubCategories] WHERE [Name] = 'Electronics'), 'Men Perfume', 0.15, GETUTCDATE());

INSERT INTO [Products] ([SubCategoryId], [Name], [CustomsRate], [CreatedAt]) VALUES ((SELECT [SubCategoryId] FROM [SubCategories] WHERE [Name] = 'Electronics'), 'Women Perfume', 0.15, GETUTCDATE());

INSERT INTO [Products] ([SubCategoryId], [Name], [CustomsRate], [CreatedAt]) VALUES ((SELECT [SubCategoryId] FROM [SubCategories] WHERE [Name] = 'Electronics'), 'Unisex Perfume', 0.15, GETUTCDATE());

INSERT INTO [Products] ([SubCategoryId], [Name], [CustomsRate], [CreatedAt]) VALUES ((SELECT [SubCategoryId] FROM [SubCategories] WHERE [Name] = 'Electronics'), 'Body Lotion', 0.08, GETUTCDATE());

INSERT INTO [Products] ([SubCategoryId], [Name], [CustomsRate], [CreatedAt]) VALUES ((SELECT [SubCategoryId] FROM [SubCategories] WHERE [Name] = 'Electronics'), 'Deodorant', 0.1, GETUTCDATE());

INSERT INTO [Products] ([SubCategoryId], [Name], [CustomsRate], [CreatedAt]) VALUES ((SELECT [SubCategoryId] FROM [SubCategories] WHERE [Name] = 'Electronics'), 'Soap', 0.05, GETUTCDATE());

INSERT INTO [Products] ([SubCategoryId], [Name], [CustomsRate], [CreatedAt]) VALUES ((SELECT [SubCategoryId] FROM [SubCategories] WHERE [Name] = 'Electronics'), 'Makeup Brushes', 0.06, GETUTCDATE());

INSERT INTO [Products] ([SubCategoryId], [Name], [CustomsRate], [CreatedAt]) VALUES ((SELECT [SubCategoryId] FROM [SubCategories] WHERE [Name] = 'Electronics'), 'Hair Dryer', 0.07, GETUTCDATE());

INSERT INTO [Products] ([SubCategoryId], [Name], [CustomsRate], [CreatedAt]) VALUES ((SELECT [SubCategoryId] FROM [SubCategories] WHERE [Name] = 'Electronics'), 'Hair Straightener', 0.07, GETUTCDATE());

INSERT INTO [Products] ([SubCategoryId], [Name], [CustomsRate], [CreatedAt]) VALUES ((SELECT [SubCategoryId] FROM [SubCategories] WHERE [Name] = 'Electronics'), 'School Books', 0.0, GETUTCDATE());

INSERT INTO [Products] ([SubCategoryId], [Name], [CustomsRate], [CreatedAt]) VALUES ((SELECT [SubCategoryId] FROM [SubCategories] WHERE [Name] = 'Electronics'), 'University Books', 0.0, GETUTCDATE());

INSERT INTO [Products] ([SubCategoryId], [Name], [CustomsRate], [CreatedAt]) VALUES ((SELECT [SubCategoryId] FROM [SubCategories] WHERE [Name] = 'Electronics'), 'Language Learning', 0.0, GETUTCDATE());

INSERT INTO [Products] ([SubCategoryId], [Name], [CustomsRate], [CreatedAt]) VALUES ((SELECT [SubCategoryId] FROM [SubCategories] WHERE [Name] = 'Electronics'), 'Romance Novel', 0.0, GETUTCDATE());

INSERT INTO [Products] ([SubCategoryId], [Name], [CustomsRate], [CreatedAt]) VALUES ((SELECT [SubCategoryId] FROM [SubCategories] WHERE [Name] = 'Electronics'), 'Mystery Novel', 0.0, GETUTCDATE());

INSERT INTO [Products] ([SubCategoryId], [Name], [CustomsRate], [CreatedAt]) VALUES ((SELECT [SubCategoryId] FROM [SubCategories] WHERE [Name] = 'Electronics'), 'Fantasy Novel', 0.0, GETUTCDATE());

INSERT INTO [Products] ([SubCategoryId], [Name], [CustomsRate], [CreatedAt]) VALUES ((SELECT [SubCategoryId] FROM [SubCategories] WHERE [Name] = 'Electronics'), 'Quran', 0.0, GETUTCDATE());

INSERT INTO [Products] ([SubCategoryId], [Name], [CustomsRate], [CreatedAt]) VALUES ((SELECT [SubCategoryId] FROM [SubCategories] WHERE [Name] = 'Electronics'), 'Bible', 0.0, GETUTCDATE());

INSERT INTO [Products] ([SubCategoryId], [Name], [CustomsRate], [CreatedAt]) VALUES ((SELECT [SubCategoryId] FROM [SubCategories] WHERE [Name] = 'Electronics'), 'Hadith Books', 0.0, GETUTCDATE());

INSERT INTO [Products] ([SubCategoryId], [Name], [CustomsRate], [CreatedAt]) VALUES ((SELECT [SubCategoryId] FROM [SubCategories] WHERE [Name] = 'Electronics'), 'Story Books', 0.0, GETUTCDATE());

INSERT INTO [Products] ([SubCategoryId], [Name], [CustomsRate], [CreatedAt]) VALUES ((SELECT [SubCategoryId] FROM [SubCategories] WHERE [Name] = 'Electronics'), 'Coloring Books', 0.0, GETUTCDATE());

INSERT INTO [Products] ([SubCategoryId], [Name], [CustomsRate], [CreatedAt]) VALUES ((SELECT [SubCategoryId] FROM [SubCategories] WHERE [Name] = 'Electronics'), 'Computer Science Book', 0.0, GETUTCDATE());

INSERT INTO [Products] ([SubCategoryId], [Name], [CustomsRate], [CreatedAt]) VALUES ((SELECT [SubCategoryId] FROM [SubCategories] WHERE [Name] = 'Electronics'), 'Engineering Book', 0.0, GETUTCDATE());

INSERT INTO [Products] ([SubCategoryId], [Name], [CustomsRate], [CreatedAt]) VALUES ((SELECT [SubCategoryId] FROM [SubCategories] WHERE [Name] = 'Electronics'), 'Medical Book', 0.0, GETUTCDATE());

INSERT INTO [Products] ([SubCategoryId], [Name], [CustomsRate], [CreatedAt]) VALUES ((SELECT [SubCategoryId] FROM [SubCategories] WHERE [Name] = 'Electronics'), 'Marvel Comics', 0.0, GETUTCDATE());

INSERT INTO [Products] ([SubCategoryId], [Name], [CustomsRate], [CreatedAt]) VALUES ((SELECT [SubCategoryId] FROM [SubCategories] WHERE [Name] = 'Electronics'), 'DC Comics', 0.0, GETUTCDATE());

INSERT INTO [Products] ([SubCategoryId], [Name], [CustomsRate], [CreatedAt]) VALUES ((SELECT [SubCategoryId] FROM [SubCategories] WHERE [Name] = 'Electronics'), 'Manga', 0.0, GETUTCDATE());

INSERT INTO [Products] ([SubCategoryId], [Name], [CustomsRate], [CreatedAt]) VALUES ((SELECT [SubCategoryId] FROM [SubCategories] WHERE [Name] = 'Electronics'), 'Fashion Magazine', 0.0, GETUTCDATE());

INSERT INTO [Products] ([SubCategoryId], [Name], [CustomsRate], [CreatedAt]) VALUES ((SELECT [SubCategoryId] FROM [SubCategories] WHERE [Name] = 'Electronics'), 'Technology Magazine', 0.0, GETUTCDATE());

INSERT INTO [Products] ([SubCategoryId], [Name], [CustomsRate], [CreatedAt]) VALUES ((SELECT [SubCategoryId] FROM [SubCategories] WHERE [Name] = 'Electronics'), 'Sports Magazine', 0.0, GETUTCDATE());

INSERT INTO [Products] ([SubCategoryId], [Name], [CustomsRate], [CreatedAt]) VALUES ((SELECT [SubCategoryId] FROM [SubCategories] WHERE [Name] = 'Electronics'), 'Plush Toys', 0.1, GETUTCDATE());

INSERT INTO [Products] ([SubCategoryId], [Name], [CustomsRate], [CreatedAt]) VALUES ((SELECT [SubCategoryId] FROM [SubCategories] WHERE [Name] = 'Electronics'), 'Toy Cars', 0.1, GETUTCDATE());

INSERT INTO [Products] ([SubCategoryId], [Name], [CustomsRate], [CreatedAt]) VALUES ((SELECT [SubCategoryId] FROM [SubCategories] WHERE [Name] = 'Electronics'), 'Building Blocks', 0.1, GETUTCDATE());

INSERT INTO [Products] ([SubCategoryId], [Name], [CustomsRate], [CreatedAt]) VALUES ((SELECT [SubCategoryId] FROM [SubCategories] WHERE [Name] = 'Electronics'), 'Puzzles', 0.08, GETUTCDATE());

INSERT INTO [Products] ([SubCategoryId], [Name], [CustomsRate], [CreatedAt]) VALUES ((SELECT [SubCategoryId] FROM [SubCategories] WHERE [Name] = 'Electronics'), 'Learning Boards', 0.08, GETUTCDATE());

INSERT INTO [Products] ([SubCategoryId], [Name], [CustomsRate], [CreatedAt]) VALUES ((SELECT [SubCategoryId] FROM [SubCategories] WHERE [Name] = 'Electronics'), 'STEM Toys', 0.09, GETUTCDATE());

INSERT INTO [Products] ([SubCategoryId], [Name], [CustomsRate], [CreatedAt]) VALUES ((SELECT [SubCategoryId] FROM [SubCategories] WHERE [Name] = 'Electronics'), 'Superhero Figures', 0.12, GETUTCDATE());

INSERT INTO [Products] ([SubCategoryId], [Name], [CustomsRate], [CreatedAt]) VALUES ((SELECT [SubCategoryId] FROM [SubCategories] WHERE [Name] = 'Electronics'), 'Anime Figures', 0.12, GETUTCDATE());

INSERT INTO [Products] ([SubCategoryId], [Name], [CustomsRate], [CreatedAt]) VALUES ((SELECT [SubCategoryId] FROM [SubCategories] WHERE [Name] = 'Electronics'), 'Barbie', 0.12, GETUTCDATE());

INSERT INTO [Products] ([SubCategoryId], [Name], [CustomsRate], [CreatedAt]) VALUES ((SELECT [SubCategoryId] FROM [SubCategories] WHERE [Name] = 'Electronics'), 'Baby Dolls', 0.12, GETUTCDATE());

INSERT INTO [Products] ([SubCategoryId], [Name], [CustomsRate], [CreatedAt]) VALUES ((SELECT [SubCategoryId] FROM [SubCategories] WHERE [Name] = 'Electronics'), 'Chess', 0.06, GETUTCDATE());

INSERT INTO [Products] ([SubCategoryId], [Name], [CustomsRate], [CreatedAt]) VALUES ((SELECT [SubCategoryId] FROM [SubCategories] WHERE [Name] = 'Electronics'), 'Monopoly', 0.06, GETUTCDATE());

INSERT INTO [Products] ([SubCategoryId], [Name], [CustomsRate], [CreatedAt]) VALUES ((SELECT [SubCategoryId] FROM [SubCategories] WHERE [Name] = 'Electronics'), 'Ludo', 0.06, GETUTCDATE());

INSERT INTO [Products] ([SubCategoryId], [Name], [CustomsRate], [CreatedAt]) VALUES ((SELECT [SubCategoryId] FROM [SubCategories] WHERE [Name] = 'Electronics'), 'Bicycles', 0.08, GETUTCDATE());

INSERT INTO [Products] ([SubCategoryId], [Name], [CustomsRate], [CreatedAt]) VALUES ((SELECT [SubCategoryId] FROM [SubCategories] WHERE [Name] = 'Electronics'), 'Scooters', 0.08, GETUTCDATE());

INSERT INTO [Products] ([SubCategoryId], [Name], [CustomsRate], [CreatedAt]) VALUES ((SELECT [SubCategoryId] FROM [SubCategories] WHERE [Name] = 'Electronics'), 'Balls', 0.05, GETUTCDATE());

INSERT INTO [Products] ([SubCategoryId], [Name], [CustomsRate], [CreatedAt]) VALUES ((SELECT [SubCategoryId] FROM [SubCategories] WHERE [Name] = 'Electronics'), 'Antibiotics', 0.0, GETUTCDATE());

INSERT INTO [Products] ([SubCategoryId], [Name], [CustomsRate], [CreatedAt]) VALUES ((SELECT [SubCategoryId] FROM [SubCategories] WHERE [Name] = 'Electronics'), 'Pain Killers', 0.0, GETUTCDATE());

INSERT INTO [Products] ([SubCategoryId], [Name], [CustomsRate], [CreatedAt]) VALUES ((SELECT [SubCategoryId] FROM [SubCategories] WHERE [Name] = 'Electronics'), 'Blood Pressure Medicine', 0.0, GETUTCDATE());

INSERT INTO [Products] ([SubCategoryId], [Name], [CustomsRate], [CreatedAt]) VALUES ((SELECT [SubCategoryId] FROM [SubCategories] WHERE [Name] = 'Electronics'), 'Cold & Flu', 0.0, GETUTCDATE());

INSERT INTO [Products] ([SubCategoryId], [Name], [CustomsRate], [CreatedAt]) VALUES ((SELECT [SubCategoryId] FROM [SubCategories] WHERE [Name] = 'Electronics'), 'Headache Relief', 0.0, GETUTCDATE());

INSERT INTO [Products] ([SubCategoryId], [Name], [CustomsRate], [CreatedAt]) VALUES ((SELECT [SubCategoryId] FROM [SubCategories] WHERE [Name] = 'Electronics'), 'Antacid', 0.0, GETUTCDATE());

INSERT INTO [Products] ([SubCategoryId], [Name], [CustomsRate], [CreatedAt]) VALUES ((SELECT [SubCategoryId] FROM [SubCategories] WHERE [Name] = 'Electronics'), 'Vitamin C', 0.05, GETUTCDATE());

INSERT INTO [Products] ([SubCategoryId], [Name], [CustomsRate], [CreatedAt]) VALUES ((SELECT [SubCategoryId] FROM [SubCategories] WHERE [Name] = 'Electronics'), 'Omega 3', 0.05, GETUTCDATE());

INSERT INTO [Products] ([SubCategoryId], [Name], [CustomsRate], [CreatedAt]) VALUES ((SELECT [SubCategoryId] FROM [SubCategories] WHERE [Name] = 'Electronics'), 'Protein Powder', 0.06, GETUTCDATE());

INSERT INTO [Products] ([SubCategoryId], [Name], [CustomsRate], [CreatedAt]) VALUES ((SELECT [SubCategoryId] FROM [SubCategories] WHERE [Name] = 'Electronics'), 'Blood Pressure Monitor', 0.04, GETUTCDATE());

INSERT INTO [Products] ([SubCategoryId], [Name], [CustomsRate], [CreatedAt]) VALUES ((SELECT [SubCategoryId] FROM [SubCategories] WHERE [Name] = 'Electronics'), 'Glucometer', 0.04, GETUTCDATE());

INSERT INTO [Products] ([SubCategoryId], [Name], [CustomsRate], [CreatedAt]) VALUES ((SELECT [SubCategoryId] FROM [SubCategories] WHERE [Name] = 'Electronics'), 'Nebulizer', 0.04, GETUTCDATE());

INSERT INTO [Products] ([SubCategoryId], [Name], [CustomsRate], [CreatedAt]) VALUES ((SELECT [SubCategoryId] FROM [SubCategories] WHERE [Name] = 'Electronics'), 'Bandages', 0.0, GETUTCDATE());

INSERT INTO [Products] ([SubCategoryId], [Name], [CustomsRate], [CreatedAt]) VALUES ((SELECT [SubCategoryId] FROM [SubCategories] WHERE [Name] = 'Electronics'), 'Antiseptics', 0.0, GETUTCDATE());

INSERT INTO [Products] ([SubCategoryId], [Name], [CustomsRate], [CreatedAt]) VALUES ((SELECT [SubCategoryId] FROM [SubCategories] WHERE [Name] = 'Electronics'), 'First Aid Kit', 0.02, GETUTCDATE());

INSERT INTO [Products] ([SubCategoryId], [Name], [CustomsRate], [CreatedAt]) VALUES ((SELECT [SubCategoryId] FROM [SubCategories] WHERE [Name] = 'Electronics'), 'Thermometer', 0.02, GETUTCDATE());

INSERT INTO [Products] ([SubCategoryId], [Name], [CustomsRate], [CreatedAt]) VALUES ((SELECT [SubCategoryId] FROM [SubCategories] WHERE [Name] = 'Electronics'), 'Face Masks', 0.01, GETUTCDATE());

INSERT INTO [Products] ([SubCategoryId], [Name], [CustomsRate], [CreatedAt]) VALUES ((SELECT [SubCategoryId] FROM [SubCategories] WHERE [Name] = 'Electronics'), 'Hand Sanitizer', 0.03, GETUTCDATE());

INSERT INTO [Products] ([SubCategoryId], [Name], [CustomsRate], [CreatedAt]) VALUES ((SELECT [SubCategoryId] FROM [SubCategories] WHERE [Name] = 'Electronics'), 'Sofas', 0.08, GETUTCDATE());

INSERT INTO [Products] ([SubCategoryId], [Name], [CustomsRate], [CreatedAt]) VALUES ((SELECT [SubCategoryId] FROM [SubCategories] WHERE [Name] = 'Electronics'), 'Coffee Tables', 0.06, GETUTCDATE());

INSERT INTO [Products] ([SubCategoryId], [Name], [CustomsRate], [CreatedAt]) VALUES ((SELECT [SubCategoryId] FROM [SubCategories] WHERE [Name] = 'Electronics'), 'TV Units', 0.07, GETUTCDATE());

INSERT INTO [Products] ([SubCategoryId], [Name], [CustomsRate], [CreatedAt]) VALUES ((SELECT [SubCategoryId] FROM [SubCategories] WHERE [Name] = 'Electronics'), 'Beds', 0.08, GETUTCDATE());

INSERT INTO [Products] ([SubCategoryId], [Name], [CustomsRate], [CreatedAt]) VALUES ((SELECT [SubCategoryId] FROM [SubCategories] WHERE [Name] = 'Electronics'), 'Wardrobes', 0.07, GETUTCDATE());

INSERT INTO [Products] ([SubCategoryId], [Name], [CustomsRate], [CreatedAt]) VALUES ((SELECT [SubCategoryId] FROM [SubCategories] WHERE [Name] = 'Electronics'), 'Nightstands', 0.06, GETUTCDATE());

INSERT INTO [Products] ([SubCategoryId], [Name], [CustomsRate], [CreatedAt]) VALUES ((SELECT [SubCategoryId] FROM [SubCategories] WHERE [Name] = 'Electronics'), 'Office Chairs', 0.07, GETUTCDATE());

INSERT INTO [Products] ([SubCategoryId], [Name], [CustomsRate], [CreatedAt]) VALUES ((SELECT [SubCategoryId] FROM [SubCategories] WHERE [Name] = 'Electronics'), 'Desks', 0.07, GETUTCDATE());

INSERT INTO [Products] ([SubCategoryId], [Name], [CustomsRate], [CreatedAt]) VALUES ((SELECT [SubCategoryId] FROM [SubCategories] WHERE [Name] = 'Electronics'), 'Book Shelves', 0.06, GETUTCDATE());

INSERT INTO [Products] ([SubCategoryId], [Name], [CustomsRate], [CreatedAt]) VALUES ((SELECT [SubCategoryId] FROM [SubCategories] WHERE [Name] = 'Electronics'), 'Cabinets', 0.06, GETUTCDATE());

INSERT INTO [Products] ([SubCategoryId], [Name], [CustomsRate], [CreatedAt]) VALUES ((SELECT [SubCategoryId] FROM [SubCategories] WHERE [Name] = 'Electronics'), 'Dining Tables', 0.06, GETUTCDATE());

INSERT INTO [Products] ([SubCategoryId], [Name], [CustomsRate], [CreatedAt]) VALUES ((SELECT [SubCategoryId] FROM [SubCategories] WHERE [Name] = 'Electronics'), 'Garden Chairs', 0.06, GETUTCDATE());

INSERT INTO [Products] ([SubCategoryId], [Name], [CustomsRate], [CreatedAt]) VALUES ((SELECT [SubCategoryId] FROM [SubCategories] WHERE [Name] = 'Electronics'), 'Outdoor Tables', 0.06, GETUTCDATE());

INSERT INTO [Products] ([SubCategoryId], [Name], [CustomsRate], [CreatedAt]) VALUES ((SELECT [SubCategoryId] FROM [SubCategories] WHERE [Name] = 'Electronics'), 'Umbrellas', 0.05, GETUTCDATE());

INSERT INTO [Products] ([SubCategoryId], [Name], [CustomsRate], [CreatedAt]) VALUES ((SELECT [SubCategoryId] FROM [SubCategories] WHERE [Name] = 'Electronics'), 'Curtains', 0.05, GETUTCDATE());

INSERT INTO [Products] ([SubCategoryId], [Name], [CustomsRate], [CreatedAt]) VALUES ((SELECT [SubCategoryId] FROM [SubCategories] WHERE [Name] = 'Electronics'), 'Carpets', 0.06, GETUTCDATE());

INSERT INTO [Products] ([SubCategoryId], [Name], [CustomsRate], [CreatedAt]) VALUES ((SELECT [SubCategoryId] FROM [SubCategories] WHERE [Name] = 'Electronics'), 'Wall Art', 0.04, GETUTCDATE());

INSERT INTO [Products] ([SubCategoryId], [Name], [CustomsRate], [CreatedAt]) VALUES ((SELECT [SubCategoryId] FROM [SubCategories] WHERE [Name] = 'Electronics'), 'Oil Painting', 0.04, GETUTCDATE());

INSERT INTO [Products] ([SubCategoryId], [Name], [CustomsRate], [CreatedAt]) VALUES ((SELECT [SubCategoryId] FROM [SubCategories] WHERE [Name] = 'Electronics'), 'Acrylic Painting', 0.04, GETUTCDATE());

INSERT INTO [Products] ([SubCategoryId], [Name], [CustomsRate], [CreatedAt]) VALUES ((SELECT [SubCategoryId] FROM [SubCategories] WHERE [Name] = 'Electronics'), 'Watercolor Painting', 0.03, GETUTCDATE());

INSERT INTO [Products] ([SubCategoryId], [Name], [CustomsRate], [CreatedAt]) VALUES ((SELECT [SubCategoryId] FROM [SubCategories] WHERE [Name] = 'Electronics'), 'Stone Sculpture', 0.05, GETUTCDATE());

INSERT INTO [Products] ([SubCategoryId], [Name], [CustomsRate], [CreatedAt]) VALUES ((SELECT [SubCategoryId] FROM [SubCategories] WHERE [Name] = 'Electronics'), 'Metal Sculpture', 0.05, GETUTCDATE());

INSERT INTO [Products] ([SubCategoryId], [Name], [CustomsRate], [CreatedAt]) VALUES ((SELECT [SubCategoryId] FROM [SubCategories] WHERE [Name] = 'Electronics'), 'Wood Sculpture', 0.045, GETUTCDATE());

INSERT INTO [Products] ([SubCategoryId], [Name], [CustomsRate], [CreatedAt]) VALUES ((SELECT [SubCategoryId] FROM [SubCategories] WHERE [Name] = 'Electronics'), 'Handmade Pottery', 0.04, GETUTCDATE());

INSERT INTO [Products] ([SubCategoryId], [Name], [CustomsRate], [CreatedAt]) VALUES ((SELECT [SubCategoryId] FROM [SubCategories] WHERE [Name] = 'Electronics'), 'Wooden Crafts', 0.04, GETUTCDATE());

INSERT INTO [Products] ([SubCategoryId], [Name], [CustomsRate], [CreatedAt]) VALUES ((SELECT [SubCategoryId] FROM [SubCategories] WHERE [Name] = 'Electronics'), 'Handmade Textile Art', 0.04, GETUTCDATE());

INSERT INTO [Products] ([SubCategoryId], [Name], [CustomsRate], [CreatedAt]) VALUES ((SELECT [SubCategoryId] FROM [SubCategories] WHERE [Name] = 'Electronics'), 'Digital Illustration', 0.02, GETUTCDATE());

INSERT INTO [Products] ([SubCategoryId], [Name], [CustomsRate], [CreatedAt]) VALUES ((SELECT [SubCategoryId] FROM [SubCategories] WHERE [Name] = 'Electronics'), '3D Art', 0.02, GETUTCDATE());

INSERT INTO [Products] ([SubCategoryId], [Name], [CustomsRate], [CreatedAt]) VALUES ((SELECT [SubCategoryId] FROM [SubCategories] WHERE [Name] = 'Electronics'), 'Digital Prints', 0.02, GETUTCDATE());

INSERT INTO [Products] ([SubCategoryId], [Name], [CustomsRate], [CreatedAt]) VALUES ((SELECT [SubCategoryId] FROM [SubCategories] WHERE [Name] = 'Electronics'), 'Paint Brushes', 0.03, GETUTCDATE());

INSERT INTO [Products] ([SubCategoryId], [Name], [CustomsRate], [CreatedAt]) VALUES ((SELECT [SubCategoryId] FROM [SubCategories] WHERE [Name] = 'Electronics'), 'Canvas', 0.03, GETUTCDATE());

INSERT INTO [Products] ([SubCategoryId], [Name], [CustomsRate], [CreatedAt]) VALUES ((SELECT [SubCategoryId] FROM [SubCategories] WHERE [Name] = 'Electronics'), 'Acrylic Paint Set', 0.03, GETUTCDATE());

INSERT INTO [Products] ([SubCategoryId], [Name], [CustomsRate], [CreatedAt]) VALUES ((SELECT [SubCategoryId] FROM [SubCategories] WHERE [Name] = 'Electronics'), 'Arabic Calligraphy Piece', 0.04, GETUTCDATE());

INSERT INTO [Products] ([SubCategoryId], [Name], [CustomsRate], [CreatedAt]) VALUES ((SELECT [SubCategoryId] FROM [SubCategories] WHERE [Name] = 'Electronics'), 'English Calligraphy Piece', 0.04, GETUTCDATE());

INSERT INTO [Products] ([SubCategoryId], [Name], [CustomsRate], [CreatedAt]) VALUES ((SELECT [SubCategoryId] FROM [SubCategories] WHERE [Name] = 'Electronics'), 'Calligraphy Tools', 0.03, GETUTCDATE());

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260309121723_SeedCustomsData_Manual_SQL', N'9.0.0');

COMMIT;
GO


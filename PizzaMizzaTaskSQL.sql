create database PizzaMizzaDb

use PizzaMizzaDb

create table Pizzas(
Id int primary key Identity,
Name nvarchar(30),
Type_Id int foreign key references PizzaTypes(Id),
Price int,
)

create table PizzaTypes(
Id int primary key identity,
Name nvarchar(30),
)


create table Ingredients(
Id int primary key identity,
Name nvarchar(30),
Quantity nvarchar(30),
)	


create table ProductsIngredients(
Pizza_Id int foreign key references Pizzas(Id),
Ingredients_Id int foreign key references Ingredients(Id)
)

create table DrinksSizes(
Id int primary key identity,
Name nvarchar(30),
)

create table Drinks(
Id int primary key identity,
Name nvarchar(30),
Size_id int foreign key references DrinksSizes(Id),
Price int ,
)

create table FoodCombos(
İd int primary key identity,
Name nvarchar(30),
Pizza_Id int foreign key references Pizzas(Id),
Drink_Id int foreign key references Drinks(Id),
Snack_Id int foreign key references Snacks(Id),
Price int 
)

create table Desserts(
Id int primary key identity,
Name nvarchar(30),
Price int 
)

create table Sauces(
Id int primary key identity,
Name nvarchar(30),
Price int 
)

create table Snacks(
Id int primary key identity,
Name nvarchar(30),
Price int 
)

create table IngredientsSnacks(
Snack_Id int foreign key references Snacks(Id),
Ingredients_Id int foreign key references Ingredients(Id)
)

create table Soups(
Id int primary key identity,
Name nvarchar(30),
Price int 
)
create table IngredientsSoups(
Soup_Id int foreign key references Soups(Id),
Ingredients_Id int foreign key references Ingredients(Id)
)

create table Pastries(
Id int primary key identity,
Name nvarchar(30),
Price int 
)

create table IngredientsPastries(
Pastrie_Id int foreign key references Pastries(Id),
Ingredients_Id int foreign key references Ingredients(Id)
)









-- PizzaTypes
INSERT INTO PizzaTypes (Name) VALUES
('boyuk')


-- Pizzas
INSERT INTO Pizzas (Name, Type_Id, Price) VALUES
('Toyuqlu', 1, 11),
('Mexicano', 2, 16);

INSERT INTO Ingredients (Name, Quantity) VALUES
('bulyon', '530g'),
('mercimek', '420g');

INSERT INTO ProductsIngredients (Pizza_Id, Ingredients_Id) VALUES
(1, 2),  -- Margarita + Cheese
(2, 3);  -- Pepperoni + Tomato

INSERT INTO DrinksSizes (Name) VALUES
('0.5L'),
('1L');

INSERT INTO Drinks (Name, Size_Id, Price) VALUES
('Coca Cola', 1, 3),
('Fanta', 2, 4);


INSERT INTO Snacks (Name, Price) VALUES
('French Fries', 5),
('Chicken Nuggets', 7);


INSERT INTO IngredientsSnacks (Snack_Id, Ingredients_Id) VALUES
(1, 1),  -- Fries + Cheese
(2, 2);  -- Nuggets + Tomato


INSERT INTO Desserts (Name, Price) VALUES
('Brownie', 6),
('Ice Cream', 4);


INSERT INTO Sauces (Name, Price) VALUES
('Garlic Sauce', 2),
('BBQ Sauce', 2);


INSERT INTO Soups (Name, Price) VALUES
('Tomato Soup', 8),
('Chicken Soup', 9);

INSERT INTO IngredientsSoups (Soup_Id, Ingredients_Id) VALUES
(1, 2),  -- Tomato Soup + Tomato
(2, 1);  -- Chicken Soup + Cheese


INSERT INTO Pastries (Name, Price) VALUES
('Pizza Bread', 3),
('Cheese Bread', 4);

INSERT INTO IngredientsPastries (Pastrie_Id, Ingredients_Id) VALUES
(1, 2),
(2, 1);


INSERT INTO FoodCombos (Name, Pizza_Id, Drink_Id, Snack_Id, Price) VALUES
('Combo 1', 1, 1, 1, 20),
('Combo 2', 2, 2, 2, 28);


select*from Pizzas


SELECT p.Id, p.Name, t.Name AS TypeName, p.Price
FROM Pizzas p
JOIN PizzaTypes t ON p.Type_Id = t.Id;

select*from Ingredients
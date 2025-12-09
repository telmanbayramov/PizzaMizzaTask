using Microsoft.Data.SqlClient;

string conn = @"Data Source=DESKTOP-F61941G\SQLEXPRESS;Database=PizzaMizzaDb;Integrated Security=True;TrustServerCertificate=True;";

using (SqlConnection sqlConnection = new SqlConnection(conn))
{
    sqlConnection.Open(); 

    while (true)
    {
        Console.WriteLine("ESAS SEHIFE");
        Console.WriteLine("1. BUTUN PIZZALAR");
        Console.WriteLine("2. PIZZA YARATMAQ");
        Console.WriteLine("SECIMINIZI QEYD EDIN");
        string input = Console.ReadLine();

        if (input == "1")
        {
            string pizzaCommand = @"
                SELECT p.Id, p.Name, t.Name AS TypeName, p.Price
                FROM Pizzas p
                JOIN PizzaTypes t ON p.Type_Id = t.Id";

            using (SqlCommand sqlCommand = new SqlCommand(pizzaCommand, sqlConnection))
            {
                using (SqlDataReader reader = sqlCommand.ExecuteReader())
                {
                    Console.WriteLine("\nPizzalar:");
                    while (reader.Read())
                    {
                        Console.WriteLine($"{reader["Id"]} - {reader["Name"]} - {reader["TypeName"]} - {reader["Price"]} AZN");
                    }
                }
            }

            Console.WriteLine("\nPizza haqqında ətraflı məlumat almaq istəyirsizsə pizzanın Id-sini daxil edin, istəmirsinizsə 0 daxil edin:");
            string idInput = Console.ReadLine();

            if (idInput == "0")
            {
                Console.Clear();
                continue;
            }

            if (int.TryParse(idInput, out int pizzaId))
            {
                string ingredientCommand = @"
                    SELECT i.Name 
                    FROM ProductsIngredients pi
                    JOIN Ingredients i ON pi.Ingredients_Id = i.Id
                    WHERE pi.Pizza_Id = @PizzaId";

                using (SqlCommand sqlCommand = new SqlCommand(ingredientCommand, sqlConnection))
                {
                    sqlCommand.Parameters.AddWithValue("@PizzaId", pizzaId);

                    using (SqlDataReader reader = sqlCommand.ExecuteReader())
                    {
                        Console.WriteLine("\nİngredientlər:");
                        while (reader.Read())
                        {
                            Console.WriteLine($"- {reader["Name"]}");
                        }
                    }
                }

                Console.WriteLine("\nMenyuya qayıtmaq üçün hər hansı bir düyməni basın");
                Console.ReadKey();
                Console.Clear();
            }
            else
            {
                Console.WriteLine("Yanlış Id daxil edildi");
            }
        }
        else if (input == "2")
        {
            Console.WriteLine("Yeni pizzanın adını daxil edin:");
            string pizzaName = Console.ReadLine();

            string typeQuery = "SELECT Id, Name FROM PizzaTypes";
            using (SqlCommand cmd = new SqlCommand(typeQuery, sqlConnection))
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    Console.WriteLine("\nMövcud pizza tipləri:");
                    while (reader.Read())
                    {
                        Console.WriteLine($"{reader["Id"]} - {reader["Name"]}");
                    }
                }
            }

            Console.WriteLine("Pizza tipinin Id-sini daxil edin:");
            int typeId = int.Parse(Console.ReadLine());

            Console.WriteLine("Pizzanın qiymətini daxil edin:");
            decimal price = decimal.Parse(Console.ReadLine());

            string ingredientQuery = "SELECT Id, Name FROM Ingredients";
            using (SqlCommand cmd = new SqlCommand(ingredientQuery, sqlConnection))
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    Console.WriteLine("\nMövcud ingredientlər:");
                    while (reader.Read())
                    {
                        Console.WriteLine($"{reader["Id"]} - {reader["Name"]}");
                    }
                }
            }

            Console.WriteLine("Seçmək istədiyiniz ingredientlərin Id-lərini vergüllə ayıraraq daxil edin (Məs: 1,2,5):");
            string selectedIngredients = Console.ReadLine();
            string[] ingredientIds = selectedIngredients.Split(',');

            string insertPizzaQuery = "INSERT INTO Pizzas (Name, Type_Id, Price) OUTPUT INSERTED.Id VALUES (@Name, @TypeId, @Price)";
            int newPizzaId;
            using (SqlCommand cmd = new SqlCommand(insertPizzaQuery, sqlConnection))
            {
                cmd.Parameters.AddWithValue("@Name", pizzaName);
                cmd.Parameters.AddWithValue("@TypeId", typeId);
                cmd.Parameters.AddWithValue("@Price", price);

                newPizzaId = (int)cmd.ExecuteScalar();
            }

            foreach (string ingIdStr in ingredientIds)
            {
                if (int.TryParse(ingIdStr.Trim(), out int ingId))
                {
                    string insertPivot = "INSERT INTO ProductsIngredients (Pizza_Id, Ingredients_Id) VALUES (@PizzaId, @IngredientId)";
                    using (SqlCommand cmd = new SqlCommand(insertPivot, sqlConnection))
                    {
                        cmd.Parameters.AddWithValue("@PizzaId", newPizzaId);
                        cmd.Parameters.AddWithValue("@IngredientId", ingId);
                        cmd.ExecuteNonQuery();
                    }
                }
            }

            Console.WriteLine("Yeni pizza uğurla əlavə olundu!\n");
        }
        else
        {
            Console.WriteLine("Yanlış seçim");
        }
    }
}

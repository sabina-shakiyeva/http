using ServerSide;

public class Program
{
    public static async Task Main(string[] args)
    {

        var client = new HttpClient();
        bool run = true;

        while (run)
        {
            Console.WriteLine("1)Get all users");
            Console.WriteLine("2)Add new user");
            Console.WriteLine("3)Delete user");
            Console.WriteLine("4)Exit");

            Console.Write("Choice");
            int choice=int.Parse(Console.ReadLine());


            if (choice == 1)
            {
                var result = await client.GetStringAsync("http://localhost:5000/");
                Console.WriteLine("Users:");
                Console.WriteLine(result);


            }
            else if (choice == 2)
            {
                Console.Write("Name:");
                string firstName = Console.ReadLine();
                Console.WriteLine("LastName:");
                string lastName = Console.ReadLine();

                var newUser = new User { FirstName = firstName, LastName = lastName };
                var userJs = System.Text.Json.JsonSerializer.Serialize(newUser);
                var content = new StringContent(userJs);

                await client.PostAsync("http://localhost:5000/", content);
                Console.WriteLine("New user added");


            }
            else if (choice == 3)
            {
                Console.Write("Id for delete:");
                int userId = int.Parse(Console.ReadLine());
                await client.DeleteAsync($"http://localhost:5000/{userId}");
                Console.WriteLine($" ID {userId} is deleted");

            }
            else if (choice == 4)
            {
                run = false;
                break;
            }
        }
    }
}


//var sql = await result.Content.ReadAsStringAsync();
//Console.WriteLine();


using Microsoft.EntityFrameworkCore;
using ServerSide;
using System.Net;

public class Host
{
    private int _port;
    private HttpListener _listener;

    public Host(int port)
    {

        _port = port;

    }
    public void Run()
    {
        _listener = new HttpListener();
        _listener.Prefixes.Add($"http://localhost:{_port}/");
        _listener.Start();

        Console.WriteLine($"Http server started on {_port}");
        while (true)
        {
            var context = _listener.GetContext();
            Task.Run(() => { HandleRequest(context); });
        }
    }

    private async void HandleRequest(HttpListenerContext context)
    {
        var response = context.Response;
        var stream = response.OutputStream;
        if (context.Request.HttpMethod == "GET")
        {
            using(var dbContext=new AppDbContext())
            {
                var users=dbContext.Users.ToList();
                var userJson=System.Text.Json.JsonSerializer.Serialize(users);
                var buffer = System.Text.Encoding.UTF8.GetBytes(userJson);
                await stream.WriteAsync(buffer, 0, buffer.Length);
                //var bytes=System.Text.Encoding.UTF8.GetBytes(userJson);
                //await stream.WriteAsync(bytes);
            }
        }
        else if (context.Request.HttpMethod == "POST")
        {
            using (var reader = new StreamReader(context.Request.InputStream))
            {
                var data=await reader.ReadToEndAsync();
                var newUser = System.Text.Json.JsonSerializer.Deserialize<User>(data);
                using(var dbContext=new AppDbContext())
                {
                    dbContext.Users.Add(newUser);
                    await dbContext.SaveChangesAsync();
                }
            }

        }
        else if(context.Request.HttpMethod == "DELETE")
        {
            var userId = int.Parse(context.Request.Url.AbsolutePath.Trim('/'));
            if (userId!=null)
            {
                using(var dbContext=new AppDbContext())
                {
                    var delete=dbContext.Users.FirstOrDefault(u=>u.Id==userId);

                    if (delete != null)
                    {
                        dbContext.Users.Remove(delete);
                        await dbContext.SaveChangesAsync();
                        Console.WriteLine($"ID {userId} is deleted");
                    }
                    else
                    {
                        Console.WriteLine("not found");
                    }
                }
            }
            else
            {
                Console.WriteLine("id not found");
            }
            
        }
        response.StatusCode = (int)HttpStatusCode.OK;
        response.Close();

    }
}
public class Program
{
    public static void Main(string[] args)
    {
        int port = 5000;
        Host host = new Host(port);
        host.Run();
    }
}
//Console.WriteLine();


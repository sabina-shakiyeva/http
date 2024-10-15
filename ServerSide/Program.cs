
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

    private void HandleRequest(HttpListenerContext context)
    {
        var response = context.Response;
        var stream = response.OutputStream;
        if (context.Request.HttpMethod == "GET")
        {

        }
        else if(context.Request.HttpMethod == "DELETE")
        {

        }
     
    }
}


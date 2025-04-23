using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using System.Threading;
using Microsoft.AspNetCore.Http;
using System.IO;

class Program
{
    private static string counterFile = Path.Combine("/data", "viewcount.txt");

    private static int SwagCountThing()
    {
        int count = 0;
        if (File.Exists(counterFile))
        {
            count = int.Parse(File.ReadAllText(counterFile));
        }
        count++;
        File.WriteAllText(counterFile, count.ToString());
        return count;
    }

    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var app = builder.Build();

        app.MapGet("/", async context =>
        {
            int currentCount = SwagCountThing();
            await context.Response.WriteAsync($@"
                <!DOCTYPE html>
                <html>
                <head>
                    <title>wow c# website</title>
                </head>
                <body>
                    <h1>hai welcome to c# website</h1>
                    <p>page has been viewed: {currentCount} times!!!</p>
                </body>
                </html>
            ");
        });

        var port = Environment.GetEnvironmentVariable("PORT") ?? "5000";
        app.Run($"http://0.0.0.0:{port}");

    }
}

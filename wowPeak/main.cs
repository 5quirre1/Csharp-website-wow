using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Http;
using System.IO;

class Program
{
    private static string counterFile = "viewcount.txt";
    private static readonly object lockObj = new object();

    private static int SwagCountThing()
    {
        int count = 0;
        lock (lockObj)
        {
            if (File.Exists(counterFile))
            {
                if (int.TryParse(File.ReadAllText(counterFile), out count))
                {
                }
                else
                {
                    count = 0;
                }
            }

            count++;
            File.WriteAllText(counterFile, count.ToString());
        }
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

        app.Run("http://0.0.0.0:5000");
    }
}

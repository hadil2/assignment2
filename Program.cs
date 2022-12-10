using Lab4.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace Lab4
{
    public class Program
    {
        public static void Main(string[] args)
        {



            var app = Startup.InitializeApp(args);



            app.Run();
        }
    }
}
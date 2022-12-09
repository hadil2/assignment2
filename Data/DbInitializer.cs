using Lab4.Models;

namespace Lab4.Data
{
    public static class DbInitializer
    {
        public static void Initialize(MarketDbContext context)
        {
            context.Database.EnsureCreated();

            if (!context.Clients.Any())
            {
                context.Clients.AddRange(new List<Client>()
                {
                    new Client{FirstName="Carson",LastName="Alexander",BirthDate=DateTime.Parse("1995-01-09")},
                    new Client{FirstName="Meredith",LastName="Alonso",BirthDate=DateTime.Parse("1992-09-05")},
                    new Client{FirstName="Arturo",LastName="Anand",BirthDate=DateTime.Parse("1993-10-09")},
                    new Client{FirstName="Gytis",LastName="Barzdukas",BirthDate=DateTime.Parse("1992-01-09")}
                });
                context.SaveChanges();
            }


            if (!context.Brokerages.Any())
            {
                context.Brokerages.AddRange(new List<Brokerage>()
                {
                    new Brokerage{Id="A1",Title="Alpha",Fee=300},
                    new Brokerage{Id="B1",Title="Beta",Fee=130},
                    new Brokerage{Id="O1",Title="Omega",Fee=390}
                });
                context.SaveChanges();
            }


            if (!context.Subscriptions.Any())
            {
                context.Subscriptions.AddRange(new List<Subscription>()
                {
                    new Subscription{ClientId=1,BrokerageId="A1"},
                    new Subscription{ClientId=1,BrokerageId="B1"},
                    new Subscription{ClientId=1,BrokerageId="O1"},
                    new Subscription{ClientId=2,BrokerageId="A1"},
                    new Subscription{ClientId=2,BrokerageId="B1"},
                    new Subscription{ClientId=3,BrokerageId="A1"}
                });
                context.SaveChanges();
            }

            if(!context.Advertisements.Any())
            {
                context.Advertisements.AddRange(new List<Advertisement>()
                {
                    new Advertisement{BrokerageID="A1", fileName="sadas", Image="~/Uploads/3a882aa7-02b3-4745-b806-ebde7e0588d4-Forza 1.png"},
                    new Advertisement{BrokerageID="B1", fileName="kxcz", Image="~/Uploads/3a882aa7-02b3-4745-b806-ebde7e0588d4-Forza 1.png"},
                    new Advertisement{BrokerageID = "O1", fileName = "saxzc", Image = "~/Uploads/3a882aa7-02b3-4745-b806-ebde7e0588d4-Forza 1.png"},
                    new Advertisement{BrokerageID = "A1", fileName = "sagfg", Image ="~/Uploads/3a882aa7-02b3-4745-b806-ebde7e0588d4-Forza 1.png"},
                    new Advertisement{BrokerageID = "B1", fileName = "saasd", Image ="~/Uploads/3a882aa7-02b3-4745-b806-ebde7e0588d4-Forza 1.png"},
                    new Advertisement{BrokerageID = "A1", fileName = "szxc", Image = "~/Uploads/3a882aa7-02b3-4745-b806-ebde7e0588d4-Forza 1.png"}
                });
                context.SaveChanges();
            }


        }
    }

}

using CoachBuddy.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CoachBuddy.Infrastructure.Seeders
{
    public class CoachBuddySeeder
    {
        private readonly CoachBuddyDbContext _dbContext;

        public CoachBuddySeeder(CoachBuddyDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Seed()
        {
            if(await _dbContext.Database.CanConnectAsync())
            {
                await _dbContext.Database.MigrateAsync();

                if (!_dbContext.Clients.Any())
                {
                    var adamK = new Domain.Entities.Client.Client()
                    {
                        Name = "Adam",
                        LastName="Kowalski",
                        Email="adamkowalski@gmail.com",
                        Description = "Some description",
                        ContactDetails = new()
                        {
                            City="Warsaw",
                            Street="Warszawska 2",
                            PostalCode="02-797",
                            PhoneNumber="+48765879564"
                        }
                    };
                    adamK.EncodeName();
               
                    _dbContext.Clients.Add(adamK);
                    await _dbContext.SaveChangesAsync();
                }

                if (!_dbContext.Groups.Any())
                {
                    var newClients = new Domain.Entities.Group.Group()
                    {
                        Name = "New Clients",
                        Description = "Some group description",
                        CreatedAt = DateTime.UtcNow
                    };
                    newClients.EncodeName();

                    _dbContext.Groups.Add(newClients);
                    await _dbContext.SaveChangesAsync();
                }
            }
        }
    }
}

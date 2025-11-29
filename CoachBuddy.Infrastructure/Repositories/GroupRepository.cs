using CoachBuddy.Domain.Entities.Client;
using CoachBuddy.Domain.Entities.Group;
using CoachBuddy.Domain.Interfaces.Group;
using CoachBuddy.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CoachBuddy.Infrastructure.Repositories
{
    public class GroupRepository : IGroupRepository
    {
        private readonly CoachBuddyDbContext _dbContext;
        public GroupRepository(CoachBuddyDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public Task Commit()
            => _dbContext.SaveChangesAsync();

        public async Task Create(Group group)
        {
            _dbContext.Add(group);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Group group)
        {
            _dbContext.Groups.Remove(group);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Group>> GetAll()
            => await _dbContext.Groups.ToListAsync();

        public async Task<Group> GetByEncodedName(string encodedName)
            => await _dbContext.Groups.FirstAsync(c => c.EncodedName == encodedName);
        public async Task<Group?> GetByEncodedNameAsync(string encodedName)
           => await _dbContext.Groups
               .Include(g => g.ClientGroups)
               .ThenInclude(cg => cg.Client)
               .FirstOrDefaultAsync(g => g.EncodedName == encodedName);
        public async Task<Group> GetByIdAsync(int id)
            => await _dbContext.Groups.FirstAsync(c=>c.Id == id);

        public Task<Group?> GetByName(string name)
            => _dbContext.Groups.FirstOrDefaultAsync(cw => cw.Name.ToLower() == name.ToLower());

        public async Task<int> GetGroupCountAsync()
        {
            return await _dbContext.Groups.CountAsync();
        }

        public async Task<List<ClientGroup>> GetClientGroupsByGroupIdAsync(int groupId)
           => await _dbContext.ClientGroups
               .Include(cg => cg.Client)
               .Where(cg => cg.GroupId == groupId)
               .ToListAsync();

        public async Task<List<Client>> GetAvailableClientsForGroupAsync(int groupId)
        {
            var assignedClientIds = await _dbContext.ClientGroups
                .Where(cg => cg.GroupId == groupId)
                .Select(cg => cg.ClientId)
                .ToListAsync();

            return await _dbContext.Clients
                .Where(c => !assignedClientIds.Contains(c.Id))
                .ToListAsync();
        }
        public async Task<Group?> GetGroupWithClientsAsync(int groupId)
        {
            return await _dbContext.Groups
                .Include(g => g.ClientGroups)
                .ThenInclude(cg => cg.Client) 
                .FirstOrDefaultAsync(g => g.Id == groupId);
        }

        public async Task SaveAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        Task<IEnumerable<object>> IGroupRepository.GetAvailableClientsForGroupAsync(int groupId)
        {
            throw new NotImplementedException();
        }
    }
}

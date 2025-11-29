using CoachBuddy.Domain.Entities.Group;

namespace CoachBuddy.Domain.Interfaces.Group
{
    public interface IGroupRepository
    {
        Task Create(Entities.Group.Group group);
        Task<Entities.Group.Group?> GetByName(string name);
        Task <IEnumerable<Entities.Group.Group>> GetAll();
        Task Commit();
        Task<Entities.Group.Group> GetByIdAsync(int Id);
        Task DeleteAsync(Entities.Group.Group group);
        Task<Entities.Group.Group> GetByEncodedName(string encodedName);
        Task<List<ClientGroup>> GetClientGroupsByGroupIdAsync(int groupId);
        Task<Entities.Group.Group> GetByEncodedNameAsync(string encodedName);
        Task<int> GetGroupCountAsync();
        Task<Entities.Group.Group> GetGroupWithClientsAsync(int groupId);
        Task SaveAsync();
        Task<IEnumerable<object>> GetAvailableClientsForGroupAsync(int groupId);
    }
}

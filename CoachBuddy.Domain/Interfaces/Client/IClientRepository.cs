namespace CoachBuddy.Domain.Interfaces.Client
{
    public interface IClientRepository
    {
        Task Create(Entities.Client.Client client);
        Task<Entities.Client.Client?> GetByName(string name);
        Task<IEnumerable<Entities.Client.Client>> GetAll();
        Task<Entities.Client.Client> GetByEncodedName(string encodedName);
        Task Commit();
        Task<Entities.Client.Client> GetByIdAsync(int Id);
        Task DeleteAsync(Entities.Client.Client client);
        Task<int> GetClientCountAsync();
        Task<List<Entities.Client.Client>> GetAllClientsAsync();
        Task<List<Entities.Client.Client>> GetAvailableClientsForGroupAsync(int groupId);
    }
}

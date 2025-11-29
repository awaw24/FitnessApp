using CoachBuddy.Application.Common;
using MediatR;

namespace CoachBuddy.Application.Client.Queries.GetAllClients
{
    public class GetAllClientsQuery : IRequest<PaginatedResult<ClientDto>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}

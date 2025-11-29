using MediatR;

namespace CoachBuddy.Application.Client.Queries.GetClientByEncodedName
{
    public class GetClientByEncodedNameQuery : IRequest<ClientDto>
    {
        public string EncodedName { get; set; }
        public GetClientByEncodedNameQuery(string encodedName)
        {
            EncodedName = encodedName;
        }
    }
}

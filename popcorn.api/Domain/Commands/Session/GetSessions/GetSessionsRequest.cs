 using MediatR;

namespace Domain.Commands.Session.GetSessions
{
    public class GetSessionsRequest : IRequest<GetSessionsResponse>
    {
        public GetSessionsRequest(int pages)
        {
            Pages = pages;
        }

        public int Pages { get; set; }
    }
}
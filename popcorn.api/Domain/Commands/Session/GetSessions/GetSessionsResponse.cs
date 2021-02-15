using System.Collections.Generic;

namespace Domain.Commands.Session.GetSessions
{
    public class GetSessionsResponse
    {
        public IEnumerable<Entities.Session> Sessions { get; set; }
        public int Page { get; set; }
        public int TotalPages { get; set; }
    }
}

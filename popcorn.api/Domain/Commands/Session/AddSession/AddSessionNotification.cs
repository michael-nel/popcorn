using MediatR;

namespace Domain.Commands.Session.AddSession
{
    public class AddSessionNotification : INotification
    {
        public AddSessionNotification(Entities.Session session)
        {
            Session = session;
        }
        public Entities.Session Session { get; set; }
    }
}

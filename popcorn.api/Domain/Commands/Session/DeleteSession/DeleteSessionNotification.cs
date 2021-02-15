using MediatR;

namespace Domain.Commands.Session.DeleteSession
{
    public class DeleteSessionNotification : INotification
    {
        public DeleteSessionNotification(Entities.Session session)
        {
            Session = session;
        }

        public Entities.Session Session { get; set; }
    }
}

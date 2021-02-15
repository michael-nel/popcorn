using MediatR;

namespace Domain.Commands.User.AddUser
{
    class AddUserNotification : INotification
    {
        public AddUserNotification(Entities.User user)
        {
            User = user;
        }
        public Entities.User User { get; set; }
    }
}

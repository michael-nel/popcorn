using Domain.Entities.Base;

namespace Domain.Entities
{
    public class Room : EntityBase
    {
        protected Room() { }

        public Room(string name, int chairs)
        {
            Name = name;
            Chairs = chairs;
        }
        public string Name { get; private set; }
        public int Chairs { get; private set; }
     }
}

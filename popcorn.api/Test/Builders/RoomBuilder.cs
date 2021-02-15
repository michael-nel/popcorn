using Bogus;
using Domain.Entities;
using System;

namespace Test.Builders
{
    public class RoomBuilder
    {
        protected Guid Id;

        protected string Name;
        protected int Chairs;
        public Room Build()
        {
            var room = new Room(Name, Chairs);

            if (Id == null) return room;
            var propertyInfo = room.GetType().GetProperty("Id");
            propertyInfo.SetValue(room, Convert.ChangeType(Id, propertyInfo.PropertyType), null);
            return room;
        }
        public static RoomBuilder New()
        {
            var faker = new Faker();

            return new RoomBuilder
            {
                Name = faker.Lorem.Paragraph(3),
                Chairs = faker.Random.Int(5,50)
            };
        }
        public RoomBuilder WithId(Guid id)
        {
            Id = id;
            return this;
        }
        public RoomBuilder WithName(string name)
        {
            Name = name;
            return this;
        }
        public RoomBuilder WithChairs(int chairs)
        {
            Chairs = chairs;
            return this;
        }
    }
}

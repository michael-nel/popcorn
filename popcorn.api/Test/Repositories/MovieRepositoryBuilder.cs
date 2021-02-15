using Domain.Entities;
using Domain.Interfaces.Repositories;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Test.Builders;

namespace Test.Repositories
{
    public class MovieRepositoryBuilder
    {
        private static MovieRepositoryBuilder _instance;
        private Mock<IRepositoryMovie> _repository;
        public MovieRepositoryBuilder()
        {
            if (_repository == null)
                _repository = new Mock<IRepositoryMovie>();
        }

        public static MovieRepositoryBuilder Instance()
        {
            _instance = new MovieRepositoryBuilder();
            return _instance;
        }

        public IRepositoryMovie Build()
        {
            return _repository.Object;
        }


        public MovieRepositoryBuilder Exists()
        {
            _repository.Setup(r => r.Exists(It.IsAny<Func<Movie, bool>>())).Returns(true);
            return this;
        }

        public MovieRepositoryBuilder Find(Guid id)
        {
            _repository.Setup(r => r.GetById(It.IsAny<Guid>())).Returns(MovieBuilder.New().WithId(id).Build());
            return this;
        }
        public MovieRepositoryBuilder NotFound()
        {
            _repository.Setup(r => r.GetById(It.IsAny<Guid>(), It.IsAny<Expression<Func<Movie, object>>>())).Returns(() => null);
            return this;
        }


    }
}

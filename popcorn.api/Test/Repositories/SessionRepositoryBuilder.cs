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
    public class SessionRepositoryBuilder
    {
        private static SessionRepositoryBuilder _instance;
        private Mock<IRepositorySession> _repository;
        public SessionRepositoryBuilder()
        {
            if (_repository == null)
                _repository = new Mock<IRepositorySession>();
        }

        public static SessionRepositoryBuilder Instance()
        {
            _instance = new SessionRepositoryBuilder();
            return _instance;
        }

        public IRepositorySession Build()
        {
            return _repository.Object;
        }

        public SessionRepositoryBuilder Find(Guid id)
        {
            _repository.Setup(r => r.GetById(It.IsAny<Guid>())).Returns(SessionBuilder.New().WithId(id).Build());
            return this;
        }

        public SessionRepositoryBuilder NotFound()
        {
            _repository.Setup(r => r.GetById(It.IsAny<Guid>(), It.IsAny<Expression<Func<Session, object>>>())).Returns(() => null);
            return this;
        }


        public SessionRepositoryBuilder NotExists()
        {
            _repository.Setup(r => r.Exists(It.IsAny<Func<Session, bool>>())).Returns(false);
            return this;
        }

        public SessionRepositoryBuilder Exists()
        {
            _repository.Setup(r => r.Exists(It.IsAny<Func<Session, bool>>())).Returns(true);
            return this;
        }
    }
}

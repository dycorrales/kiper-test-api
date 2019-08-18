using Kiper.Condominio.Core.Helpers;
using System;
using FluentValidation.Results;

namespace Kiper.Condominio.Core.Entities
{
    public abstract class Entity
    {
        public Guid Id { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public Guid CreatedBy { get; private set; }
        public DateTime ModifiedAt { get; private set; }
        public Guid ModifiedBy { get; private set; }
        public Status Status { get; private set; }

        protected Entity() { }

        protected Entity(Guid id, Guid userId)
        {
            Id = id;
            CreatedAt = DateTime.Today;
            ModifiedAt = DateTime.Today;
            CreatedBy = userId;
            ModifiedBy = userId;
            Status = Status.Active;
        }

        protected Entity(Guid userId)
        {
            Id = Guid.NewGuid();
            CreatedAt = DateTime.Today;
            ModifiedAt = DateTime.Today;
            CreatedBy = userId;
            ModifiedBy = userId;
            Status = Status.Active;
        }

        public void Delete(Guid userId)
        {
            Status = Status.Deleted;
            Modify(userId);
        }

        public void UndoDelete()
        {
            Status = Status.Active;
        }

        public void Modify(Guid userId)
        {
            ModifiedAt = DateTime.Today;
            ModifiedBy = userId;
        }

        public abstract bool IsValid();
        public ValidationResult ValidationResult { get; set; }
    }
}
using System;
using MediatR;
using Kiper.Condominio.Core.Helpers;

namespace Kiper.Condominio.Core.Notifications
{
    public class DomainNotification : INotification
    {
        public Guid Id { get; }
        public string Key { get; }
        public string Value { get; }
        public int Version { get; }
        public NotificationType Type { get; }

        public DomainNotification(string key, string value, NotificationType type)
        {
            Id = Guid.NewGuid();
            Key = key;
            Value = value;
            Type = type;
            Version = 1;
        }
    }
}

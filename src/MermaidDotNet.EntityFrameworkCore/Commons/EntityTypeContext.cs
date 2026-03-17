using System;
using System.Collections.Generic;
using System.Linq;

#if NET8_0
using Microsoft.EntityFrameworkCore.Metadata;
#endif

namespace MermaidDotNet.EntityFrameworkCore.Contexts
{
    internal class EntityTypeContext
    {
        public Type ClrType { get; }
        public string Name { get; }
        public bool IsOwned { get; }
        public List<PropertyTypeContext> Properties { get; } = new List<PropertyTypeContext>();

#if NET8_0
        private readonly IEntityType _entityType;

        public EntityTypeContext(IEntityType entityType)
        {
            _entityType = entityType;
            ClrType = _entityType.ClrType;
            Name = _entityType.ClrType.Name;
            IsOwned = _entityType.IsOwned();
            Properties = _entityType.GetProperties()
                .Select(p => new PropertyTypeContext(p))
                .OrderByDescending(p => p.IsPrimaryKey).ThenBy(p => p.Name)
                .ToList();
        }
#endif

        public IEnumerable<NavigationContext> GetNavigations()
        {
#if NET8_0
            return _entityType.GetNavigations()
                .Select(n => new NavigationContext(n));
#endif
        }

        public bool Equals(EntityTypeContext other)
        {
            if (other == null) return false;
            return ClrType == other.ClrType && Name == other.Name;
        }
    }

    internal class NavigationContext
    {
#if NET8_0
        private readonly INavigation _navigation;

        public NavigationContext(INavigation navigation)
        {
            _navigation = navigation;
        }

        public string Name => _navigation.Name;
        public EntityTypeContext TargetEntityType => new EntityTypeContext(_navigation.TargetEntityType);
#endif
    }
}
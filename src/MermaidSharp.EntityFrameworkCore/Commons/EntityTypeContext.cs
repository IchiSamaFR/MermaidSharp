using System;
using System.Collections.Generic;
using System.Linq;

#if NET48
using System.Data.Entity.Core.Metadata.Edm;
#else
using Microsoft.EntityFrameworkCore.Metadata;
#endif

namespace MermaidSharp.EntityFrameworkCore.Contexts
{
    internal class EntityTypeContext
    {
        public Type ClrType { get; }
        public string Name { get; }
        public bool IsOwned { get; }
        public List<PropertyTypeContext> Properties { get; } = new List<PropertyTypeContext>();

#if NET48
        private readonly EntityType _entityType;
        private readonly MetadataWorkspace _workspace;

        public EntityTypeContext(EntityType entityType, MetadataWorkspace workspace)
        {
            _entityType = entityType;
            _workspace = workspace;
            ClrType = Ef6ClrTypeResolver.Resolve(entityType, workspace);
            Name = entityType.Name;
            IsOwned = false;
            Properties = _entityType.Properties
                .Where(p => p.TypeUsage.EdmType is PrimitiveType)
                .Select(p => new PropertyTypeContext(p, ClrType))
                .OrderByDescending(p => p.IsPrimaryKey).ThenBy(p => p.Name)
                .ToList();
        }
#else
        private readonly IEntityType _entityType;

        public EntityTypeContext(IEntityType entityType)
        {
            _entityType = entityType;
            ClrType = _entityType.ClrType;
            Name = _entityType.ClrType.Name.Split('`').FirstOrDefault() ?? _entityType.Name;
            IsOwned = _entityType.IsOwned();
            Properties = _entityType.GetProperties()
                .Select(p => new PropertyTypeContext(p))
                .OrderByDescending(p => p.IsPrimaryKey).ThenBy(p => p.Name)
                .ToList();
        }
#endif

        public IEnumerable<NavigationContext> GetNavigations()
        {
#if NET48
            return _entityType.NavigationProperties
                .Select(n => new NavigationContext(n, _workspace));
#else
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
#if NET48
        private readonly NavigationProperty _navigation;
        private readonly MetadataWorkspace _workspace;

        public NavigationContext(NavigationProperty navigation, MetadataWorkspace workspace)
        {
            _navigation = navigation;
            _workspace = workspace;
        }

        public string Name => _navigation.Name;

        public EntityTypeContext TargetEntityType
        {
            get
            {
                var targetType = _navigation.ToEndMember.GetEntityType();
                return new EntityTypeContext(targetType, _workspace);
            }
        }
#else
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

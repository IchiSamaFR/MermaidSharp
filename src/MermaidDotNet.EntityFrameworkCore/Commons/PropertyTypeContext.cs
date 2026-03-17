using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using MermaidDotNet.EntityFrameworkCore.Enums;

#if NET8_0
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using MermaidDotNet.EntityFrameworkCore.Extensions;
#endif

namespace MermaidDotNet.EntityFrameworkCore.Contexts
{
    internal class PropertyTypeContext
    {
        public Type ClrType { get; }
        public string Name { get; }
        public bool IsNullable { get; }
        public bool IsPrimaryKey { get; }
        public bool IsForeignKey { get; }
        public bool IsRequired { get; }
        public bool IsUnique { get; }
        public string Description { get; }

        public Type ForeignParentType { get; set; }
        public string ForeignKeyName { get; set; }
        public Enums.DeleteBehavior DeleteBehavior { get; set; }

#if NET8_0
        public PropertyTypeContext(IProperty property)
        {
            Name = property.Name;
            ClrType = Nullable.GetUnderlyingType(property.ClrType) ?? property.ClrType;
            IsNullable = property.IsNullable;
            IsPrimaryKey = property.IsPrimaryKey();
            IsForeignKey = property.IsForeignKey();
            IsUnique = property.IsUniqueIndex();
            Description = property.GetDescription() ?? string.Empty;

            var foreignKey = property.GetContainingForeignKeys().FirstOrDefault();
            if (foreignKey != null)
            {
                IsRequired = foreignKey.IsRequired;
                ForeignParentType = foreignKey.PrincipalEntityType.ClrType;
                ForeignKeyName = foreignKey.Properties.FirstOrDefault()?.Name ?? string.Empty;
                DeleteBehavior = (Enums.DeleteBehavior)foreignKey.DeleteBehavior;
            }
        }
#endif
    }

}
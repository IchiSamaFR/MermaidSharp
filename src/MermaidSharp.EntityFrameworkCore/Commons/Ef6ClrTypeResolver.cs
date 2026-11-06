#if NET48
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Reflection;

namespace MermaidSharp.EntityFrameworkCore.Contexts
{
    /// <summary>
    /// Resolves the CLR type mapped to an Entity Framework 6 conceptual entity type.
    /// </summary>
    internal static class Ef6ClrTypeResolver
    {
        /// <summary>
        /// Returns the CLR type of the entity, using the object-space mapping when available
        /// and falling back to a name lookup in the loaded assemblies.
        /// </summary>
        public static Type Resolve(EntityType entityType, MetadataWorkspace workspace)
        {
            if (workspace != null
                && workspace.TryGetItemCollection(DataSpace.OSpace, out var itemCollection)
                && itemCollection is ObjectItemCollection objectItems
                && workspace.TryGetObjectSpaceType(entityType, out var objectSpaceType))
            {
                var clrType = objectItems.GetClrType(objectSpaceType);
                if (clrType != null)
                    return clrType;
            }

            var loadedTypes = AppDomain.CurrentDomain.GetAssemblies()
                .Where(a => !a.IsDynamic)
                .SelectMany(GetLoadableTypes)
                .ToList();

            return loadedTypes.FirstOrDefault(t => t.FullName == entityType.FullName)
                ?? loadedTypes.FirstOrDefault(t => t.Name == entityType.Name)
                ?? typeof(object);
        }

        private static IEnumerable<Type> GetLoadableTypes(Assembly assembly)
        {
            try
            {
                return assembly.GetTypes();
            }
            catch (ReflectionTypeLoadException ex)
            {
                return ex.Types.Where(t => t != null);
            }
        }
    }
}
#endif

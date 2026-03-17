using MermaidDotNet.Attributes;
using System;
using System.ComponentModel;

namespace MermaidDotNet.Enums
{
    [Flags]
    public enum ColumnKeyType
    {
        None = 0,

        [MermaidEnum("PK")]
        PrimaryKey = 1,

        [MermaidEnum("FK")]
        ForeignKey = 2,

        [MermaidEnum("UK")]
        UniqueKey = 4,

        [MermaidEnum("PK, FK")]
        PrimaryKeyForeignKey = PrimaryKey | ForeignKey,

        [MermaidEnum("PK, UK")]
        PrimaryKeyUniqueKey = PrimaryKey | UniqueKey,

        [MermaidEnum("FK, UK")]
        ForeignKeyUniqueKey = ForeignKey | UniqueKey,

        [MermaidEnum("PK, FK, UK")]
        PrimaryKeyForeignKeyUniqueKey = PrimaryKey | ForeignKey | UniqueKey,
    }
}

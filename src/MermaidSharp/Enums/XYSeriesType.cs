using MermaidSharp.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MermaidSharp.Enums
{
    /// <summary>
    /// Specifies the available series types for XY charts.
    /// </summary>
    /// <remarks>Use this enumeration to select the visual representation of data in an XY chart, such as a
    /// line or bar series.</remarks>
    public enum XYSeriesType
    {
#pragma warning disable CS1591
        [MermaidEnum("line")]
        Line,

        [MermaidEnum("bar")]
        Bar
#pragma warning restore CS1591
    }
}

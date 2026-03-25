using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MermaidSharp.Models
{
    /// <summary>
    /// Represents a Git merge action for merging a specified branch into the current branch within a Mermaid diagram context.
    /// </summary>
    /// <remarks>Use this class to represent a merge operation in a Git graph diagram. The resulting graph can be used to
    /// generate Mermaid diagrams for documentation or visualization purposes. The class enforces branch existence and naming
    /// constraints to ensure valid Git graph construction.</remarks>
    public class GitMerge : AGitAction
    {
        /// <summary>
        /// Gets the mermaid name associated with the current instance.
        /// </summary>
        protected override string Name => "merge";

        /// <summary>
        /// Gets the name of the branch to be merged into the current branch.
        /// </summary>
        public string Branch { get; }

        /// <summary>
        /// Gets the tag associated with the merge action.
        /// </summary>
        public string Tag { get; }

        /// <summary>
        /// Initializes a new instance with the specified branch and optional tag.
        /// </summary>
        /// <param name="branch">The name of the branch to merge.</param>
        /// <param name="tag">An optional tag associated with the merge. If not specified, no tag is used.</param>
        public GitMerge(string branch, string tag = "")
        {
            Branch = branch;
            Tag = tag;
        }

        /// <summary>
        /// Returns the mermaid representation of the current instance.
        /// </summary>
        public override string ToString()
        {
            var returned = $"{Name} {Branch}";
            if (!string.IsNullOrWhiteSpace(Tag))
                returned += $" tag: \"{Tag}\"";
            return returned;
        }
    }
}

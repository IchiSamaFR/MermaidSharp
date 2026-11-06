using MermaidSharp.Configs;
using MermaidSharp.Diagrams;
using MermaidSharp.Enums;

namespace MermaidSharp.Samples.Examples
{
    /// <summary>
    /// Provides git graph samples.
    /// </summary>
    public static class GitGraphExamples
    {
        public static IEnumerable<DiagramExample> Create()
        {
            yield return FeatureWorkflow();
            yield return HotfixWithCherryPick();
            yield return CustomConfiguration();
        }

        private static DiagramExample FeatureWorkflow()
        {
            var graph = new GitGraph("Feature workflow")
                .Commit("init", "v1.0.0")
                .Branch("develop")
                .Commit("dev-setup")
                .Branch("feature-login")
                .Commit("login-form")
                .Commit("login-api", commitType: GitCommitType.Highlight)
                .Checkout("develop")
                .Merge("feature-login", "merge-login")
                .Checkout("main")
                .Merge("develop", "release-1.1", "v1.1.0");

            return new DiagramExample(DiagramKind.GitGraph, "git-feature-workflow", "Feature workflow",
                "Branches, merges with ids and tags, highlighted commit.", graph.CalculateDiagram());
        }

        private static DiagramExample HotfixWithCherryPick()
        {
            var graph = new GitGraph("Hotfix")
                .Commit("c1")
                .Branch("develop")
                .Commit("c2")
                .Commit("c3")
                .Checkout("main")
                .Merge("develop", "m1", "v1.0.0")
                .Branch("hotfix")
                .Commit("fix-1", commitType: GitCommitType.Reverse)
                .Checkout("develop")
                .CherryPick("fix-1")
                .Checkout("main")
                .Merge("hotfix", "m2", "v1.0.1");

            return new DiagramExample(DiagramKind.GitGraph, "git-hotfix", "Hotfix and cherry-pick",
                "A reverse commit on a hotfix branch, cherry-picked to develop and merged to main.",
                graph.CalculateDiagram());
        }

        private static DiagramExample CustomConfiguration()
        {
            var config = new GitGraphConfig(ConfigTheme.Forest,
                showCommitLabel: true,
                showBranches: true,
                rotateCommitLabel: false,
                mainBranchName: "trunk");
            var graph = new GitGraph("Trunk based", GitDirection.TopBottom, config)
                .Commit("a")
                .Branch("feature-x")
                .Commit("b")
                .Checkout("trunk")
                .Commit("c")
                .Merge("feature-x", "d");

            return new DiagramExample(DiagramKind.GitGraph, "git-configuration", "Direction and configuration",
                "Top-to-bottom direction, custom main branch name and Forest theme.", graph.CalculateDiagram());
        }
    }
}

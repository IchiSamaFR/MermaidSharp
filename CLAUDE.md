## Project instructions

The project instructions are shared by Claude and GitHub Copilot (`.github/copilot-instructions.md` points to the same file). Always apply them:

@instructions.md

## graphify

This project has a knowledge graph at graphify-out/ with god nodes, community structure, and cross-file relationships.

Rules:
- When graphify-out/graph.json exists, run `graphify query "<question>"` before your first grep, search or source-file read for a codebase question, and put this rule in every subagent prompt that explores code. Read raw files only for lines the graph or an earlier read already located. Use `graphify path "<A>" "<B>"` for relationships and `graphify explain "<concept>"` for focused concepts. These return a scoped subgraph, usually much smaller than GRAPH_REPORT.md or raw grep output.
- If graphify-out/wiki/index.md exists, use it for broad navigation instead of raw source browsing.
- Read graphify-out/GRAPH_REPORT.md only for broad architecture review or when query/path/explain do not surface enough context.
- After modifying code, run `graphify update .` to keep the graph current (AST-only, no API cost).
- graphify-out/ is generated locally and ignored by git (see .gitignore). Do not commit it; rebuild it with `graphify update .` when it is missing or stale.
- graphify is optional: if the `graphify` command is not installed, skip these rules and explore the code with the usual tools.

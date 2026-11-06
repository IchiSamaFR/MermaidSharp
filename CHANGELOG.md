# Changelog

All notable changes to this project are documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.1.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [Unreleased]

### Added
- .NET 10 target framework for all libraries (EF Core 10 dependency on `net10.0`).
- `GitGraph.CurrentBranch` property.
- MIT `LICENSE` file, `CHANGELOG.md`, `.editorconfig` and `.gitattributes`.
- `build` and `Release` GitHub Actions workflows (NuGet trusted publishing on `v*` tags).
- Regression tests for the fixes below.

### Changed
- `GitGraph.Branch()` now switches to the created branch, as Mermaid does.
- Node, link and subgraph ids throw `ArgumentNullException` when null; setters strip spaces like constructors.
- Labels with special characters are quoted and escaped; YAML values and titles are escaped.
- Configuration collections are written as a single YAML sequence; empty strings are skipped.
- Samples target .NET 10; template pages (Counter, Weather, Privacy) removed.
- NuGet package versions are managed centrally in `src/Directory.Packages.props`.
- Configuration and theme variable serialization share a single implementation.

### Fixed
- GitGraph: duplicate commit ids and branches are rejected, self-merges and invalid cherry-picks are rejected, merge cherry-pick parent resolution.
- Flowchart: `linkStyle` index now counts every link in declaration order.
- Entity Framework 6: entity CLR types are resolved from the object-space mapping.
- AutoDiagram: assemblies with unloadable types no longer throw.
- AutoDiagram package now includes its README; EF6 tests are discovered by `dotnet test`.

## [1.4.0] - 2026-05-08

### Added
- Quadrant charts (config, theme variables, diagram generation).
- XY charts.

## [1.3.0] - 2026-05-03

### Added
- Config and theme variables for entity relationship diagrams, git graphs and flowcharts.
- `ConfigVariableAttribute` for configuration serialization.
- Pie chart slice colors.

## [1.2.0] - 2026-04-09

### Added
- Pie charts (config, theme variables, diagram generation).
- Title support for git graphs.
- Shared package configuration through `Directory.Build.props`.

## [1.1.0] - 2026-03-30

### Added
- Git graphs, with commit types and directions.
- Entity Framework diagram generation for generic entity classes.

## [1.0.0] - 2026-03-20

- First release of MermaidSharp, forked from [MermaidDotNet](https://github.com/samsmithnz/MermaidDotNet).

[Unreleased]: https://github.com/IchiSamaFR/MermaidSharp/compare/v1.4.0...HEAD
[1.4.0]: https://github.com/IchiSamaFR/MermaidSharp/compare/v1.3.0...v1.4.0
[1.3.0]: https://github.com/IchiSamaFR/MermaidSharp/compare/v1.2.0...v1.3.0
[1.2.0]: https://github.com/IchiSamaFR/MermaidSharp/compare/v1.1.0...v1.2.0
[1.1.0]: https://github.com/IchiSamaFR/MermaidSharp/compare/v1.0.0...v1.1.0
[1.0.0]: https://github.com/IchiSamaFR/MermaidSharp/releases/tag/v1.0.0

# .NET 10 migration validation (issue #9)

## Scope
Branch `migration/dotnet10-20261116` is an isolated **candidate**, not a validated production release. Base: `5be0b1390ea69d138ea51aa67eb15fb1ea54517b`. Four C# projects target `net10.0`. The legacy SSDT `.sqlproj` is unchanged.

## Changes
- Set the four C# TargetFramework values to net10.0.
- Align explicit ASP.NET Core/Blazor/Identity and Microsoft.Extensions package references to the **initial 10.0.0 compatibility baseline**. Before merging, select and verify the current supported 10.0 servicing patch; 10.0.0 is not a final security-patch recommendation.
- Select the 10.0 SDK via global.json, allowing newer installed feature bands.
- Build each C# project independently in CI; publish the server and background service. The SQL project is intentionally excluded from Linux CI.

## Baseline and verification limits
- Last known master server-build CI success: GitHub Actions run 41073923850, 2026-10-30.
- No complete .NET 8 integration baseline or tests are available in the repository.
- The execution environment does not have the dotnet SDK, a SQL Server test instance, browser or SMTP sandbox. Therefore no local restore, build, publish, SQL, browser or mail test was performed.
- CI results must be inspected before opening a non-draft PR or merging. Do not infer runtime compatibility from a green compile.

## Candidate CI results (2026-11-16)
- Workflow run [43891445919](https://github.com/AlthausDev/10-EcoLogTracking/actions/runs/43891445919): all four project jobs restored and built successfully; the server and console service published successfully.
- The first CI attempts revealed a missing `IsNullOrEmpty` extension method for collections under the new dependency graph. Replaced its two server uses with `list?.Any() != true` and the console use with `listaLogs is { Count: > 0 }`, preserving empty/null handling.
- CI green proves compilation/publication only. No browser, SQL, JWT, NLog or SMTP regression tests have run.
- The package versions 10.0.0 are a baseline, **not** the final supported servicing versions. Update to a verified current patch before approval.

## Compatibility and risks
- Blazor InteractiveAuto and WebAssembly static assets, routing, JS interop, Blazor.Bootstrap 2.2.0, Blazored.LocalStorage 4.5.0, ClosedXML 0.102.2: test browser startup, prerender, WASM activation, Excel download and navigation.
- JWT bearer authentication: validate valid, invalid and expired tokens, roles, 401/403 and logout.
- SQL Server, Dapper, Microsoft.Data.SqlClient: test read, write, filter and retention on disposable data; confirm TLS and target SQL Server version.
- NLog, NLog.Database, NLog.MailKit: test each configured target and error logging.
- MailKit/MimeKit: use sandbox SMTP and check generated attachment.
- EcoConsoleBackService: existing StartAsync bypasses base.StartAsync and ExecuteAsync throws NotImplementedException. Timer callback uses async void and fire-and-forget requests. Verify lifecycle and daily schedule; do not assume this is a .NET 10 regression.
- Existing 8.x CodeGeneration.Design and third-party packages remain pinned pending compatibility evidence; review warnings and package graph before deciding updates/removals.
- Review committed configuration for active secrets and rotate if applicable; do not copy credentials into reports.

## Acceptance gates
1. All four CI project jobs restore and compile in Release.
2. Server and service publish successfully.
3. Run against isolated SQL Server and SMTP sandbox, with representative appsettings supplied securely.
4. Pass login/logout/JWT/role checks; Blazor InteractiveAuto/JS/static assets; log CRUD/filter/export; NLog targets; scheduled background email and retention.
5. Audit transitive NuGet vulnerabilities, select current .NET 10 patch versions, document known pre-existing defects, and prepare rollback to previous published artifact.
6. Do not merge into master until functional evidence and deployment prerequisites are available.

## Rollback
Do not change database schema as part of this candidate. Keep previous .NET 8 artifact/configuration for rollback (not a supported long-term runtime after 2026-11-10).

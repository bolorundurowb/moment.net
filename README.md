<div align="center">
  <img
    height="175"
    width="175"
    src="https://raw.githubusercontent.com/bolorundurowb/moment.net/refs/heads/master/assets/logo.png"
    alt="moment.net logo"  />
  <h1 align="center">moment.net</h1>
</div>

<p align="center">
  <a href="https://github.com/bolorundurowb/moment.net/actions/workflows/build-and-test.yml">
    <img src="https://github.com/bolorundurowb/moment.net/actions/workflows/build-and-test.yml/badge.svg" alt=".NET Build, Test and Coverage">
  </a>  
  <a href="https://codecov.io/gh/bolorundurowb/moment.net">
    <img src="https://codecov.io/gh/bolorundurowb/moment.net/graph/badge.svg?token=WIGQVIAR70" alt="codecov">
  </a>  
  <a href="LICENSE">
    <img src="https://img.shields.io/badge/License-MIT-yellow.svg" alt="License: MIT">
  </a>  
  <a href="https://www.nuget.org/packages/moment.net">
    <img src="https://img.shields.io/nuget/v/moment.net" alt="NuGet Version">
  </a>  
  <a href="https://www.nuget.org/packages/moment.net">
    <img src="https://img.shields.io/nuget/dt/moment.net" alt="NuGet Downloads">
  </a>
</p>

**moment.net** is a multi-target library (`netstandard2.0`, `net6.0`, `net8.0`) that brings moment.js-style fluent date and time operations to C#. Extension methods on `DateTime`, `DateTimeOffset`, and — on .NET 6 and later — `DateOnly` and `TimeOnly` cover relative time, calendar labels, date manipulation, comparisons, business days, and ranges. Relative time and the default calendar labels are localised for English, Spanish, French, Portuguese, German, and Russian.

## Installation

### .NET CLI

```bash
dotnet add package moment.net
```

### Package Manager

```powershell
Install-Package moment.net
```

### PackageReference

```xml
<PackageReference Include="moment.net" Version="2.1.0" />
```

## Quick start

```csharp
using System.Globalization;
using MomentNet;
using MomentNet.Display;
using MomentNet.Manipulate;
using MomentNet.Plugins.BusinessDays;
using MomentNet.Query;

var past = new DateTime(2020, 1, 1);
var future = new DateTime(2026, 1, 1);

past.From(future);                                       // "6 years ago"
past.From(future, withoutSuffix: true);                 // "6 years"
past.From(future, new CultureInfo("es"));               // "6 años atrás"
past.To(future);                                         // "in 6 years"

var date = new DateTime(2024, 3, 15);                   // Friday

date.StartOf(DateTimeAnchor.Month);                     // 2024-03-01 00:00:00
date.EndOf(DateTimeAnchor.Day);                         // 2024-03-15 23:59:59.999
date.Next(DayOfWeek.Monday);                            // Monday 18 March 2024
date.IsBetween(new DateTime(2024, 3, 1), new DateTime(2024, 3, 31));
date.AddBusinessDays(1);                                // Monday 18 March 2024

var range = Moment.Range(new DateTime(2024, 1, 1), new DateTime(2024, 1, 31));
range.Contains(new DateTime(2024, 1, 15));              // true
```

## Namespace groups

Import only the groups you call:

| Namespace                        | Includes                                                                    |
|----------------------------------|-----------------------------------------------------------------------------|
| `MomentNet`                      | `Moment.Min`, `Moment.Max`, `Moment.Range` (includes `DateOnly` on .NET 6+) |
| `MomentNet.Display`              | Relative time, calendar time, formatting, Unix timestamps, date differences |
| `MomentNet.Display.Models`       | Display configuration models such as `CalendarTimeFormats`                  |
| `MomentNet.GetSet`               | Quarter, week, ISO week, first and last date in the week                    |
| `MomentNet.Manipulate`           | `StartOf`, `EndOf`, `Next`, `Last`, `Final`, `DateTimeAnchor`               |
| `MomentNet.Query`                | Comparison helpers, leap year checks, daylight saving checks                |
| `MomentNet.Plugins.BusinessDays` | Business day and holiday-aware business day helpers                         |
| `MomentNet.Plugins.Range`        | Date range types and range operations (`MomentDateOnlyRange` on .NET 6+)    |

## Documentation

The full documentation is in the [docs](https://github.com/bolorundurowb/moment.net/tree/master/docs) directory:

- [Guide](docs/guide.md) — receivers, cultures, format strings, and the rules that are easy to get wrong
- [API reference](docs/api.md) — methods, receivers, and target frameworks
- [Migrating from v1.x to v2.x](docs/migration-v1-to-v2.md)
- [Contributing](docs/contributing.md)

Release history is in [CHANGELOG.md](CHANGELOG.md).

Agents writing code that uses this library should follow [.agents/skills/moment-net/SKILL.md](.agents/skills/moment-net/SKILL.md).

## Licence

This project is licensed under the MIT Licence — see the [LICENSE](LICENSE) file for details.

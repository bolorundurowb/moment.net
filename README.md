# moment.net

[![.NET Build, Test and Coverage](https://github.com/bolorundurowb/moment.net/actions/workflows/build-and-test.yml/badge.svg)](https://github.com/bolorundurowb/moment.net/actions/workflows/build-and-test.yml) [![codecov](https://codecov.io/gh/bolorundurowb/moment.net/graph/badge.svg?token=WIGQVIAR70)](https://codecov.io/gh/bolorundurowb/moment.net) [![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](LICENSE) [![NuGet Version](https://img.shields.io/nuget/v/moment.net)](https://www.nuget.org/packages/moment.net)

**moment.net** is a .NET library that provides a set of extension methods for `DateTime` and `DateTimeOffset` to simplify common date and time operations. It is inspired by the popular moment.js library, offering a more fluent and readable API for handling relative time, date anchors, business days, and more.

## Installation

You can install Moment.net via NuGet using any of the following methods:

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
<PackageReference Include="moment.net" Version="1.4.0" />
```

## Features

### Relative Time
Calculate human-readable relative time strings.

```csharp
var past = new DateTime(2020, 1, 1);
var future = new DateTime(2026, 1, 1);
past.From(future);   // "6 years ago"
future.From(past);   // "6 years from now"

past.FromNow();      // relative to DateTime.UtcNow

var startDateTime = new DateTime(2012, 12, 12);
var sameDay = new DateTime(2012, 12, 12, 12, 0, 0);
startDateTime.CalendarTime(sameDay); // "Today at 12:00 PM"
```

### Date Anchors and Manipulation
Fluent methods to find specific points in time.

```csharp
var date = DateTime.Parse("5/1/2008 8:30:52 AM");

date.StartOf(DateTimeAnchor.Day);      // 01/05/2008 00:00:00
date.EndOf(DateTimeAnchor.Day);        // 01/05/2008 23:59:59
date.FirstDateInWeek();                // Start of the week
date.LastDateInWeek();                 // End of the week
date.Next(DayOfWeek.Thursday);         // Next occurrence of Thursday
date.Last(DayOfWeek.Friday);           // Previous occurrence of Friday
date.Final().Monday().InMonth();       // Last Monday of the month
date.Final().Monday().InYear();        // Last Monday of the year
```

### Business Days
Support for business day calculations, useful for financial and scheduling applications.

```csharp
var dateTime = DateTime.Parse("2023-10-20"); // Friday
dateTime.IsBusinessDay();      // true
dateTime.AddBusinessDays(2);   // 2023-10-24 (skips the weekend)
```

### Validation and Comparison
Readable methods for checking dates and calculating differences.

```csharp
date.IsLeapYear();
date.IsWeekend();
date.IsWeekday();
date.IsBetween(start, end);

date.DiffInDays(otherDate);
date.DiffInMonths(otherDate);
date.DiffInYears(otherDate);
```

### Formatting and Unix Time
```csharp
date.Format("yyyy MMM dd");             // "1971 Jan 01"
date.UnixTimestampInMilliseconds();     // 31536000000
```

## Localisation

Moment.net supports multiple languages for relative time and calendar time strings. Pass a `CultureInfo` to any method that produces human-readable output:

```csharp
var past = new DateTime(2020, 1, 1);
var future = new DateTime(2026, 1, 1);

past.From(future, new CultureInfo("es"));   // "6 años atrás"
past.From(future, new CultureInfo("de"));   // "6 Jahre her"
past.From(future, new CultureInfo("fr"));   // "6 ans il y a"
past.From(future, new CultureInfo("ru"));   // "6 лет назад"
```

Currently supported languages:

| Code | Language          |
|------|-------------------|
| `en` | English (default) |
| `es` | Spanish           |
| `fr` | French            |
| `pt` | Portuguese        |
| `de` | German            |
| `ru` | Russian           |

## Contributing

Contributions are welcome! If you'd like to help improve Moment.net:

1. **Add Languages:** Create a new `Strings.[language-code].resx` file in the `src/moment.net` project, following the pattern of existing localization files. No `.csproj` changes are needed — .NET picks up satellite resource files by convention.
2. **Report Bugs:** Open an issue on GitHub to report bugs or request features.
3. **Pull Requests:** Submit pull requests for bug fixes or new features. Please ensure your changes include relevant tests.

See [CHANGELOG.md](CHANGELOG.md) for a full history of changes.

## License

This project is licensed under the MIT License; see the [LICENSE](LICENSE) file for details.

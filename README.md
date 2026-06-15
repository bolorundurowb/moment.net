# moment.net

[![.NET Build, Test and Coverage](https://github.com/bolorundurowb/moment.net/actions/workflows/build-and-test.yml/badge.svg)](https://github.com/bolorundurowb/moment.net/actions/workflows/build-and-test.yml)
[![codecov](https://codecov.io/gh/bolorundurowb/moment.net/graph/badge.svg?token=WIGQVIAR70)](https://codecov.io/gh/bolorundurowb/moment.net)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](LICENSE)
[![NuGet Version](https://img.shields.io/nuget/v/moment.net)](https://www.nuget.org/packages/moment.net)
[![NuGet Downloads](https://img.shields.io/nuget/dt/moment.net)](https://www.nuget.org/packages/moment.net)

**moment.net** is a .NET Standard 2.0 library that brings moment.js-style fluent date/time operations to C#. It provides extension methods on both `DateTime` and `DateTimeOffset` for relative time, calendar formatting, date manipulation, business day calculations, and more — all with full localization support.

## Table of Contents

- [Installation](#installation)
- [Quick Start](#quick-start)
- [API Reference](#api-reference)
  - [Relative Time](#relative-time)
  - [Calendar Time](#calendar-time)
  - [Date Anchors](#date-anchors)
  - [Date Positioning](#date-positioning)
  - [Date Comparison](#date-comparison)
  - [Date Differences](#date-differences)
  - [Business Days](#business-days)
  - [Utilities and Ranges](#utilities-and-ranges)
  - [Formatting](#formatting)
  - [Unix Time](#unix-time)
- [DateTimeOffset Support](#datetimeoffset-support)
- [Localization](#localization)
- [Configuration](#configuration)
- [Contributing](#contributing)
- [License](#license)

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
<PackageReference Include="moment.net" Version="2.0.0" />
```

## Quick Start

```csharp
using MomentNet;

var date = new DateTime(2024, 3, 15);

// Relative time
date.FromNow();                    // "a year ago"

// Calendar time
date.CalendarTime();               // "03/15/2024"

// Date manipulation
date.StartOf(DateTimeAnchor.Month); // 03/01/2024 00:00:00
date.EndOf(DateTimeAnchor.Month);   // 03/31/2024 23:59:59

// Date positioning
date.Next(DayOfWeek.Monday);       // next Monday
date.Last(DayOfWeek.Friday);       // previous Friday
date.Final().Monday().InMonth();   // last Monday of the month

// Comparison
date.IsWeekend();                  // false
date.IsLeapYear();                 // true

// All APIs also work with DateTimeOffset — the UTC offset is preserved
var dto = new DateTimeOffset(2024, 3, 15, 9, 0, 0, TimeSpan.FromHours(5));
dto.FromNow();                         // "a year ago"
dto.StartOf(DateTimeAnchor.Month);     // 2024-03-01T00:00:00+05:00
dto.Next(DayOfWeek.Monday);            // next Monday, same +05:00 offset
dto.IsWeekend();                       // false
dto.UnixTimestampInSeconds();          // seconds since epoch, normalized to UTC
```

## API Reference

### Relative Time

Human-readable time differences between dates.

| Method        | Description                        | Example                               |
|---------------|------------------------------------|---------------------------------------|
| `FromNow()`   | Time from date to now              | `date.FromNow()` → `"5 minutes ago"`  |
| `From(other)` | Time from date to another date     | `past.From(future)` → `"6 years ago"` |
| `ToNow()`     | Time from now to date (future)     | `future.ToNow()` → `"in 6 years"`     |
| `To(other)`   | Time from date to another (future) | `start.To(end)` → `"in 3 days"`       |

```csharp
var past = new DateTime(2020, 1, 1);
var future = new DateTime(2026, 1, 1);

past.From(future);    // "6 years ago"
past.From(future, true); // "6 years"
past.FromNow();       // relative to DateTime.UtcNow or DateTime.Now
past.FromNow(true);   // "6 years" (no "ago" suffix)
future.ToNow();       // "in X years/months/days"
future.ToNow(true);   // "X years/months/days"
```

### Calendar Time

Display dates relative to a reference point with contextual labels.

| Method                             | Description                               |
|------------------------------------|-------------------------------------------|
| `CalendarTime()`                   | Calendar time relative to now             |
| `CalendarTime(reference)`          | Calendar time relative to a specific date |
| `CalendarTime(reference, formats)` | With custom format strings                |

```csharp
var startDateTime = new DateTime(2012, 12, 12);
var sameDay = new DateTime(2012, 12, 12, 12, 0, 0);

startDateTime.CalendarTime(sameDay);  // "Today at 12:00 PM"

// Custom formats
var formats = new CalendarTimeFormats(
    sameDay: "'Today' 'at' HH:mm",
    nextDay: "'Tomorrow' 'at' HH:mm",
    nextWeek: "dddd 'at' HH:mm",
    lastDay: "'Yesterday' 'at' HH:mm",
    lastWeek: "'Last' dddd 'at' HH:mm",
    everythingElse: "yyyy-MM-dd"
);
date.CalendarTime(formats: formats);
```

### Date Anchors

Get the start or end of a time period.

| Method            | Description     | Example                                                    |
|-------------------|-----------------|------------------------------------------------------------|
| `StartOf(anchor)` | Start of period | `date.StartOf(DateTimeAnchor.Day)` → `01/05/2008 00:00:00` |
| `EndOf(anchor)`   | End of period   | `date.EndOf(DateTimeAnchor.Day)` → `01/05/2008 23:59:59`   |

Supported anchors: `Minute`, `Hour`, `Day`, `Week`, `IsoWeek`, `Month`, `Quarter`, `Year`

```csharp
var date = new DateTime(2008, 5, 1, 8, 30, 52);

date.StartOf(DateTimeAnchor.Year);   // 01/01/2008 00:00:00
date.StartOf(DateTimeAnchor.Quarter); // first day of quarter
date.StartOf(DateTimeAnchor.Month);  // 01/05/2008 00:00:00
date.StartOf(DateTimeAnchor.Week);   // varies by culture
date.StartOf(DateTimeAnchor.IsoWeek); // Monday of ISO week
date.StartOf(DateTimeAnchor.Day);    // 01/05/2008 00:00:00
date.StartOf(DateTimeAnchor.Hour);   // 01/05/2008 08:00:00
date.StartOf(DateTimeAnchor.Minute); // 01/05/2008 08:30:00

date.EndOf(DateTimeAnchor.Year);     // 12/31/2008 23:59:59
date.EndOf(DateTimeAnchor.Quarter);  // last day of quarter
date.EndOf(DateTimeAnchor.Month);    // 05/31/2008 23:59:59

date.Quarter();       // 2
date.Week();          // culture-specific week of year
date.IsoWeek();       // ISO-8601 week number
date.IsoWeekYear();   // ISO-8601 week-numbering year
```

### Date Positioning

Find specific days relative to a date.

| Method                   | Description                    | Example                           |
|--------------------------|--------------------------------|-----------------------------------|
| `Next(dayOfWeek)`        | Next occurrence of day         | `date.Next(DayOfWeek.Monday)`     |
| `Next(dayOfWeek, count)` | Nth next occurrence            | `date.Next(DayOfWeek.Monday, 2)`  |
| `Last(dayOfWeek)`        | Previous occurrence of day     | `date.Last(DayOfWeek.Friday)`     |
| `Last(dayOfWeek, count)` | Nth previous occurrence        | `date.Last(DayOfWeek.Friday, 2)`  |
| `Final()`                | Builder for last day in period | `date.Final().Monday().InMonth()` |
| `FirstDateInWeek()`      | First day of current week      | varies by culture                 |
| `LastDateInWeek()`       | Last day of current week       | varies by culture                 |

```csharp
var date = new DateTime(2024, 3, 15); // Friday

date.Next(DayOfWeek.Monday);           // Monday, March 18
date.Next(DayOfWeek.Monday, 2);        // Monday, March 25 (2 Mondays ahead)
date.Last(DayOfWeek.Friday);           // Friday, March 8 (previous Friday)
date.Last(DayOfWeek.Friday, 2);        // Friday, March 1 (2 Fridays back)

// Final day of period
date.Final().Monday().InMonth();       // Last Monday of March 2024
date.Final().Monday().InYear();        // Last Monday of 2024
date.Final().Friday().InMonth();       // Last Friday of March 2024
```

### Date Comparison

Boolean checks for date properties and relationships.

| Method                  | Description                     | Example                                          |
|-------------------------|---------------------------------|--------------------------------------------------|
| `IsLeapYear()`          | Check if year is a leap year    | `new DateTime(2024, 1, 1).IsLeapYear()` → `true` |
| `IsWeekend()`           | Check if Saturday or Sunday     | `date.IsWeekend()`                               |
| `IsWeekday()`           | Check if Monday to Friday       | `date.IsWeekday()`                               |
| `IsBusinessDay()`       | Alias for IsWeekday             | `date.IsBusinessDay()`                           |
| `IsDaylightSavingTime()` | Check daylight saving time      | `date.IsDaylightSavingTime()`                    |
| `IsSame(other)`         | Exact equality (UTC normalized) | `date.IsSame(other)`                             |
| `IsBefore(other)`       | Comes before another date       | `date.IsBefore(other)`                           |
| `IsSameOrBefore(other)` | Same or before                  | `date.IsSameOrBefore(other)`                     |
| `IsAfter(other)`        | Comes after another date        | `date.IsAfter(other)`                            |
| `IsSameOrAfter(other)`  | Same or after                   | `date.IsSameOrAfter(other)`                      |
| `IsBetween(start, end)` | Within a date range (inclusive) | `date.IsBetween(start, end)`                     |

```csharp
var date = new DateTime(2024, 3, 15);
var start = new DateTime(2024, 3, 1);
var end = new DateTime(2024, 3, 31);

date.IsLeapYear();             // true
date.IsWeekend();              // false (Friday)
date.IsWeekday();              // true
date.IsBetween(start, end);    // true
```

### Date Differences

Calculate the difference between two dates.

| Method                | Description          | Returns  |
|-----------------------|----------------------|----------|
| `DiffInDays(other)`   | Difference in days   | `double` |
| `DiffInMonths(other)` | Difference in months | `double` |
| `DiffInQuarters(other)` | Difference in quarters | `double` |
| `DiffInYears(other)`  | Difference in years  | `double` |

```csharp
var start = new DateTime(2020, 1, 1);
var end = new DateTime(2024, 6, 15);

end.DiffInDays(start);    // ~1627.0
end.DiffInMonths(start);  // ~53.5
end.DiffInQuarters(start); // ~17.8
end.DiffInYears(start);   // ~4.46
```

### Business Days

Utilities for business day calculations.

| Method                         | Description                                      | Example                              |
|--------------------------------|--------------------------------------------------|--------------------------------------|
| `IsBusinessDay()`              | Check if Mon-Fri                                 | `date.IsBusinessDay()`               |
| `IsBusinessDay(holidays)`      | Check if Mon-Fri and not a supplied holiday      | `date.IsBusinessDay(holidays)`       |
| `AddBusinessDays(n)`           | Add n business days (skips weekends)             | `date.AddBusinessDays(5)`            |
| `AddBusinessDays(n, holidays)` | Add n business days, skipping weekends/holidays  | `date.AddBusinessDays(5, holidays)`  |

```csharp
var friday = DateTime.Parse("2023-10-20"); // Friday
var holidays = new[] { DateTime.Parse("2023-10-23") };

friday.IsBusinessDay();       // true
friday.AddBusinessDays(2);    // 2023-10-24 (Tuesday, skips weekend)
friday.AddBusinessDays(-3);   // 2023-10-17 (Tuesday, negative works too)
friday.AddBusinessDays(1, holidays); // 2023-10-24 (skips Monday holiday)
```

### Utilities and Ranges

```csharp
Moment.Min(date1, date2, date3); // earliest date
Moment.Max(date1, date2, date3); // latest date

var range = Moment.Range(new DateTime(2024, 1, 1), new DateTime(2024, 1, 31));
range.Contains(new DateTime(2024, 1, 15)); // true
range.Overlaps(Moment.Range(new DateTime(2024, 1, 20), new DateTime(2024, 2, 5))); // true
range.Intersect(Moment.Range(new DateTime(2024, 1, 20), new DateTime(2024, 2, 5))); // Jan 20-31
```

### Formatting

| Method                     | Description                | Example                                              |
|----------------------------|----------------------------|------------------------------------------------------|
| `Format()`                 | ISO-8601 format (default)  | `date.Format()` → `"1971-01-01T00:00:00+00:00"`      |
| `Format(pattern)`          | Custom format string       | `date.Format("yyyy MMM dd")` → `"1971 Jan 01"`       |
| `Format(pattern, culture)` | Custom format with culture | `date.Format("dd MMMM yyyy", new CultureInfo("de"))` |

### Unix Time

| Method                          | Description                               | Example                              |
|---------------------------------|-------------------------------------------|--------------------------------------|
| `UnixTimestampInSeconds()`      | Seconds since Unix epoch (1970-01-01 UTC) | `date.UnixTimestampInSeconds()`      |
| `UnixTimestampInMilliseconds()` | Milliseconds since Unix epoch             | `date.UnixTimestampInMilliseconds()` |

```csharp
var date = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
date.UnixTimestampInSeconds();      // 0
date.UnixTimestampInMilliseconds(); // 0
```

## DateTimeOffset Support

Every API in moment.net has a `DateTimeOffset` overload. The behaviour is identical to the `DateTime` counterpart with two additions:

- **UTC offset is preserved.** Methods that return a new date value (e.g. `StartOf`, `EndOf`, `Next`, `Last`, `Final`, `FirstDateInWeek`, `LastDateInWeek`, `AddBusinessDays`) keep the original `TimeSpan` offset in the returned `DateTimeOffset`.
- **Comparisons are offset-aware.** `IsSame`, `IsBefore`, `IsAfter`, `IsBetween`, `DiffInDays`, and the relative-time methods (`FromNow`, `From`, `ToNow`, `To`) compare the underlying UTC instants, so values with different offsets are handled correctly.

### Examples

```csharp
var dto = new DateTimeOffset(2024, 3, 15, 9, 0, 0, TimeSpan.FromHours(5));

// Relative time — always compares against DateTimeOffset.UtcNow
dto.FromNow();                           // "a year ago"
dto.ToNow();                             // "in X ..."

// Two different representations of the same instant compare equal
var a = new DateTimeOffset(2024, 3, 15, 9, 0, 0, TimeSpan.FromHours(5));
var b = new DateTimeOffset(2024, 3, 15, 4, 0, 0, TimeSpan.Zero);
a.IsSame(b);    // true  (same UTC instant)
a.From(b);      // "few seconds ago"

// Start/end of period — offset preserved
dto.StartOf(DateTimeAnchor.Day);    // 2024-03-15T00:00:00+05:00
dto.StartOf(DateTimeAnchor.Quarter); // 2024-01-01T00:00:00+05:00
dto.StartOf(DateTimeAnchor.IsoWeek); // Monday of ISO week, +05:00
dto.EndOf(DateTimeAnchor.Month);    // 2024-03-31T23:59:59+05:00

// Next / Last / Final
dto.Next(DayOfWeek.Monday);          // next Monday, +05:00
dto.Last(DayOfWeek.Friday);          // previous Friday, +05:00
dto.Final().Monday().InMonth();      // last Monday of March 2024, +05:00

// Week boundaries
dto.FirstDateInWeek();               // first day of week (culture-dependent), +05:00
dto.LastDateInWeek();                // last day of week (culture-dependent), +05:00
dto.IsoWeek();                       // ISO-8601 week number
dto.IsoWeekYear();                   // ISO-8601 week-numbering year

// Business days
dto.IsBusinessDay();                 // true (Friday)
dto.AddBusinessDays(3);              // 3 business days later, +05:00

// Comparison
dto.IsLeapYear();                    // true
dto.IsBefore(dto.AddDays(1));        // true
dto.IsBetween(dto.AddDays(-1), dto.AddDays(1)); // true

// Differences
var other = new DateTimeOffset(2023, 3, 15, 9, 0, 0, TimeSpan.FromHours(5));
dto.DiffInDays(other);               // ~366.0
dto.DiffInMonths(other);             // ~12.0
dto.DiffInQuarters(other);           // ~4.0
dto.DiffInYears(other);              // ~1.0

// Unix timestamps — always UTC
dto.UnixTimestampInSeconds();        // seconds since 1970-01-01T00:00:00Z
dto.UnixTimestampInMilliseconds();   // milliseconds since epoch

// Formatting
dto.Format();                        // "2024-03-15T09:00:00+05:00"
dto.Format("dd MMMM yyyy");          // "15 March 2024"

// Calendar time
dto.CalendarTime();                  // relative to DateTimeOffset.Now
```

## Localization

Moment.net supports multiple languages for relative time and calendar output. Pass a `CultureInfo` to any method that produces human-readable text:

```csharp
var past = new DateTime(2020, 1, 1);
var future = new DateTime(2026, 1, 1);

past.From(future, new CultureInfo("en"));  // "6 years ago"
past.From(future, new CultureInfo("es"));  // "6 años atrás"
past.From(future, new CultureInfo("fr"));  // "6 ans il y a"
past.From(future, new CultureInfo("pt"));  // "6 anos atrás"
past.From(future, new CultureInfo("de"));  // "6 Jahre her"
past.From(future, new CultureInfo("ru"));  // "6 лет назад"
```

### Supported Languages

| Code | Language          |
|------|-------------------|
| `en` | English (default) |
| `es` | Spanish           |
| `fr` | French            |
| `pt` | Portuguese        |
| `de` | German            |
| `ru` | Russian           |

### Adding a New Language

1. Create a new `Strings.[language-code].resx` file in `src/moment.net/`
2. Copy all string keys from `Strings.resx` and translate the values
3. No `.csproj` changes needed — .NET picks up satellite resource files by convention

## Configuration

### Default Culture

By default, moment.net uses the current thread's culture. You can change this behavior:

```csharp
// Use a specific default culture instead of thread culture
CultureWrapper.DefaultCulture = new CultureInfo("en-US");
CultureWrapper.UseCurrentThreadCultureAsDefault = false;
```

## Contributing

Contributions are welcome! Here's how you can help:

1. **Add Languages:** Create a new `Strings.[language-code].resx` file in `src/moment.net/`
2. **Report Bugs:** Open an issue on GitHub
3. **Pull Requests:** Submit PRs for bug fixes or features with tests

### Running Tests

```bash
dotnet test
```

See [CHANGELOG.md](CHANGELOG.md) for the full release history.

## License

This project is licensed under the MIT License — see the [LICENSE](LICENSE) file for details.

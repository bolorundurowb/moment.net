---
name: moment-net
description: >-
  Use the moment.net NuGet package for moment.js-style date and time operations
  in C#. Use when adding relative time, calendar labels, start or end of a
  period, business days, date ranges, Unix timestamps, or localised date text,
  or when the user mentions moment.net, MomentNet, or moment.js-style dates in C#.
license: MIT
---

# moment.net

Call the extension methods on the date value you already have. The package id is `moment.net`. It targets `netstandard2.0`, `net6.0`, and `net8.0`.

```bash
dotnet add package moment.net
```

For the full method list, see [references/api.md](references/api.md).

## Import only the namespaces you call

| Namespace | Use it for |
| --- | --- |
| `MomentNet` | `Moment.Min`, `Moment.Max`, `Moment.Range` |
| `MomentNet.Display` | Relative time, calendar time, `Format`, Unix timestamps, date differences |
| `MomentNet.Display.Models` | `CalendarTimeFormats` |
| `MomentNet.GetSet` | Quarter, week, ISO week, first and last date in the week |
| `MomentNet.Manipulate` | `StartOf`, `EndOf`, `Next`, `Last`, `Final`, `DateTimeAnchor` |
| `MomentNet.Query` | Comparisons, leap year, daylight saving |
| `MomentNet.Plugins.BusinessDays` | Business days, weekends, weekdays |
| `MomentNet.Plugins.Range` | `MomentRange`, `MomentRangeOffset`, `MomentDateOnlyRange` |

Version 1 used `using moment.net` and `CultureWrapper`. Those types are gone. Pass a `CultureInfo` on the call that produces text.

## Choose the receiver

- Use `DateTime` on every target framework.
- Use `DateTimeOffset` when `StartOf`, `EndOf`, `Next`, `Last`, `Final`, and `AddBusinessDays` must keep the original offset. Comparisons and `DiffInDays` use the UTC instant. `DiffInMonths`, `DiffInQuarters`, and `DiffInYears` use each value's year, month, and day in that offset.
- Use `DateOnly` and `TimeOnly` only when the project targets .NET 6 or later. `ReadOnlySpan` overloads of `Moment.Min` and `Moment.Max` exist only on .NET 8 or later.

`DateOnly` relative time compares calendar dates. The conversion used inside those methods is midnight.

## Common calls

```csharp
using System.Globalization;
using MomentNet;
using MomentNet.Display;
using MomentNet.Display.Models;
using MomentNet.Manipulate;
using MomentNet.Plugins.BusinessDays;
using MomentNet.Query;

var date = new DateTime(2024, 3, 15);

date.FromNow();                                          // relative to now
date.From(new DateTime(2026, 1, 1), new CultureInfo("es"));
date.To(new DateTime(2024, 3, 18));                      // "in 3 days"
date.FromNow(withoutSuffix: true);                      // no "ago"

date.CalendarTime();
date.CalendarTime(new DateTime(2024, 3, 15, 12, 0, 0));
date.Format("dd MMMM yyyy", new CultureInfo("de"));

date.StartOf(DateTimeAnchor.Month);
date.EndOf(DateTimeAnchor.Day);
date.Next(DayOfWeek.Monday);
date.Next(DayOfWeek.Monday, 2);
date.Last(DayOfWeek.Friday);
date.Final().Monday().InMonth();

date.IsBetween(new DateTime(2024, 3, 1), new DateTime(2024, 3, 31));
date.DiffInMonths(new DateTime(2024, 1, 1));
date.AddBusinessDays(5);
date.UnixTimestampInSeconds();

var range = Moment.Range(new DateTime(2024, 1, 1), new DateTime(2024, 1, 31));
range.Contains(new DateTime(2024, 1, 15));
```

`CalendarTimeFormats` takes six .NET format strings: same day, next day, next week, last day, last week, and everything else. The parameterless constructor builds the localised defaults for the supplied culture, or for `CultureInfo.CurrentCulture` when culture is omitted.

## Rules

- `From` describes the receiver against the other value. `past.From(future)` reads as time already elapsed, such as "6 years ago". `To` describes the other value as ahead of the receiver. `start.To(end)` reads as time still to come, such as "in 3 days". `withoutSuffix` is the `bool` before `CultureInfo`. It drops words such as "ago" and "in".
- For `DateTime`, `FromNow` and `ToNow` use `DateTime.UtcNow` when `Kind` is `Utc`, and `DateTime.Now` otherwise. `DateTimeOffset` relative time uses `DateTimeOffset.UtcNow`. `CalendarTime()` with no reference uses `DateTime.Now` or `DateTimeOffset.Now`.
- `DateTime` comparison methods (`IsSame`, `IsBefore`, `IsAfter`, and the inclusive variants) convert both values with `ToUniversalTime` before comparing. `DiffInDays` subtracts the stored values. `DiffInMonths` uses the calendar components of the stored values, and the fractional day is divided by the number of days in the source month.
- Format strings are .NET custom patterns (`yyyy`, `MMM`, `dd`). Moment.js tokens such as `YYYY` are not valid here. Defaults are `yyyy-MM-ddTHH:mm:sszzz` for `DateTime` and `DateTimeOffset`, `yyyy-MM-dd` for `DateOnly`, and `HH:mm:ss` for `TimeOnly`. On .NET 6 and later, `Format` and `CalendarTime` are marked as requiring unreferenced code because formatting uses reflection.
- `CalendarTime` formats the source value. The reference value only chooses which format string applies. Same calendar day, the next day, the previous day, the following two to seven days, and the previous two to seven days each have their own string. Every other gap uses the remaining string.
- Supported cultures for relative time and the default calendar labels are `en`, `es`, `fr`, `pt`, `de`, and `ru`. Pass the culture on each call. The library leaves `Thread.CurrentThread.CurrentCulture` unchanged. An omitted culture uses `CultureInfo.CurrentCulture`.
- `StartOf` and `EndOf` take `DateTimeAnchor`: `Minute`, `Hour`, `Day`, `Week`, `IsoWeek`, `Month`, `Quarter`, `Year`. Week boundaries follow the supplied `CultureInfo`, or `CultureInfo.CurrentCulture` when omitted. ISO weeks start on Monday. `DateOnly` accepts `Day`, `Week`, `IsoWeek`, `Month`, `Quarter`, and `Year`. `TimeOnly` accepts `Minute` and `Hour`. Any other anchor throws `ArgumentException`.
- `EndOf` for `DateTime` and `DateTimeOffset` sets the clock to 59 seconds and 999 milliseconds, for example `23:59:59.999`, and keeps `DateTime.Kind` or the offset. `TimeOnly` `EndOf` also uses 59 seconds and 999 milliseconds. `DateOnly` `EndOf` returns the last calendar day of the period.
- `Next` and `Last` move to a different day. If the receiver is already that weekday, `Next` and `Last` continue to the following or previous match. `count` must be greater than zero or the method throws `ArgumentOutOfRangeException`. `Final().Monday().InMonth()` and `InYear()` return the last matching weekday in that period. The same weekday methods exist for Tuesday through Sunday.
- `IsBetween` includes both ends. `IsBusinessDay()` with no holiday list means Monday to Friday. Holiday overloads ignore the time of day. Negative `AddBusinessDays` walks backwards and also skips weekends and any supplied holidays.
- `Moment.Range` throws `ArgumentException` when the start is after the end. `Contains` and `Overlaps` are inclusive unless you pass `inclusive: false`. `Intersect` returns null when the ranges do not overlap. `Moment.Min` and `Moment.Max` throw when the sequence is null or empty.
- Unix timestamps are seconds or milliseconds since `1970-01-01T00:00:00Z`. `DateTime` values whose `Kind` is not `Utc` are converted with `ToUniversalTime` first. `DateTimeOffset` uses `UtcDateTime`.

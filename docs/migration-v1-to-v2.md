# Migrating from v1.x to v2.x

Version 2.0 introduces `DateTimeOffset` support across the entire API, restructures namespaces, and fixes several correctness issues. The changes below are what you need to update when upgrading.

Current behaviour for each receiver is described in the [guide](guide.md). The method list is in the [API reference](api.md).

## Namespace changes

In v1.x, all extension methods lived in a single namespace (`MomentNet`). v2.0 organises them into focused groups. Replace your old `using` statements:

| v1.x                | v2.x                                                                                               |
|---------------------|----------------------------------------------------------------------------------------------------|
| `using moment.net;` | `using MomentNet;` (static helpers: `Moment.Min`, `Moment.Max`, `Moment.Range`)                    |
| *(same as above)*   | `using MomentNet.Display;` (relative time, calendar time, formatting, Unix timestamps, date diffs) |
| *(same as above)*   | `using MomentNet.Display.Models;` (`CalendarTimeFormats`)                                          |
| *(same as above)*   | `using MomentNet.GetSet;` (quarter, week, ISO week, first/last date in week)                       |
| *(same as above)*   | `using MomentNet.Manipulate;` (`StartOf`, `EndOf`, `Next`, `Last`, `Final`, `DateTimeAnchor`)      |
| *(same as above)*   | `using MomentNet.Query;` (comparisons, leap year, daylight saving)                                 |
| *(same as above)*   | `using MomentNet.Plugins.BusinessDays;` (business day utilities)                                   |
| *(same as above)*   | `using MomentNet.Plugins.Range;` (date range types)                                                |

You only need to import the groups you use.

## CultureWrapper removed

`CultureWrapper`, `LocalizationManager`, and the static `CultureWrapper.DefaultCulture` / `UseCurrentThreadCultureAsDefault` properties have been removed. Display methods that produce localised text now accept an optional `CultureInfo` parameter directly:

```csharp
// v1.x
using var wrapper = new CultureWrapper(new CultureInfo("es"));
var result = date.FromNow();

// v2.0
var result = date.FromNow(new CultureInfo("es"));
```

The library no longer mutates `Thread.CurrentThread.CurrentCulture` at any point.

## CalendarTime now describes the source date

In v1.x, `someDate.CalendarTime(referenceDate)` incorrectly formatted the *reference* date. In v2.0, it correctly formats `someDate` (the source) relative to `referenceDate`. The format labels describe the source date:

```csharp
var yesterday = DateTime.Now.AddDays(-1);

// v1.x → "Tomorrow at 10:00 AM" (incorrectly formatted the reference)
// v2.0 → "Yesterday at 10:00 AM" (correctly describes the source)
yesterday.CalendarTime();
```

## DiffInMonths fractional calculation

The fractional day component now divides by the day count of the **source** month (`dateTime`) rather than the comparison month. This gives more accurate results when the two months have different lengths:

```csharp
var feb29 = new DateTime(2024, 2, 29);
var feb28 = new DateTime(2023, 2, 28);

// v1.x → 12.0357… (divided by 28)
// v2.0 → 12.0345… (divided by 29 — February 2024 has 29 days)
feb29.DiffInMonths(feb28);
```

## FirstDateInWeek / StartOf(Week) corrected

The private `FirstDateInWeek` helper used `Math.Abs` which produced incorrect week boundaries when the current day was numerically less than the culture's first day of week. This affected `StartOf(DateTimeAnchor.Week)` and `EndOf(DateTimeAnchor.Week)`. The fix uses modular arithmetic that correctly handles all day-of-week and culture combinations.

## AddBusinessDays performance

The non-holiday `AddBusinessDays(int)` overload is now O(1) instead of O(n). Large values like `AddBusinessDays(1000)` no longer loop 1000+ times — they batch by full weeks.

## New: DateTimeOffset support

Every method that previously only accepted `DateTime` now has a `DateTimeOffset` overload. The UTC offset is preserved on all returned values. Comparisons and `DiffInDays` use the UTC instant. `DiffInMonths`, `DiffInQuarters`, and `DiffInYears` use each value's year, month, and day in that offset. No migration action is needed for existing `DateTime` code — existing `DateTime` APIs remain in place.

## New in v2.1: DateOnly and TimeOnly

On .NET 6 and later, most APIs also accept `DateOnly`, and `TimeOnly` for time-period and comparison helpers. `ReadOnlySpan` overloads for `Moment.Min` and `Moment.Max` are available on .NET 8 and later. See the [guide](guide.md) for which receiver to use, and the [API reference](api.md) for the method list.

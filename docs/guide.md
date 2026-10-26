# Guide

This page describes the behaviour that is easy to get wrong. The method list, receivers, and target frameworks are in the [API reference](api.md).

## Choosing a receiver

Use `DateTime` on every target framework.

Use `DateTimeOffset` when `StartOf`, `EndOf`, `Next`, `Last`, `Final`, and `AddBusinessDays` must keep the original offset. Comparisons and `DiffInDays` use the UTC instant. `DiffInMonths`, `DiffInQuarters`, and `DiffInYears` use each value's year, month, and day in that offset.

Use `DateOnly` and `TimeOnly` only when the project targets .NET 6 or later. `ReadOnlySpan` overloads of `Moment.Min` and `Moment.Max` exist only on .NET 8 or later.

`DateOnly` relative time compares calendar dates. The conversion used inside those methods is midnight.

## Relative time

`From` describes the receiver against the other value. `past.From(future)` reads as time already elapsed, such as "6 years ago". `To` describes the other value as ahead of the receiver. `start.To(end)` reads as time still to come, such as "in 6 years". `withoutSuffix` is the `bool` before `CultureInfo`. It drops words such as "ago" and "in".

```csharp
using MomentNet.Display;

var past = new DateTime(2020, 1, 1);
var future = new DateTime(2026, 1, 1);

past.From(future);                       // "6 years ago"
past.From(future, withoutSuffix: true); // "6 years"
past.To(future);                         // "in 6 years"
```

For `DateTime`, `FromNow` and `ToNow` use `DateTime.UtcNow` when `Kind` is `Utc`, and `DateTime.Now` otherwise. `DateTimeOffset` relative time uses `DateTimeOffset.UtcNow`.

## Calendar time

`CalendarTime` formats the source value. The reference value only chooses which format string applies. `CalendarTime()` with no reference uses `DateTime.Now` or `DateTimeOffset.Now`.

Same calendar day, the next day, the previous day, the following two to seven days, and the previous two to seven days each have their own string. Every other gap uses the remaining string.

`CalendarTimeFormats` takes six .NET format strings: same day, next day, next week, last day, last week, and everything else. The parameterless constructor builds the localised defaults for the supplied culture, or for `CultureInfo.CurrentCulture` when culture is omitted.

```csharp
using MomentNet.Display;
using MomentNet.Display.Models;

var formats = new CalendarTimeFormats(
    sameDay: "'Today' 'at' HH:mm",
    nextDay: "'Tomorrow' 'at' HH:mm",
    nextWeek: "dddd 'at' HH:mm",
    lastDay: "'Yesterday' 'at' HH:mm",
    lastWeek: "'Last' dddd 'at' HH:mm",
    everythingElse: "yyyy-MM-dd"
);

var source = new DateTime(2012, 12, 12); // midnight
source.CalendarTime(new DateTime(2012, 12, 12, 12, 0, 0), formats);
// Same calendar day, so the same-day pattern is applied to source: "Today at 00:00"
```

## Formatting

Format strings are .NET custom patterns (`yyyy`, `MMM`, `dd`). Moment.js tokens such as `YYYY` are not valid here.

Defaults are `yyyy-MM-ddTHH:mm:sszzz` for `DateTime` and `DateTimeOffset`, `yyyy-MM-dd` for `DateOnly`, and `HH:mm:ss` for `TimeOnly`.

On .NET 6 and later, `Format` and `CalendarTime` are marked as requiring unreferenced code because formatting uses reflection.

`Format` uses the supplied `CultureInfo` for .NET formatting. That works for cultures beyond the six languages shipped for relative time and the default calendar labels.

## Start and end of a period

`StartOf` and `EndOf` take `DateTimeAnchor`: `Minute`, `Hour`, `Day`, `Week`, `IsoWeek`, `Month`, `Quarter`, `Year`.

Week boundaries follow the supplied `CultureInfo`, or `CultureInfo.CurrentCulture` when omitted. ISO weeks start on Monday.

`DateOnly` accepts `Day`, `Week`, `IsoWeek`, `Month`, `Quarter`, and `Year`. `TimeOnly` accepts `Minute` and `Hour`. Any other anchor throws `ArgumentException`.

`EndOf` for `DateTime` and `DateTimeOffset` sets the clock to 59 seconds and 999 milliseconds, for example `23:59:59.999`, and keeps `DateTime.Kind` or the offset. `TimeOnly` `EndOf` also uses 59 seconds and 999 milliseconds. `DateOnly` `EndOf` returns the last calendar day of the period.

```csharp
using MomentNet.Manipulate;

var date = new DateTime(2024, 3, 15, 8, 30, 52);

date.StartOf(DateTimeAnchor.Day);    // 2024-03-15 00:00:00
date.EndOf(DateTimeAnchor.Day);      // 2024-03-15 23:59:59.999
date.StartOf(DateTimeAnchor.IsoWeek); // Monday of that ISO week
```

## Next, last, and final

`Next` and `Last` move to a different day. If the receiver is already that weekday, `Next` and `Last` continue to the following or previous match. `count` must be greater than zero or the method throws `ArgumentOutOfRangeException`.

`Final().Monday().InMonth()` and `InYear()` return the last matching weekday in that period. The same weekday methods exist for Tuesday through Sunday. `DateTimeOffset` keeps its offset. `DateOnly` returns a `DateOnly`.

```csharp
using MomentNet.Manipulate;

var friday = new DateTime(2024, 3, 15);

friday.Next(DayOfWeek.Monday);     // Monday 18 March 2024
friday.Next(DayOfWeek.Friday);     // Friday 22 March 2024
friday.Last(DayOfWeek.Friday);     // Friday 8 March 2024
friday.Final().Monday().InMonth(); // last Monday of March 2024
```

## Comparisons and differences

`IsBetween` includes both ends.

`DateTime` comparison methods (`IsSame`, `IsBefore`, `IsAfter`, and the inclusive variants) convert both values with `ToUniversalTime` before comparing. `DiffInDays` subtracts the stored values. `DiffInMonths` uses the calendar components of the stored values, and the fractional day is divided by the number of days in the source month. `DiffInQuarters` is that month difference divided by 3. `DiffInYears` is that month difference divided by 12.

`DateTimeOffset` comparisons and `DiffInDays` use the UTC instant, so two values that name the same moment with different offsets compare as equal. `DiffInMonths`, `DiffInQuarters`, and `DiffInYears` use each value's year, month, and day in that offset.

`IsDaylightSavingTime()` uses `TimeZoneInfo.Local`. The overload that takes a `TimeZoneInfo` throws if that zone is null.

## Business days

`IsBusinessDay()` with no holiday list means Monday to Friday. `IsWeekend()` is Saturday or Sunday. `IsWeekday()` is Monday to Friday.

Holiday overloads ignore the time of day. When a holiday list is passed, a Monday-to-Friday date is not a business day if its calendar day is in that list.

Negative `AddBusinessDays` walks backwards and also skips weekends and any supplied holidays. Zero returns the same value.

```csharp
using MomentNet.Plugins.BusinessDays;

var friday = new DateTime(2023, 10, 20);
var holidays = new[] { new DateTime(2023, 10, 23) };

friday.AddBusinessDays(1);            // Monday 23 October 2023
friday.AddBusinessDays(1, holidays);  // Tuesday 24 October 2023
friday.AddBusinessDays(-3);           // Tuesday 17 October 2023
```

## Ranges, minimum, and maximum

`Moment.Range` throws `ArgumentException` when the start is after the end. `Contains` and `Overlaps` are inclusive unless you pass `inclusive: false`. `Intersect` returns null when the ranges do not overlap.

`Moment.Min` and `Moment.Max` throw when the sequence is null or empty. On .NET 8 and later, the `ReadOnlySpan` overloads throw when the span is empty.

## Unix timestamps

Unix timestamps are seconds or milliseconds since `1970-01-01T00:00:00Z`. `DateTime` values whose `Kind` is not `Utc` are converted with `ToUniversalTime` first. `DateTimeOffset` uses `UtcDateTime`. `DateOnly` is available on .NET 6 and later.

## Cultures

Supported cultures for relative time and the default calendar labels are `en`, `es`, `fr`, `pt`, `de`, and `ru`. Pass the culture on each call. An omitted culture uses `CultureInfo.CurrentCulture`. The library leaves `Thread.CurrentThread.CurrentCulture` unchanged.

```csharp
using System.Globalization;
using MomentNet.Display;

var past = new DateTime(2020, 1, 1);
var future = new DateTime(2026, 1, 1);

past.From(future, new CultureInfo("es"));             // "6 años atrás"
past.From(future, true, new CultureInfo("de"));       // "6 Jahre"
past.To(future, true, new CultureInfo("de"));         // "6 Jahre"
```

| Code | Language          |
|------|-------------------|
| `en` | English (default) |
| `es` | Spanish           |
| `fr` | French            |
| `pt` | Portuguese        |
| `de` | German            |
| `ru` | Russian           |

To add a language, see [Contributing](contributing.md).

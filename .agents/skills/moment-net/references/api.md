# moment.net public API

Import the namespace in the first column. Receivers marked **6+** exist only on `net6.0` and `net8.0`. `ReadOnlySpan` overloads exist only on `net8.0`.

## MomentNet

| Method | Receivers | Notes |
| --- | --- | --- |
| `Min(params T[])`, `Min(IEnumerable<T>)` | `DateTime`, `DateTimeOffset`, `DateOnly` **6+** | Throws on null or empty input. Returns the earliest value. |
| `Max(params T[])`, `Max(IEnumerable<T>)` | `DateTime`, `DateTimeOffset`, `DateOnly` **6+** | Throws on null or empty input. Returns the latest value. |
| `Min(params ReadOnlySpan<T>)`, `Max(params ReadOnlySpan<T>)` | `DateTime`, `DateTimeOffset`, `DateOnly` | .NET 8+ only. Throws on an empty span. |
| `Range(start, end)` | `DateTime` → `MomentRange`, `DateTimeOffset` → `MomentRangeOffset`, `DateOnly` **6+** → `MomentDateOnlyRange` | Throws when `start` is after `end`. |

## MomentNet.Plugins.Range

`MomentRange`, `MomentRangeOffset`, and `MomentDateOnlyRange` each expose:

| Member | Notes |
| --- | --- |
| `Start`, `End` | Boundaries stored on the range. |
| `Duration` | `End - Start` as a `TimeSpan`. |
| `Contains(value, inclusive = true)` | A single value inside this range. |
| `Contains(other, inclusive = true)` | The other range is fully inside this range. Throws if `other` is null. |
| `Overlaps(other, inclusive = true)` | Throws if `other` is null. |
| `Intersect(other)` | Overlapping range, or null when there is no overlap. Throws if `other` is null. |

## MomentNet.Display

Optional `CultureInfo` parameters default to `CultureInfo.CurrentCulture`. `withoutSuffix` defaults to false on the shorter overloads.

| Method | Receivers | Signature shape |
| --- | --- | --- |
| `FromNow` | `DateTime`, `DateTimeOffset`, `DateOnly` **6+** | `(CultureInfo?)` and `(bool withoutSuffix, CultureInfo?)` |
| `From` | same | `(T other, CultureInfo?)` and `(T other, bool withoutSuffix, CultureInfo?)` |
| `ToNow` | same | `(CultureInfo?)` and `(bool withoutSuffix, CultureInfo?)` |
| `To` | same | `(T other, CultureInfo?)` and `(T other, bool withoutSuffix, CultureInfo?)` |
| `CalendarTime` | `DateTime`, `DateTimeOffset`, `DateOnly` **6+** | `(CalendarTimeFormats? formats = null)` compares with now. `(T reference, CalendarTimeFormats? formats = null, CultureInfo? ci = null)` compares with that reference. |
| `Format` | `DateTime`, `DateTimeOffset`, `DateOnly` **6+**, `TimeOnly` **6+** | `(string? format = null, CultureInfo? cultureInfo = null)` |
| `UnixTimestampInSeconds` | `DateTime`, `DateTimeOffset`, `DateOnly` **6+** | `()` returns `double` |
| `UnixTimestampInMilliseconds` | same | `()` returns `double` |
| `DiffInDays` | `DateTime`, `DateTimeOffset`, `DateOnly` **6+** | `(T other)` returns `double`. `DateTimeOffset` subtraction uses the UTC instant. |
| `DiffInMonths` | same | `(T other)` returns `double`. Fractional days use the source month length. `DateTimeOffset` uses each value's year, month, and day. |
| `DiffInQuarters` | same | `(T other)` returns `double`, from `DiffInMonths / 3` |
| `DiffInYears` | same | `(T other)` returns `double`, from `DiffInMonths / 12` |

## MomentNet.Display.Models

`CalendarTimeFormats` has `SameDay`, `NextDay`, `NextWeek`, `LastDay`, `LastWeek`, and `EverythingElse`.

| Constructor | Notes |
| --- | --- |
| `CalendarTimeFormats(CultureInfo? ci = null)` | Localised default patterns for that culture. |
| `CalendarTimeFormats(string sameDay, string nextDay, string nextWeek, string lastDay, string lastWeek, string everythingElse)` | Each argument is a .NET format pattern. |

## MomentNet.GetSet

`Week`, `FirstDateInWeek`, and `LastDateInWeek` have an overload that takes `CultureInfo`. The shorter overload uses `CultureInfo.CurrentCulture`.

| Method | Receivers | Returns |
| --- | --- | --- |
| `Quarter` | `DateTime`, `DateTimeOffset`, `DateOnly` **6+** | `int`, 1 to 4 |
| `Week` | same | `int`, culture-specific week of the year |
| `IsoWeek` | same | `int`, ISO-8601 week |
| `IsoWeekYear` | same | `int`, ISO week-numbering year |
| `FirstDateInWeek` | same | Same type as the receiver, at the culture's first day of the week. `DateTimeOffset` keeps its offset. |
| `LastDateInWeek` | same | Six days after `FirstDateInWeek`. `DateTimeOffset` keeps its offset. |

## MomentNet.Manipulate

`DateTimeAnchor` values: `Minute`, `Hour`, `Day`, `Week`, `IsoWeek`, `Month`, `Quarter`, `Year`.

| Method | Receivers | Signature shape |
| --- | --- | --- |
| `StartOf` | `DateTime`, `DateTimeOffset`, `DateOnly` **6+** | `(DateTimeAnchor)` and `(DateTimeAnchor, CultureInfo)`. Culture affects `Week` only. |
| `EndOf` | same | Same overloads as `StartOf`. |
| `StartOf`, `EndOf` | `TimeOnly` **6+** | `(DateTimeAnchor)`. Only `Minute` and `Hour`. |
| `Next` | `DateTime`, `DateTimeOffset`, `DateOnly` **6+** | `(DayOfWeek)` and `(DayOfWeek, int count)`. `count` must be greater than zero. |
| `Last` | same | Same overloads as `Next`. |
| `Final` | `DateTime` → `FinalDays`, `DateTimeOffset` → `FinalDaysOffset`, `DateOnly` **6+** → `FinalDaysDateOnly` | Then `Monday()` through `Sunday()`, then `InMonth()` or `InYear()`. The result matches the original receiver type. `DateTimeOffset` keeps its offset. |

`DateOnly` `StartOf` and `EndOf` accept `Day`, `Week`, `IsoWeek`, `Month`, `Quarter`, and `Year`.

## MomentNet.Query

| Method | Receivers | Signature shape |
| --- | --- | --- |
| `IsLeapYear` | `DateTime`, `DateTimeOffset`, `DateOnly` **6+** | `()` |
| `IsSame`, `IsBefore`, `IsSameOrBefore`, `IsAfter`, `IsSameOrAfter` | `DateTime`, `DateTimeOffset`, `DateOnly` **6+**, `TimeOnly` **6+** | `(T other)` |
| `IsBetween` | same | `(T start, T end)`, inclusive |
| `IsDaylightSavingTime` | `DateTime`, `DateTimeOffset`, `DateOnly` **6+** | `()` uses `TimeZoneInfo.Local`. `(TimeZoneInfo timeZone)` uses that zone and throws if it is null. |

## MomentNet.Plugins.BusinessDays

Holiday lists use the same type as the receiver. The date portion is what matters.

| Method | Receivers | Signature shape |
| --- | --- | --- |
| `IsBusinessDay` | `DateTime`, `DateTimeOffset`, `DateOnly` **6+** | `()` and `(IEnumerable<T> holidays)` |
| `IsWeekend` | same | `()` |
| `IsWeekday` | same | `()` |
| `AddBusinessDays` | same | `(int days)` and `(int days, IEnumerable<T> holidays)`. Zero returns the same value. A negative count moves backward. |

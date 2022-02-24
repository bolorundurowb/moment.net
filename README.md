# Moment.net

[![Build Status](https://app.travis-ci.com/bolorundurowb/moment.net.svg?branch=master)](https://app.travis-ci.com/bolorundurowb/moment.net) [![Coverage Status](https://coveralls.io/repos/github/bolorundurowb/moment.net/badge.svg)](https://coveralls.io/github/bolorundurowb/moment.net)  [![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](LICENSE) [![NuGet Badge](https://buildstats.info/nuget/moment.net)](https://www.nuget.org/packages/moment.net)

## Overview
This library aims to port as many bits of functionality from moment.js as is necessary. A few have been ported thus far `FromNow`, `From`, `ToNow`, `To`, `StartOf`, `EndOf` and `CalendarTime`. Attempts are also being made to add some functionalities that might not exist in moment.js.

## Getting started
We recommend getting Moment.net via nuget package manager.

#### Package Manager
```
PM > Install-Package moment.net
```

#### .NET CLI
```
> dotnet add package moment.net
```

#### PackageReference
```csharp
<PackageReference Include="moment.net" />
```

#### Package Manager
`PM > Install-Package moment.net`

## Example Usage

#### FromNow
```csharp
var dateTime = new DateTime(2017, 1, 1);
var relativeTime = dateTime.FromNow(); // 2 years ago
```

#### From
```csharp
var past = new DateTime(2017, 1, 1);
var future = new DateTime(2020, 1, 1);
var relativeTime = past.From(future); // 3 years ago
```


#### ToNow
```csharp
var dateTime = new DateTime(2020, 1, 1);
var relativeTime = dateTime.ToNow(); // in one year
```

#### To
```csharp
var past = new DateTime(2019, 1, 1);
var future = new DateTime(2021, 1, 1);
var relativeTime = past.From(future); // in 2 years
```

#### StartOf
```csharp
var date = DateTime.Parse("5/1/2008 8:30:52 AM", System.Globalization.CultureInfo.InvariantCulture);
var startOfMinute = date.StartOf(DateTimeAnchor.Minute);	//01/05/2008 08:30:00"
var startOfHour = date.StartOf(DateTimeAnchor.Hour);		//01/05/2008 08:00:00" 
var startOfDay = date.StartOf(DateTimeAnchor.Day);		//01/05/2008 00:00:00"
var startOfWeek = date.StartOf(DateTimeAnchor.Week);		//27/04/2008 00:00:00" (previous month)
var startOfMonth = date.StartOf(DateTimeAnchor.Month);		//01/05/2008 00:00:00"
var startOfYear = date.StartOf(DateTimeAnchor.Year);		//01/01/2008 00:00:00"
```

#### EndOf
```csharp
var date = DateTime.Parse("5/1/2008 8:30:52 AM", System.Globalization.CultureInfo.InvariantCulture);
var endOfMinute = date.EndOf(DateTimeAnchor.Minute);	        // 01/05/2008 08:30:59
var endOfHour = date.EndOf(DateTimeAnchor.Hour);		// 01/05/2008 08:59:59
var endOfDay = date.EndOf(DateTimeAnchor.Day);		        // 01/05/2008 23:59:59
var endOfWeek = date.EndOf(DateTimeAnchor.Week);		// 03/05/2008 23:59:59
var endOfMonth = date.EndOf(DateTimeAnchor.Month);		// 31/05/2008 23:59:59
var endOfYear = date.EndOf(DateTimeAnchor.Year);		// 31/12/2008 23:59:59
```

#### CalendarTime
Calendar Time supports creating formats for displaying the return string. The format works in the standard DateTime format string manner

```csharp
var startDateTime = new DateTime(2012, 12, 12);
var sameDay = new DateTime(2012, 12, 12, 12, 0, 0);
var endDateTime = new DateTime(2012, 12, 13);
var calendarTime = startDateTime.CalendarTime(endDateTime); // Tomorrow at 00:00 AM
calendarTime = endDateTime.CalendarTime(startDateTime); // Yesterday at 00:00 AM
calendarTime = startDateTime.CalendarTime(sameDay); // Today at 12:00 PM
```

#### UnixTime
UnixTime supports retrieving the number of seconds or milliseconds that have elapsed since the [unix epoch](https://en.wikipedia.org/wiki/Unix_time)

```csharp
var dateTime = new DateTime(1971, 01, 01, 0, 0, 0, DateTimeKind.Utc);
var millisecondsElapsed = dateTime.UnixTimestampInMilliseconds(); // 31536000000
var secondsElapsed = dateTime.UnixTimestampInSeconds(); // 31536000
```

#### First and Last Date in a week
Moment.net supports retrieving the first or last day in a week given a specific ``DateTime``
```csharp
date.FirstDateInWeek()  // 27/04/2008 00:00:00 (previous month)
date.LastDateInWeek()   // 03/05/2008 00:00:00
```
The example above uses the current ``CultureInfo`` for the system in use, to specify a ``CultureInfo``, moment.net has an overloaded method that takes takes ``CultureInfo`` as an argument ``date.FirstDateInWeek(someCultureInfo)``.

#### Next
Returns the date of the next Thursday.
```csharp
var date = DateTime.Parse("5/1/2008 8:30:52 AM", System.Globalization.CultureInfo.InvariantCulture);
date.Next(DayOfWeek.Thursday) // 08/05/2008 08:30:52
```
Returns the 3rd next Thursday use
```csharp
date.Next(DayOfWeek.Thursday, 3) // 22/05/2008 08:30:52
```
#### Last
Returns the date of the previous Friday.
```csharp
var date = DateTime.Parse("5/1/2008 8:30:52 AM", System.Globalization.CultureInfo.InvariantCulture);
date.Last(DayOfWeek.Friday) // 25/04/2008 08:30:52
```
Returns the 3rd previous Thursday.
```csharp
date.Last(DayOfWeek.Thursday, 3) // 10/04/2008 08:30:52
```

#### Final
Fluently returns the final day of the week in a month or year given a date

```csharp
var date = DateTime.Parse("5/1/2008 8:30:52 AM", System.Globalization.CultureInfo.InvariantCulture);
// final monday in may 2008
date.Final().Monday().InMonth(); // 26/05/2008 00:00:00

// final sunday in 2008
date.Final().Sunday().InYear();  // 28/12/2008 00:00:00
```

#### Format
Fluently returns a formatted string for a given date. if no format string is provided the format defaults to the `ISO-8601` standard with no fractional seconds.

```csharp
var dateTime = new DateTime(1971, 01, 01, 0, 0, 0, DateTimeKind.Utc);
date.Format(); // 1971-01-01T00:00:00+00:00
date.Format("yyyy MMM dd"); // 1971 Jan 01
```

#### IsLeapYear
Determines if a given date falls in a leap year.

```csharp
var dateTime = DateTime.Parse("1992-02-01");
dateTime.IsLeapYear(); // True

var dateTime = DateTime.Parse("1900-02-01");
dateTime.IsLeapYear(); // False
```

#### IsSame
Checks in a timezone-safe manner whether two dates are the same

```csharp
var one = DateTime.Parse("2022-01-01T23:00:00-01:00");
var another = DateTime.Parse("2022-01-02T03:00:00+03:00");
one.IsSame(another); // True

var wrong = DateTime.Parse("1900-02-01");
wrong.IsSame(one); // False
```

#### IsBefore
Checks in a timezone-safe manner whether one date comes before another

```csharp
var one = DateTime.Parse("2022-01-02");
var another = DateTime.Parse("2022-01-03");
one.IsSameOrBefore(another); // True

var wrong = DateTime.Parse("2022-02-01");
one.IsBefore(wrong); // False
```

#### IsSameOrBefore
Checks in a timezone-safe manner whether one date is the same as or comes before another

```csharp
var one = DateTime.Parse("2022-01-02");
var another = DateTime.Parse("2022-01-03");
one.IsSameOrBefore(another); // True

var same = DateTime.Parse("2022-01-02");
one.IsSameOrBefore(same); // True

var wrong = DateTime.Parse("2022-01-01");
one.IsSameOrBefore(wrong); // False
```

#### IsAfter
Checks in a timezone-safe manner whether one date comes after another

```csharp
var one = DateTime.Parse("2022-01-03");
var another = DateTime.Parse("2022-01-02");
one.IsAfter(another); // True

var wrong = DateTime.Parse("2022-02-01");
one.IsAfter(wrong); // False
```

#### IsSameOrAfter
Checks in a timezone-safe manner whether one date is the same as or comes after another

```csharp
var one = DateTime.Parse("2022-01-03");
var another = DateTime.Parse("2022-01-02");
one.IsSameOrAfter(another); // True

var same = DateTime.Parse("2022-01-03");
one.IsSameOrAfter(same); // True

var wrong = DateTime.Parse("2022-02-01");
one.IsSameOrAfter(wrong); // False
```
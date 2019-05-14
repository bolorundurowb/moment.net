# Moment.net

[![Build Status](https://travis-ci.org/bolorundurowb/moment.net.svg?branch=master)](https://travis-ci.org/bolorundurowb/moment.net)  [![Coverage Status](https://coveralls.io/repos/github/bolorundurowb/moment.net/badge.svg)](https://coveralls.io/github/bolorundurowb/moment.net)  [![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](LICENSE) [![NuGet Badge](https://buildstats.info/nuget/moment.net)](https://www.nuget.org/packages/moment.net)

## Overview
This library aims to port as many bits of functionality from moment.js as is necessary. A few have been ported thus far `FromNow`, `From`, `ToNow`, `To`, `StartOf`, `EndOf` and `CalendarTime`.

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
``PM > Install-Package moment.net -Version 1.1.0``

## Usages

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
date.Next(DayOfWeek.Thursday) // 08/05/2008 08:30:52
```
#### Last
Returns the date of the previous Friday.
```csharp
date.Last(DayOfWeek.Friday) // 25/04/2008 08:30:52
```
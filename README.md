# moment.net

[![Build Status](https://travis-ci.org/bolorundurowb/moment.net.svg?branch=master)](https://travis-ci.org/bolorundurowb/moment.net)  [![Coverage Status](https://coveralls.io/repos/github/bolorundurowb/moment.net/badge.svg)](https://coveralls.io/github/bolorundurowb/moment.net)  [![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](LICENSE)


This library aims to port as many bits of functionality from moment.js as is necessary. A few have been ported thus far `FromNow`, `From`, `ToNow`, `To` and `StartOf`.

## Usages

- FromNow
```csharp
var dateTime = new DateTime(2017, 1, 1);
var relativeTime = dateTime.FromNow(); // 2 years ago
```

- From
```csharp
var past = new DateTime(2017, 1, 1);
var future = new DateTime(2020, 1, 1);
var relativeTime = past.From(future); // 3 years ago
```


- ToNow
```csharp
var dateTime = new DateTime(2020, 1, 1);
var relativeTime = dateTime.ToNow(); // in one year
```

- To
```csharp
var past = new DateTime(2019, 1, 1);
var future = new DateTime(2021, 1, 1);
var relativeTime = past.From(future); // in 2 years
```

- StartOfX
```csharp
var date = DateTime.Parse("5/1/2008 8:30:52 AM", System.Globalization.CultureInfo.InvariantCulture);
var startOfMinute = date.StartOf(DateTimeAnchor.Minute);	//01/05/2008 08:30:00"
var startOfHour = date.StartOf(DateTimeAnchor.Hour);		//01/05/2008 08:00:00" 
var startOfDay = date.StartOf(DateTimeAnchor.Day);			//01/05/2008 00:00:00"
var startOfWeek = date.StartOf(DateTimeAnchor.Week);		//27/04/2008 00:00:00" (previous month)
var startOfMonth = date.StartOf(DateTimeAnchor.Month);		//01/05/2008 00:00:00"
var startOfYear = date.StartOf(DateTimeAnchor.Year);		//01/01/2008 00:00:00"
```

# Moment.net ⏰

[![Build Status](https://app.travis-ci.com/bolorundurowb/moment.net.svg?branch=master)](https://app.travis-ci.com/bolorundurowb/moment.net) [![Coverage Status](https://coveralls.io/repos/github/bolorundurowb/moment.net/badge.svg)](https://coveralls.io/github/bolorundurowb/moment.net)  [![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](LICENSE) ![NuGet Version](https://img.shields.io/nuget/v/moment.net)


## Overview 📌
This library aims to port as many bits of functionality from moment.js as necessary. Currently supported features include:
`FromNow`, `From`, `ToNow`, `To`, `StartOf`, `EndOf`, and `CalendarTime`. Additional functionalities not present in moment.js are also being added.

## Getting Started 🚀
Install Moment.net via NuGet:

### Package Manager 📦
```
PM > Install-Package moment.net
```

### .NET CLI 💻
```
> dotnet add package moment.net
```

### PackageReference 📝
```csharp
<PackageReference Include="moment.net" />
```

## Example Usage 🛠️

### FromNow ⏳
```csharp
var dateTime = new DateTime(2017, 1, 1);
var relativeTime = dateTime.FromNow(); // 2 years ago
```

### From ⏪
```csharp
var past = new DateTime(2017, 1, 1);
var future = new DateTime(2020, 1, 1);
var relativeTime = past.From(future); // 3 years ago
```

### ToNow ⏩
```csharp
var dateTime = new DateTime(2020, 1, 1);
var relativeTime = dateTime.ToNow(); // in one year
```

### To 🎯
```csharp
var past = new DateTime(2019, 1, 1);
var future = new DateTime(2021, 1, 1);
var relativeTime = past.To(future); // in 2 years
```

### StartOf 🏁
```csharp
var date = DateTime.Parse("5/1/2008 8:30:52 AM", System.Globalization.CultureInfo.InvariantCulture);
var startOfDay = date.StartOf(DateTimeAnchor.Day); // 01/05/2008 00:00:00
```

### EndOf 🔚
```csharp
var date = DateTime.Parse("5/1/2008 8:30:52 AM", System.Globalization.CultureInfo.InvariantCulture);
var endOfDay = date.EndOf(DateTimeAnchor.Day); // 01/05/2008 23:59:59
```

### CalendarTime 📅
```csharp
var startDateTime = new DateTime(2012, 12, 12);
var sameDay = new DateTime(2012, 12, 12, 12, 0, 0);
var calendarTime = startDateTime.CalendarTime(sameDay); // Today at 12:00 PM
```

### UnixTime ⌛
```csharp
var dateTime = new DateTime(1971, 01, 01, 0, 0, 0, DateTimeKind.Utc);
var millisecondsElapsed = dateTime.UnixTimestampInMilliseconds(); // 31536000000
```

### First and Last Date in a Week 📆
```csharp
date.FirstDateInWeek();  // 27/04/2008 00:00:00 (previous month)
date.LastDateInWeek();   // 03/05/2008 00:00:00
```

### Next 🔜
```csharp
var date = DateTime.Parse("5/1/2008 8:30:52 AM", System.Globalization.CultureInfo.InvariantCulture);
date.Next(DayOfWeek.Thursday); // 08/05/2008 08:30:52
```

### Last 🔙
```csharp
var date = DateTime.Parse("5/1/2008 8:30:52 AM", System.Globalization.CultureInfo.InvariantCulture);
date.Last(DayOfWeek.Friday); // 25/04/2008 08:30:52
```

### Final 📍
```csharp
var date = DateTime.Parse("5/1/2008 8:30:52 AM", System.Globalization.CultureInfo.InvariantCulture);
date.Final().Monday().InMonth(); // 26/05/2008 00:00:00
```

### Format 🖋️
```csharp
var dateTime = new DateTime(1971, 01, 01, 0, 0, 0, DateTimeKind.Utc);
date.Format("yyyy MMM dd"); // 1971 Jan 01
```

### IsLeapYear 🏅
```csharp
var dateTime = DateTime.Parse("1992-02-01");
dateTime.IsLeapYear(); // True
```

### Localization 🌍
```csharp
var dateTime = new DateTime(2017, 1, 1);
var relativeTime = dateTime.FromNow(new CultureInfo("es")); // 6 años atrás
```

### Contributing 🤝
Want to add more languages? Simply create a `String.[language identifier].resx` file and follow the `Strings.es.resx` example.

## License 📜
This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.


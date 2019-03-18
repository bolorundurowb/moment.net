# moment.net

[![Build Status](https://travis-ci.org/bolorundurowb/moment.net.svg?branch=master)](https://travis-ci.org/bolorundurowb/moment.net)  [![Coverage Status](https://coveralls.io/repos/github/bolorundurowb/moment.net/badge.svg)](https://coveralls.io/github/bolorundurowb/moment.net)  [![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](LICENSE)


This library aims to port as many bits of functionality from moment.js as is necessary. A few have been ported thus far `FromNow`, `From`, `ToNow` and `To`.

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
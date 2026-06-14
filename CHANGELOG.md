# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [1.4.0]

### Added
- German (`de`) localization for all relative time and calendar strings
- Russian (`ru`) localization for all relative time and calendar strings

### Fixed
- `FinalSpan.InMonth()` off-by-one error: the 7-day scan window now starts at the correct day (`DaysInMonth - 6`) so the true last occurrence of a weekday is always returned
- Spanish `TIME_LAST` string changed from `"El último"` to `"el pasado"` for natural phrasing (e.g. "el pasado lunes")
- Russian `CalendarTime` tests updated to assert against localized Russian strings now that the resource file exists

### Changed
- All test methods across the test suite renamed to follow the C# `MethodUnderTest_Scenario_ExpectedResult` convention
- Expanded `Final.Tests.cs` with coverage for all 7 days of the week, leap/non-leap February, `DateTimeKind` preservation, and parameterized `[TestCase]` suites for `InMonth()` and `InYear()`
- Expanded `TimeFrom.Tests.cs` with boundary tests for every threshold in `ParseTimeDifference`, reversed-date symmetry, and localized culture assertions

## [1.3.5] - 2026-02-24

### Added
- README included in the NuGet package (`PackageReadmeFile`)
- `InternalsVisibleTo` declaration so the test project can access internal members

### Changed
- Project file and package metadata cleanup

## [1.3.x] - Earlier 2026

### Added
- `IsWeekend()`, `IsWeekday()`, `IsBetween()` extension methods
- `DiffInDays()`, `DiffInMonths()`, `DiffInYears()` extension methods
- Business day utilities: `IsBusinessDay()`, `AddBusinessDays()`
- Date positioning methods: `Next()`, `Last()`, `Final().{Day}().InMonth()`, `Final().{Day}().InYear()`
- `Format()` extension method
- `UnixTimestampInMilliseconds()` and related Unix time helpers
- French (`fr`) and Portuguese (`pt`) localization files
- Spanish (`es`) localization file

### Fixed
- `From()` / `FromNow()` returning incorrect results for certain edge cases
- `DateTimeKind` being dropped when chaining date manipulation methods

### Changed
- Switched CI/CD and code coverage tooling
- Improved culture consistency across unit tests

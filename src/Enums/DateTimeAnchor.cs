using System;

namespace moment.net.Enums;

/// <summary>
/// Specifies anchors for date and time operations to define the starting point for calculations
/// such as trimming or aligning a <see cref="DateTime"/> value.
/// </summary>
public enum DateTimeAnchor
{
    Minute,
    Hour,
    Day,
    Week,
    Month,
    Year
}
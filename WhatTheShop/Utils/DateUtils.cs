namespace WhatTheShop.Utils;

public static class DateUtils
{
    public static IEnumerable<(DateTime, DateTime)> SplitDateRangeByMonth(int monthCount)
    {
        var now = DateTime.UtcNow;
        var start = new DateTime(now.Year, now.Month, 1, 0, 0, 0).AddMonths(-monthCount);
        var end = new DateTime(now.Year, now.Month, now.Day, 0, 0, 0).AddMilliseconds(-1);

        DateTime chunkEnd;
        
        while ((chunkEnd = start.AddMonths(1).AddMilliseconds(-1)) < end)
        {
            yield return (start, chunkEnd);
            start = chunkEnd.AddMilliseconds(1);
        }

        yield return (start, chunkEnd);
    }
}
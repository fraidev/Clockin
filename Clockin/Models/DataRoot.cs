using System;
using System.Collections.Generic;
using System.Linq;
using Clockin.Extensions;
using Spectre.Console;

namespace Clockin.Models
{
    public record DataRoot
    {
        public int MaxLines { get; set; } = 10;
        public IDictionary<DateTime, SortedSet<TimeSpan>> Shifts { get; set; } = new Dictionary<DateTime, SortedSet<TimeSpan>>();

        public void PushShift(DateTime date, TimeSpan time)
        {
            if (Shifts.TryGetValue(date, out var shifts))
            {
                shifts.Add(time);
            }
            else
            {
                Shifts.Add(date, new SortedSet<TimeSpan>() { time });
            }
        }

        public void PopShift()
        {
            if (!Shifts.Any())
            {
                return;
            }
            
            var (key, value) = Shifts.LastOrDefault();

            if (!value.Any())
            {
                Shifts.Remove(key);
                PopShift();
            }
            
            var lastWorkerTime = value.LastOrDefault();

            value.Remove(lastWorkerTime);
            
            if (!value.Any())
            {
                Shifts.Remove(key);
            }
        }
        
        public void FillTable(Table table, int count = 10)
        {
            foreach (var (key, value) in Shifts.Reverse().Take(count))
            {
                table.AddRow(@$"[#5B86B3]{key:d}[/]", @$"[#A57EA8]{GetTimes(value)}[/]", @$"[#048479]{Total(value)}[/]");
            }
        }

        private static string GetTimes(IEnumerable<TimeSpan> shifts) =>
            string.Join(" [yellow]-[/] ", shifts.GetSlices(2).Select(shift =>
            {
                var leftStr = $"{shift[0]} ";
                var rightStr = shift.Length > 1 ? $"- {shift[1]}" : "...";

                return leftStr + rightStr;
            }));

        private static TimeSpan Total(IReadOnlyCollection<TimeSpan> shifts)
        {
            var total = shifts.Count;

            if (total % 2 == 1)
                total -= 1;

            return shifts.Take(total).GetSlices(2).Aggregate(new TimeSpan(),
                (current, shift) => current + (shift[1] - shift[0]));
        }
    }
}
using System;
using Clockin.Options;
using Spectre.Console;

namespace Clockin
{
    public class CommandHandler
    {
        private readonly IStorage _storage;

        public CommandHandler(IStorage storage)
        {
            _storage = storage;
        }

        public int Add(string? arg, bool optsNow)
        {
            TimeSpan shift;

            if (optsNow)
            {
                shift = DateTime.Now.TimeOfDay;
                AddTime(shift);
                ShowList();
                return 0;
            }
            
            if (arg is not {Length: > 0})
            {
                return 1;
            }

            var time = arg.Split(":");

            if (!int.TryParse(time[0], out var hours) || !int.TryParse(time[1], out var minutes))
            {
                AnsiConsole.Markup("Invalid format!");
                return 1;
            }

            shift = new TimeSpan(hours, minutes, 0);
            AddTime(shift);
            ShowList();

            return 0;
        }

        public int Pop()
        {
            var dataRoot = _storage.Get();
            dataRoot.PopShift();
            _storage.Save(dataRoot);

            ShowList();

            return 0;
        }

        public int ShowList(int count = 0)
        {
            var dataRoot = _storage.Get();
            if (count == 0)
            {
                count = dataRoot.MaxLines;
            }
            
            var table = new Table {Border = TableBorder.None};
            table.AddColumn("[yellow]Date[/]");
            table.AddColumn("[yellow]Times[/]", x => x.Alignment = Justify.Center);
            table.AddColumn("[yellow]Total[/]", x => x.Alignment = Justify.Right);
            
            dataRoot.FillTable(table, count);
            
            AnsiConsole.Render(table);
            
            return 0;
        }

        public int MaxLines(PreferenceOptions o)
        {
            if (o.MaxLines < 1)
            {
                return 1;
            }
            
            var dataRoot = _storage.Get();
            dataRoot.MaxLines = o.MaxLines;
            _storage.Save(dataRoot);

            return 0;
        }

        private void AddTime(TimeSpan shift)
        {
            var dataRoot = _storage.Get();
            dataRoot.PushShift(DateTime.Today, shift);
            _storage.Save(dataRoot);
        }
    }
}
using Clockin;
using Clockin.Options;
using CommandLine;

var handler = new CommandHandler(new Storage());
Parser.Default.ParseArguments<AddOptions, PopOptions, ListOptions, PreferenceOptions>(args)
    .MapResult((AddOptions opts) => handler.Add(opts.Time, opts.Now),
        (PopOptions _) => handler.Pop(),
        (ListOptions _) => handler.ShowList(),
        (PreferenceOptions opts) => handler.MaxLines(opts),
        _ => 1);

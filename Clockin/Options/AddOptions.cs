using CommandLine;

namespace Clockin.Options
{
    [Verb("add", HelpText = "Add new shift time")]
    public class AddOptions
    {
        [Option('t', "time", Required = false, HelpText = "Insert a time, Example format: 08:01")]
        public string? Time { get; set; }
        
        [Option('n', "now", Required = false, HelpText = "Insert now")]
        public bool Now { get; set; }
    }
}
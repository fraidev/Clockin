using CommandLine;

namespace Clockin.Options
{
    [Verb("ls", HelpText = "Show your last shifts")]
    public class ListOptions
    {
        [Option('c', "count", Required = false, HelpText = "shifts count")]
        public int Count { get; set; }
    }
}
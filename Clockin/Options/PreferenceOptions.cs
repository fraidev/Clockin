using CommandLine;

namespace Clockin.Options
{
    [Verb("sp", HelpText = "Set preference options.")]
    public class PreferenceOptions
    {
        [Option('m', "max-lines", Required = false, HelpText = "set preference for max lines to Show")]
        public int MaxLines { get; set; }
    }
}
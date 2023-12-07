using FkThat.Console;

namespace FkThat.HiddenInput;

internal sealed class ZeroMaskConsoleAdapter : IConsoleAdapter
{
    private readonly IConsoleText _console;

    public ZeroMaskConsoleAdapter(IConsoleText console)
    {
        _console = console;
    }

    public void ExecuteCommand(ConsoleCommand command)
    {
        if (command is NewLineConsoleCommand)
        {
            _console.Error.WriteLine();
        }
    }
}

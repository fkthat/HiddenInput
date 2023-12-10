using FkThat.Console;

namespace FkThat.HiddenInput;

internal sealed class CharMaskConsoleAdapter : IConsoleAdapter
{
    private readonly IConsoleText _console;
    private readonly char _mask;
    private int _counter;

    public CharMaskConsoleAdapter(IConsoleText console, char mask)
    {
        _console = console;
        _mask = mask;
    }

    public void ExecuteCommand(ConsoleCommand command)
    {
        if (command is CharConsoleCommand)
        {
            _console.Error.Write(_mask);
            _counter++;
            return;
        }

        if (command is BackspaceConsoleCommand)
        {
            if (_counter > 0)
            {
                _console.Error.Write("\b \b");
                _counter--;
            }

            return;
        }

        if (command is EraseConsoleCommand)
        {
            var b = new string('\b', _counter);
            var s = new string(' ', _counter);
            _console.Error.Write($"{b}{s}{b}");
            _counter = 0;
            return;
        }

        if (command is NewLineConsoleCommand)
        {
            _console.Error.WriteLine();
            _counter = 0;
            return;
        }

        // should never be here
        throw new NotSupportedException();
    }
}

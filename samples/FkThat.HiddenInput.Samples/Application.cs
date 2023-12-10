using FkThat.Console;

namespace FkThat.HiddenInput.Samples;

internal class Application(IConsoleText console, IConsoleHiddenInput hiddenInput)
{
    private readonly IConsoleText _console = console;
    private readonly IConsoleHiddenInput _hiddenInput = hiddenInput;

    public void Run()
    {
        _console.Error.Write("Password (Win): ");
        var pwd = _hiddenInput.ReadLine('*');
        _console.Out.WriteLine(pwd);
        _console.Error.Write("Password (Unix): ");
        pwd = _hiddenInput.ReadLine('\0');
        _console.Out.WriteLine(pwd);
    }
}

using FkThat.Console;

namespace FkThat.HiddenInput;

internal sealed class StateMachineFactory(IConsoleText console)
    : IStateMachineFactory
{
    private readonly IConsoleText _console = console;

    public IStateMachine CreateStateMachine(char mask)
    {
        return new StateMachine((mask != '\0')
            ? new CharMaskConsoleAdapter(_console, mask)
            : new ZeroMaskConsoleAdapter(_console));
    }
}

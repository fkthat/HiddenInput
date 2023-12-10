namespace FkThat.HiddenInput;

internal sealed class StateMachineFactory(IConsoleAdapter console)
    : IStateMachineFactory
{
    private readonly IConsoleAdapter _console = console;

    public IStateMachine CreateStateMachine() => new StateMachine(_console);
}

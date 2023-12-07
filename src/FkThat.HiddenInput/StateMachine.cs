namespace FkThat.HiddenInput;

internal sealed class StateMachine(IConsoleAdapter console, string data = "")
    : IStateMachine
{
    private readonly IConsoleAdapter _console = console;
    private readonly Stack<char> _buffer = new(data);

    public bool IsFinished { get; private set; }

    public string Data => new(_buffer.Reverse().ToArray());

    public void ExecuteCommand(KeyboardCommand command)
    {
        if (command is CharKeyboardCommand charCmd)
        {
            _console.ExecuteCommand(new CharConsoleCommand());
            _buffer.Push(charCmd.Char);
            return;
        }

        if (command is BackspaceKeyboardCommand)
        {
            if (_buffer.Count > 0)
            {
                _console.ExecuteCommand(new BackspaceConsoleCommand());
                _buffer.Pop();
            }

            return;
        }

        if (command is EraseKeyboardCommand)
        {
            _console.ExecuteCommand(new EraseConsoleCommand());
            _buffer.Clear();
            return;
        }

        if (command is NewLineKeyboardCommand)
        {
            _console.ExecuteCommand(new NewLineConsoleCommand());
            IsFinished = true;
            return;
        }

        if (command is NopKeyboardCommand)
        {
            return;
        }

        throw new NotSupportedException();
    }
}

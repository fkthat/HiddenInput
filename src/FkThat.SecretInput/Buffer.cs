namespace FkThat.SecretInput;

internal sealed class Buffer : IBuffer, IKeyboardCommandHandler
{
    private readonly Stack<char> _buffer = new Stack<char>();

    public string Content => new string(_buffer.Reverse().ToArray());

    public void Handle(CharKeyboardCommand command) => _buffer.Push(command.Char);

    public void Handle(BackspaceKeyboardCommand command)
    {
        if (_buffer.Count > 0)
        {
            _buffer.Pop();
        }
    }

    public void Handle(EnterKeyboardCommand command)
    {
        // intentionally empty
    }
}

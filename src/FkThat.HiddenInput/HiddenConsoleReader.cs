using FkThat.Console;

namespace FkThat.HiddenInput;

/// <inheritdoc/>
public sealed class HiddenConsoleReader : IHiddenConsoleReader
{
    private readonly IConsoleText _consoleText;
    private readonly IConsoleKeyboard _consoleKeyboard;

    /// <summary>
    /// Initialize a new instance of the <see cref="HiddenConsoleReader"/> class.
    /// </summary>
    /// <param name="consoleText">Console text I/O abstraction.</param>
    /// <param name="consoleKeyboard">Console keyboard abstraction.</param>
    /// <exception cref="ArgumentNullException">
    /// The <paramref name="consoleText"/> or <paramref name="consoleKeyboard"/> is <see
    /// langword="null"/>.
    /// </exception>
    public HiddenConsoleReader(IConsoleText consoleText, IConsoleKeyboard consoleKeyboard)
    {
        _consoleText = consoleText ?? throw new ArgumentNullException(nameof(consoleText));
        _consoleKeyboard = consoleKeyboard ?? throw new ArgumentNullException(nameof(consoleKeyboard));
    }

    /// <inheritdoc/>
    public string ReadLine(char maskChar = '*')
    {
        Stack<char> buffer = new();

        while (true)
        {
            var keyInfo = _consoleKeyboard.ReadKey(true);

            if (keyInfo.KeyChar != '\0')
            {
                WriteMaskChar(maskChar);
                buffer.Push(keyInfo.KeyChar);
            }
            else if (keyInfo.Key == ConsoleKey.Backspace)
            {
                if (buffer.Count > 0)
                {
                    WriteBackspace(maskChar);
                    buffer.Pop();
                }
            }
            else if (keyInfo.Key == ConsoleKey.Enter)
            {
                WriteEnter();
                return new string(buffer.Reverse().ToArray());
            }
        }
    }

    private void WriteMaskChar(char maskChar)
    {
        if (maskChar != '\0')
        {
            _consoleText.Out.Write(maskChar);
        }
    }

    private void WriteBackspace(char maskChar)
    {
        if (maskChar != '\0')
        {
            _consoleText.Out.Write("\b \b");
        }
    }

    private void WriteEnter()
    {
        _consoleText.Out.WriteLine();
    }
}

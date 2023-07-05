using FkThat.Console;

namespace FkThat.HiddenInput;

/// <inheritdoc/>
public sealed class ConsoleHiddenInput : IHiddenInput
{
    private readonly IConsoleKeyboard _consoleKeyboard;
    private readonly IConsoleText _consoleText;

    /// <summary>
    /// Initialize a new instance of the <see cref="ConsoleHiddenInput"/> class.
    /// </summary>
    /// <param name="consoleKeyboard">Console keyboard abstraction.</param>
    /// <param name="consoleText">Console text I/O abstraction.</param>
    /// <param name="maskChar">
    /// The char to mask the input. The null ('\0') char means the UNIX-like input.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// The <paramref name="consoleText"/> or <paramref name="consoleKeyboard"/> is <see
    /// langword="null"/>.
    /// </exception>
    public ConsoleHiddenInput(
        IConsoleKeyboard consoleKeyboard,
        IConsoleText consoleText,
        char maskChar = '*')
    {
        _consoleKeyboard = consoleKeyboard ??
            throw new ArgumentNullException(nameof(consoleKeyboard));

        _consoleText = consoleText ??
            throw new ArgumentNullException(nameof(consoleText));
    }

    /// <inheritdoc/>
    public string ReadLine()
    {
        throw new NotImplementedException();
    }
}

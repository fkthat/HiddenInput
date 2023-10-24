using System.Diagnostics.CodeAnalysis;

using FkThat.Console;

namespace FkThat.SecretInput;

/// <inheritdoc/>
public class SecretConsoleReader : ISecretConsoleReader
{
    private readonly IConsoleText _consoleText;
    private readonly IConsoleKeyboard _consoleKeyboard;

    /// <summary>
    /// Initialize a new instance of the <see cref="SecretConsoleReader"/> class.
    /// </summary>
    /// <param name="consoleText">Console text I/O abstraction.</param>
    /// <param name="consoleKeyboard">Console keyboard abstraction.</param>
    /// <exception cref="ArgumentNullException">
    /// The <paramref name="consoleText"/> or <paramref name="consoleKeyboard"/> is <see
    /// langword="null"/>.
    /// </exception>
    public SecretConsoleReader(IConsoleText consoleText, IConsoleKeyboard consoleKeyboard)
    {
        _consoleText = consoleText ?? throw new ArgumentNullException(nameof(consoleText));
        _consoleKeyboard = consoleKeyboard ?? throw new ArgumentNullException(nameof(consoleKeyboard));
    }

    /// <inheritdoc/>
    [ExcludeFromCodeCoverage]
    public string ReadLine(char maskChar = '*')
    {
        throw new NotImplementedException();
    }
}

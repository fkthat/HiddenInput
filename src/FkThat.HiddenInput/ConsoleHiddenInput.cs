using FkThat.Console;

namespace FkThat.HiddenInput;

/// <inheritdoc/>
public sealed class ConsoleHiddenInput : IConsoleHiddenInput
{
    private readonly IKeyboardAdapter _keyboard;
    private readonly IStateMachineFactory _factory;

    /// <summary>
    /// Initialize a new instance of the <see cref="ConsoleHiddenInput"/> class.
    /// </summary>
    /// <param name="keyboard">Console keyboard abstraction.</param>
    /// <param name="console">Console text I/O abstraction.</param>
    /// <param name="mask">
    /// The char to mask the input. The null ('\0') char means the UNIX-like input.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// The <paramref name="console"/> or <paramref name="keyboard"/> is <see
    /// langword="null"/>.
    /// </exception>
    public ConsoleHiddenInput(IConsoleKeyboard keyboard, IConsoleText console, char mask = '*')
    {
        ArgumentNullException.ThrowIfNull(keyboard, nameof(keyboard));
        ArgumentNullException.ThrowIfNull(console, nameof(console));

        _keyboard = new KeyboardAdapter(keyboard);

        IConsoleAdapter con = (mask != '\0')
            ? new CharMaskConsoleAdapter(console, mask)
            : new ZeroMaskConsoleAdapter(console);

        _factory = new StateMachineFactory(con);
    }

    internal ConsoleHiddenInput(IKeyboardAdapter keyboard, IStateMachineFactory factory)
    {
        _keyboard = keyboard;
        _factory = factory;
    }

    /// <inheritdoc/>
    public string ReadLine()
    {
        var machine = _factory.CreateStateMachine();

        while (!machine.IsFinished)
        {
            machine.ExecuteCommand(_keyboard.ReadCommand());
        }

        return machine.Data;
    }
}

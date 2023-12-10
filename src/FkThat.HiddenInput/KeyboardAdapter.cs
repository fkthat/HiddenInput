using FkThat.Console;

namespace FkThat.HiddenInput;

internal sealed class KeyboardAdapter : IKeyboardAdapter
{
    private readonly IConsoleKeyboard _keyboard;

    public KeyboardAdapter(IConsoleKeyboard keyboard)
    {
        _keyboard = keyboard;
    }

    public KeyboardCommand ReadCommand()
    {
        var ki = _keyboard.ReadKey(true);

        var keyChar = ki.KeyChar;
        var key = ki.Key;
        var shift = ki.Modifiers.HasFlag(ConsoleModifiers.Shift);
        var alt = ki.Modifiers.HasFlag(ConsoleModifiers.Alt);
        var ctl = ki.Modifiers.HasFlag(ConsoleModifiers.Control);

        // char
        if (keyChar != '\0' && !char.IsControl(keyChar))
        {
            return new CharKeyboardCommand(keyChar);
        }

        // backspace or Shift + backspace
        if (key == ConsoleKey.Backspace && !ctl)
        {
            return new BackspaceKeyboardCommand();
        }

        // Ctl + backspace
        if (key == ConsoleKey.Backspace && ctl)
        {
            return new EraseKeyboardCommand();
        }

        // Escape
        if (key == ConsoleKey.Escape)
        {
            return new EraseKeyboardCommand();
        }

        // enter
        if (key == ConsoleKey.Enter)
        {
            return new NewLineKeyboardCommand();
        }

        // undefined key combination
        return new NopKeyboardCommand();
    }
}

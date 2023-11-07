using FkThat.Console;

namespace FkThat.HiddenInput;

internal sealed class ConsoleKeyboardAdapter : IConsoleKeyboardAdapter
{
    private readonly IConsoleKeyboard _consoleKeyboard;

    public ConsoleKeyboardAdapter(IConsoleKeyboard consoleKeyboard)
    {
        _consoleKeyboard = consoleKeyboard;
    }

    public KeyboardCommand ReadKeyboardCommand()
    {
        while (true)
        {
            var keyInfo = _consoleKeyboard.ReadKey(true);

            if (keyInfo.KeyChar != '\0')
            {
                return new CharKeyboardCommand(keyInfo.KeyChar);
            }
            else if (keyInfo.Key == ConsoleKey.Backspace)
            {
                return new BackspaceKeyboardCommand();
            }
            else if (keyInfo.Key == ConsoleKey.Enter)
            {
                return new EnterKeyboardCommand();
            }
        }
    }
}

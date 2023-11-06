using FkThat.Console;

namespace FkThat.SecretInput
{
    internal sealed class ConsoleTextAdapter : IKeyboardCommandHandler
    {
        private readonly IConsoleText _consoleText;
        private readonly char _maskChar;

        private int _count;

        public ConsoleTextAdapter(IConsoleText consoleText, char maskChar)
        {
            _consoleText = consoleText;
            _maskChar = maskChar;
        }

        public void Handle(CharKeyboardCommand command)
        {
            if (_maskChar != '\0')
            {
                _consoleText.Out.Write(_maskChar);
                _count++;
            }
        }

        public void Handle(BackspaceKeyboardCommand command)
        {
            if (_count > 0)
            {
                _consoleText.Out.Write("\b \b");
                _count--;
            }
        }

        public void Handle(EnterKeyboardCommand command)
        {
            _consoleText.Out.WriteLine();
        }
    }
}

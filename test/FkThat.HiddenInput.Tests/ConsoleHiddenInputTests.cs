using FkThat.Console;

namespace FkThat.HiddenInput;

public class ConsoleHiddenInputTests
{
    [Fact]
    public void Ctor_should_check_null_consoleText()
    {
        IConsoleKeyboard consoleKeyboard = A.Fake<IConsoleKeyboard>();
        IConsoleText consoleText = null!;

        FluentActions.Invoking(() => new ConsoleHiddenInput(consoleKeyboard, consoleText, '*'))
            .Should().Throw<ArgumentNullException>().WithParameterName(nameof(consoleText));
    }

    [Fact]
    public void Ctor_should_check_null_consoleKeyboard()
    {
        IConsoleText consoleText = A.Fake<IConsoleText>();
        IConsoleKeyboard consoleKeyboard = null!;

        FluentActions.Invoking(() => new ConsoleHiddenInput(consoleKeyboard, consoleText, '*'))
            .Should().Throw<ArgumentNullException>().WithParameterName(nameof(consoleKeyboard));
    }

    [Fact]
    public void Ctor_should_accept_not_null_arguments()
    {
        IConsoleText consoleText = A.Fake<IConsoleText>();
        IConsoleKeyboard consoleKeyboard = A.Fake<IConsoleKeyboard>();

        FluentActions.Invoking(() => new ConsoleHiddenInput(consoleKeyboard, consoleText, '*'))
            .Should().NotThrow();
    }

}

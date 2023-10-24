using FkThat.Console;

namespace FkThat.SecretInput.Tests;

public class SecretConsoleReaderTests
{
    [Fact]
    public void Ctor_should_check_null_consoleText()
    {
        IConsoleText consoleText = null!;
        IConsoleKeyboard consoleKeyboard = A.Fake<IConsoleKeyboard>();

        FluentActions.Invoking(() => new SecretConsoleReader(consoleText, consoleKeyboard))
            .Should().Throw<ArgumentNullException>().WithParameterName(nameof(consoleText));
    }

    [Fact]
    public void Ctor_should_check_null_consoleKeyboard()
    {
        IConsoleText consoleText = A.Fake<IConsoleText>();
        IConsoleKeyboard consoleKeyboard = null!;

        FluentActions.Invoking(() => new SecretConsoleReader(consoleText, consoleKeyboard))
            .Should().Throw<ArgumentNullException>().WithParameterName(nameof(consoleKeyboard));
    }

    [Fact]
    public void Ctor_should_accept_not_null_arguments()
    {
        IConsoleText consoleText = A.Fake<IConsoleText>();
        IConsoleKeyboard consoleKeyboard = A.Fake<IConsoleKeyboard>();

        FluentActions.Invoking(() => new SecretConsoleReader(consoleText, consoleKeyboard))
            .Should().NotThrow();
    }
}

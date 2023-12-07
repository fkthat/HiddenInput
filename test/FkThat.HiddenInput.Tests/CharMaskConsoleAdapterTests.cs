using FkThat.Console;

namespace FkThat.HiddenInput;

public class CharMaskConsoleAdapterTests
{
    [Fact]
    public void ExecuteCommand_char()
    {
        var console = A.Fake<IConsoleText>();
        var sw = new StringWriter();
        A.CallTo(() => console.Error).Returns(sw);

        CharMaskConsoleAdapter sut = new(console, '*');
        sut.ExecuteCommand(new CharConsoleCommand());

        sw.ToString().Should().Be("*");
    }

    [Fact]
    public void ExecuteCommand_backspace_empty()
    {
        var console = A.Fake<IConsoleText>();
        var sw = new StringWriter();
        A.CallTo(() => console.Error).Returns(sw);

        CharMaskConsoleAdapter sut = new(console, '*');
        sut.ExecuteCommand(new BackspaceConsoleCommand());

        A.CallTo(console).MustNotHaveHappened();
    }

    [Fact]
    public void ExecuteCommand_backspace_not_empty()
    {
        var console = A.Fake<IConsoleText>();
        var sw = new StringWriter();
        A.CallTo(() => console.Error).Returns(sw);

        CharMaskConsoleAdapter sut = new(console, '*');
        sut.ExecuteCommand(new CharConsoleCommand());
        sut.ExecuteCommand(new BackspaceConsoleCommand());

        sw.ToString().Should().Be("*\b \b");
    }

    [Fact]
    public void ExecuteCommand_erase()
    {
        var console = A.Fake<IConsoleText>();
        var sw = new StringWriter();
        A.CallTo(() => console.Error).Returns(sw);

        CharMaskConsoleAdapter sut = new(console, '*');
        sut.ExecuteCommand(new CharConsoleCommand());
        sut.ExecuteCommand(new CharConsoleCommand());
        sut.ExecuteCommand(new CharConsoleCommand());
        sut.ExecuteCommand(new EraseConsoleCommand());

        sw.ToString().Should().Be("***\b\b\b   \b\b\b");
    }

    [Fact]
    public void ExecuteCommand_newline()
    {
        var console = A.Fake<IConsoleText>();
        var sw = new StringWriter();
        A.CallTo(() => console.Error).Returns(sw);

        CharMaskConsoleAdapter sut = new(console, '*');
        sut.ExecuteCommand(new NewLineConsoleCommand());

        sw.ToString().Should().Be(Environment.NewLine);
    }

    [Fact]
    public void ExecuteCommand_unsupported()
    {
        var console = A.Fake<IConsoleText>();
        var sw = new StringWriter();
        A.CallTo(() => console.Error).Returns(sw);

        CharMaskConsoleAdapter sut = new(console, '*');

        sut.Invoking(s => s.ExecuteCommand(new UnsupportedConsoleCommand()))
            .Should().Throw<NotSupportedException>();
    }
}

sealed file record UnsupportedConsoleCommand() : ConsoleCommand;

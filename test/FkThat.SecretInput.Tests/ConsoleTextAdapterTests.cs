using FkThat.Console;

namespace FkThat.SecretInput.Tests;

public class ConsoleTextAdapterTests
{
    [Theory]
    [InlineData('*')]
    [InlineData('\0')]
    public void Handle_CharKeyboardCommand_should_write_char(char maskChar)
    {
        StringWriter sw = new();
        var consoleText = A.Fake<IConsoleText>();
        A.CallTo(() => consoleText.Out).Returns(sw);

        ConsoleTextAdapter sut = new(consoleText, maskChar);
        sut.Handle(new CharKeyboardCommand('Z'));

        if (maskChar != '\0')
        {
            sw.ToString().Should().Be($"{maskChar}");
        }
        else
        {
            sw.ToString().Should().BeEmpty();
        }
    }

    [Theory]
    [InlineData('*')]
    [InlineData('\0')]
    public void Handle_BackspaceKeyboardCommand_should_backspace_when_not_empty(char maskChar)
    {
        StringWriter sw = new();
        var consoleText = A.Fake<IConsoleText>();
        A.CallTo(() => consoleText.Out).Returns(sw);

        ConsoleTextAdapter sut = new(consoleText, maskChar);
        sut.Handle(new CharKeyboardCommand('F'));
        sut.Handle(new BackspaceKeyboardCommand());

        if (maskChar != '\0')
        {
            sw.ToString().Should().Be($"{maskChar}\b \b");
        }
        else
        {
            sw.ToString().Should().BeEmpty();
        }
    }

    [Theory]
    [InlineData('*')]
    [InlineData('\0')]
    public void Handle_BackspaceKeyboardCommand_should_donothing_when_empty(char maskChar)
    {
        StringWriter sw = new();
        var consoleText = A.Fake<IConsoleText>();
        A.CallTo(() => consoleText.Out).Returns(sw);

        ConsoleTextAdapter sut = new(consoleText, maskChar);
        sut.Handle(new BackspaceKeyboardCommand());

        sw.ToString().Should().BeEmpty();
    }


    [Fact]
    public void Haandle_EnterKeyboardCommand_should_write_new_line()
    {
        StringWriter sw = new();
        var consoleText = A.Fake<IConsoleText>();
        A.CallTo(() => consoleText.Out).Returns(sw);

        ConsoleTextAdapter sut = new(consoleText, '*');
        sut.Handle(new EnterKeyboardCommand());

        sw.ToString().Should().Be($"{Environment.NewLine}");
    }
}

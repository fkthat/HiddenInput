using FkThat.Console;

namespace FkThat.HiddenInput.Tests;

public class HiddenConsoleReaderTests
{
    [Fact]
    public void Ctor_should_check_null_consoleText()
    {
        IConsoleText consoleText = null!;
        IConsoleKeyboard consoleKeyboard = A.Fake<IConsoleKeyboard>();

        FluentActions.Invoking(() => new HiddenConsoleReader(consoleText, consoleKeyboard))
            .Should().Throw<ArgumentNullException>().WithParameterName(nameof(consoleText));
    }

    [Fact]
    public void Ctor_should_check_null_consoleKeyboard()
    {
        IConsoleText consoleText = A.Fake<IConsoleText>();
        IConsoleKeyboard consoleKeyboard = null!;

        FluentActions.Invoking(() => new HiddenConsoleReader(consoleText, consoleKeyboard))
            .Should().Throw<ArgumentNullException>().WithParameterName(nameof(consoleKeyboard));
    }

    [Fact]
    public void Ctor_should_accept_not_null_arguments()
    {
        IConsoleText consoleText = A.Fake<IConsoleText>();
        IConsoleKeyboard consoleKeyboard = A.Fake<IConsoleKeyboard>();

        FluentActions.Invoking(() => new HiddenConsoleReader(consoleText, consoleKeyboard))
            .Should().NotThrow();
    }

    [Theory]
    [InlineData('*', "*\b \b********")]
    [InlineData('\0', "")]
    public void ReadLine_should_process_input(char maskChar, string expectedOutput)
    {
        var keyInfo = new[]
        {
            new ConsoleKeyInfo('P', ConsoleKey.P, shift:true, alt:false, control:false),
            new ConsoleKeyInfo('\0', ConsoleKey.Backspace, shift:false, alt:false, control:false),
            new ConsoleKeyInfo('\0', ConsoleKey.Backspace, shift:false, alt:false, control:false),
            new ConsoleKeyInfo('P', ConsoleKey.P, shift:true, alt:false, control:false),
            new ConsoleKeyInfo('@', ConsoleKey.D2, shift:true, alt:false, control:false),
            new ConsoleKeyInfo('s', ConsoleKey.S, shift:false, alt:false, control:false),
            new ConsoleKeyInfo('s', ConsoleKey.S, shift:false, alt:false, control:false),
            new ConsoleKeyInfo('w', ConsoleKey.W, shift:false, alt:false, control:false),
            new ConsoleKeyInfo('0', ConsoleKey.D0, shift:false, alt:false, control:false),
            new ConsoleKeyInfo('r', ConsoleKey.R, shift:false, alt:false, control:false),
            new ConsoleKeyInfo('d', ConsoleKey.D, shift:true, alt:false, control:false),
            new ConsoleKeyInfo('\0', ConsoleKey.LeftArrow, shift:false, alt:false, control:false),
            new ConsoleKeyInfo('\0', ConsoleKey.Enter, shift:false, alt:false, control:false)
        };

        var consoleKeyboard = A.Fake<IConsoleKeyboard>();

        var r = A.CallTo(() => consoleKeyboard.ReadKey(true)).Returns(keyInfo[0]).Once();

        for (int i = 1; i < keyInfo.Length; i++)
        {
            r = r.Then.Returns(keyInfo[i]).Once();
        }

        StringWriter stringWriter = new();
        var consoleText = A.Fake<IConsoleText>();
        A.CallTo(() => consoleText.Out).Returns(stringWriter);

        HiddenConsoleReader sut = new(consoleText, consoleKeyboard);
        var actual = sut.ReadLine(maskChar);

        actual.Should().Be("P@ssw0rd");
        stringWriter.ToString().Should().Be(expectedOutput + Environment.NewLine);
    }
}

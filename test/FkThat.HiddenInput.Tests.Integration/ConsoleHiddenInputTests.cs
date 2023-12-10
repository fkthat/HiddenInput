using FkThat.Console;

namespace FkThat.HiddenInput;

public class ConsoleHiddenInputTests
{
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

        for (var i = 1; i < keyInfo.Length; i++)
        {
            r = r.Then.Returns(keyInfo[i]).Once();
        }

        StringWriter stringWriter = new();
        var consoleText = A.Fake<IConsoleText>();
        A.CallTo(() => consoleText.Error).Returns(stringWriter);

        ConsoleHiddenInput sut = new(consoleKeyboard, consoleText);
        var actual = sut.ReadLine(maskChar);

        actual.Should().Be("P@ssw0rd");
        stringWriter.ToString().Should().Be(expectedOutput + Environment.NewLine);
    }
}

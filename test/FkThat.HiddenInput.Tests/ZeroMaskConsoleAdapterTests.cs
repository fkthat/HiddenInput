using FkThat.Console;

namespace FkThat.HiddenInput;

public class ZeroMaskConsoleAdapterTests
{
    [Fact]
    public void ExecuteCommand_newline()
    {
        var console = A.Fake<IConsoleText>();
        var sw = new StringWriter();
        A.CallTo(() => console.Error).Returns(sw);
        ZeroMaskConsoleAdapter sut = new(console);
        sut.ExecuteCommand(new NewLineConsoleCommand());
        sw.ToString().Should().Be(Environment.NewLine);
    }

    [Theory]
    [MemberData(nameof(IgnoredConsoleCommands))]
    public void ExecuteCommand_ignored(object command)
    {
        var console = A.Fake<IConsoleText>();
        var sw = new StringWriter();
        A.CallTo(() => console.Error).Returns(sw);
        ZeroMaskConsoleAdapter sut = new(console);
        sut.ExecuteCommand((ConsoleCommand)command);
        A.CallTo(console).MustNotHaveHappened();
    }

    public static IEnumerable<object[]> IgnoredConsoleCommands =>
        new object[][] {
            [new CharConsoleCommand()],
            [new BackspaceConsoleCommand()],
            [new EraseConsoleCommand()]
        };
}

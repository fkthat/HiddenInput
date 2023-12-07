namespace FkThat.HiddenInput;

public class StateMachineTests
{
    [Fact]
    public void Ctor_inits_data()
    {
        var console = A.Fake<IConsoleAdapter>();
        StateMachine sut = new(console, "foo");
        sut.Data.Should().Be("foo");
    }

    [Fact]
    public void ExecuteCommand_char()
    {
        var console = A.Fake<IConsoleAdapter>();

        StateMachine sut = new(console, "foo");
        sut.ExecuteCommand(new CharKeyboardCommand('F'));

        sut.Data.Should().Be("fooF");

        A.CallTo(() => console.ExecuteCommand(new CharConsoleCommand()))
            .MustHaveHappened(1, Times.Exactly);
    }

    [Fact]
    public void ExecuteCommand_backspace_empty()
    {
        var console = A.Fake<IConsoleAdapter>();

        StateMachine sut = new(console, "");
        sut.ExecuteCommand(new BackspaceKeyboardCommand());

        sut.Data.Should().Be("");
        A.CallTo(console).MustNotHaveHappened();
    }

    [Fact]
    public void ExecuteCommand_backspace_notempty()
    {
        var console = A.Fake<IConsoleAdapter>();

        StateMachine sut = new(console, "foo");
        sut.ExecuteCommand(new BackspaceKeyboardCommand());

        sut.Data.Should().Be("fo");

        A.CallTo(() => console.ExecuteCommand(new BackspaceConsoleCommand()))
            .MustHaveHappened(1, Times.Exactly);
    }

    [Fact]
    public void ExecuteCommand_erase()
    {
        var console = A.Fake<IConsoleAdapter>();

        StateMachine sut = new(console, "foo");
        sut.ExecuteCommand(new EraseKeyboardCommand());

        sut.Data.Should().Be("");

        A.CallTo(() => console.ExecuteCommand(new EraseConsoleCommand()))
            .MustHaveHappened(1, Times.Exactly);
    }

    [Fact]
    public void ExecuteCommand_newline()
    {
        var console = A.Fake<IConsoleAdapter>();

        StateMachine sut = new(console, "foo");
        sut.ExecuteCommand(new NewLineKeyboardCommand());

        sut.Data.Should().Be("foo");
        sut.IsFinished.Should().BeTrue();

        A.CallTo(() => console.ExecuteCommand(new NewLineConsoleCommand()))
            .MustHaveHappened(1, Times.Exactly);
    }

    [Fact]
    public void ExecuteCommand_nop()
    {
        var console = A.Fake<IConsoleAdapter>();

        StateMachine sut = new(console, "foo");
        sut.ExecuteCommand(new NopKeyboardCommand());

        sut.Data.Should().Be("foo");
        sut.IsFinished.Should().BeFalse();
        A.CallTo(console).MustNotHaveHappened();
    }

    [Fact]
    public void ExecuteCommand_unsupported()
    {
        var console = A.Fake<IConsoleAdapter>();

        StateMachine sut = new(console, "foo");

        sut.Invoking(s => s.ExecuteCommand(new UnsupportedKeyboardCommand()))
            .Should().Throw<NotSupportedException>();
    }
}

file record UnsupportedKeyboardCommand() : KeyboardCommand;

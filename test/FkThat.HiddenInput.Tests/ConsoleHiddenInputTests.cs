using FkThat.Console;

namespace FkThat.HiddenInput;

public class ConsoleHiddenInputTests
{
    [Fact]
    public void Ctor_checks_null_args()
    {
        var keyboard = A.Fake<IConsoleKeyboard>();
        var console = A.Fake<IConsoleText>();

        FluentActions.Invoking(() => new ConsoleHiddenInput(null!, console, '*'))
            .Should().Throw<ArgumentNullException>().WithParameterName(nameof(keyboard));

        FluentActions.Invoking(() => new ConsoleHiddenInput(keyboard, null!, '*'))
            .Should().Throw<ArgumentNullException>().WithParameterName(nameof(console));

        FluentActions.Invoking(() => new ConsoleHiddenInput(keyboard, console, '*'))
            .Should().NotThrow();

        FluentActions.Invoking(() => new ConsoleHiddenInput(keyboard, console, '\0'))
            .Should().NotThrow();
    }

    [Fact]
    public void ReadLine()
    {
        var keyboard = A.Fake<IKeyboardAdapter>();
        var factory = A.Fake<IStateMachineFactory>();
        var machine = A.Fake<IStateMachine>();
        A.CallTo(() => factory.CreateStateMachine()).Returns(machine);

        var commands = new KeyboardCommand[] {
            new CharKeyboardCommand('f'),
            new CharKeyboardCommand('o'),
            new CharKeyboardCommand('o'),
            new NewLineKeyboardCommand()
        };

        var r = A.CallTo(() => keyboard.ReadCommand())
            .Returns(commands.First()).Once();

        foreach (var cmd in commands.Skip(1))
        {
            r = r.Then.Returns(cmd).Once();
        }

        var finished = false;
        var foo = "";

        A.CallTo(() => machine.IsFinished).ReturnsLazily(() => finished);

        A.CallTo(() => machine.ExecuteCommand(A<KeyboardCommand>._))
            .Invokes((KeyboardCommand c) =>
            {
                if (c is CharKeyboardCommand cmd)
                {
                    foo += cmd.Char;
                    return;
                }

                if (c is NewLineKeyboardCommand)
                {
                    finished = true;
                    return;
                }
            });

        A.CallTo(() => machine.Data).ReturnsLazily(() => foo);

        ConsoleHiddenInput sut = new(keyboard, factory);
        var result = sut.ReadLine();

        result.Should().Be("foo");
    }
}

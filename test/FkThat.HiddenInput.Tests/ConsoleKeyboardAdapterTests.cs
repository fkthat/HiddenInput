using FkThat.Console;

namespace FkThat.HiddenInput.Tests;

public class ConsoleKeyboardAdapterTests
{
    [Fact]
    public void ReadKeyboardCommand_should_handle_char_key()
    {
        var consoleKeyboard = A.Fake<IConsoleKeyboard>();

        A.CallTo(() => consoleKeyboard.ReadKey(true)).Returns(
            new ConsoleKeyInfo('Q', ConsoleKey.Q, true, false, false));

        ConsoleKeyboardAdapter sut = new(consoleKeyboard);

        var actual = sut.ReadKeyboardCommand();

        actual.Should().BeOfType<CharKeyboardCommand>()
            .Which.Char.Should().Be('Q');
    }

    [Fact]
    public void ReadKeyboardCommand_should_handle_backspace_key()
    {
        var consoleKeyboard = A.Fake<IConsoleKeyboard>();

        A.CallTo(() => consoleKeyboard.ReadKey(true)).Returns(
            new ConsoleKeyInfo('\0', ConsoleKey.Backspace, false, false, false));

        ConsoleKeyboardAdapter sut = new(consoleKeyboard);

        var actual = sut.ReadKeyboardCommand();

        actual.Should().BeOfType<BackspaceKeyboardCommand>();
    }

    [Fact]
    public void ReadKeyboardCommand_should_handle_enter_key()
    {
        var consoleKeyboard = A.Fake<IConsoleKeyboard>();

        A.CallTo(() => consoleKeyboard.ReadKey(true)).Returns(
            new ConsoleKeyInfo('\0', ConsoleKey.Enter, false, false, false));

        ConsoleKeyboardAdapter sut = new(consoleKeyboard);

        var actual = sut.ReadKeyboardCommand();

        actual.Should().BeOfType<EnterKeyboardCommand>();
    }

    [Fact]
    public void ReadKeyboardCommand_should_ignore_special_keys()
    {
        var consoleKeyboard = A.Fake<IConsoleKeyboard>();

        A.CallTo(() => consoleKeyboard.ReadKey(true))
            .Returns(new ConsoleKeyInfo('\0', ConsoleKey.LeftArrow, false, false, false)).Once()
            .Then.Returns(new ConsoleKeyInfo('f', ConsoleKey.F, false, false, false));

        ConsoleKeyboardAdapter sut = new(consoleKeyboard);

        var actual = sut.ReadKeyboardCommand();

        actual.Should().BeOfType<CharKeyboardCommand>()
            .Which.Char.Should().Be('f');
    }
}

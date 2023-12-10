using FkThat.Console;

namespace FkThat.HiddenInput;

public class KeyboardAdapterTests
{
    [Theory]
    [MemberData(nameof(GetReadCases))]
    public void ReadCommand_reads(ConsoleKeyInfo keyInfo, object command)
    {
        var keyboard = A.Fake<IConsoleKeyboard>();
        A.CallTo(() => keyboard.ReadKey(true)).Returns(keyInfo);
        KeyboardAdapter sut = new(keyboard);
        var result = sut.ReadCommand();
        result.Should().Be(command);
    }

    [Theory]
    [MemberData(nameof(GetReadCasesNop))]
    public void ReadCommand_reads_undefined(ConsoleKeyInfo keyInfo)
    {
        var keyboard = A.Fake<IConsoleKeyboard>();
        A.CallTo(() => keyboard.ReadKey(true)).Returns(keyInfo);
        KeyboardAdapter sut = new(keyboard);
        var result = sut.ReadCommand();
        result.Should().BeOfType<NopKeyboardCommand>();
    }

    public static IEnumerable<object[]> GetReadCases() => new object[][] {
        // char
        [
            new ConsoleKeyInfo('f', ConsoleKey.F, false, false, false),
            new CharKeyboardCommand('f')
        ],
        // Shift + char
        [
            new ConsoleKeyInfo('F', ConsoleKey.F, true, false, false),
            new CharKeyboardCommand('F')
        ],
        // Backspace
        [
            new ConsoleKeyInfo('\0', ConsoleKey.Backspace, false, false, false),
            new BackspaceKeyboardCommand()
        ],
        // Shift + Backspace
        [
            new ConsoleKeyInfo('\0', ConsoleKey.Backspace, true, false, false),
            new BackspaceKeyboardCommand()
        ],
        // Ctl + Backspace
        [
            new ConsoleKeyInfo('\0', ConsoleKey.Backspace, false, false, true),
            new EraseKeyboardCommand()
        ],
        // Escape
        [
            new ConsoleKeyInfo('\0', ConsoleKey.Escape, false, false, false),
            new EraseKeyboardCommand()
        ],
        // Enter
        [
            new ConsoleKeyInfo('\0', ConsoleKey.Enter, false, false, false),
            new NewLineKeyboardCommand()
        ],
    };

    public static IEnumerable<object[]> GetReadCasesNop() => new object[][] {
        // Tab
        [new ConsoleKeyInfo('\0', ConsoleKey.Tab, false, false, false)],
        // [new ConsoleKeyInfo('\0', ConsoleKey.Enter, false, false, false)],
    };
}

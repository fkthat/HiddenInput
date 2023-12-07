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
        // Alt + char
        [new ConsoleKeyInfo('\0', ConsoleKey.F, false, true, false)],
        // Ctl + char
        [new ConsoleKeyInfo('\0', ConsoleKey.F, false, false, true)],
        // Alt + backspace
        [new ConsoleKeyInfo('\0', ConsoleKey.Backspace, false, true, false)],
        // Shift + escape
        [new ConsoleKeyInfo('\0', ConsoleKey.Escape, true, false, false)],
        // Alt + escape
        [new ConsoleKeyInfo('\0', ConsoleKey.Escape, false, true, false)],
        // Ctl + escape
        [new ConsoleKeyInfo('\0', ConsoleKey.Escape, false, false, true)],
        // Shift + enter
        [new ConsoleKeyInfo('\0', ConsoleKey.Enter, true, false, false)],
        // Alt + enter
        [new ConsoleKeyInfo('\0', ConsoleKey.Enter, false, true, false)],
        // Ctl + enter
        [new ConsoleKeyInfo('\0', ConsoleKey.Enter, false, false, true)],
        // Tab (for instance
        [new ConsoleKeyInfo('\0', ConsoleKey.Tab, false, false, false)],
        // [new ConsoleKeyInfo('\0', ConsoleKey.Enter, false, false, false)],
    };
}

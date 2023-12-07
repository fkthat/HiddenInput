namespace FkThat.HiddenInput;

internal abstract record KeyboardCommand();

internal sealed record NopKeyboardCommand() : KeyboardCommand;

internal sealed record CharKeyboardCommand(char Char) : KeyboardCommand;

internal sealed record BackspaceKeyboardCommand() : KeyboardCommand;

internal sealed record EraseKeyboardCommand() : KeyboardCommand;

internal sealed record NewLineKeyboardCommand() : KeyboardCommand;

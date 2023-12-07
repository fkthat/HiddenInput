namespace FkThat.HiddenInput;

internal abstract record ConsoleCommand();

internal sealed record CharConsoleCommand() : ConsoleCommand;

internal sealed record BackspaceConsoleCommand() : ConsoleCommand;

internal sealed record EraseConsoleCommand() : ConsoleCommand;

internal sealed record NewLineConsoleCommand() : ConsoleCommand;

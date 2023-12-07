namespace FkThat.HiddenInput;

/// <summary>
/// Read text from the command line in the secret fascion.
/// </summary>
public interface IConsoleHiddenInput
{
    /// <summary>
    /// Read a line from the command line in the secret fascion.
    /// </summary>
    /// <returns>The line.</returns>
    string ReadLine();
}

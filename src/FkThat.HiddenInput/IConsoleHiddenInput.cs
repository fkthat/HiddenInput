namespace FkThat.HiddenInput;

/// <summary>
/// Read text from the command line in the secret fascion.
/// </summary>
public interface IConsoleHiddenInput
{
    /// <summary>
    /// Read a line from the command line in the secret fascion.
    /// </summary>
    /// <param name="mask">
    /// The char to mask the input. The null ('\0') char means the UNIX-like input.
    /// </param>
    /// <returns>The line.</returns>
    string ReadLine(char mask);
}

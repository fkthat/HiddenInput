namespace FkThat.SecretInput;

/// <summary>
/// Read text from the command line in the secret fashion.
/// </summary>
public interface ISecretConsoleReader
{
    /// <summary>
    /// Read a line from the command line in the secret fashion.
    /// </summary>
    /// <param name="maskChar">
    /// The char to mask the input. The null ('\0') char means the UNIX-like input.
    /// </param>
    /// <returns>The unmasked line.</returns>
    string ReadLine(char maskChar = '*');
}

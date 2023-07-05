﻿namespace FkThat.HiddenInput;

/// <summary>
/// Read text from the command line in the secret fashion.
/// </summary>
public interface IHiddenInput
{
    /// <summary>
    /// Read a line from the command line in the secret fashion.
    /// </summary>
    /// <returns>The line.</returns>
    string ReadLine();
}

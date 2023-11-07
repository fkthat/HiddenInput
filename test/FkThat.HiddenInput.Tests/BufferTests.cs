namespace FkThat.HiddenInput.Tests;

public class BufferTests
{
    [Fact]
    public void Handle_on_CharKeyboardCommand_should_add_char()
    {
        Buffer sut = new();
        sut.Handle(new CharKeyboardCommand('X'));
        sut.Content.Should().Be("X");
    }

    [Fact]
    public void Handle_on_empty_buffer_should_ignore_BackspaceKeyboardCommand()
    {
        Buffer sut = new();
        sut.Handle(new BackspaceKeyboardCommand());
        sut.Content.Should().BeEmpty();
    }

    [Fact]
    public void Handle_on_BackspaceKeyboardCommand_should_remove_last_char()
    {
        Buffer sut = new();
        sut.Handle(new CharKeyboardCommand('X'));
        sut.Handle(new BackspaceKeyboardCommand());
        sut.Content.Should().BeEmpty();
    }

    [Fact]
    public void Handle_on_should_ignore_EnterKeyboardCommand()
    {
        Buffer sut = new();
        sut.Handle(new EnterKeyboardCommand());
        sut.Content.Should().BeEmpty();
    }
}

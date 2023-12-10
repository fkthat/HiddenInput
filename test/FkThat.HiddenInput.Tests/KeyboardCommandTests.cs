namespace FkThat.HiddenInput;

public class CharKeyboardCommandTests
{
    [Fact]
    public void Ctor_initializes_properties()
    {
        CharKeyboardCommand sut = new('F');
        sut.Char.Should().Be('F');
    }
}

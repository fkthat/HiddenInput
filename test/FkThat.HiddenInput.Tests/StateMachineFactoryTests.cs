using FkThat.Console;

namespace FkThat.HiddenInput;

public class StateMachineFactoryTests
{
    [Theory]
    [InlineData('*')]
    [InlineData('\0')]
    public void CreateStateMachine_returns_new_state_machine(char mask)
    {
        var console = A.Fake<IConsoleText>();

        StateMachineFactory sut = new(console);
        var result = sut.CreateStateMachine(mask);

        result.Should().BeOfType<StateMachine>();
        result.IsFinished.Should().BeFalse();
        result.Data.Should().BeEmpty();
    }
}

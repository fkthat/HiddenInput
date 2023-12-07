namespace FkThat.HiddenInput;

public class StateMachineFactoryTests
{
    [Fact]
    public void CreateStateMachine_returns_new_state_machine()
    {
        var console = A.Fake<IConsoleAdapter>();

        StateMachineFactory sut = new(console);
        var result = sut.CreateStateMachine();

        result.Should().BeOfType<StateMachine>();
        result.IsFinished.Should().BeFalse();
        result.Data.Should().BeEmpty();
    }
}

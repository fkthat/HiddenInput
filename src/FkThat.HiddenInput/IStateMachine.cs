namespace FkThat.HiddenInput;

internal interface IStateMachine
{
    void ExecuteCommand(KeyboardCommand command);

    bool IsFinished { get; }

    string Data { get; }
}

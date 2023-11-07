namespace FkThat.HiddenInput;

internal interface IKeyboardCommandHandler
{
    void Handle(CharKeyboardCommand command);

    void Handle(BackspaceKeyboardCommand command);

    void Handle(EnterKeyboardCommand command);
}

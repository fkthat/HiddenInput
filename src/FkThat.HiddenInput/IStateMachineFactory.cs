﻿namespace FkThat.HiddenInput;

internal interface IStateMachineFactory
{
    IStateMachine CreateStateMachine(char mask);
}

﻿namespace SharedKernel.Domain;

public interface IBusinessRule
{
    bool IsBroken();
    string Message { get; }
}
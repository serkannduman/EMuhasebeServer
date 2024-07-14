﻿using MediatR;
using TS.Result;

namespace EMuhasebeServer.Application.Features.Auth.Login
{
    public sealed record LoginCommand(
        string EmailOrUserName,
        string Password) : IRequest<Result<LoginCommandResponse>>;
}

﻿using MediatR;
using TS.Result;

namespace EMuhasebeServer.Application.Features.Users.CreateUser;

public sealed record CreateUserCommand(
    string FirstName,
    string LastName,
    string UserName,
    string Email,
    string Password) : IRequest<Result<string>>;

using EMuhasebeServer.Domain.Entities;
using MediatR;
using TS.Result;

namespace EMuhasebeServer.Application.Features.Users.GetAllUsers;

public sealed record GetAllUsersQuery() :  IRequest<Result<List<AppUser>>>
{
}

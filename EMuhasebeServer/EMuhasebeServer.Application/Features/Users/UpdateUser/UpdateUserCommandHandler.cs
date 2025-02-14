﻿using AutoMapper;
using EMuhasebeServer.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace EMuhasebeServer.Application.Features.Users.UpdateUser;

internal sealed class UpdateUserCommandHandler(
    UserManager<AppUser> userManager,
    IMapper mapper) : IRequestHandler<UpdateUserCommand, Result<string>>
{
    public async Task<Result<string>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        AppUser? appUser = await userManager.FindByIdAsync(request.Id.ToString());
        bool isMailChanged = false;

        if (appUser is null)
            return Result<string>.Failure("Kullanıcı bulunamadı");

        if(appUser.UserName != request.UserName)
        {
            bool isUserNameExists = await userManager.Users.AnyAsync(x => x.UserName == request.UserName);

            if (isUserNameExists)
                return Result<string>.Failure("Bu kullanıcı adı daha önce kullanılmış");
        }

        if (appUser.Email != request.Email)
        {
            bool isEmailExists = await userManager.Users.AnyAsync(x => x.Email == request.Email);

            if (isEmailExists)
                return Result<string>.Failure("Bu mail daha önce kullanılmış");
            isMailChanged = true;
            appUser.EmailConfirmed = false;
        }

        mapper.Map(request,appUser);

        IdentityResult identityResult = await userManager.UpdateAsync(appUser);

        if (!identityResult.Succeeded)
            return Result<string>.Failure(identityResult.Errors.Select(x => x.Description).ToList());

        string token = await userManager.GeneratePasswordResetTokenAsync(appUser);

        identityResult = await userManager.ResetPasswordAsync(appUser, token,request.Password);

        if (!identityResult.Succeeded)
            return Result<string>.Failure(identityResult.Errors.Select(x => x.Description).ToList());

        if (isMailChanged)
        {
            //Tekrardan onay maili gönder
        }

        return "Kullanıcı başarıyla güncellendi";
    }
}

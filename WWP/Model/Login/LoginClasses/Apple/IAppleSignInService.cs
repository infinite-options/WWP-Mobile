﻿using System;
using System.Threading.Tasks;

namespace WWP.Model.Login.LoginClasses.Apple
{
    public interface IAppleSignInService
    {
        bool IsAvailable { get; }
        Task<AppleSignInCredentialState> GetCredentialStateAsync(string userId);
        Task<AppleAccount> SignInAsync();
    }
}

using System;
using System.Threading.Tasks;
using MarketPlace.DataLayer.DTOs.Account;
using MarketPlace.DataLayer.Entities.Account;
using Microsoft.AspNetCore.Http;

namespace MarketPlace.Application.Services.Interfaces
{
    public interface IUserService : IAsyncDisposable
    {
        #region account

        Task<RegisterUserResult> RegisterUser(RegisterUserDTO register);
        Task<bool> IsUserExistsByMobileNumber(string mobile);
        Task<LoginUserResult> GetUserForLogin(LoginUserDTO login);
        Task<User> GetUserByMobile(string mobile);
        Task<ForgotPasswordResult> RecoverUserPassword(ForgotPasswordDTO forgot);
        Task<bool> ActivateMobile(ActivateMobileDTO activate);
        Task<bool> ChangeUserPassword(ChangePasswordDTO changePass, long currentUserId);
        Task<EditUserProfileDTO> GetProfileForEdit(long userId);
        Task<User> GetUserById(long userId);
        Task<EditUserProfileResult> EditUserProfile(EditUserProfileDTO profile, long userId, IFormFile avatarImage);

        #endregion
    }
}
using ProductManagementSystem.Core.Domain.Models.ViewModels;
using ProductManagementSystem.Core.Domain.Models;
using RepositoryAndServices.Common;
using RepositoryAndServices.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryAndServices.Service.CustomService.UserService
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> repository;

        public UserService(IRepository<User> repository)
        {
            this.repository = repository;
        }

        public async Task<bool> DeleteAsync(int Id)
        {
            var deleteuser = await repository.GetByIDAsync(Id);
            if (deleteuser != null)
            {
                await repository.DeleteAsync(deleteuser);
                return true;
            }
            return false;
        }

        public async Task<User> FindAsync(Expression<Func<User, bool>> match)
        {
            return await repository.FindAsync(match);
        }

        public async Task<ICollection<UserViewModel>> GetAllAsync()
        {
            var getall = await repository.GetAllAsync();

            return getall.Select(x => new UserViewModel
            {
                Id = x.Id,
                UserName = x.UserName,
                UserEmail = x.UserEmail,
                UserPhoneNo = x.UserPhoneNo
            }).ToList();
        }

        public async Task<UserViewModel> GetByIDAsync(int Id)
        {
            var getid = await repository.GetByIDAsync(Id);
            if (getid == null)
            {
                return null;
            }

            return new UserViewModel
            {
                Id = getid.Id,
                UserName = getid.UserName,
                UserEmail = getid.UserEmail,
                UserPhoneNo = getid.UserPhoneNo
            };
        }

        public async Task<bool> InsertAsync(UserInsertModel userInsertModel)
        {
            if (userInsertModel == null)
            {
                return false;
            }
            else
            {
                User user = new()
                {

                    UserName = userInsertModel.UserName,
                    UserEmail = userInsertModel.UserEmail,
                    UserPassword = Encrypter.EncryptString(userInsertModel.UserPassword),
                    UserPhoneNo = userInsertModel.UserPhoneNo
                };
                return await repository.InsertAsync(user);
            }

        }

        public async Task<bool> UpdateAsync(UserUpdateModel userUpdateModel)
        {
            var userupdate = await repository.GetByIDAsync(userUpdateModel.Id);
            if (userupdate != null)
            {
                userupdate.Id = userUpdateModel.Id;
                userupdate.UserName = userUpdateModel.UserName;
                userupdate.UserEmail = userUpdateModel.UserEmail;
                userupdate.UserPhoneNo = userUpdateModel.UserPhoneNo;

                return await repository.UpdateAsync(userupdate);
            }
            else
            {
                return false;
            }
        }
    }
}

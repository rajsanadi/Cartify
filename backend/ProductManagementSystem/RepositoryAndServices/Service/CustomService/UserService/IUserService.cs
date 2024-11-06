using ProductManagementSystem.Core.Domain.Models.ViewModels;
using ProductManagementSystem.Core.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryAndServices.Service.CustomService.UserService
{
    public interface IUserService
    {
        Task<ICollection<UserViewModel>> GetAllAsync();

        Task<UserViewModel> GetByIDAsync(int Id);

        Task<bool> InsertAsync(UserInsertModel userInsertModel);

        Task<bool> UpdateAsync(UserUpdateModel userUpdateModel);

        Task<bool> DeleteAsync(int Id);

        Task<User> FindAsync(Expression<Func<User, bool>> match);
    }
}

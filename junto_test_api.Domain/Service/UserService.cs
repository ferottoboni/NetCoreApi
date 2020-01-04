using AutoMapper;
using junto_test_api.Entity;
using junto_test_api.Entity.UnitOfWork;
using System.Linq;

namespace junto_test_api.Domain.Service
{

    public class UserService<TViewModel, TEntity> : GenericService<TViewModel, TEntity>
                                                where TViewModel : UserViewModel
                                                where TEntity : User
    {
        public UserService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            if (_unitOfWork == null)
                _unitOfWork = unitOfWork;
            if (_mapper == null)
                _mapper = mapper;
        }

        public bool ChangePassword(ChangePasswordViewModel newPass)
        {
            var repository = _unitOfWork.GetRepository<User>();
            var user = repository.Get(x => x.Email == newPass.Email && x.Password == newPass.OldPassword).FirstOrDefault();

            if (user == null)
                return false;


            user.Password = newPass.NewPassword;
            repository.Update(user.Id, user);
            _unitOfWork.Save();

            return true;
        }
    }

}

using AutoMapper;
using junto_test_api.Entity;
using junto_test_api.Entity.UnitOfWork;

namespace junto_test_api.Domain.Service
{

    public class AccountService<TViewModel, TEntity> : GenericService<TViewModel, TEntity>
                                        where TViewModel : AccountViewModel
                                        where TEntity : Account
    {
        public AccountService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            if (_unitOfWork == null)
                _unitOfWork = unitOfWork;
            if (_mapper == null)
                _mapper = mapper;
        }
    }

}

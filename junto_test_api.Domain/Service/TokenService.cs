using AutoMapper;
using junto_test_api.Entity;
using junto_test_api.Entity.UnitOfWork;
using System;
using System.Linq;

namespace junto_test_api.Domain.Service
{

    public class TokenService<TViewModel, TEntity> : GenericService<TViewModel, TEntity>
                                                where TViewModel : TokenViewModel
                                                where TEntity : Token
    {
        public TokenService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            if (_unitOfWork == null)
                _unitOfWork = unitOfWork;
            if (_mapper == null)
                _mapper = mapper;
        }

        public TokenViewModel CreateNewToken(CreateTokenViewModel createTokenViewModel)
        {
            var account = _unitOfWork.GetRepository<Account>().Get(x => x.PublicKey == createTokenViewModel.IntegrationKey).FirstOrDefault();

            if (account == null)
                return null;

            var rand = new Random();

            var token = new Token()
            {
                Account = account,
                AccountId = account.Id,
                Created = DateTime.UtcNow,
                Modified = DateTime.UtcNow,
                Key = DomainHelper.GetHashString(string.Concat(createTokenViewModel.IntegrationKey, rand.Next())),
                DueDate = DateTime.UtcNow.AddDays(1)
            };

            _unitOfWork.GetRepository<Token>().Insert(token);
            _unitOfWork.Save();

            return new TokenViewModel()
            {
                DueDate = token.DueDate,
                Id = token.Id,
                Key = token.Key
            };
        }

        public bool ValidateToken(string token)
        {
            return _unitOfWork.GetRepository<Token>().Get(x => x.Key == token && x.DueDate >= DateTime.UtcNow).Any();
        }

        public int GetAccountIdByToken(string token)
        {
            var validToken = _unitOfWork.GetRepository<Token>().Get(x => x.Key == token && x.DueDate >= DateTime.UtcNow).FirstOrDefault();
            return validToken.AccountId;
        }
    }

}

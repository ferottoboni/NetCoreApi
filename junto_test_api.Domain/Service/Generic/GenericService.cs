using AutoMapper;
using junto_test_api.Entity;
using junto_test_api.Entity.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace junto_test_api.Domain.Service
{
    public class GenericService<TViewModel, TEntity> : IService<TViewModel, TEntity> where TViewModel : BaseDomain
                                      where TEntity : BaseEntity
    {

        protected IUnitOfWork _unitOfWork;
        protected IMapper _mapper;
        public GenericService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public GenericService()
        {
        }

        public virtual IEnumerable<TViewModel> GetAll()
        {
            var entities = _unitOfWork.GetRepository<TEntity>()
            .GetAll();
            return _mapper.Map<IEnumerable<TViewModel>>(source: entities);
        }
        public virtual TViewModel GetOne(int id)
        {
            var entity = _unitOfWork.GetRepository<TEntity>()
                .GetOne(predicate: x => x.Id == id);
            return _mapper.Map<TViewModel>(source: entity);
        }

        public virtual int Add(TViewModel view)
        {
            var entity = _mapper.Map<TEntity>(source: view);
            _unitOfWork.GetRepository<TEntity>().Insert(entity);
            _unitOfWork.Save();
            return entity.Id;
        }

        public virtual int Update(TViewModel view)
        {
            _unitOfWork.GetRepository<TEntity>().Update(view.Id, _mapper.Map<TEntity>(source: view));
            return _unitOfWork.Save();
        }


        public virtual int Remove(int id)
        {
            TEntity entity = _unitOfWork.Context.Set<TEntity>().Find(id);
            _unitOfWork.GetRepository<TEntity>().Delete(entity);
            return _unitOfWork.Save();
        }

        public virtual IEnumerable<TViewModel> Get(Expression<Func<TEntity, bool>> predicate)
        {
            var entities = _unitOfWork.GetRepository<TEntity>()
                .Get(predicate: predicate);
            return _mapper.Map<IEnumerable<TViewModel>>(source: entities);
        }
    }
}

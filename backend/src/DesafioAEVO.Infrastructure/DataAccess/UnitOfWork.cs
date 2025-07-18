using DesafioAEVO.Domain.Abstractions.Repositories;

namespace DesafioAEVO.Infrastructure.DataAccess
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DesafioAEVOdbContext _dbcontext;
        public UnitOfWork(DesafioAEVOdbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }
        public async Task SaveChangesOnDBAsync() => await _dbcontext.SaveChangesAsync();
    }
}

namespace DesafioAEVO.Domain.Abstractions.Repositories
{
    public interface IUnitOfWork
    {
        public Task SaveChangesOnDBAsync();
    }
}

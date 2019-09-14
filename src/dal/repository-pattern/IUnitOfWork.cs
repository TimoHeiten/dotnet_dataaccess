using System.Threading.Tasks;

namespace dal.repositorypattern
{
    public interface IUnitOfWork
    {
        Task CommitAsync();
    }
}
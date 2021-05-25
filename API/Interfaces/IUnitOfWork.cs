using System.Threading.Tasks;

namespace API.Interfaces
{
    public interface IUnitOfWork
    {
        IUserRepository UserRepository {get;}
        IMessageRepository MessageRepository {get;}
        IFollowingRepository FollowingRepository {get;}
        Task<bool> Complete();
        bool HasChanges();
    }
}
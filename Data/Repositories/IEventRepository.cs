using Data.Entities;
using Data.Models;
using System.Linq.Expressions;

namespace Data.Repositories
{
    public interface IEventRepository : IBaseRepository<EventEntity>
    {
    }
}
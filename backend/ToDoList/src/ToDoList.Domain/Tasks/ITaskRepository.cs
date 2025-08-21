using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoList.Domain.Tasks
{
    public interface ITaskRepository
    {
        Task<ToDoTask?> GetById(Guid id, Guid userId);
        Task Add(ToDoTask task);
        Task<List<ToDoTask>> GetAll(Guid userId);
        void Update(ToDoTask task);
        void Delete(ToDoTask task);
    }
}
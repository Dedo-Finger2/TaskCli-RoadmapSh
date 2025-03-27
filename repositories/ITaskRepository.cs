using TaskCli.entities;

namespace TaskCli.repositories
{
    internal interface ITaskRepository
    {
        public int Create(string description);
        public int UpdateDescriptionById(int id, string newDescription);
        public void DeleteById(int id);
        public int UpdateStatusById(int id, entities.TaskStatus status);
        public List<TaskEntity> FindAll();
        public List<TaskEntity> FindAllByStatus(entities.TaskStatus status);
    }
}

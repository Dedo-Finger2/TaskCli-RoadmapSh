namespace TaskCli.entities
{
    internal class TaskEntity
    {
        public int id;
        public string description;
        public TaskStatus status;
        public DateTime createdAt;
        public DateTime? updatedAt;

        public TaskEntity(int id, string description)
        {
            this.id = id;
            this.description = description;
            this.status = TaskStatus.Todo;
            createdAt = DateTime.UtcNow;
        }
    }
}

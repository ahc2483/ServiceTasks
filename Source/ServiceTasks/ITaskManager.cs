namespace ServiceTasks
{
    public interface ITaskManager
    {
        /// <summary>
        /// Schedules tasks with Service Bus
        /// </summary>
        void ScheduleTasks();
    }
}

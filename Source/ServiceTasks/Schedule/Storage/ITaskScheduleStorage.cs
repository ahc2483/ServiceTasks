namespace ServiceTasks.Schedule.Storage
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public interface ITaskScheduleStorage
    {
        /// <summary>
        /// Gets a list of all scheduled task from the task storage
        /// </summary>
        /// <returns></returns>
        IList<ScheduledTask> GetSchedule();
    }
}

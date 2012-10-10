namespace ServiceTasks
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using log4net;
    using NServiceBus;
    using NServiceBus.Unicast;
    using ServiceTasks.Schedule;
    using ServiceTasks.Schedule.Storage;

    public class NServiceBusTaskManager : ITaskManager, IWantToRunWhenTheBusStarts
    {
        private const String ClassName = "NServiceBusTaskManager";
        private readonly IBus bus;
        private readonly ITaskScheduleStorage taskSchedule;
        private readonly ILog logger;

        public NServiceBusTaskManager(ITaskScheduleStorage schedule, IBus bus)
        {
            if (null == schedule)
                throw new ArgumentNullException("schedule");

            if (null == bus)
                throw new ArgumentNullException("bus");

            this.taskSchedule = schedule;
            this.bus = bus;
            this.logger = LogManager.GetLogger(ClassName);
        }

        #region ITaskManager Members
        
        public void ScheduleTasks()
        {
            IList<ScheduledTask> schedule = this.taskSchedule.GetSchedule();

            foreach (ScheduledTask task in schedule)
            {
                LogManager.GetLogger(ClassName).Info(
                    String.Format("Scheduling {0}: {1} will be sent every {2} minutes", task.Name, task.MessageType.Name, task.Interval));

                NServiceBus.Schedule.Every(TimeSpan.FromMinutes(task.Interval)).Action(() =>
                {
                    LogManager.GetLogger(ClassName).Info(String.Format("Executing {0} task", task.Name));

                    if (null == this.bus)
                        LogManager.GetLogger(ClassName).Warn("Bus is null");

                    var command = this.bus.CreateInstance(task.MessageType);

                    if (null == command)
                        LogManager.GetLogger(ClassName).Warn(String.Format("Creating message for type {0} failed", task.MessageType.Name));

                    this.bus.SendLocal(command);
                });
            }
        }

        #endregion

        #region IWantToRunWhenTheBusStarts Members

        public void Run()
        {
            this.ScheduleTasks();
        }

        #endregion
    }
}

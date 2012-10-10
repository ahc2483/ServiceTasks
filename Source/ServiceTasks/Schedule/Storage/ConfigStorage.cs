namespace ServiceTasks.Schedule.Storage
{
    using System;
    using System.Collections.Generic;
    using log4net;

    public class ConfigStorage : ITaskScheduleStorage
    {
        private readonly TaskScheduleConfiguration config;
        private const String ClassName = "ConfigStorage";
        private readonly ILog logger;

        public ConfigStorage()
        {
            this.config = TaskScheduleConfiguration.GetConfig();
            this.logger = LogManager.GetLogger(ClassName);
        }

        public IList<ScheduledTask> GetSchedule()
        {
            IList<ScheduledTask> schedule = new List<ScheduledTask>();

            if (config != null)
            {
                foreach (ScheduledTaskConfigurationElement configTask in config.ScheduledTasks)
                {
                    try
                    {
                        schedule.Add(new ScheduledTask()
                        {
                            Name = configTask.Name,
                            Interval = configTask.Interval,
                            MessageType = Type.GetType(configTask.MessageType, true)
                        });
                    }
                    catch (TypeLoadException tle)
                    {
                        logger.Warn(String.Format("Could not load scheduled task {0}: {1}", configTask.Name, tle.Message));
                    }
                }
            }

            return schedule;
        }
    }
}

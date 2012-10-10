namespace ServiceTasks.Schedule
{
    using System;

    public class ScheduledTask
    {
        public String Name { get; set; }
        public Type MessageType { get; set; }
        public Int32 Interval { get; set; }
    }
}

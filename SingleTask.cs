using System;

namespace WindowsFormsApp1
{
    class SingleTask
    {
        public TimeSpan TimeCreated;
        public String Task;
        public TimeSpan ScheduledTime;
        public SingleTask(TimeSpan time_created, string task)
        {
            this.TimeCreated = time_created;
            this.Task = task;
        }
        public SingleTask(TimeSpan time_created, string task, TimeSpan time)
        {
            this.TimeCreated = time_created;
            this.Task = task;
            this.ScheduledTime = time; 
        }
    }
}

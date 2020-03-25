using System;

namespace WindowsFormsApp1
{
    class SingleTask
    {
        public TimeSpan TimeCreated;
        public string Task;
        public TimeSpan TimeScheduled;
        public SingleTask(TimeSpan time_created, string task, TimeSpan time)
        {
            this.TimeCreated = time_created;
            this.Task = task;
            this.TimeScheduled = time; 
        }
    }
}

using System;
using System.Collections.Generic;

namespace WindowsFormsApp1
{
    class Task : IDisposable
    {
        private DateTime taskDateTime;
        private List<SingleTask> tasks;

        internal List<SingleTask> Tasks { get => tasks; set => tasks = value; }
        public DateTime TaskDateTime { get => taskDateTime; set => taskDateTime = value; }

        public Task(DateTime Date)
        {
            TaskDateTime = Date;
            Tasks = new List<SingleTask>();
            if (ENV.IsTaskFileExist(Date))
            {
                Tasks = ENV.GetTask(Date);
            }
            else
            {
                if(TaskDateTime.Date >= DateTime.Now.Date)
                    ENV.CreateTodaysTaskFile(TaskDateTime);
            }
        }
        public void AddTask(string Task,TimeSpan ScheduledTime)
        {
            if (IsConflictingTime(ScheduledTime))
            {
                throw new ConflictingScheduledTimeException();
            }
            if(Task == null)
            {
                throw new AddTaskWindowClosedException();
            }
            SingleTask CurrentSingleTask = new SingleTask(DateTime.Now.TimeOfDay, Task, ScheduledTime); //creating a new task
            ENV.AddToTodayTaskFile(TaskDateTime,CurrentSingleTask); // updating teh corresponding file with the current task
        }
        private bool IsConflictingTime(TimeSpan NewScheduledTime)
        {
            foreach(SingleTask EachTask in Tasks)
            {
                if (EachTask.ScheduledTime == NewScheduledTime)
                    return true;
            }
            return false;
        }
        public void DeleteTask(int Index)
        {
            Tasks.RemoveAt(Index - 1); //(index - 1) because indexing in list is 0 (zero) based
            ENV.UpdateTodayTaskFile(this);
        }
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
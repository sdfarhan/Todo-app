using System;
using System.Collections.Generic;
using System.IO;

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
            Tasks = ENV.GetTasks(Date);
        }
        public void AddTask(string Task,TimeSpan ScheduledTime)
        {
            if(Task == null)
            {
                throw new AddTaskWindowClosedException();
            }
            if (IsConflictingTime(ScheduledTime))
            {
                throw new ConflictingScheduledTimeException();
            }
            SingleTask CurrentSingleTask = new SingleTask(DateTime.Now.TimeOfDay, Task, ScheduledTime); //creating a new task
            ENV.AddToTaskFile(TaskDateTime,CurrentSingleTask); // updating teh corresponding file with the current task
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
        public static Task GetRequiredTasksObject(DateTime Date)
        {
            if (ENV.IsTaskFileExist(Date))
            {
                return new Task(Date);
            }
            ENV.CreateTaskFile(Date);
            return new Task(Date);
        }
        public void DeleteTask(int Index)
        {
            Tasks.RemoveAt(Index - 1); //(index - 1) because indexing in list is 0 (zero) based
            ENV.UpdateTaskFile(this);
        }
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
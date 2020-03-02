using System;
using System.Collections.Generic;

namespace WindowsFormsApp1
{
    class Task : IDisposable
    {
        public DateTime Date;
        public List<SingleTask> Tasks;
        public Task(DateTime date)
        {
            Date = date;
            Tasks = new List<SingleTask>();
            if (ENV.IsTaskFileExist(date))
            {
                Tasks = ENV.GetTask(date);
            }
            else
            {
                if(Date.Date >= DateTime.Now.Date)
                    ENV.CreateTodaysTaskFile(Date);
            }
        }
        public void AddTask(string task,TimeSpan ScheduledTime)
        {
            SingleTask CurrentSingleTask = new SingleTask(DateTime.Now.TimeOfDay, task, ScheduledTime);
            ENV.AddToTodayTaskFile(Date,CurrentSingleTask);
            this.Tasks.Add(CurrentSingleTask);
        }
        public void GetTaskFromFile()
        {
            Tasks = ENV.GetTask(Date);
        }
        public void deleteTask(int index)
        {
            Tasks.RemoveAt(index-1);
            ENV.UpdateTodayTaskFile(this);
        }
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
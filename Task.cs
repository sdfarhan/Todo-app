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
        public void AddTask(string task)
        {
            ENV.AddToTodayTaskFile(Date,task);
            this.Tasks.Add(new SingleTask(DateTime.Now.TimeOfDay, task));
        }
        public void GetTaskFromFile()
        {
            Tasks = ENV.GetTask(Date);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
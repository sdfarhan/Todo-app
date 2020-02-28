using System;
using System.Collections.Generic;

namespace WindowsFormsApp1
{
    class Task
    {
        public DateTime Date;
        public List<SingleTask> Tasks;
        public Task(DateTime date)
        {
            Date = date;
            Tasks = new List<SingleTask>();
            if (ENV.IsTodayTaskFileExist(date))
            {
                Tasks = ENV.GetTodayTaskFile(date);
            }
            else
            {
                if(Date.Date >= DateTime.Now.Date)
                    ENV.CreateTodaysTaskFile(Date);
            }
        }
        public void AddTask(DateTime Date,string task)
        {
            ENV.AddToTodayTaskFile(Date,task);
            this.Tasks.Add(new SingleTask(DateTime.Now.TimeOfDay, task));
        }
    }
}
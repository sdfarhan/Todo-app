using System;
using System.Diagnostics;

namespace WindowsFormsApp1
{
    class Todo
    {
        public Task TodayTasks,YesterdayTasks;
        public Todo()
        {
            TodayTasks = new Task(DateTime.Now);
            YesterdayTasks = new Task(TodayTasks.Date.AddDays(-1));
        } 
        public void ChangeDay()
        {
            YesterdayTasks = TodayTasks;
            TodayTasks = new Task(YesterdayTasks.Date.AddDays(1));
        }
    }
}
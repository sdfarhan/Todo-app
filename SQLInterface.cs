using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    class SQLInterface : IDisposable
    {
        public DateTime Date;
        public List<SingleTask> Tasks;
        public SQLInterface(DateTime Date)
        {
            this.Date = Date;
            using (MySql DatabaseHandler = new MySql())
            {
                Tasks = DatabaseHandler.Select(Date);
            }
        }
        public void AddTask(string Task, TimeSpan TimeScheduled)
        {
            if (Task == null || Task.Length == 0)
            {
                throw new WindowClosedException();
            }
            if (IsConflictingTime(TimeScheduled))
            {
                throw new ConflictingScheduledTimeException();
            }
            using (MySql DatabaseHandler = new MySql())
            {
                DatabaseHandler.Insert(new SingleTask(DateTime.Now.TimeOfDay,Task,TimeScheduled),DateChange.DateChangeOccur);
            }
        }
        public void DeleteTask(int Index)
        {
            using (MySql DatabaseHandler = new MySql())
            {
                try
                {
                    DatabaseHandler.Delete(Tasks[Index-1]);
                }
                catch (Exception)
                {
                    throw new IncorrectInputException(Tasks.Count.ToString());
                }
            }
        }
        private bool IsConflictingTime(TimeSpan NewScheduledTime)
        {
            foreach (SingleTask EachTask in Tasks)
            {
                if (EachTask.TimeScheduled == NewScheduledTime)
                    return true;
            }
            return false;
        }
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
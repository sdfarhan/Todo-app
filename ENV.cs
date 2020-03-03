using System;
using System.Collections.Generic;
using System.IO;

namespace WindowsFormsApp1
{
    class ENV
    {
        private static string TaskFilePath = @"C:\Users\SF185122\databases\todo-app\tasks\";
        private static string TaskFileExt = "-task.txt";
        private static string GetTaskFilePath(DateTime date)
        {
            return TaskFilePath + date.Month + date.Day + date.Year + TaskFileExt;
        }
        public static bool IsTaskFileExist(DateTime date)
        {
            string Path = GetTaskFilePath(date);
            return File.Exists(Path);
        }
        private static string GetPathAndThrowExeptionIfRequired(DateTime Date)
        {
            string Path = GetTaskFilePath(Date);
            if (!File.Exists(Path))
            {
                throw new FileNotFoundException();            
            }
            return Path;
        }
        public static void CreateTaskFile(DateTime date)
        {
            string Path = GetTaskFilePath(date);
            using (StreamWriter SW = new StreamWriter(Path))
            {
                File.SetAttributes(Path, File.GetAttributes(Path) & FileAttributes.ReadOnly);
                SW.WriteLine(date.ToShortDateString() + "  " + date.DayOfWeek);
                SW.WriteLine("TIME" + "TASK".PadLeft(20));
            }
        }
        public static void AddToTaskFile(DateTime date,SingleTask CurrentSingleTask)
        {
            string Path = GetPathAndThrowExeptionIfRequired(date);
            using (StreamWriter SW = new StreamWriter(Path,true))
            {
                SW.WriteLine(CurrentSingleTask.TimeCreated +" "+ CurrentSingleTask.Task+" AT "+CurrentSingleTask.ScheduledTime);
            }
        }
        public static List<SingleTask> GetTasks(DateTime Date)
        {
            string Path = GetPathAndThrowExeptionIfRequired(Date);
            List<SingleTask> AllTask = new List<SingleTask>();
            string CurrTask;
            using (StreamReader SR = new StreamReader(Path))
            {
                CurrTask = SR.ReadLine();
                CurrTask = SR.ReadLine();
                while ((CurrTask = SR.ReadLine()) != null)
                {
                    char[] seperator = { ' ' };
                    string[] tokenize = CurrTask.Split(seperator,2,StringSplitOptions.RemoveEmptyEntries);
                    if(tokenize.Length == 2)
                        AllTask.Add(new SingleTask(
                            TimeSpan.Parse(tokenize[0]),
                            tokenize[1],
                            TimeSpan.Parse(tokenize[1].Substring(tokenize[1].Length-8))
                            ));
                }
            }
            return AllTask;
        }
        public static void UpdateTaskFile(Task CurrenTask) 
        {
            DateTime Date = CurrenTask.TaskDateTime;
            string Path = GetPathAndThrowExeptionIfRequired(Date);
            CreateTaskFile(Date);
            using (StreamWriter SW = new StreamWriter(Path, true))
            {
                foreach(SingleTask EachTask in CurrenTask.Tasks)
                {
                    SW.WriteLine(
                        EachTask.TimeCreated.ToString() 
                        +" "
                        + EachTask.Task.PadLeft(15)
                        );
                }
            }
        }
    }
}
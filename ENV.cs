using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        public static void CreateTodaysTaskFile(DateTime date)
        {
            string path = GetTaskFilePath(date);
            using (StreamWriter sw = new StreamWriter(path))
            {
                sw.WriteLine(date.ToShortDateString() + "  " + date.DayOfWeek);
                sw.WriteLine("TIME" + "TASK".PadLeft(20));
            }
        }
        public static void AddToTodayTaskFile(DateTime date,SingleTask CurrentSingleTask)
        {
            string path = GetTaskFilePath(date);
            using (StreamWriter sw = new StreamWriter(path,true))
            {
                sw.WriteLine(CurrentSingleTask.TimeCreated +" "+ CurrentSingleTask.Task+" AT "+CurrentSingleTask.ScheduledTime);
            }
        }
        public static bool IsTaskFileExist(DateTime date)
        {
            string path = GetTaskFilePath(date);
            return File.Exists(path);
        }
        public static List<SingleTask> GetTask(DateTime date)
        {
            string path = GetTaskFilePath(date);
            List<SingleTask> AllTask = new List<SingleTask>();
            string CurrTask;
            using (StreamReader sw = new StreamReader(path))
            {
                CurrTask = sw.ReadLine();
                CurrTask = sw.ReadLine();
                while ((CurrTask = sw.ReadLine()) != null)
                {
                    char[] seperator = { ' ' };
                    string[] tokenize = CurrTask.Split(seperator,2,StringSplitOptions.RemoveEmptyEntries);
                    if(tokenize.Length == 2)
                        AllTask.Add(new SingleTask(TimeSpan.Parse(tokenize[0]), tokenize[1]));
                }
            }
            return AllTask;
        }
        public static void UpdateTodayTaskFile(Task todaytask) 
        {
            DateTime date = todaytask.Date;
            string path = GetTaskFilePath(date);
            using (StreamWriter sw = new StreamWriter(path, false))
            {
                sw.WriteLine(date.ToShortDateString() + "  " + date.DayOfWeek);
                sw.WriteLine("TIME" + "TASK".PadLeft(20));
                foreach(SingleTask EachTask in todaytask.Tasks)
                {
                    sw.WriteLine(EachTask.TimeCreated.ToString() + EachTask.Task.PadLeft(15));
                }
            }
        }
    }
}
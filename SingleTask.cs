using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    class SingleTask
    {
        public TimeSpan Time;
        public String Task;

        public SingleTask(TimeSpan time, string task)
        {
            this.Time = time;
            this.Task = task;
        }
    }
}

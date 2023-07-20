using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NewFrameTest
{
    public class ThreadPoll
    {
        public static void testthredpool()
        {
            List<ManualResetEvent> manualEvents = new List<ManualResetEvent>();

            for (int i = 0; i < 10; i++)
            {
                ManualResetEvent mre = new ManualResetEvent(false);
                manualEvents.Add(mre);
                ThreadPool.QueueUserWorkItem(ThreadMethod, mre);
            }
            WaitHandle.WaitAll(manualEvents.ToArray());//程序会在此处暂停，等待子线程运行结束。
        }
        public static void ThreadMethod(object obj)
        {
            ManualResetEvent mre = (ManualResetEvent)obj;
            ////处理逻辑

            mre.Set();
        }
    }

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace BackFiles_New.UserControls
{
    /// <summary>
    /// window 锁屏
    /// </summary>
    public static class MonitorWindowLockTools
    {
        public delegate void WindowLockEventHandler(SessionSwitchReason switchReason);
        static event WindowLockEventHandler windowlockevent;
        public static void AddWindowLockEvent(WindowLockEventHandler delegata)
        {
            windowlockevent -= delegata;
            windowlockevent += delegata;
        }
        public static void RemoveWindowLockEvent(WindowLockEventHandler delegata)
        {
            windowlockevent -= delegata;            
        }
        static MonitorWindowLockTools()
        {
            SystemEvents.SessionSwitch += new SessionSwitchEventHandler(SystemEvents_SessionSwitch);
        }

        static void SystemEvents_SessionSwitch(object sender, SessionSwitchEventArgs e)
        {
            if (windowlockevent != null)
                windowlockevent.Invoke(e.Reason);
            /*
            switch (e.Reason)
            {
                case SessionSwitchReason.SessionLogon:
                case SessionSwitchReason.SessionUnlock:
                    Console.WriteLine("解锁");
                    break;

                case SessionSwitchReason.SessionLock:
                case SessionSwitchReason.SessionLogoff:
                    Console.WriteLine("锁屏");
                    break;
            }
            */            
        }       
    }
}

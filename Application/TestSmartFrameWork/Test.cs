using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;

namespace TestSmartFrameWork
{
    public class Test
    {
        public bool test(string ss)
        {
            return true;// ss.IsNullOrEmptyOrBlank();
        }
        static Test()
        {
            ////throw new Exception("Error");
            var ss = "123";
            ss.PadLeft(4, '0');
        }
        public static int Pri1()
        {
            System.Console.WriteLine("父级打印1");
            return 0;
        }
        public static int Pri2()
        {
            System.Console.WriteLine("父级打印2");
            return 0;
        }
    }
    public  class testchild : Test
    {
        public static void t()
        {
            Pri1();
            Pri2();
        }
        public new static int Pri1()
        {
            System.Console.WriteLine("子级打印1");
            return 1;
        }       
    }
    public interface iT
    {

    }

}

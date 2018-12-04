using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackFiles_New.BLL
{
    public static class ExecuteCMDCommand
    {
        // <summary>
        /// 执行cmd命令方法
        /// </summary>
        /// <param name="commandQueue">执行cmd命令的输入队列</param>        
        public static void Cmd(Queue<string> commandQueue, string workingDirectory)
        {
            if (commandQueue == null || commandQueue.Count == 0)
                return;
            Process p = new Process();
            p.StartInfo.FileName = "cmd.exe";
            p.StartInfo.UseShellExecute = false;
            if (!string.IsNullOrEmpty(workingDirectory))
                p.StartInfo.WorkingDirectory = workingDirectory;
            p.StartInfo.RedirectStandardInput = true;
            p.StartInfo.RedirectStandardOutput = false;
            p.StartInfo.RedirectStandardError = false;
            p.StartInfo.CreateNoWindow = false;
            p.StartInfo.ErrorDialog = true;
            //p.StartInfo.StandardOutputEncoding = System.Text.Encoding.UTF8;
            p.Start();
            //p.StandardInput.AutoFlush = true;
            string command;
            while (!string.IsNullOrEmpty(command = commandQueue.Dequeue()))
            {
                p.StandardInput.WriteLine(command);
                if (commandQueue.Count == 0)
                {
                    command = null;
                    break;
                }
            }
            p.WaitForExit();  // 等待退出
        }
    }
}

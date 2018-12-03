using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;

namespace ExtendsToolsForm.BLL.CHMUtil
{
    public class ChmHelp
    {
        private string sourcePath;

        private StringBuilder hhcBody = new StringBuilder();

        private StringBuilder hhpBody = new StringBuilder();

        private StringBuilder hhkBody = new StringBuilder();

        private bool debug = true;

        public string ChmFileName
        {
            get;
            set;
        }

        public string Title
        {
            get;
            set;
        }

        public string SourcePath
        {
            get
            {
                return this.sourcePath;
            }
            set
            {
                this.sourcePath = Path.GetFullPath(value);
                if (!this.sourcePath.EndsWith("\\"))
                {
                    this.sourcePath += "\\";
                }
            }
        }

        public string DefaultPage
        {
            get;
            set;
        }

        private void Create(string path)
        {
            string[] files = Directory.GetFiles(path);
            string[] directories = Directory.GetDirectories(path);
            if (files.Length > 0 || directories.Length > 0)
            {
                this.hhcBody.AppendLine("\t<UL>");
            }
            string[] array = files;
            for (int i = 0; i < array.Length; i++)
            {
                string text = array[i];
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.AppendLine("\t<LI> <OBJECT type=\"text/sitemap\">");
                stringBuilder.AppendLine("\t\t<param name=\"Name\" value=\"{0}\">".Formats(Path.GetFileNameWithoutExtension(text)));
                stringBuilder.AppendLine("\t\t<param name=\"Local\" value=\"{0}\">".Formats(text.Replace(this.SourcePath, string.Empty)));
                stringBuilder.AppendLine("\t\t<param name=\"ImageNumber\" value=\"11\">");
                stringBuilder.AppendLine("\t\t</OBJECT>");
                this.hhpBody.AppendLine(text);
                this.hhcBody.Append(stringBuilder.ToString());
                this.hhkBody.Append(stringBuilder.ToString());
            }
            array = directories;
            for (int i = 0; i < array.Length; i++)
            {
                string path2 = array[i];
                this.hhcBody.AppendLine("\t<LI> <OBJECT type=\"text/sitemap\">");
                this.hhcBody.AppendLine("\t\t<param name=\"Name\" value=\"{0}\">".Formats(Path.GetFileName(path2)));
                this.hhcBody.AppendLine("\t\t<param name=\"ImageNumber\" value=\"1\">");
                this.hhcBody.AppendLine("\t\t</OBJECT>");
                this.Create(path2);
            }
            if (files.Length > 0 || directories.Length > 0)
            {
                this.hhcBody.AppendLine("\t</UL>");
            }
        }

        private void CreateHHC()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("<!DOCTYPE HTML PUBLIC \"-//IETF//DTD HTML//EN\">");
            stringBuilder.AppendLine("<HTML>");
            stringBuilder.AppendLine("<HEAD>");
            stringBuilder.AppendLine("<meta name=\"GENERATOR\" content=\"EasyCHM.exe  www.zipghost.com\">");
            stringBuilder.AppendLine("<!-- Sitemap 1.0 -->");
            stringBuilder.AppendLine("</HEAD><BODY>");
            stringBuilder.AppendLine("<OBJECT type=\"text/site properties\">");
            stringBuilder.AppendLine("\t<param name=\"ExWindow Styles\" value=\"0x200\">");
            stringBuilder.AppendLine("\t<param name=\"Window Styles\" value=\"0x800025\">");
            stringBuilder.AppendLine("\t<param name=\"Font\" value=\"MS Sans Serif,9,0\">");
            stringBuilder.AppendLine("</OBJECT>");
            stringBuilder.Append(this.hhcBody.ToString());
            stringBuilder.AppendLine("</BODY></HTML>");
            File.WriteAllText(Path.Combine(this.SourcePath, "chm.hhc"), stringBuilder.ToString(), Encoding.GetEncoding("gb2312"));
        }

        private void CreateHHP()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("[OPTIONS]");
            stringBuilder.AppendLine("CITATION=Made by mj");
            stringBuilder.AppendLine("Compatibility=1.1 or later");
            stringBuilder.AppendLine("Compiled file=" + this.ChmFileName);
            stringBuilder.AppendLine("Contents file=chm.HHC");
            stringBuilder.AppendLine("COPYRIGHT=www.lztkdr.com");
            stringBuilder.AppendLine("Default topic={1}");
            stringBuilder.AppendLine("Default Window=Main");
            stringBuilder.AppendLine("Display compile notes=Yes");
            stringBuilder.AppendLine("Display compile progress=Yes");
            stringBuilder.AppendLine("Full-text search=Yes");
            stringBuilder.AppendLine("Index file=chm.HHK");
            stringBuilder.AppendLine("Title={0}");
            stringBuilder.AppendLine("Enhanced decompilation=yes");
            stringBuilder.AppendLine();
            stringBuilder.AppendLine("[WINDOWS]");
            stringBuilder.AppendLine("Main=\"{0}\",\"chm.hhc\",\"chm.hhk\",\"{1}\",\"{1}\",,,,,0x63520,180,0x104E, [0,0,745,509],0x0,0x0,,,,,0");
            stringBuilder.AppendLine();
            stringBuilder.AppendLine("[MERGE FILES]");
            stringBuilder.AppendLine();
            stringBuilder.AppendLine("[FILES]");
            stringBuilder.Append(this.hhpBody.ToString());
            File.WriteAllText(Path.Combine(this.SourcePath, "chm.hhp"), stringBuilder.ToString().Formats(new object[]
            {
                this.Title,
                this.DefaultPage
            }), Encoding.GetEncoding("gb2312"));
        }

        private void CreateHHK()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("<!DOCTYPE HTML PUBLIC \"-//IETF//DTD HTML//EN\">");
            stringBuilder.AppendLine("<HTML>");
            stringBuilder.AppendLine("<HEAD>");
            stringBuilder.AppendLine("<meta name=\"GENERATOR\" content=\"EasyCHM.exe  www.zipghost.com\">");
            stringBuilder.AppendLine("<!-- Sitemap 1.0 -->");
            stringBuilder.AppendLine("</HEAD><BODY>");
            stringBuilder.AppendLine("<OBJECT type=\"text/site properties\">");
            stringBuilder.AppendLine("\t<param name=\"ExWindow Styles\" value=\"0x200\">");
            stringBuilder.AppendLine("\t<param name=\"Window Styles\" value=\"0x800025\">");
            stringBuilder.AppendLine("\t<param name=\"Font\" value=\"MS Sans Serif,9,0\">");
            stringBuilder.AppendLine("</OBJECT>");
            stringBuilder.AppendLine("<UL>");
            stringBuilder.Append(this.hhkBody.ToString());
            stringBuilder.AppendLine("</UL>");
            stringBuilder.AppendLine("</BODY></HTML>");
            File.WriteAllText(Path.Combine(this.SourcePath, "chm.hhk"), stringBuilder.ToString(), Encoding.GetEncoding("gb2312"));
        }

        public bool Compile()
        {
            this.Create(this.SourcePath);
            this.CreateHHC();
            this.CreateHHK();
            this.CreateHHP();
            string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles);
            string text = folderPath + "\\HTML Help Workshop\\hhc.exe";
            if (folderPath.IndexOf(" (x86)") <= -1)
            {
                if (!File.Exists(text))
                {
                    if (Directory.Exists(folderPath + " (x86)"))
                    {
                        text = folderPath + " (x86)\\HTML Help Workshop\\hhc.exe";
                    }
                }
            }
            if (!File.Exists(text))
                text = AppDomain.CurrentDomain.BaseDirectory + "\\CHMWorkshop\\hhc.exe";

            if (!File.Exists(text))
            {
                MessageBox.Show("未安装HTML Help Workshop！", "提示");
            }
            Process process = new Process();
            bool result;
            try
            {
                process.StartInfo = new ProcessStartInfo
                {
                    WindowStyle = ProcessWindowStyle.Hidden,
                    FileName = text,
                    Arguments = "\"{0}\"".Formats(new object[]
                    {
                        Path.Combine(this.SourcePath, "chm.hhp")
                    }),
                    UseShellExecute = false,
                    CreateNoWindow = true
                };
                process.Start();
                process.WaitForExit();
                if (process.ExitCode == 0)
                {
                    result = false;
                    return result;
                }
            }
            catch
            {
                result = false;
                return result;
            }
            finally
            {
                process.Close();
                if (!this.debug)
                {
                    string[] array = new string[]
                    {
                        "chm.hhc",
                        "chm.hhp",
                        "chm.hhk"
                    };
                    string[] array2 = array;
                    for (int i = 0; i < array2.Length; i++)
                    {
                        string path = array2[i];
                        string path2 = Path.Combine(this.SourcePath, path);
                        if (File.Exists(path2))
                        {
                            File.Delete(path2);
                        }
                    }
                }
            }
            result = true;
            return result;
        }

        public bool DeCompile()
        {
            string directoryName = Path.GetDirectoryName(this.ChmFileName);
            string arguments = " -decompile " + directoryName + " " + this.ChmFileName;
            Process process = Process.Start("hh.exe", arguments);
            process.WaitForExit();
            return true;
        }
    }
}

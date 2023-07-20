using CHMUtil;
using PdmModels;
using PdmUtil;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace PDMToCHM
{
	public class Form1 : Form
	{
		private static HashSet<string> lstPhs = new HashSet<string>();

		private static string title = string.Empty;

		private static IList<TableInfo> lstTabs = new List<TableInfo>();

		private IContainer components = null;

		private Button BtnBrow;

		private Label label1;

		private Button BtnBuild;

		private TextBox txtMulItem;

		private TextBox txtChmName;

		private Label label2;

		private Label labMsg;

		public Form1()
		{
			this.InitializeComponent();
			Control.CheckForIllegalCrossThreadCalls = false;
		}

		private void BtnBrow_Click(object sender, EventArgs e)
		{
			OpenFileDialog openFileDialog = new OpenFileDialog
			{
				FileName = "",
				Filter = "(*.pdm)|*.pdm",
				Multiselect = true
			};
			if (openFileDialog.ShowDialog() == DialogResult.OK)
			{
				this.txtMulItem.Text = string.Join("\r\n", openFileDialog.FileNames);
			}
		}

		private void BtnBuild_Click(object sender, EventArgs e)
		{
			string[] array = this.txtMulItem.Text.Split(new string[]
			{
				"\r\n"
			}, StringSplitOptions.RemoveEmptyEntries);
			string[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				string text = array2[i];
				string a = Path.GetExtension(text).ToLower();
				if (File.Exists(text) && a == ".pdm")
				{
					Form1.lstPhs.Add(text);
				}
			}
			this.txtMulItem.Text = string.Join("\r\n", Form1.lstPhs);
			Form1.title = this.txtChmName.Text.TrimEnd(new char[]
			{
				'.',
				'c',
				'h',
				'm',
				'C',
				'H',
				'M'
			});
			new Thread(new ParameterizedThreadStart(this.CrateCHM))
			{
				IsBackground = true
			}.Start(Form1.lstPhs.ToArray<string>());
		}

		private static IList<TableInfo> GetTables(params string[] pdmPaths)
		{
			List<TableInfo> list = new List<TableInfo>();
			PdmReader pdmReader = new PdmReader();
			for (int i = 0; i < pdmPaths.Length; i++)
			{
				string text = pdmPaths[i];
				if (File.Exists(text))
				{
					PdmUtil.PdmModels pdmModels = pdmReader.ReadFromFile(text);
					list.AddRange(pdmModels.Tables);
				}
			}
			return (from t in list
			orderby t.Code
			select t).ToList<TableInfo>();
		}

		private void CrateCHM(object phs)
		{
			try
			{
				string[] pdmPaths = phs as string[];
				Form1.lstTabs = Form1.GetTables(pdmPaths);
				string text = "数据库表目录.html";
				string fullPath = Path.GetFullPath("tmp");
				if (!Directory.Exists(fullPath))
				{
					Directory.CreateDirectory(fullPath);
				}
				else
				{
					Directory.Delete(fullPath, true);
					Directory.CreateDirectory(fullPath);
				}
				ChmHtmlHelper.CreateDirHtml("数据库表目录", Form1.lstTabs, Path.Combine(fullPath, text));
				fullPath = Path.GetFullPath("tmp\\表结构");
				if (!Directory.Exists(fullPath))
				{
					Directory.CreateDirectory(fullPath);
				}
				ChmHtmlHelper.CreateHtml(Form1.lstTabs, fullPath);
				ChmHelp chmHelp = new ChmHelp();
				chmHelp.DefaultPage = text;
				chmHelp.Title = Form1.title;
				chmHelp.ChmFileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), chmHelp.Title + ".chm");
				chmHelp.SourcePath = "tmp";
				chmHelp.Compile();
				this.SetMsg("生成成功！文件路径：" + chmHelp.ChmFileName, true);
			}
			catch (Exception ex)
			{
				this.SetMsg(ex.Message, false);
			}
		}

		private void Form1_Load(object sender, EventArgs e)
		{
		}

		private void SetMsg(string msg, bool isok)
		{
			this.labMsg.Text = msg;
			if (isok)
			{
				this.labMsg.ForeColor = Color.Green;
			}
			else
			{
				this.labMsg.ForeColor = Color.Red;
			}
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void InitializeComponent()
		{
			this.BtnBrow = new Button();
			this.label1 = new Label();
			this.BtnBuild = new Button();
			this.txtMulItem = new TextBox();
			this.txtChmName = new TextBox();
			this.label2 = new Label();
			this.labMsg = new Label();
			base.SuspendLayout();
			this.BtnBrow.Location = new Point(617, 21);
			this.BtnBrow.Name = "BtnBrow";
			this.BtnBrow.Size = new Size(99, 36);
			this.BtnBrow.TabIndex = 0;
			this.BtnBrow.Text = "浏览(可多选)";
			this.BtnBrow.UseVisualStyleBackColor = true;
			this.BtnBrow.Click += new EventHandler(this.BtnBrow_Click);
			this.label1.AutoSize = true;
			this.label1.Location = new Point(12, 33);
			this.label1.Name = "label1";
			this.label1.Size = new Size(89, 12);
			this.label1.TabIndex = 2;
			this.label1.Text = "Pdm 文件路径：";
			this.BtnBuild.Location = new Point(617, 96);
			this.BtnBuild.Name = "BtnBuild";
			this.BtnBuild.Size = new Size(99, 42);
			this.BtnBuild.TabIndex = 3;
			this.BtnBuild.Text = "生成";
			this.BtnBuild.UseVisualStyleBackColor = true;
			this.BtnBuild.Click += new EventHandler(this.BtnBuild_Click);
			this.txtMulItem.Location = new Point(107, 9);
			this.txtMulItem.Multiline = true;
			this.txtMulItem.Name = "txtMulItem";
			this.txtMulItem.Size = new Size(486, 57);
			this.txtMulItem.TabIndex = 4;
			this.txtChmName.Location = new Point(107, 111);
			this.txtChmName.Name = "txtChmName";
			this.txtChmName.Size = new Size(486, 21);
			this.txtChmName.TabIndex = 5;
			this.label2.AutoSize = true;
			this.label2.Location = new Point(12, 111);
			this.label2.Name = "label2";
			this.label2.Size = new Size(83, 12);
			this.label2.TabIndex = 6;
			this.label2.Text = "Chm文件名称：";
			this.labMsg.AutoSize = true;
			this.labMsg.Font = new Font("宋体", 7.5f, FontStyle.Bold, GraphicsUnit.Point, 134);
			this.labMsg.Location = new Point(14, 165);
			this.labMsg.Name = "labMsg";
			this.labMsg.Size = new Size(0, 10);
			this.labMsg.TabIndex = 7;
			base.AutoScaleDimensions = new SizeF(6f, 12f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.ClientSize = new Size(757, 183);
			base.Controls.Add(this.labMsg);
			base.Controls.Add(this.label2);
			base.Controls.Add(this.txtChmName);
			base.Controls.Add(this.txtMulItem);
			base.Controls.Add(this.BtnBuild);
			base.Controls.Add(this.label1);
			base.Controls.Add(this.BtnBrow);
			base.FormBorderStyle = FormBorderStyle.Fixed3D;
			base.MaximizeBox = false;
			base.Name = "Form1";
			base.StartPosition = FormStartPosition.CenterScreen;
			this.Text = "Pdm To CHM";
			base.Load += new EventHandler(this.Form1_Load);
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}

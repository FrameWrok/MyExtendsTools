using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using Newtonsoft.Json;

namespace BackFiles_New.UserControls
{
    /// <summary>
    /// JsonStringFormaterControl.xaml 的交互逻辑
    /// </summary>
    public partial class JsonStringFormaterControl : UserControl
    {
        public JsonStringFormaterControl()
        {
            InitializeComponent();
        }
        public string test(string oldjson)
        {
            JsonSerializer serializer = new JsonSerializer();
            TextReader tr = new StringReader(oldjson);
            JsonTextReader jtr = new JsonTextReader(tr);
            object obj = serializer.Deserialize(jtr);
            if (obj != null)
            {
                StringWriter textWriter = new StringWriter();
                JsonTextWriter jsonWriter = new JsonTextWriter(textWriter)
                {
                    Formatting = Formatting.Indented,
                    Indentation = 4,
                    IndentChar = ' '
                };
                serializer.Serialize(jsonWriter, obj);
                return textWriter.ToString();
            }
            else
            {
                return oldjson;
            }

        }
    }
}

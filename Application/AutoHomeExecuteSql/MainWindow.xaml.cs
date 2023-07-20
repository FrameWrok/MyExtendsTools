using AutoHomeExecuteSql.Model;
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
using System.Configuration;
using System.Collections.ObjectModel;

namespace AutoHomeExecuteSql
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<ConnectionSettingModel> DataSourcesConnection { get; set; }
        public ObservableCollection<ExecuteTypeModel> DataSourcesExecuteType { get; set; }
        public ConnectionSettingModel CurrentConnection { get; set; }
        public ExecuteTypeModel CurrentExecuteType { get; set; }
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_ContentRendered(object sender, EventArgs e)
        {
            DataSourcesConnection = new ObservableCollection<ConnectionSettingModel>();
            foreach (ConnectionStringSettings conn in ConfigurationManager.ConnectionStrings)
            {
                if(conn.Name!="LocalSqlServer")
                DataSourcesConnection.Add(new ConnectionSettingModel() { ConnectionName = conn.Name, ConnectionString = conn.ConnectionString });
            }
            this.ddlDataSources.ItemsSource = DataSourcesConnection;

            DataSourcesExecuteType = new ObservableCollection<ExecuteTypeModel>();
            DataSourcesExecuteType.Add(new ExecuteTypeModel() { ExecuteType = "ExecuteDataSet" });
            DataSourcesExecuteType.Add(new ExecuteTypeModel() { ExecuteType = "ExecuteNonQuery" });
            DataSourcesExecuteType.Add(new ExecuteTypeModel() { ExecuteType = "ExecuteScalar" });
            this.ddlExecuteType.ItemsSource = DataSourcesExecuteType;
        }
    }
}

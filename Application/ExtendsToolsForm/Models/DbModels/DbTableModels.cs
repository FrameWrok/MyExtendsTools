using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;

namespace ExtendsToolsForm.Models.DbModels
{
    /// <summary>
    /// 数据库表模型
    /// </summary>
    public class DbTableModels : INotifyPropertyChanged
    {

        private string name;
        /// <summary>
        /// 表说明
        /// </summary>
        public string Description { get; set; }

        private bool isChecked;
        /// <summary>
        /// 表名称
        /// </summary>
        public string Name
        {
            get => name; set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }

        /// <summary>
        /// 是否选中
        /// </summary>
        public bool IsChecked { get => isChecked; set { isChecked = value; OnPropertyChanged("IsChecked"); } }
        private Visibility visibility = Visibility.Visible;//Hidden
        /// <summary>
        /// 是否显示"Visible";//Hidden
        /// </summary>
        public Visibility Visibility
        {
            get { return visibility; }
            set { visibility = value; OnPropertyChanged("Visibility"); }
        }


        protected internal virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        public event PropertyChangedEventHandler PropertyChanged;

        public string sqlconnection { get; set; }
    }
}

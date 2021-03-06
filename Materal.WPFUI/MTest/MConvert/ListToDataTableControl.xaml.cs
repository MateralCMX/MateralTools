﻿using System.Windows;
using System.Windows.Controls;

namespace Materal.WPFUI.MTest.MConvert
{
    /// <summary>
    /// ListToDataTableControl.xaml 的交互逻辑
    /// </summary>
    public partial class ListToDataTableControl : UserControl
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public ListToDataTableControl()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 空间模型对象
        /// </summary>
        private ListToDataTableControlModel _controlM;
        /// <summary>
        /// 控件加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            _controlM = new ListToDataTableControlModel();
            this.listDataGrid.ItemsSource = _controlM.ListUserM;
        }
        /// <summary>
        /// 按钮单击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ConvertBtn_Click(object sender, RoutedEventArgs e)
        {
            _controlM.ListToDataTable();
            dtDataGrid.ItemsSource = _controlM.DvUser;
        }
    }
}

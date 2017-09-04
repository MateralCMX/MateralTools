using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace Materal.WPFUI
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
        }
        #endregion
        #region 私有方法
        /// <summary>
        /// 获取自定义控件的名称
        /// </summary>
        /// <param name="uc">自定义控件对象</param>
        /// <returns>自定义控件名称</returns>
        private string GetCustomControlName(UserControl uc)
        {
            object[] objs = uc.GetType().GetCustomAttributes(typeof(CustomControlAttribute), true);
            string ucName = string.Empty;
            foreach (var item in objs)
            {
                ucName = ((CustomControlAttribute)item).Name;
            }
            return ucName;
        }
        /// <summary>
        /// 设置自定义控件默认属性
        /// </summary>
        /// <param name="uc">自定义控件对象</param>
        private void SetCustomControlDefaultProperty(UserControl uc)
        {
            Grid.SetRow(uc, 1);
            uc.VerticalAlignment = VerticalAlignment.Top;
        }
        /// <summary>
        /// 添加控件
        /// </summary>
        /// <param name="uc">要添加的控件</param>
        private void AddControl(UserControl uc)
        {
            RemoveControl();
            string ucName = GetCustomControlName(uc);
            SetLeftTxt(string.Format("正在载入 {0} ......", ucName));
            SetCustomControlDefaultProperty(uc);
            this.MainPanel.Children.Add(uc);
            SetRightTxt(ucName);
            SetLeftTxt("就绪");
        }
        /// <summary>
        /// 移除控件
        /// </summary>
        private void RemoveControl()
        {
            SetLeftTxt("正在返回主界面......");
            for (int i = 2; i < this.MainPanel.Children.Count; i++)
            {
                this.MainPanel.Children.RemoveAt(i);
            }
            SetRightTxt("主界面");
            SetLeftTxt("就绪");
        }
        #endregion
        #region 公有方法
        /// <summary>
        /// 设置左边提示文本
        /// </summary>
        /// <param name="message">要显示的信息</param>
        public void SetLeftTxt(string message)
        {
            this.LeftTxt.Text = message;
        }
        /// <summary>
        /// 设置右边提示文本
        /// </summary>
        /// <param name="message">要显示的信息</param>
        public void SetRightTxt(string message)
        {
            this.RightTxt.Text = message;
        }
        #endregion
        #region 事件处理
        /// <summary>
        /// 返回主界面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BackMainFormMenuItem_Click(object sender, RoutedEventArgs e)
        {
            RemoveControl();
        }
        /// <summary>
        /// 枚举测试工具
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MEnumMenuItem_Click(object sender, RoutedEventArgs e)
        {
            AddControl(new MTools.MEnum.MEnumControl());
        }
        #endregion
        private void MDataTableToListMenuItem_Click(object sender, RoutedEventArgs e)
        {
            AddControl(new MTools.MConvert.DataTableToListControl());
        }
        private void MListToDataTableMenuItem_Click(object sender, RoutedEventArgs e)
        {
            AddControl(new MTools.MConvert.ListToDataTableControl());
        }
    }
}

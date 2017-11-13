using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Materal.WPFUI.MTest.MEnum
{
    /// <summary>
    /// MEnumForm.xaml 的交互逻辑
    /// </summary>
    public partial class MEnumControl : UserControl
    {
        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        public MEnumControl()
        {
            InitializeComponent();
        }
        #endregion
        #region 成员
        /// <summary>
        /// 当前窗口模型对象
        /// </summary>
        private MEnumControlModel _controlM;
        #endregion
        #region 事件
        /// <summary>
        /// 空间加载完毕事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            _controlM = new MEnumControlModel();
            DataContext = _controlM;
        }
        #endregion
        /// <summary>
        /// 枚举下拉框选项更改事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EnumComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.enumTextBlock.Text = string.Format("当前选择项信息:\r\n实际值:{0}\r\n显示值:{1}\r\n类型:{2}", _controlM.SelectedAnimal.ToString(), _controlM.SelectedAnimalName, _controlM.SelectedAnimal.GetType().ToString());
        }
    }
}

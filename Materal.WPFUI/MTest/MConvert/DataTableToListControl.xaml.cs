using System.Windows;
using System.Windows.Controls;

namespace Materal.WPFUI.MTest.MConvert
{
    /// <summary>
    /// DataTableToListControl.xaml 的交互逻辑
    /// </summary>
    public partial class DataTableToListControl : UserControl
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public DataTableToListControl()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 空间模型对象
        /// </summary>
        private DataTableToListControlModel _controlM;
        /// <summary>
        /// 控件加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            _controlM = new DataTableToListControlModel();
            dtDataGrid.ItemsSource = _controlM.DvUser;
        }
        /// <summary>
        /// 按钮单击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ConvertBtn_Click(object sender, RoutedEventArgs e)
        {
            _controlM.DataTableToList();
            this.listDataGrid.ItemsSource = _controlM.ListUserM;
        }
    }
}

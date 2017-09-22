using MateralTools.MConvert;

namespace Materal.WPFUI.MTest.MConvert
{
    /// <summary>
    /// 数据表到List转换模型
    /// </summary>
    public class DataTableToListControlModel : MControlModel
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        public DataTableToListControlModel()
        {
            DtUser = UserModel.GetDefualtDataTable();
        }
        /// <summary>
        /// 列表转换为数据表
        /// </summary>
        public void DataTableToList()
        {
            ListUserM = ConvertManager.DataTableToList<UserModel>(DtUser);
        }
    }
}

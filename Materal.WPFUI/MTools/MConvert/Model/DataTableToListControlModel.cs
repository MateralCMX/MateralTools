using MateralTools.MConvert;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Materal.WPFUI.MTools.MConvert
{
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

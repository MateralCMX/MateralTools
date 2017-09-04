using MateralTools.MConvert;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Materal.WPFUI.MTools.MConvert
{
    /// <summary>
    /// List到数据表转换模型
    /// </summary>
    public class ListToDataTableControlModel : MControlModel
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        public ListToDataTableControlModel()
        {
            ListUserM = UserModel.GetDefualtList();
        }
        /// <summary>
        /// 列表转换为数据表
        /// </summary>
        public void ListToDataTable()
        {
            DtUser = ConvertManager.ListToDataTable(ListUserM);
        }
    }
}

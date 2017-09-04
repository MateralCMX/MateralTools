using MateralTools.MConvert;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Materal.WPFUI.MTools.MConvert
{
    /// <summary>
    /// 转换模型
    /// </summary>
    public class MControlModel
    {
        /// <summary>
        /// 用户列表
        /// </summary>
        private List<UserModel> _listUserM;
        /// <summary>
        /// 用户列表
        /// </summary>
        public List<UserModel> ListUserM { get => _listUserM; set => _listUserM = value; }
        /// <summary>
        /// 用户列表动态集合
        /// </summary>
        private ObservableCollection<UserModel> ObsUserM
        {
            get
            {
                return ConvertManager.ListToObservableCollection(ListUserM);
            }
        }
        /// <summary>
        /// 用户数据表
        /// </summary>
        private DataTable dtUser;
        /// <summary>
        /// 用户数据表
        /// </summary>
        public DataTable DtUser { get => dtUser; set => dtUser = value; }
        /// <summary>
        /// 用户数据视图
        /// </summary>
        public DataView DvUser
        {
            get
            {
                return DtUser?.DefaultView;
            }
        }
    }
}

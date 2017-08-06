using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Materal.WPFUI.MTools.MConvert
{
    public class UserModel
    {
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 年龄
        /// </summary>
        public int Age { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
        public UserModel() { }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="name">姓名</param>
        /// <param name="age">年龄</param>
        /// <param name="remark">备注</param>
        public UserModel(string name,int age,string remark)
        {
            Name = name;
            Age = age;
            Remark = remark;
        }
        /// <summary>
        /// 获得默认列表
        /// </summary>
        /// <returns>默认列表</returns>
        public static List<UserModel> GetDefualtList()
        {
            List<UserModel> listM = new List<UserModel>();
            listM.Add(new UserModel("Materal", 24, "0"));
            listM.Add(new UserModel("Matreal", 25, "1"));
            listM.Add(new UserModel("Materay", 26, "2"));
            listM.Add(new UserModel("Materao", 27, "3"));
            listM.Add(new UserModel("Materap", 28, "4"));
            return listM;
        }
        /// <summary>
        /// 获得默认数据表
        /// </summary>
        /// <returns>默认数据表</returns>
        public static DataTable GetDefualtDataTable()
        {
            DataTable dt = new DataTable();
            DataColumn dc;
            dc = new DataColumn(nameof(Name), typeof(string));
            dt.Columns.Add(dc);
            dc = new DataColumn(nameof(Age), typeof(int));
            dt.Columns.Add(dc);
            dc = new DataColumn(nameof(Remark), typeof(string));
            dt.Columns.Add(dc);
            DataRow dr;
            dr = dt.NewRow();
            dr[nameof(Name)] = "Materal";
            dr[nameof(Age)] = 24;
            dr[nameof(Remark)] = "0";
            dt.Rows.Add(dr);
            dr = dt.NewRow();
            dr[nameof(Name)] = "Matreal";
            dr[nameof(Age)] = 25;
            dr[nameof(Remark)] = "1";
            dt.Rows.Add(dr);
            dr = dt.NewRow();
            dr[nameof(Name)] = "Materay";
            dr[nameof(Age)] = 26;
            dr[nameof(Remark)] = "2";
            dt.Rows.Add(dr);
            dr = dt.NewRow();
            dr[nameof(Name)] = "Materao";
            dr[nameof(Age)] = 27;
            dr[nameof(Remark)] = "3";
            dt.Rows.Add(dr);
            dr = dt.NewRow();
            dr[nameof(Name)] = "Materap";
            dr[nameof(Age)] = 28;
            dr[nameof(Remark)] = "4";
            dt.Rows.Add(dr);
            return dt;
        }
    }
}

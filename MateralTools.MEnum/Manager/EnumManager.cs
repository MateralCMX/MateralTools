using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MateralTools.MEnum
{
    public class EnumManager
    {
        /// <summary>
        /// 获取枚举的显示名称
        /// </summary>
        /// <param name="enumM">枚举</param>
        /// <returns>显示名称</returns>
        public static string GetShowName(Enum enumM)
        {
            string name = string.Empty;
            Type enumType = enumM.GetType();
            FieldInfo fieldInfo = enumType.GetField(enumM.ToString());
            if (fieldInfo != null)
            {
                object[] attrs = fieldInfo.GetCustomAttributes(typeof(EnumShowNameAttribute), false);
                foreach (EnumShowNameAttribute attr in attrs)
                {
                    name = attr.ShowName;
                }
            }
            return name;
        }
        /// <summary>
        /// 获取枚举总数
        /// </summary>
        /// <param name="enumType">枚举类型</param>
        /// <returns></returns>
        public static int GetCount(Type enumType)
        {
            if (enumType.IsEnum)
            {
                return Enum.GetValues(enumType).Length;
            }
            else
            {
                throw new ApplicationException("该类型不是枚举类型");
            }
        }
        /// <summary>
        /// 获取枚举总数
        /// </summary>
        /// <param name="enumM">枚举</param>
        /// <returns>该枚举总数</returns>
        public static int GetCount(Enum enumM)
        {
            return GetCount(enumM.GetType());
        }
    }
}

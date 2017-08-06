﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MateralTools.MEnum
{
    /// <summary>
    /// 枚举模型组(动态集合)
    /// </summary>
    public class EnumsObservableModel : ObservableCollection<EnumModel>
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        public EnumsObservableModel()
        {
        }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="enumType">枚举类型</param>
        public EnumsObservableModel(Type enumType)
        {
            if (enumType.IsEnum)
            {
                Array allEnums = Enum.GetValues(enumType);
                EnumModel enumM;
                foreach (object item in allEnums)
                {
                    enumM = new EnumModel((Enum)item);
                    Add(enumM);
                }
            }
            else
            {
                throw new ApplicationException("该类型不是枚举类型。");
            }
        }
    }
    /// <summary>
    /// 枚举模型组
    /// </summary>
    public class EnumsModel : CollectionBase
    {
        public void Add(EnumModel enumM)
        {
            List.Add(enumM);
        }
        public void Remove(EnumModel enumM)
        {
            List.Remove(enumM);
        }
        /// <summary>
        /// 构造方法
        /// </summary>
        public EnumsModel()
        {
        }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="enumType">枚举类型</param>
        public EnumsModel(Type enumType)
        {
            if (enumType.IsEnum)
            {
                Array allEnums = Enum.GetValues(enumType);
                EnumModel enumM;
                foreach (object item in allEnums)
                {
                    enumM = new EnumModel((Enum)item);
                    Add(enumM);
                }
            }
            else
            {
                throw new ApplicationException("该类型不是枚举类型。");
            }
        }
    }
}

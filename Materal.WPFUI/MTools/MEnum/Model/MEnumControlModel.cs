using MateralTools.MEnum;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Materal.WPFUI.MTools.MEnum
{
    /// <summary>
    /// 枚举窗口模型
    /// </summary>
    public class MEnumControlModel
    {
        /// <summary>
        /// 当前选择的动物
        /// </summary>
        private AnimalEnum _selectedAnimal;
        /// <summary>
        /// 当前选择的动物
        /// </summary>
        public AnimalEnum SelectedAnimal
        {
            get
            {
                return _selectedAnimal;
            }
            set
            {
                _selectedAnimal = value;
                //OnPropertyChanged(nameof(SelectedAnimal));
            }
        }
        /// <summary>
        /// 当前选择的动物显示名称
        /// </summary>
        public string SelectedAnimalName
        {
            get
            {
                return EnumManager.GetShowName(_selectedAnimal);
            }
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        public MEnumControlModel()
        {
            SelectedAnimal = AnimalEnum.Cat;
        }
        ///// <summary>
        ///// 属性更改事件注册
        ///// </summary>
        //public event PropertyChangedEventHandler PropertyChanged;
        ///// <summary>
        ///// 属性更改事件
        ///// </summary>
        ///// <param name="propertyName">属性名称</param>
        //private void OnPropertyChanged(string propertyName)
        //{
        //    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        //}
    }
}

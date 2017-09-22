using MateralTools.MEnum;

namespace Materal.WPFUI.MTest.MEnum
{
    /// <summary>
    /// 动物枚举
    /// </summary>
    public enum AnimalEnum : int
    {
        /// <summary>
        /// 猫
        /// </summary>
        [EnumShowName("猫")]
        Cat=0,
        /// <summary>
        /// 狗
        /// </summary>
        [EnumShowName("狗")]
        Dog =1,
        /// <summary>
        /// 牛
        /// </summary>
        [EnumShowName("牛")]
        Cow =2,
        /// <summary>
        /// 猪
        /// </summary>
        [EnumShowName("猪")]
        Pig =3
    }
    /// <summary>
    /// 动物枚举绑定模型
    /// </summary>
    public class AnimalEnumBindModel : EnumsObservableModel
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        public AnimalEnumBindModel() : base(typeof(AnimalEnum))
        {
        }
    }
}

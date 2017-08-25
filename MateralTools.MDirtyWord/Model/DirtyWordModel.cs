using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MateralTools.MDirtyWord
{
    public class DirtyWordModel
    {
        /// <summary>
        /// 索引位置
        /// </summary>
        private int _index;
        /// <summary>
        /// 脏字
        /// </summary>
        private string _keyword;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="index">文本索引位置</param>
        /// <param name="keyword">找到的文本</param>
        public DirtyWordModel(int index, string keyword)
        {
            _index = index;
            _keyword = keyword;
        }
        /// <summary>
        /// 索引位置
        /// </summary>
        public int Index
        {
            get { return _index; }
        }
        /// <summary>
        /// 找到的文字
        /// </summary>
        public string Keyword
        {
            get { return _keyword; }
        }
        /// <summary>
        /// 初始对象
        /// </summary>
        public static DirtyWordModel Empty
        {
            get
            {
                return new DirtyWordModel(-1, "");
            }
        }
    }
}

using MateralTools.Base;
using MateralTools.MDataBase;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MateralTools.MDataBaseTests
{
    /// <summary>
    /// 用户模型
    /// </summary>
    [BsonIgnoreExtraElements]
    [TableModel("T_User","ID")]
    public class UserModel
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        public UserModel()
        {
            ID = ObjectId.GenerateNewId();
        }
        /// <summary>
        /// 唯一标识
        /// </summary>
        public ObjectId ID { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 年龄
        /// </summary>
        public int Age { get; set; }
    }
}

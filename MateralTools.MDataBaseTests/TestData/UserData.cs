using MateralTools.MDataBase.Manager;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MateralTools.MDataBaseTests
{
    public class UserData:MongoDBManager<UserModel>
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        public UserData() : base()
        {
            MongoDbConnectionStr = "mongodb://127.0.0.1:27017/test";
        }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="connectionStr">链接字符串</param>
        public UserData(string connectionStr) : base(connectionStr)
        {
        }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="connectionStr">链接字符串</param>
        /// <param name="collectionName ">表名</param>
        /// <param name="pkName">主键名称</param>
        public UserData(string connectionStr, string collectionName, string pkName) : base(connectionStr, collectionName, pkName)
        {
        }
        /// <summary>
        /// 根据用户名称获得用户信息
        /// </summary>
        /// <param name="Name">用户名称</param>
        /// <returns>用户信息</returns>
        public List<UserModel> GetUserInfoByName(string Name)
        {
            IMongoCollection<UserModel> mongoDBCollection = GetCollection();
            List<UserModel> listM = mongoDBCollection.Find(m => m.Name.Contains(Name)).ToList();
            return listM;
        }
    }
}

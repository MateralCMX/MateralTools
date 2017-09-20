using Microsoft.VisualStudio.TestTools.UnitTesting;
using MateralTools.MDataBase.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MateralTools.MDataBaseTests;

namespace MateralTools.MDataBase.Manager.Tests
{
    [TestClass()]
    public class MongoDBManagerTests
    {
        private UserData userDa = new UserData();
        [TestMethod()]
        public void AddTest()
        {
            UserModel userM = new UserModel() { Age = 24, Name = "Materal" };
            userDa.Add(userM);
        }

        [TestMethod()]
        public void UpdateTest()
        {
            List<UserModel> listM = userDa.GetAllInfo();
            if (listM.Count > 0)
            {
                listM[0].Name = "Materal";
                listM[0].Age = 25;
                userDa.Update(listM[0]);
            }
        }

        [TestMethod()]
        public void DeleteTest()
        {
            List<UserModel> listM = userDa.GetAllInfo();
            foreach (UserModel item in listM)
            {
                userDa.Delete(item);
            }
        }

        [TestMethod()]
        public void GetAllInfoTest()
        {
            List<UserModel> listM = userDa.GetAllInfo();
        }

        [TestMethod()]
        public void GetInfoByPKTest()
        {
            List<UserModel> listM = userDa.GetAllInfo();
            if (listM.Count > 0)
            {
                UserModel userM = userDa.GetInfoByPK(listM[0].ID);
            }
        }

        [TestMethod()]
        public void GetUserInfoByNameTest()
        {
            List<UserModel> listM = userDa.GetUserInfoByName("Materal");
        }
    }
}
﻿using MateralTools.Base;
using MateralTools.MConvert;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MateralTools.MDataBase
{
    public class SQLiteManager
    {
        /// <summary>
        /// 创建新的数据库
        /// </summary>
        /// <param name="name"></param>
        public static void CreateNewDataBase(string name)
        {
            SQLiteConnection.CreateFile(name);
        }
        /// <summary>
        /// 创建新的表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public static void CreateNewTable<T>(string ConStrName = null)
        {
            TSQLModel tsqlM = new TSQLModel();
            Type tType = typeof(T);
            TableModelAttribute[] tableMAtts = (TableModelAttribute[])tType.GetCustomAttributes(typeof(TableModelAttribute), false);
            tsqlM.SQLStr = string.Format("create table {0} (", tableMAtts[0].DBTableName);
            PropertyInfo[] props = tType.GetProperties();
            ColumnModelAttribute cma;
            foreach (PropertyInfo prop in props)
            {
                foreach (Attribute attr in Attribute.GetCustomAttributes(prop))
                {
                    if (attr.GetType() == typeof(ColumnModelAttribute))
                    {
                        cma = attr as ColumnModelAttribute;
                        tsqlM.SQLStr += string.Format("{0} {1}, ", cma.DBColumnName, cma.DBType);
                    }
                }
            }
            int SQLLength = tsqlM.SQLStr.Length;
            tsqlM.SQLStr = tsqlM.SQLStr.Remove(SQLLength - 2) + ")";
            ExecuteNonQuery(tsqlM.SQLStr, null, ConStrName);
        }
        /// <summary>
        /// 查询表
        /// </summary>
        /// <typeparam name="T">查询的类型</typeparam>
        /// <param name="sql">查询sql</param>
        /// <param name="cmt">命令类型</param>
        /// <param name="paras">参数</param>
        /// <param name="IsAttribut">拥有Atribut</param>
        /// <returns>查询结果</returns>
        private static List<List<T>> Selects<T>(string whereStr = null, T model = default(T), bool IsAttribut = false, string ConStrName = null)
        {
            TSQLModel tsqlM = TSQLManager.SelectTSQL<T>(whereStr, model);
            DataSet ds = SQLiteHelper.ExecuteDataSet(SQLiteHelper.GetConnection(ConStrName), tsqlM.SQLStr, tsqlM.GetSQLParameters<SQLiteParameter>());
            return ConvertManager.DataSetToList<T>(ds, IsAttribut);
        }
        /// <summary>
        /// 查询
        /// </summary>
        /// <typeparam name="T">查询的类型</typeparam>
        /// <param name="sql">查询sql</param>
        /// <param name="cmt">命令类型</param>
        /// <param name="paras">参数</param>
        /// <param name="IsAttribut">拥有Atribut</param>
        /// <returns>查询结果</returns>
        public static List<T> Select<T>(string whereStr = null, T model = default(T), bool IsAttribut = false, string ConStrName = null)
        {
            return Selects<T>(whereStr, model, IsAttribut, ConStrName).First();
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <typeparam name="T">要添加的类型</typeparam>
        /// <param name="model">要添加的实体</param>
        /// <returns>影响的行数</returns>
        public static int Insert<T>(T model, string ConStrName = null)
        {
            TSQLModel tsqlM = TSQLManager.InsertTSQL(model);
            return ExecuteNonQuery(tsqlM.SQLStr, tsqlM.GetSQLParameters<SQLiteParameter>(), ConStrName);
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <typeparam name="T">要修改的类型</typeparam>
        /// <param name="model">要修改的实体</param>
        /// <returns>影响的行数</returns>
        public static int Update<T>(T model, string ConStrName = null)
        {
            TSQLModel tsqlM = TSQLManager.UpdateTSQL(model);
            return ExecuteNonQuery(tsqlM.SQLStr, tsqlM.GetSQLParameters<SQLiteParameter>(), ConStrName);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <typeparam name="T">要删除的类型</typeparam>
        /// <param name="model">要删除的实体</param>
        /// <returns>影响的行数</returns>
        public static int Delete<T>(T model, string ConStrName = null)
        {
            TSQLModel tsqlM = TSQLManager.DeleteTSQL(model);
            return ExecuteNonQuery(tsqlM.SQLStr, tsqlM.GetSQLParameters<SQLiteParameter>(), ConStrName);
        }
        /// <summary>
        /// 执行非查询语句或存储过程
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <param name="paras">参数</param>
        /// <returns>影响的行数</returns>
        public static int ExecuteNonQuery(string sql, SQLiteParameter[] paras, string ConStrName = null)
        {
            return SQLiteHelper.ExecuteNonQuery(SQLiteHelper.GetConnection(ConStrName), sql, paras);
        }
    }
}

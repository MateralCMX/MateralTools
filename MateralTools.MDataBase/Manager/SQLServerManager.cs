﻿using MateralTools.Base;
using MateralTools.MConvert;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;

namespace MateralTools.MDataBase
{
    /// <summary>
    /// SQLServer操作管理器
    /// </summary>
    public class SQLServerManager
    {
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
            DataSet ds = SqlServerHelper.ExecuteDataset(SqlServerHelper.GetConnection(ConStrName), CommandType.Text, tsqlM.SQLStr, tsqlM.GetSQLParameters<SqlParameter>());
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
            return ExecuteNonQuery(tsqlM.SQLStr, tsqlM.GetSQLParameters<SqlParameter>(), ConStrName);
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
            return ExecuteNonQuery(tsqlM.SQLStr, tsqlM.GetSQLParameters<SqlParameter>(), ConStrName);
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
            return ExecuteNonQuery(tsqlM.SQLStr, tsqlM.GetSQLParameters<SqlParameter>(), ConStrName);
        }
        /// <summary>
        /// 执行非查询语句或存储过程
        /// </summary>
        /// <param name="cmt">命令类型</param>
        /// <param name="sql">SQL语句</param>
        /// <param name="paras">参数</param>
        /// <returns>影响的行数</returns>
        public static int ExecuteNonQuery(string sql, SqlParameter[] paras, string ConStrName = null)
        {
            return SqlServerHelper.ExecuteNonQuery(SqlServerHelper.GetConnection(ConStrName), CommandType.Text, sql, paras);
        }
    }
}
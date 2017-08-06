using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MateralTools.MDataBase
{
    /// <summary>
    /// TSQL模型
    /// </summary>
    public class TSQLModel
    {
        public string SQLStr { get; set; }
        public List<TSQLParameter> SQLParameters { get; set; }

        public SqlParameter[] GetMSSQLParameter()
        {
            List<SqlParameter> mssqlParameter = new List<SqlParameter>();
            foreach (TSQLParameter item in SQLParameters)
            {
                mssqlParameter.Add(new SqlParameter(item.ParameterName, item.Value));
            }
            return mssqlParameter.ToArray();
        }

        public T[] GetSQLParameters<T>()
        {
            List<T> listM = new List<T>();
            Type tType = typeof(T);
            Type[] pType = new Type[2];
            pType[0] = typeof(string);
            pType[1] = typeof(object);
            ConstructorInfo constructor = tType.GetConstructor(pType);
            if (constructor != null)
            {
                T model;
                object[] objs;
                foreach (TSQLParameter item in this.SQLParameters)
                {
                    objs = new object[2];
                    objs[0] = item.ParameterName;
                    objs[1] = item.Value;
                    model = (T)constructor.Invoke(objs);
                    listM.Add(model);
                }
            }
            return listM.ToArray();
        }
    }
    /// <summary>
    /// TSQL参数模型
    /// </summary>
    public class TSQLParameter
    {
        public string ParameterName { get; set; }
        public object Value { get; set; }
        public TSQLParameter(string parameterName, object value)
        {
            this.ParameterName = parameterName;
            this.Value = value;
        }
    }
}

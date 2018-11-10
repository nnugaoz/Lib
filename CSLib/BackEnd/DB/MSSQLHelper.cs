using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace CSLib
{
    public class MSSQLHelper
    {
        public string mConnString = "";

        public MSSQLHelper(String pConnString)
        {
            mConnString = pConnString;
        }

        #region 执行简单SQL语句

        // 执行单条SQL语句，返回影响的记录数
        public int ExecuteSql(string SQLString)
        {
            using (SqlConnection conn = new SqlConnection(mConnString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(SQLString, conn);

                return cmd.ExecuteNonQuery();
            }
        }

        // 执行多条SQL语句，实现数据库事务。
        public void ExecuteSqlTran(List<String> SQLStringList)
        {
            using (SqlConnection conn = new SqlConnection(mConnString))
            {
                conn.Open();
                SqlTransaction tx = conn.BeginTransaction();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.Transaction = tx;
                try
                {
                    foreach (string strsql in SQLStringList)
                    {
                        if (strsql.Trim().Length > 1)
                        {
                            cmd.CommandText = strsql;
                            cmd.ExecuteNonQuery();
                        }
                    }
                    tx.Commit();
                }
                catch (System.Data.SqlClient.SqlException ex)
                {
                    tx.Rollback();
                    throw ex;
                }
            }
        }

        // 执行查询语句，返回DataSet
        public DataSet Query(string SQLString)
        {
            using (SqlConnection conn = new SqlConnection(mConnString))
            {
                DataSet ds = new DataSet();
                conn.Open();
                SqlDataAdapter command = new SqlDataAdapter(SQLString, conn);
                command.Fill(ds, "ds");
                return ds;
            }
        }
        #endregion 执行简单SQL语句

        #region 执行带参数的SQL语句

        // 执行SQL语句，返回影响的记录数
        public int ExecuteSql(string SQLString, params SqlParameter[] cmdParms)
        {
            using (SqlConnection conn = new SqlConnection(mConnString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = SQLString;
                cmd.CommandType = CommandType.Text;
                if (cmdParms != null)
                {
                    foreach (SqlParameter lParm in cmdParms)
                        cmd.Parameters.Add(lParm);
                }
                return cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// 有序执行
        /// </summary>
        /// <param name="SQLStringList">多条SQL</param>
        public void ExecuteSqlTran_sort(Dictionary<string, object> SQLStringList)
        {
            using (SqlConnection conn = new SqlConnection(mConnString))
            {

                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                SqlCommand cmd = new SqlCommand();
                try
                {
                    foreach (KeyValuePair<string, object> item in SQLStringList)
                    {
                        cmd.Connection = conn;
                        cmd.CommandText = item.Key.ToString(); ;
                        cmd.CommandType = CommandType.Text;
                        SqlParameter[] cmdParms = (SqlParameter[])item.Value;
                        if (cmdParms != null)
                        {
                            foreach (SqlParameter lParm in cmdParms)
                                cmd.Parameters.Add(lParm);
                        }
                        cmd.ExecuteNonQuery();
                        cmd.Parameters.Clear();
                    }

                    trans.Commit();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    throw ex;
                }
            }
        }


        // 执行查询语句，返回DataSet
        public DataSet Query(string SQLString, params SqlParameter[] cmdParms)
        {
            using (SqlConnection conn = new SqlConnection(mConnString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = SQLString;
                cmd.CommandType = CommandType.Text;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds, "ds");
                return ds;
            }
        }

        #endregion 执行带参数的SQL语句

        #region 存储过程操作
        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="storedProcName">存储过程名</param>
        /// <param name="parameters">存储过程参数</param>
        /// <returns>SqlDataReader</returns>
        public SqlDataReader RunProcedure(string storedProcName, IDataParameter[] parameters)
        {
            SqlConnection connection = new SqlConnection(mConnString);
            SqlDataReader returnReader;
            connection.Open();
            SqlCommand command = BuildQueryCommand(connection, storedProcName, parameters);
            command.CommandType = CommandType.StoredProcedure;
            returnReader = command.ExecuteReader();
            return returnReader;
        }
        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="storedProcName">存储过程名</param>
        /// <param name="parameters">存储过程参数</param>
        /// <param name="tableName">DataSet结果中的表名</param>
        /// <returns>DataSet</returns>
        public DataSet RunProcedure(string storedProcName, IDataParameter[] parameters, string tableName)
        {
            using (SqlConnection connection = new SqlConnection(mConnString))
            {
                DataSet dataSet = new DataSet();
                connection.Open();
                SqlDataAdapter sqlDA = new SqlDataAdapter();
                sqlDA.SelectCommand = BuildQueryCommand(connection, storedProcName, parameters);
                sqlDA.Fill(dataSet, tableName);
                connection.Close();
                return dataSet;
            }
        }
        /// <summary>
        /// 构建 SqlCommand 对象(用来返回一个结果集，而不是一个整数值)
        /// </summary>
        /// <param name="connection">数据库连接</param>
        /// <param name="storedProcName">存储过程名</param>
        /// <param name="parameters">存储过程参数</param>
        /// <returns>SqlCommand</returns>
        private SqlCommand BuildQueryCommand(SqlConnection connection, string storedProcName, IDataParameter[] parameters)
        {
            SqlCommand command = new SqlCommand(storedProcName, connection);
            command.CommandType = CommandType.StoredProcedure;
            foreach (SqlParameter parameter in parameters)
            {
                command.Parameters.Add(parameter);
            }
            return command;
        }
        /// <summary>
        /// 执行存储过程，返回影响的行数
        /// </summary>
        /// <param name="storedProcName">存储过程名</param>
        /// <param name="parameters">存储过程参数</param>
        /// <param name="rowsAffected">影响的行数</param>
        /// <returns></returns>
        public int RunProcedure(string storedProcName, IDataParameter[] parameters, out int rowsAffected)
        {
            using (SqlConnection connection = new SqlConnection(mConnString))
            {
                int result;
                connection.Open();
                SqlCommand command = BuildIntCommand(connection, storedProcName, parameters);
                rowsAffected = command.ExecuteNonQuery();
                result = (int)command.Parameters["ReturnValue"].Value;
                //Connection.Close();
                return result;
            }
        }
        /// <summary>
        /// 创建 SqlCommand 对象实例(用来返回一个整数值)
        /// </summary>
        /// <param name="storedProcName">存储过程名</param>
        /// <param name="parameters">存储过程参数</param>
        /// <returns>SqlCommand 对象实例</returns>
        private SqlCommand BuildIntCommand(SqlConnection connection, string storedProcName, IDataParameter[] parameters)
        {
            SqlCommand command = BuildQueryCommand(connection, storedProcName, parameters);
            command.Parameters.Add(new SqlParameter("ReturnValue",
            SqlDbType.Int, 4, ParameterDirection.ReturnValue,
            false, 0, 0, string.Empty, DataRowVersion.Default, null));
            return command;
        }
        #endregion 存储过程操作
    }
}

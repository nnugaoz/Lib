using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSLib
{
    public class DBHelper
    {

        private static MSSQLHelper mMSSQLHelper = new MSSQLHelper();

        public static EXESQLRET GetDataTable(string sql, ref DataTable dt)
        {
            try
            {
                DataSet ds = new DataSet();
                ds = mMSSQLHelper.Query(sql);

                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    // 存在数据
                    dt = ds.Tables[0];
                    return EXESQLRET.SUCCESS;
                }
                else
                {
                    // 不存在数据
                    return EXESQLRET.NODATA;
                }
            }
            catch (Exception ex)
            {
                return EXESQLRET.ERROR;
            }
        }

        public static EXESQLRET ExecuteSQL(string sql)
        {
            try
            {
                int ret = mMSSQLHelper.ExecuteSql(sql);
                if (ret > 0)
                {
                    return EXESQLRET.SUCCESS;
                }
                else if (ret == 0)
                {
                    return EXESQLRET.NODATA;
                }
                else
                {
                    return EXESQLRET.ERROR;
                }
            }
            catch (Exception ex)
            {
                return EXESQLRET.ERROR;
            }
        }

    }
}

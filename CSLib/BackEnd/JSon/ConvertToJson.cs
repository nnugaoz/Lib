using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSLib.BackEnd.JSon
{
    public class ConvertToJson
    {

        public static String FromDataTable(DataTable pDT)
        {
            String lJsonStr = "";
            if (pDT != null && pDT.Rows.Count > 0)
            {
                lJsonStr = "[";
                foreach (DataRow lDR in pDT.Rows)
                {
                    lJsonStr += "{";
                    foreach (DataColumn lDC in pDT.Columns)
                    {
                        lJsonStr += "\"" + lDC.ColumnName + "\":\"" + lDR[lDC.ColumnName] + "\",";
                    }
                    if (lJsonStr.EndsWith(","))
                    {
                        lJsonStr = lJsonStr.Substring(0, lJsonStr.Length - 1);
                    }
                    lJsonStr += "},";
                }

                if (lJsonStr.EndsWith(","))
                {
                    lJsonStr = lJsonStr.Substring(0, lJsonStr.Length - 1);
                }
                lJsonStr += "]";
            }
            return lJsonStr;
        }
    }
}

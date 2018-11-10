using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSLib
{
    public class DataB2F
    {
        public int Status { get; set; }

        public string Message { get; set; }

        public Dictionary<String, DataB2F_D> DataDic { get; set; }

        public DataB2F()
        {
            Status = 1;
            Message = "";
            DataDic = new Dictionary<String, DataB2F_D>();
        }

        public string FormatToJsonString()
        {
            String lDataStr = "";
            lDataStr = JsonConvert.SerializeObject(this);
            return lDataStr;
        }
    }
}

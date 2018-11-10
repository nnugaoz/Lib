using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSLib
{
    public class DataB2F_D
    {
        public int Status { get; set; }

        public string Message { get; set; }

        public DataTable DTable { get; set; }

        public DataB2F_D()
        {
            Status = 1;
            Message = "";
        }
    }
}

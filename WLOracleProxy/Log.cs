using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WLOracleProxy
{
    class Log
    {
        public static void writeLog(string str)
        {
            EClient.OracleInterface oi = new EClient.OracleInterface();
            string strsql = String.Format("insert into proxylog(" +
            "recordtime," +
            "content"+
            ") values(" +
            "to_date('{0}','YYYY-MM-DD HH24:MI:SS')," +
            "'{1}'"+
            ")",DateTime.Now, str);
            oi.query(strsql);
            oi.closeConnect();
        }
    }
}

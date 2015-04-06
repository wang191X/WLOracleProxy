// **********************************************************************
//
// Copyright (c) 2003-2011 ZeroC, Inc. All rights reserved.
//
// This copy of Ice is licensed to you under the terms described in the
// ICE_LICENSE file included in this distribution.
//
// **********************************************************************

using Demo;
using System.Data;
using System.Xml;
using System.IO;

public class DBFuncI : DBFuncDisp_
{
    public override void queryOne(string strSql, Ice.Current current)
    {
        EClient.OracleInterface oi=new EClient.OracleInterface();
        oi.queryOne(strSql);
        oi.closeConnect();

        WLOracleProxy.Log.writeLog(strSql);
    }

    public override string queryDS(string strSql, Ice.Current current)
    {
        EClient.OracleInterface oi = new EClient.OracleInterface();
        DataSet ds = oi.queryDataset(strSql);
        string str2=ds.ToString();
        oi.closeConnect();
        string str = ds.GetXmlSchema();
        string str1 = ds.GetXml();

        strSql=strSql.Replace("'", "");
        WLOracleProxy.Log.writeLog(strSql);

        return str+str1;
    }

    public override int query(string strSql, Ice.Current current)
    {
        EClient.OracleInterface oi = new EClient.OracleInterface();
        int ret=oi.query(strSql);
        oi.closeConnect();

        WLOracleProxy.Log.writeLog(strSql);

        return ret;
    }

    public override int batchInsert(string data, Ice.Current current)
    {
        LineSpy.DAL.OOracleApi oa = new LineSpy.DAL.OOracleApi();
        string conStr = "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=127.0.0.1) (PORT=1521)))" +
                   "(CONNECT_DATA=(SERVICE_NAME=orcl)));Persist Security Info=False;User Id=eightyg; Password=123456";
        oa.conn(conStr);

        StringReader sr = new StringReader(data);
        DataSet ds = new DataSet();
        ds.ReadXml(sr);
        int ret=oa.batchInsert(ds.Tables[0]);
        oa.close();

        WLOracleProxy.Log.writeLog("batchinsert");

        return ret;
    }
    
    public override void shutdown(Ice.Current current)
    {
        System.Console.Out.WriteLine("Shutting down...");
        current.adapter.getCommunicator().shutdown();
    }
}

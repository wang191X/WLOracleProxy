using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.IO;

namespace Client
{
    public partial class MainFrm : Form
    {
        public MainFrm()
        {
            InitializeComponent();
        }

        private void btQueryDS_Click(object sender, EventArgs e)
        {
            int status = 0;
            Ice.Communicator ic = null;

            try
            {
                ic = Ice.Util.initialize();
                Ice.ObjectPrx obj = ic.stringToProxy("dbfunc:tcp -h 127.0.0.1 -p 10001");
                Demo.DBFuncPrx dbfunc = Demo.DBFuncPrxHelper.checkedCast(obj);

                if (dbfunc == null)
                {
                    throw new ApplicationException("Invalid proxy");
                }

                string strSql = "select * from TESTICE1";
                string str=dbfunc.queryDS(strSql);
                StringReader sr = new StringReader(str);
                DataSet ds = new DataSet();
                ds.ReadXml(sr);

                dataGridView1.DataSource = ds.Tables[0];
            }
            catch (Exception e1)
            {
                //Console.Error.WriteLine(e1);
                MessageBox.Show(e1.Message);
                status = 1;
            }

            if (ic != null)
            {
                try
                {
                    ic.destroy();
                }
                catch (Exception e1)
                {
                    Console.Error.WriteLine(e1);
                    status = 1;
                }
            }

            //Environment.Exit(status);
        }

        private void btBatchInsert_Click(object sender, EventArgs e)
        {
            int status = 0;
            Ice.Communicator ic = null;

            try
            {
                ic = Ice.Util.initialize();
                Ice.ObjectPrx obj = ic.stringToProxy("dbfunc:tcp -h 127.0.0.1 -p 10001");
                Demo.DBFuncPrx dbfunc = Demo.DBFuncPrxHelper.checkedCast(obj);

                if (dbfunc == null)
                {
                    throw new ApplicationException("Invalid proxy");
                }

                
                DataTable dt = new DataTable();
                dt.TableName = "testice1";
                DataColumn dc = null;

                dc = new DataColumn();
                dc.DataType = System.Type.GetType("System.String");
                dc.ColumnName = "name";
                dc.Unique = false;
                dt.Columns.Add(dc);

                dc = new DataColumn();
                dc.DataType = System.Type.GetType("System.Int32");
                dc.ColumnName = "id";
                dc.Unique = false;
                dt.Columns.Add(dc);

                string name = "tom";
                int id = 5;
                int i = 0;

                while (true)
                {
                    dt.Rows.Add(name, id);
                    i++;

                    if (i == 100)
                        break;
                }

                DataSet ds = new DataSet();
                ds.Tables.Add(dt);
                string xmlData=ds.GetXml();
                dbfunc.batchInsert(xmlData);
            }
            catch (Exception e1)
            {
                Console.Error.WriteLine(e1);
                status = 1;
            }

            if (ic != null)
            {
                try
                {
                    ic.destroy();
                }
                catch (Exception e1)
                {
                    Console.Error.WriteLine(e1);
                    status = 1;
                }
            }
        }
    }
}

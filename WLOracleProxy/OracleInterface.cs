using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OracleClient;
using System.Data;
using System.Windows.Forms;

namespace EClient
{
    class OracleInterface
    {
        private OracleConnection connection;

        public OracleInterface()
        {
            string conStr = "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=127.0.0.1) (PORT=1521)))" +
                    "(CONNECT_DATA=(SERVICE_NAME=orcl)));Persist Security Info=False;User Id=eightyg; Password=123456;Unicode=false";
            //string conStr = "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=11.39.109.249) (PORT=1521)))" +
            //        "(CONNECT_DATA=(SERVICE_NAME=rulesdb)));Persist Security Info=False;User Id=ruleadmin; Password=123456;Unicode=false";
            connection = new OracleConnection(conStr);

            try
            {
                connection.Open();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.ToString());
            }
        }

        public int closeConnect()
        {
            connection.Close();
            return 0;
        }

        /// <summary>
        /// 单条查询
        /// </summary>
        /// <param name="strSql"></param>
        /// <returns></returns>
        public int queryOne(string strSql)
        {
            try
            {
                OracleCommand command = connection.CreateCommand();
                command.CommandText = strSql;
                OracleDataReader reader = command.ExecuteReader();

                while(reader.Read())
                {
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.ToString());
            }

            return 0;
        }

        /// <summary>
        /// 查询返回数据集
        /// </summary>
        /// <param name="strSql"></param>
        /// <returns></returns>
        public DataSet queryDataset(string strSql)
        {
            DataSet ds=new DataSet();

            try
            {
                OracleCommand cmd = new OracleCommand(strSql, connection);
                OracleDataAdapter adpt = new OracleDataAdapter(cmd);
                adpt.Fill(ds);
            }
            catch (Exception err)
            {
                MessageBox.Show(err.ToString());
            }
            finally
            {
               // connection.Close();
            }

            return ds;
        }

        /// <summary>
        /// 插入或者修改等无返回的查询
        /// </summary>
        /// <param name="strSql"></param>
        /// <returns></returns>
        public int query(string strSql)
        {
            try
            {
                OracleCommand command = connection.CreateCommand();
                command.CommandText = strSql;
                command.CommandType = CommandType.Text;
                command.ExecuteNonQuery();
            }
            catch (Exception err)
            {
                //MessageBox.Show(err.ToString());
				return -1;
            }

            return 0;
        }

        /// <summary>
        /// 批量插入
        /// </summary>
        /// <returns></returns>
        public int batchInsert()
        {
            return 0;
        }
    }
}
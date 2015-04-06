using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;

namespace LineSpy.DAL
{
    public class OOracleApi
    {
        private OracleConnection oracleConnection;

        /// <summary>
        /// 打开连接
        /// </summary>
        /// <param name="conn"></param>
        /// <returns></returns>
        public int conn(string conn)
        {
            try
            {
                oracleConnection = new OracleConnection(conn);
                oracleConnection.Open();
            }
            catch (OracleException oe)
            {
                string err = oe.ToString();
                return -1;
            }

            return 0;
        }

        /// <summary>
        /// 关闭连接
        /// </summary>
        /// <returns></returns>
        public int close()
        {
            try
            {
                oracleConnection.Close();
                oracleConnection.Dispose();
            }
            catch (OracleException oe)
            {
                string err = oe.ToString();
                return -1;
            }

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
                // Create the OracleCommand
                OracleCommand cmd = new OracleCommand(strSql);
                cmd.Connection = oracleConnection;
                cmd.CommandType = CommandType.Text;

                // Execute command, create OracleDataReader object
                OracleDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    // output
                }

                // Clean up
                reader.Dispose();
                cmd.Dispose();
            }
            catch (OracleException oe)
            {
                string err = oe.ToString();
                return -1;
            }

            return 0;
        }

        /// <summary>
        /// 查询标量
        /// </summary>
        /// <param name="strSql"></param>
        /// <returns></returns>
        public object queryScalar(string strSql)
        {
            object sal = null;

            try
            {
                OracleCommand cmd = new OracleCommand(strSql, oracleConnection);
                sal = cmd.ExecuteScalar();

                // Clean up
                cmd.Dispose();
            }
            catch (OracleException oe)
            {
                string err = oe.ToString();
                return sal;
            }

            return sal;
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
                OracleCommand cmd = new OracleCommand(strSql, oracleConnection);
                int rowsUpdated = cmd.ExecuteNonQuery();

                if (rowsUpdated > 0)
                {
                }

                // Clean up
                cmd.Dispose();
            }
            catch (OracleException oe)
            {
                string err = oe.ToString();
                return -1;
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
            // Create and fill the DataSet
            DataSet dataset = new DataSet();

            try
            {
                // Create the adapter with the selectCommand txt and the
                // connection string
                OracleDataAdapter adapter = new OracleDataAdapter(strSql, oracleConnection);

                // Create the builder for the adapter to automatically generate
                // the Command when needed
                OracleCommandBuilder builder = new OracleCommandBuilder(adapter);
                
                adapter.Fill(dataset);
            }
            catch (OracleException oe)
            {
                string err = oe.ToString();
                return dataset;
            }

            return dataset;
        }

        /// <summary>
        /// 大批量入库处理
        /// </summary>
        /// <param name="dtData"></param>
        /// <returns></returns>
        public int batchInsert(DataTable dtData)
        {
            OracleBulkCopy bulkCopy = null;

            try
            {
                bulkCopy = new OracleBulkCopy(oracleConnection);
                bulkCopy.DestinationTableName = dtData.TableName;
                bulkCopy.WriteToServer(dtData);
            }
            catch (OracleException oe)
            {
                string err = oe.ToString();
                return -1;
            }

            return 0;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace HsrsIndia.Models
{
    public class Database
    {
        private SqlConnection CONDB = new SqlConnection();

        public void Connect()
        {
            try
            {
                if (CONDB.State != ConnectionState.Open)
                {
                    CONDB = new SqlConnection();
                    CONDB.ConnectionString = ConfigurationManager.ConnectionStrings["Connectionstringnamein"].ConnectionString;
                    if (CONDB.State == ConnectionState.Closed)
                    {
                        CONDB.Open();
                    }
                    else
                    {
                        CONDB.Close();
                        System.Threading.Thread.Sleep(10);
                        CONDB.Open();
                    }

                }
            }
            catch
            {
                CONDB.Close();
                CONDB.Dispose();
            }

        }

        public void Close()
        {
            try
            {
                if (CONDB.State == ConnectionState.Open)
                    CONDB.Close();
            }
            catch (Exception ex)
            {
                //#if DEBUG
                //                throw ex;
                //#endif
            }
            CONDB.Dispose();
        }

        public bool StoreprocedureExecuteQuery(string ProcedureName, string[,] parameters)
        {
            SqlCommand cmd = new SqlCommand(ProcedureName);
            bool ReturnValue = false;
            try
            {

                cmd.CommandType = CommandType.StoredProcedure;
                for (int i = 0; i < parameters.Length / 2; i++)
                {
                    cmd.Parameters.AddWithValue(parameters[i, 0], parameters[i, 1]);
                }
                Connect();
                cmd.Connection = CONDB;
                cmd.ExecuteNonQuery();
                ReturnValue = true;

            }
            catch
            {

            }
            Close();
            cmd.Dispose();
            return ReturnValue;

        }

        public string StoreprocedureExecuteQueryReturned(string ProcedureName, string[,] parameters)
        {
            try
            {
                SqlCommand cmd = new SqlCommand(ProcedureName);
                cmd.CommandType = CommandType.StoredProcedure;
                for (int i = 0; i < parameters.Length / 2; i++)
                {
                    cmd.Parameters.AddWithValue(parameters[i, 0], parameters[i, 1]);
                }
                Connect();
                cmd.Connection = CONDB;
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    string s = reader.GetValue(0).ToString();
                    if (reader.IsClosed == false)
                        reader.Close();
                    return s;
                }

                if (reader.IsClosed == false)
                    reader.Close();
                cmd.Dispose();
                return "";
            }
            catch
            {
                Close();
                return "";
            }


        }

        public DataTable StoreprocedureExecuteQueryDTReturned(string ProcedureName, string[,] parameters)
        {
            SqlCommand cmd = new SqlCommand(ProcedureName);
            DataTable dt = new DataTable();
            try
            {

                cmd.CommandType = CommandType.StoredProcedure;
                for (int i = 0; i < parameters.Length / 2; i++)
                {
                    cmd.Parameters.AddWithValue(parameters[i, 0], parameters[i, 1]);
                }
                SqlDataAdapter adp = new SqlDataAdapter();

                adp.SelectCommand = cmd;
                Connect();
                cmd.Connection = CONDB;

                adp.Fill(dt);

            }
            catch (Exception ex)
            {

            }

            Close();
            cmd.Dispose();
            return dt;


        }

        public DataSet StoreprocedureExecuteDsReturned(string ProcedureName, string[,] parameters)
        {
            SqlCommand cmd = new SqlCommand(ProcedureName);
            SqlDataAdapter adp = new SqlDataAdapter();
            DataSet ds = new DataSet();
            try
            {

                cmd.CommandType = CommandType.StoredProcedure;
                for (int i = 0; i < parameters.Length / 2; i++)
                {
                    cmd.Parameters.AddWithValue(parameters[i, 0], parameters[i, 1]);
                }


                Connect();
                cmd.Connection = CONDB;
                adp.SelectCommand = cmd;
                adp.Fill(ds);

            }
            catch
            {

            }
            Close();
            cmd.Dispose();
            adp.Dispose();
            return ds;


        }

        public object StoreprocedureExecuteScalar(string ProcedureName, string[,] parameters)
        {
            SqlCommand cmd = new SqlCommand(ProcedureName);
            try
            {

                cmd.CommandType = CommandType.StoredProcedure;
                for (int i = 0; i < parameters.Length / 2; i++)
                {
                    cmd.Parameters.AddWithValue(parameters[i, 0], parameters[i, 1]);
                }
                Connect();
                cmd.Connection = CONDB;
                object obj = cmd.ExecuteScalar();
                cmd.Dispose();
                Close();
                return obj;
            }
            catch
            {
                Close();
                cmd.Dispose();
                return 0;
            }
        }
    }
}
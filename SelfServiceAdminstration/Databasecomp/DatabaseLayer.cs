using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using System.Collections;
using System.Data;
using System.Configuration;
using System.Data.Sql;
using System.Data.SqlClient;


namespace SelfServiceAdminstration.Databasecomp
{
    public class DatabaseLayer
    {
        public SqlConnection getSQLConnection()
        {
            SqlConnection conn = null;

            conn = new SqlConnection();
            string server = ConfigurationManager.AppSettings["servername"];
            string username = ConfigurationManager.AppSettings["username"];
            string password = ConfigurationManager.AppSettings["password"];
            string dbname = ConfigurationManager.AppSettings["dbname"];

            conn.ConnectionString = String.Format("server={0};user id={1}; password={2}; database={3}; pooling=false",
                server, username, password, dbname);

            conn.Open();
            return conn;
        }

        public void closeSQLDataConn(SqlConnection conn)
        {
            if (conn != null)
                conn.Close();
        }
        public MySqlConnection getConnection()
        {
            MySqlConnection conn = null;

            conn = new MySqlConnection();
            string server = ConfigurationManager.AppSettings["servername"];
            string username = ConfigurationManager.AppSettings["username"];
            string password = ConfigurationManager.AppSettings["password"];
            string dbname = ConfigurationManager.AppSettings["dbname"];

            conn.ConnectionString = String.Format("server={0};user id={1}; password={2}; database={3}; pooling=false",
                server, username, password, dbname);

            conn.Open();
            return conn;

        }
        public void closeDataConn(MySqlConnection conn)
        {
            if (conn != null)
                conn.Close();
        }

        public MySqlTransaction getTransConnection(MySqlConnection conn)
        {

            MySqlTransaction trans = null;
            trans = conn.BeginTransaction();
            return trans;

        }
        public SqlTransaction getSQLTransConnection(SqlConnection conn)
        {

            SqlTransaction trans = null;
            trans = conn.BeginTransaction();
            return trans;

        }
        public void connectionCommit(MySqlTransaction trans)
        {


            trans.Commit();


        }

        public void connectionSQLCommit(SqlTransaction trans)
        {


            trans.Commit();


        }

        public void connectionRollBack(MySqlTransaction trans)
        {


            trans.Rollback();


        }

        public void connectionRollBack(SqlTransaction trans)
        {


            trans.Rollback();


        }


        public Hashtable getTableData(string tableName, ArrayList colnames, string primaryVal, string whereStr)
        {
            Hashtable dataHash = null;
            string colStr = "";
            MySqlConnection con = null;
            IEnumerator enumerator = null;

            try
            {
                dataHash = new Hashtable();
                con = getConnection();
                MySqlCommand cmd = new MySqlCommand();
                if (colnames != null)
                {
                    enumerator = colnames.GetEnumerator();
                    while (enumerator.MoveNext())
                    {
                        colStr += " " + enumerator.Current.ToString() + " , ";
                    }
                    enumerator.Reset();
                    colStr = colStr.Substring(0, colStr.LastIndexOf(","));

                }
                string query = "";
                if (whereStr != null)
                {
                    query = "select  " + colStr + "  from  " + tableName + " where " + whereStr;
                }
                else
                {
                    query = "select  " + colStr + " from " + tableName;
                }
                cmd.CommandText = query;
                cmd.Connection = con;
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    ArrayList colVal = new ArrayList();
                    while (enumerator.MoveNext())
                    {


                        String str = reader[enumerator.Current.ToString()].ToString();
                        colVal.Add(reader[enumerator.Current.ToString()].ToString());

                    }

                    string primaryv = reader[primaryVal].ToString();
                    dataHash.Add(reader[primaryVal], colVal);
                    enumerator.Reset();
                }



            }
            catch (Exception ex)
            {
                return null;
            }
            if (con != null)
                closeDataConn(con);
            return dataHash;
        }

        public ArrayList getTableDataQuery(string query, string whereStr, string primaryVal, ArrayList colnames)
        {
            Hashtable dataHash = null;
            
            MySqlConnection con = null;
            
            ArrayList colVal = null;
            IEnumerator enumerator = null;

            try
            {
                colVal = new ArrayList();
                dataHash = new Hashtable();
                con = getConnection();
                MySqlCommand cmd = new MySqlCommand();
                enumerator = colnames.GetEnumerator();
               
                if (whereStr != null)
                {
                    query = "select  " + query +  " where " + whereStr;
                }
                else
                {
                    query = "select  " + query ;
                }
                cmd.CommandText = query;
                cmd.Connection = con;
                MySqlDataReader reader = cmd.ExecuteReader();

                int i = 0;
                
                while (reader.Read())
                {

                    
                    while (enumerator.MoveNext())
                    {


                        String str = reader[enumerator.Current.ToString()].ToString();
                        colVal.Add(reader[enumerator.Current.ToString()].ToString());

                    }


                        
                       

                    
                    i++;
                }



            }
            catch (Exception ex)
            {
                return null;
            }
            if (con != null)
                closeDataConn(con);
            return colVal;
        }


        public bool getTablerowCount(string tableName, string whereStr)
        {
            Hashtable dataHash = null;
           
            MySqlConnection con = null;
            

            try
            {
                dataHash = new Hashtable();
                con = getConnection();
                MySqlCommand cmd = new MySqlCommand();
                
                string query = "";
                query = "select  count(*)   from  " + tableName + " where " + whereStr;
                
                cmd.CommandText = query;
                cmd.Connection = con;
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    if (reader.GetInt32(0) == 0)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }


                return true;


            }
            catch (Exception ex)
            {
                if (con != null)
                    closeDataConn(con);
                return false;
            }
            
            
        }

        public Hashtable getMultiTableData(string[] tableName, ArrayList colnames, string primaryVal, string whereStr)
        {
            Hashtable dataHash = null;
            string colStr = "";
            MySqlConnection con = null;
            IEnumerator enumerator = null;

            try
            {
                dataHash = new Hashtable();
                con = getConnection();
                MySqlCommand cmd = new MySqlCommand();
                if (colnames != null)
                {
                    enumerator = colnames.GetEnumerator();
                    while (enumerator.MoveNext())
                    {
                        colStr += " " + enumerator.Current.ToString() + " , ";
                    }
                    enumerator.Reset();
                    colStr = colStr.Substring(0, colStr.LastIndexOf(","));

                }
                string query = " ";
                string tableStr = "";
                for (int i = 0; i < tableName.Length; i++)
                {
                    tableStr += tableName[i] + " ";
                }
                if (whereStr != null)
                {
                    query = "select  " + colStr + "  from  " + tableStr + " where " + whereStr;
                }
                else
                {
                    query = "select  " + colStr + " from " + tableStr;
                }
                cmd.CommandText = query;
                cmd.Connection = con;
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    ArrayList colVal = new ArrayList();
                    while (enumerator.MoveNext())
                    {


                        String str = reader[enumerator.Current.ToString()].ToString();
                        colVal.Add(reader[enumerator.Current.ToString()].ToString());

                    }

                    string primaryv = reader[primaryVal].ToString();
                    dataHash.Add(reader[primaryVal], colVal);
                    enumerator.Reset();
                }



            }
            catch (Exception ex)
            {
                return null;
            }
            if (con != null)
                closeDataConn(con);
            return dataHash;
        }

        public DataSet getTableDataGrid(string query)
        {
            DataSet gridData = null;

            MySqlConnection con = null;


            try
            {
                gridData = new DataSet();
                con = getConnection();

                MySqlDataAdapter dataAdap = new MySqlDataAdapter(query, con);
                MySqlCommandBuilder cmdBuilder = new MySqlCommandBuilder(dataAdap);
                dataAdap.Fill(gridData);


            }
            catch (Exception ex)
            {
                return null;
            }
            if (con != null)
                closeDataConn(con);
            return gridData;
        }


       

        //insert data

        public void insertTableData(string tableName, Hashtable dataHash)
        {
            MySqlConnection con = null;
            try
            {
                string namesStr = "";
                string valuesStr = "";
                string query = "";
                //Parse Hash Table

                IDictionaryEnumerator dataEnum = dataHash.GetEnumerator();
                while (dataEnum.MoveNext())
                {
                    namesStr += dataEnum.Key.ToString() + " ,";
                    valuesStr += "'" + dataEnum.Value.ToString() + "',";

                }

                namesStr = namesStr.Substring(0, namesStr.LastIndexOf(","));
                valuesStr = valuesStr.Substring(0, valuesStr.LastIndexOf(","));

                query = "insert into " + tableName + " ( " + namesStr + ") values ( " + valuesStr + " )";
                //database starts here

                con = getConnection();

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandText = query;
                cmd.Connection = con;
                MySqlTransaction trans = con.BeginTransaction();
                int i = cmd.ExecuteNonQuery();

                if (i <= 0)
                {
                    trans.Rollback();
                }
                else
                {
                    trans.Commit();

                }

                closeDataConn(con);




            }
            catch (Exception er)
            {
                if (con != null)
                {
                    con.Close();
                }
            }
        }


        public bool insertTableDataStatus(string tableName, Hashtable dataHash)
        {
            MySqlConnection con = null;
            try
            {
                string namesStr = "";
                string valuesStr = "";
                string query = "";
                //Parse Hash Table

                IDictionaryEnumerator dataEnum = dataHash.GetEnumerator();
                while (dataEnum.MoveNext())
                {
                    namesStr += dataEnum.Key.ToString() + " ,";
                    valuesStr += "'" + dataEnum.Value.ToString() + "',";

                }

                namesStr = namesStr.Substring(0, namesStr.LastIndexOf(","));
                valuesStr = valuesStr.Substring(0, valuesStr.LastIndexOf(","));

                query = "insert into " + tableName + " ( " + namesStr + ") values ( " + valuesStr + " )";
                //database starts here

                con = getConnection();

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandText = query;
                cmd.Connection = con;
                MySqlTransaction trans = con.BeginTransaction();
                int i = cmd.ExecuteNonQuery();

                if (i <= 0)
                {
                    trans.Rollback();
                    closeDataConn(con);
                    return false;
                }
                else
                {
                    trans.Commit();
                    closeDataConn(con);
                    return true;

                }

               





            }
            catch (Exception er)
            {
                if (con != null)
                {
                    con.Close();
                }
                return false;
            }
        }
        public void insertTableData(string query)
        {
            MySqlConnection con = null;
            try
            {

                con = getConnection();

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandText = query;
                cmd.Connection = con;
                MySqlTransaction trans = con.BeginTransaction();
                int i = cmd.ExecuteNonQuery();

                if (i <= 0)
                {
                    trans.Rollback();
                }
                else
                {
                    trans.Commit();

                }

                closeDataConn(con);




            }
            catch (Exception er)
            {
                if (con != null)
                {
                    con.Close();
                }
            }
        }


        public bool deleteTableData(string query)
        {
            MySqlConnection con = null;
            bool successFlag = false;
            try
            {

                con = getConnection();

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandText = query;
                cmd.Connection = con;
                MySqlTransaction trans = con.BeginTransaction();
                int i = cmd.ExecuteNonQuery();

                if (i < 0)
                {
                    trans.Rollback();
                    successFlag = false;
                }
                else
                {
                    trans.Commit();
                    successFlag = true;

                }

                closeDataConn(con);




            }
            catch (Exception er)
            {
                if (con != null)
                {
                    con.Close();
                }
                successFlag = false;
            }
            return successFlag;
        }

        //To this method send the connection which is open
        //Call transaction obeject

        public int insertMultiTableData(string tableName, Hashtable dataHash, MySqlConnection con, MySqlTransaction trans)
        {

            try
            {
                string namesStr = "";
                string valuesStr = "";
                string query = "";
                //Parse Hash Table

                IDictionaryEnumerator dataEnum = dataHash.GetEnumerator();
                while (dataEnum.MoveNext())
                {
                    namesStr += dataEnum.Key.ToString() + " ,";
                    valuesStr += "'" + dataEnum.Value.ToString() + "',";

                }

                namesStr = namesStr.Substring(0, namesStr.LastIndexOf(","));
                valuesStr = valuesStr.Substring(0, valuesStr.LastIndexOf(","));

                query = "insert into " + tableName + " ( " + namesStr + ") values ( " + valuesStr + " )";
                //database starts here


                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandText = query;
                cmd.Connection = con;

                //int i = cmd.ExecuteNonQuery();

                int lastId = cmd.ExecuteNonQuery();
                lastId = (int)cmd.LastInsertedId;

                return lastId;






            }
            catch (Exception er)
            {
                return -1;
            }
        }

        public int updateMultiTableData(string tableName, Hashtable dataHash, MySqlConnection con, MySqlTransaction trans, string whereClaues)
        {

            try
            {
                string namesStr = "";
                string valuesStr = "";
                string query = "";
                string updateStr = "";
                //Parse Hash Table

                IDictionaryEnumerator dataEnum = dataHash.GetEnumerator();
                while (dataEnum.MoveNext())
                {

                    updateStr += dataEnum.Key.ToString() + " = " + "'" + dataEnum.Value.ToString() + "'";


                }


                query = "update  " + tableName + " set  " + updateStr + "  where " + whereClaues;
                //database starts here


                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandText = query;
                cmd.Connection = con;

                //int i = cmd.ExecuteNonQuery();

                int lastId = cmd.ExecuteNonQuery();
                lastId = (int)cmd.LastInsertedId;

                return lastId;






            }
            catch (Exception er)
            {
                return -1;
            }
        }


        public void updateTableData(string tableName, Hashtable dataHash, string whereClause)
        {
            MySqlConnection con = null;
            try
            {
                string namesStr = "";
                string valuesStr = "";
                string query = "";
                string updateStr = "";
                //Parse Hash Table

                IDictionaryEnumerator dataEnum = dataHash.GetEnumerator();
                while (dataEnum.MoveNext())
                {
                    updateStr += dataEnum.Key.ToString() + " = " + "'" + dataEnum.Value.ToString() + "' , ";

                }

                updateStr = updateStr.Substring(0, updateStr.LastIndexOf(","));
                query = "update  " + tableName + " set  " + updateStr + "  where " + whereClause;
                //database starts here

                con = getConnection();

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandText = query;
                cmd.Connection = con;
                MySqlTransaction trans = con.BeginTransaction();
                int i = cmd.ExecuteNonQuery();

                if (i <= 0)
                {
                    trans.Rollback();
                }
                else
                {
                    trans.Commit();

                }

                closeDataConn(con);




            }
            catch (Exception er)
            {
                if (con != null)
                {
                    con.Close();
                }
            }
        }

        public bool updateTableDataStatus(string tableName, Hashtable dataHash, string whereClause)
        {
            MySqlConnection con = null;
            try
            {
               
                string query = "";
                string updateStr = "";
                //Parse Hash Table

                IDictionaryEnumerator dataEnum = dataHash.GetEnumerator();
                while (dataEnum.MoveNext())
                {
                    updateStr += dataEnum.Key.ToString() + " = " + "'" + dataEnum.Value.ToString() + "' , ";

                }

                updateStr = updateStr.Substring(0, updateStr.LastIndexOf(","));
                query = "update  " + tableName + " set  " + updateStr + "  where " + whereClause;
                //database starts here

                con = getConnection();

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandText = query;
                cmd.Connection = con;
                MySqlTransaction trans = con.BeginTransaction();
                int i = cmd.ExecuteNonQuery();

                if (i <= 0)
                {
                    trans.Rollback();
                    closeDataConn(con);
                    return false;
                }
                else
                {
                    trans.Commit();
                    closeDataConn(con);
                    return true;

                }

                




            }
            catch (Exception er)
            {
                if (con != null)
                {
                    con.Close();
                }
                return false;
            }
        }

    }
}
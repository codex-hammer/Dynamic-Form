using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace TemplateGenerator.Classes
{
    public class DataService
    {
        public filter_template GetTemplate(string pageid)
        {
            dbConnector objConn = new dbConnector();
            SqlConnection Conn = objConn.GetConnection;
            Conn.Open();
            try
            {
                filter_template ft = new filter_template();
                filter_template_header ft_head = new filter_template_header();
                List<filter_template_details> ft_details = new List<filter_template_details>();
                //if (Conn.State != System.Data.ConnectionState.Open) Conn.Open();

                SqlCommand objCommand = new SqlCommand("GetFilterTemplate", Conn);
                objCommand.CommandType = CommandType.StoredProcedure;
                objCommand.Parameters.AddWithValue("@pageid", pageid);
                SqlDataAdapter adp = new SqlDataAdapter(objCommand);
                DataSet ds = new DataSet();
                adp.Fill(ds);
               // SqlDataReader rdr = objCommand.ExecuteReader();

                foreach (DataRow rdr in ds.Tables[0].Rows)
                {
                    filter_template_details ft_detail = new filter_template_details();
                    ft_head.pageid = rdr["pageid"].ToString();
                    ft_head.pagetitle = rdr["pagetitle"].ToString();
                    ft_head.exiturl = rdr["exiturl"].ToString();
                    ft_head.navigateurl = rdr["navigateurl"].ToString();
                    ft_head.modelname = rdr["modelname"].ToString();
                    ft_head.methodname = rdr["methodname"].ToString();
                    ft_head.pagesize = rdr["pagesize"].ToString();
                    ft_head.exportexcel = rdr["exportexcel"].ToString();
                    ft_head.apipath = rdr["apipath"].ToString();
                    ft_head.action = rdr["action"].ToString();
                    ft_detail.DBColumnName = rdr["DBColumnName"].ToString();
                    ft_detail.DisplayColumnName = rdr["DisplayColumnName"].ToString();
                    ft_detail.IsHidden = rdr["IsHidden"].ToString();
                    ft_detail.IsAvailableForSearch = rdr["IsAvailableForSearch"].ToString();
                    ft_detail.IsNavigateParameter = rdr["IsNavigateParameter"].ToString();
                    ft_detail.NavigateParameterName = rdr["NavigateParameterName"].ToString();
                    ft_detail.IsDateRange = rdr["IsDateRange"].ToString();
                    ft_detail.SequenceId = rdr["SequenceId"].ToString();
                    ft_detail.IsYesNo = rdr["IsYesNo"].ToString();
                    ft_detail.IsAmount = rdr["IsAmount"].ToString();
                    ft_detail.IsSearchValue = rdr["IsSearchValue"].ToString();
                    ft_detail.IsLookUp = rdr["IsLookUp"].ToString();
                    ft_detail.LookupDBColName = rdr["LookupDBColName"].ToString();
                    ft_detail.LookupPage = rdr["LookupPage"].ToString();
                    ft_detail.Method = rdr["Method"].ToString();

                    if (ft_detail.Method != null && ft_detail.Method !="")
                    {
                        ft_detail.options = new List<searchcombobox>();

                        SqlCommand objCommand2 = new SqlCommand(ft_detail.Method, Conn);
                        objCommand2.CommandType = CommandType.StoredProcedure;
                        SqlDataAdapter adp2 = new SqlDataAdapter(objCommand2);
                        DataSet ds2 = new DataSet();
                        adp2.Fill(ds2);
                        if (ds2.Tables[0].Columns.Count == 1)
                        {
                            foreach (DataRow rdr2 in ds2.Tables[0].Rows)
                            {
                                searchcombobox option = new searchcombobox();
                                option.fieldtext = rdr2[0].ToString();
                                option.fieldvalue = rdr2[0].ToString();
                                ft_detail.options.Add(option);

                            }
                        }
                        else
                        {
                            foreach (DataRow rdr2 in ds2.Tables[0].Rows)
                            {
                                searchcombobox option = new searchcombobox();
                                option.fieldtext = rdr2[1].ToString();
                                option.fieldvalue = rdr2[0].ToString();
                                ft_detail.options.Add(option);

                            }
                        }
                    }
                    else 
                    {
                        ft_detail.options = new List<searchcombobox>();
                    }

                    ft_details.Add(ft_detail);
                }
                ft.ft_head = ft_head;
                ft.ft_detail = ft_details;
                return ft;
            }
            catch
            {
                throw;
            }
            finally
            {
                //if (Conn != null)
                //{
                //    if (Conn.State == ConnectionState.Open)
                //    {
                //        Conn.Close();
                //        Conn.Dispose();
                //    }
                //}
            }
        }

        //public List<searchcombobox> selectapiUtil(searchvaluequery query)
        //{
        //    dbConnector objConn = new dbConnector();
        //    SqlConnection Conn = objConn.GetConnection;
        //    Conn.Open();
        //    try
        //    {
        //        List<searchcombobox> ls = new List<searchcombobox>();
        //        if (Conn.State != System.Data.ConnectionState.Open) Conn.Open();
        //        SqlCommand objCommand = new SqlCommand(query.methodname, Conn);
        //        objCommand.CommandType = CommandType.StoredProcedure;
        //        SqlDataReader rdr = objCommand.ExecuteReader();
        //        var columns = new List<string>();
        //        for (int i = 0; i < rdr.FieldCount; i++)
        //        {
        //            columns.Add(rdr.GetName(i));
        //        }
                
        //        while(rdr.Read()){
        //            searchcombobox option = new searchcombobox();
        //            option.fieldtext = rdr[columns[0]].ToString();
        //            option.fieldvalue = rdr[columns[0]].ToString();
        //            ls.Add(option);
        //        }

        //        return ls;
        //    }
        //    catch
        //    {
        //        throw;
        //    }
        //    finally
        //    {
        //        if (Conn != null)
        //        {
        //            if (Conn.State == ConnectionState.Open)
        //            {
        //                Conn.Close();
        //                Conn.Dispose();
        //            }
        //        }
        //    }
            
        //}



        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////DUMMY API//////////////////////////////////////////////////////////////////////////////////
        public List<result> GetResult(Query query)
        {
            dbConnector objConn = new dbConnector();
            SqlConnection Conn = objConn.GetConnection;
            Conn.Open();
            try
            {
                List<result> ls = new List<result>();
                string location = query.gpn;
                if (Conn.State != System.Data.ConnectionState.Open) Conn.Open();
                SqlCommand objCommand = new SqlCommand(query.method, Conn);
                objCommand.CommandType = CommandType.StoredProcedure;
                objCommand.Parameters.AddWithValue("@empgpn", query.gpn);
                objCommand.Parameters.AddWithValue("@wherecond", query.where_statement);
                SqlDataReader rdr = objCommand.ExecuteReader();
                while (rdr.Read())
                {
                    result temp = new result();
                    temp.loginId = rdr["LoginID"].ToString();
                    temp.name = rdr["Name"].ToString();
                    temp.status = rdr["Status"].ToString();
                    temp.loginId = rdr["LoginID"].ToString();
                    temp.location = rdr["Location"].ToString();
                    temp.counselorgpn = rdr["CouncelorGPN"].ToString();
                    temp.createdon = rdr["CreatedOn"].ToString();
                    temp.createdby = rdr["CreatedBy"].ToString();
                    ls.Add(temp);
                }
                return ls;
            }


            catch
            {
                throw;
            }
            finally
            {
                if (Conn != null)
                {
                    if (Conn.State == ConnectionState.Open)
                    {
                        Conn.Close();
                        Conn.Dispose();
                    }
                }
            }

        }




        
        
    }
}

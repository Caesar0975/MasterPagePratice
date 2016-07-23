using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Text;
using System.Configuration;

public static partial class ConnectionManager
{
    //連線
    public static SqlConnection GetConnection()
    {
        return new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
    }
}

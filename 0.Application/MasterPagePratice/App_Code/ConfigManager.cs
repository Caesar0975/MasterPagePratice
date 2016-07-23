using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Text;

public enum ConfigKey { IsNew, ActiveMeansImage, RewardListImage}

public static partial class ConfigManager
{
    private static Dictionary<ConfigKey, string> Configs;

    //上傳路徑
    private static string UploadPath = "/_Upload/";
    private static string PhysicalUploadPath = HttpContext.Current.Server.MapPath(UploadPath);

    public static string GetUploadPath()
    {
        return UploadPath;
    }

    public static string GetPhysicalUploadPath()
    {
        if (System.IO.Directory.Exists(PhysicalUploadPath) == false) { System.IO.Directory.CreateDirectory(PhysicalUploadPath); }

        return PhysicalUploadPath;
    }

    //初始化
    public static void Initial()
    {
        Configs = new Dictionary<ConfigKey, string>();

        //建立結構
        foreach (ConfigKey GlobalConfigKey in Enum.GetValues(typeof(ConfigKey)))
        { Configs.Add(GlobalConfigKey, ""); }

        using (SqlConnection SqlConnection = ConnectionManager.GetConnection())
        {

            SqlCommand SqlCommand = SqlConnection.CreateCommand();

            SqlCommand.CommandText = "SELECT "
                                  + " * "
                                  + "FROM "
                                  + " Config ";

            SqlConnection.Open();
            SqlDataReader SqlDataReader = SqlCommand.ExecuteReader();

            while (SqlDataReader.Read())
            {
                ConfigKey Key = (ConfigKey)Enum.Parse(typeof(ConfigKey), SqlDataReader["ConfigKey"].ToString());
                string Value = SqlDataReader["Value"].ToString();

                if (Configs.ContainsKey(Key) == false) { continue; }

                Configs[Key] = Value;
            }
        }
    }

    //取得所有的Configs
    public static Dictionary<ConfigKey, string> GetAll()
    {
        return Configs;
    }

    public static string Get(ConfigKey ConfigKey)
    {
        return Configs[ConfigKey];
    }

    //儲存Configs
    public static void Save(Dictionary<ConfigKey, string>  Configs)
    {
        using (SqlConnection SqlConnection = ConnectionManager.GetConnection())
        {
            SqlCommand SqlCommand = SqlConnection.CreateCommand();

            StringBuilder Commandtext = new StringBuilder();
            Commandtext.Append("DELETE FROM Config WHERE 1 = 1 ;");

            foreach (KeyValuePair<ConfigKey, string> Item in Configs)
            {
                Commandtext.Append("INSERT INTO "
                                  + " Config "
                                  + "( ConfigKey ,Value ) "
                                  + "VALUES "
                                  + " ( @ConfigKey" + Item.Key + " ,@Value" + Item.Key + " ) ;"
                                  );

                SqlCommand.Parameters.AddWithValue("ConfigKey" + Item.Key, Item.Key.ToString());
                SqlCommand.Parameters.AddWithValue("Value" + Item.Key, Item.Value);
            }

            SqlCommand.CommandText = Commandtext.ToString();

            SqlConnection.Open();
            SqlCommand.ExecuteNonQuery();
        }
    }
}

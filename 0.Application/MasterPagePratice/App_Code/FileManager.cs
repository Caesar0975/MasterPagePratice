using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;
using System.Text;
using System.IO;

public class FileManager
{

    //刪除實體檔案
    public static void DeletePhysicalFile(string PhysicalPath, string FileName)
    {
        if (FileName == "") { return; }

        if (System.IO.File.Exists(PhysicalPath + FileName) == false) { return; }

        //刪除實體檔案 
        System.IO.File.Delete(PhysicalPath + FileName);
    }

    //輸出實體檔案
    public static void OutputFile(string PhysicalPath, string PhysicalFileName, string OutputFileName)
    {
        //中文檔名作轉換
        OutputFileName = HttpUtility.UrlEncode(OutputFileName, Encoding.UTF8);

        FileStream fr = new FileStream(PhysicalPath + PhysicalFileName, FileMode.Open);
        Byte[] buf = new Byte[fr.Length];

        fr.Read(buf, 0, Convert.ToInt32(fr.Length));
        fr.Close();
        fr.Dispose();

        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.ClearHeaders();
        HttpContext.Current.Response.Buffer = true;
        //轉換文字檔編碼格式用，但本次輸出無文字檔，故註解此段
        //Response.ContentEncoding = parEncoding;
        HttpContext.Current.Response.AddHeader("content-disposition", "attachment; filename=" + OutputFileName);

        HttpContext.Current.Response.BinaryWrite(buf);
        HttpContext.Current.Response.End();
    }

}


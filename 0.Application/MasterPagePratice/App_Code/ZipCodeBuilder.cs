using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Services;

[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// 若要允許使用 ASP.NET AJAX 從指令碼呼叫此 Web 服務，請取消註解下列一行。
[System.Web.Script.Services.ScriptService]
public class ZipCodeBuilder : System.Web.Services.WebService
{
    public ZipCodeBuilder()
    {
        //如果使用設計的元件，請取消註解下列一行
        //InitializeComponent(); 
    }

    public class ZipCodeItem
    {
        public string ZipCode { get; private set; }
        public string City { get; private set; }
        public string Area { get; private set; }
        public string Road { get; private set; }
        public string Scope { get; private set; }

        internal ZipCodeItem(string ZipCode, string City, string Area, string Road, string Scope)
        {
            this.ZipCode = ZipCode;
            this.City = City;
            this.Area = Area;
            this.Road = Road;
            this.Scope = Scope;
        }
    }

    //快取
    private static List<ZipCodeItem> ZipCodeCache;
    public static void InitialCache()
    {
        if (ZipCodeCache != null) { return; }

        string ZipCodeSourceString = "";

        StreamReader StreamReader = new System.IO.StreamReader(HttpContext.Current.Request.PhysicalApplicationPath + "_Element/ZipCodeBuilder/ZipSource.csv", System.Text.Encoding.Default);
        ZipCodeSourceString = StreamReader.ReadToEnd();//(各筆資料分隔符號為\r\n)
        ZipCodeSourceString = ZipCodeSourceString.Replace(" ", "");
        StreamReader.Dispose();

        ZipCodeCache = new List<ZipCodeItem>();
        foreach (string ZipCodeString in ZipCodeSourceString.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries))
        {
            string[] ZipCodeValue = ZipCodeString.Split(',');

            //ZipCode
            int ZipCode;
            if (int.TryParse(ZipCodeValue[0], out ZipCode) == false) { continue; }

            //City
            string City = ZipCodeValue[1];

            //Area
            string Area = ZipCodeValue[2];

            //Road
            string Road = ZipCodeValue[3];

            //Scope
            string Scope = ZipCodeValue[4];

            ZipCodeItem NewZipCodeItem = new ZipCodeItem(ZipCode.ToString(), City, Area, Road, Scope);
            ZipCodeCache.Add(NewZipCodeItem);
        }

    }

    //取得全部ZipCodeItem
    public static List<ZipCodeItem> GetAllZipCodeItem()
    {
        return ZipCodeCache.ToList();
    }

    //透過ZipCode取得ZipCodeItem
    public static ZipCodeItem GetZipCodeItem(string ZipCode)
    {
        return ZipCodeCache.FirstOrDefault(z => z.ZipCode == ZipCode);
    }

    public static string GetTempZipCode(string City, string Area, string Road, string Scope)
    {
        return ZipCodeCache.First(z => z.City == City && z.Area == Area && z.Road == Road && z.Scope == Scope).ZipCode;
    }

    [System.Web.Services.WebMethod]
    public string GetZipCode(string City, string Area, string Road, string Scope)
    {
        return ZipCodeCache.First(z => z.City == City && z.Area == Area && z.Road == Road && z.Scope == Scope).ZipCode;
    }

    [System.Web.Services.WebMethod]
    public string GetCity()
    {
        return string.Join(",", ZipCodeCache.Select(z => z.City).Distinct());
    }

    [System.Web.Services.WebMethod]
    public string GetArea(string City)
    {
        return string.Join(",", ZipCodeCache.Where(z => z.City == City).Select(z => z.Area).Distinct());
    }

    [System.Web.Services.WebMethod]
    public string GetRoad(string City, string Area)
    {
        return string.Join(",", ZipCodeCache.Where(z => z.City == City && z.Area == Area).Select(z => z.Road).Distinct());
    }

    [System.Web.Services.WebMethod]
    public string GetScope(string City, string Area, string Road)
    {
        return string.Join(",", ZipCodeCache.Where(z => z.City == City && z.Area == Area && z.Road == Road).Select(z => z.Scope).Distinct());
    }
}

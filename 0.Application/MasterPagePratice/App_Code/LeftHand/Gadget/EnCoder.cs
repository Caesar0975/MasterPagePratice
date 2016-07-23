using System;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using System.Collections.Generic;
using System.Web;

namespace LeftHand.Gadget
{
    public class Encoder
    {
        private static byte[] RijndaelKey = new byte[] { 78, 210, 116, 255, 191, 179, 227, 81, 121, 211, 182, 204, 122, 103, 23, 20, 195, 192, 96, 27, 83, 52, 77, 18, 142, 214, 247, 11, 146, 207, 161, 214 };
        private static byte[] RijndaelIV = new byte[] { 146, 205, 113, 116, 103, 75, 41, 253, 163, 23, 163, 255, 105, 27, 223, 22 };
        private static Encoding RijndaelEncoding = Encoding.UTF8;

        //AES加密
        public static string AES_Encryption(string PlainCode)
        {
            RijndaelManaged RijndaelObject = new RijndaelManaged()
            {
                Key = RijndaelKey,
                IV = RijndaelIV
            };

            byte[] DataArray = RijndaelEncoding.GetBytes(PlainCode);
            ICryptoTransform transform = RijndaelObject.CreateEncryptor(RijndaelKey, RijndaelIV);
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, transform, CryptoStreamMode.Write);

            cs.Write(DataArray, 0, DataArray.Length);
            cs.Close();
            return Convert.ToBase64String(ms.ToArray());
        }

        //AES解密
        public static string AES_Decryption(string PlainCode)
        {
            RijndaelManaged RijndaelObject = new RijndaelManaged()
            {
                Key = RijndaelKey,
                IV = RijndaelIV
            };

            byte[] DataArray = Convert.FromBase64String(PlainCode);
            ICryptoTransform transform = RijndaelObject.CreateDecryptor(RijndaelObject.Key, RijndaelObject.IV);
            MemoryStream ms = new MemoryStream(DataArray);
            CryptoStream cs = new CryptoStream(ms, transform, CryptoStreamMode.Read);

            byte[] ReadArray = new byte[DataArray.Length];
            int ReadLenth = cs.Read(ReadArray, 0, ReadArray.Length);
            return RijndaelEncoding.GetString(ReadArray, 0, ReadLenth);
        }

        //MD5加密
        public static string MD5_Encryption(string PlainCode)
        {
            MD5 MD5 = MD5.Create(); //使用MD5
            byte[] Change = MD5.ComputeHash(Encoding.Default.GetBytes(PlainCode));//進行加密
            return BitConverter.ToString(Change).Replace("-", "");
        }

        //SHA1加密
        public static string SHA1_Encryption(string PlainCode)
        {
            SHA1 sha1Hasher = SHA1.Create();
            byte[] data = sha1Hasher.ComputeHash(Encoding.UTF8.GetBytes(PlainCode));
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            return sBuilder.ToString();
        }

        //Url加密傳值
        public static string DictionaryEncoder(Dictionary<string, string> PlainData)
        {
            string ReturnString = "";

            StringBuilder StringBuilder = new StringBuilder();

            foreach (KeyValuePair<string, string> item in PlainData)
            {
                StringBuilder.Append(item.Key + "，" + item.Value + "│");
            }

            ReturnString = AES_Encryption(StringBuilder.ToString());

            return ReturnString;
        }

        //Url傳值解密
        public static Dictionary<string, string> DictionaryDecoder(string EncoderData)
        {
            Dictionary<string, string> DecodeDateDictionary = new Dictionary<string, string>(); ;

            string PlainCode = AES_Decryption(EncoderData);

            foreach (string item in PlainCode.Split('│'))
            {
                if (string.IsNullOrEmpty(item) == true) { break; }

                int SparatorIndex = item.IndexOf('，');
                string Key = item.Substring(0, SparatorIndex);
                string Value = item.Substring(SparatorIndex + 1);

                DecodeDateDictionary.Add(Key, Value);
            }

            return DecodeDateDictionary;
        }
    }
}
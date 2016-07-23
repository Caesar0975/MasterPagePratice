using System;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using System.Collections.Generic;
using System.Web;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

namespace LeftHand.Gadget
{
    public class ObjControl<T>
    {
        public static T Clone(T obj)
        {
            MemoryStream ms = new MemoryStream();
            BinaryFormatter bf = new BinaryFormatter(null, new StreamingContext(StreamingContextStates.Clone));

            bf.Serialize(ms, obj);
            ms.Position = 0;
            T res = (T)bf.Deserialize(ms);
            ms.Close();

            return (T)res;
        }
    }
}
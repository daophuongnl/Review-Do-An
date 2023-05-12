using SingleData;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility
{
    public static class JsonUtil
    {
        private static JsonData jsonData => JsonData.Instance;

        public static string JsonSerialize<T>(this T obj)
        {
            var jss = jsonData.JavaScriptSerializer;
            var objString = jss.Serialize(obj);
            return objString;
        }

        public static T JsonDeserialize<T>(this string objString)
        {
            var jss = jsonData.JavaScriptSerializer;
            var obj = jss.Deserialize<T>(objString);

            return obj;
        }
    }
}
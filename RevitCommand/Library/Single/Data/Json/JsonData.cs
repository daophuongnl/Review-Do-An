using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.IO;
using Nancy.Json;

namespace SingleData
{
    public class JsonData
    {
        private static JsonData? instance;
        public static JsonData Instance
        {
            get => instance ??= new JsonData();
            set => instance = value;
        }

        private JavaScriptSerializer? javaScriptSerializer;
        public JavaScriptSerializer JavaScriptSerializer => javaScriptSerializer ??= new JavaScriptSerializer { };
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SingleData;

namespace Utility
{
    public static class DataBaseUtil
    {
        public static void RefreshDocument(this DataBase dataBase)
        {
            dataBase.Document = null;
            dataBase.InstanceElements = null;
            dataBase.TypeElements = null;
            dataBase.FamilyInstances = null;
            dataBase.FamilySymbols = null;
            dataBase.Families = null;
        }

        public static void RefreshUIDocument(this DataBase dataBase)
        {
            dataBase.RefreshDocument();

            dataBase.UIDocument = null;
        }
    }
}

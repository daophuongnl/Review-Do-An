using Model.Entity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Documents;
using SingleData;
using Autodesk.Revit.DB;
using System.IO;

namespace Utility
{
    public static class EntComponentFamilyUtil
    {
        private static RevitData revitData => RevitData.Instance;


        #region Property

        public static Family GetRevitFamily(this EntComponentFamily ecf)
        {
            var family = revitData.Families.SingleOrDefault(x => x.FamilyCategory.IsEqual(ecf.TargetBuiltInCategory) && x.Name == ecf.Name);

            if (family == null)
            {
                throw new Exception("No family satisfied the condition available");
            }

            return family;
        }

        public static IEnumerable<FamilySymbol> GetFamilySymbols(this EntComponentFamily ecf)
        {
            var fsIds = ecf.RevitFamily.GetFamilySymbolIds();
            var fss = fsIds.Select(x => x.AddAndGetElement<FamilySymbol>());
            revitData.TypeElements.AddRange(fss);
            revitData.FamilySymbols.AddRange(fss);
            return fss;
        }

        public static FamilySymbol GetDefaultFamilySymbol(this EntComponentFamily ecf)
        {
            var fs = ecf.FamilySymbols.First();
            return fs;
        }

        #endregion

        #region Method
        public static FamilyInstance CreateDefaultInstance(this EntComponentFamily ecf, XYZ insertPnt = null, FamilySymbol fs = null)
        {
            if (fs == null)
            {
                fs = ecf.DefaultFamilySymbol;
            }
            if (!fs.IsActive)
            {
                fs.Activate();
            }
            if (insertPnt == null)
            {
                insertPnt = XYZ.Zero;
            }

            var fi = revitData.Document.Create.NewFamilyInstance(insertPnt, fs, Autodesk.Revit.DB.Structure.StructuralType.NonStructural);
            return fi;
        }
        #endregion
    }
}

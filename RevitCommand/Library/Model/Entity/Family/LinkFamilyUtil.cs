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
using System.Windows;

namespace Utility
{
    public static class LinkFamilyUtil
    {
        public static LinkFamily Get(BuiltInCategory bic, string name)
        {
            var linkFamily = new LinkFamily
            {
                TargetBuiltInCategory = bic,
                Name = name
            };

            return linkFamily;
        }

        #region Property


        #endregion

        #region Method



        #endregion
    }
}

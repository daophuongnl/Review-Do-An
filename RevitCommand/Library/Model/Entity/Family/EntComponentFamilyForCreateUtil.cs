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
    public static class EntFamilyForCreateUtil
    {
        private static RevitData revitData => RevitData.Instance;


        #region Property
        public static Document GetDocument(this EntComponentFamilyForCreate efcr)
        {
            var famdoc = revitData.Application.NewFamilyDocument(efcr.FamilyTemplatePath);
            return famdoc;
        }

        public static Family GetRevitFamily(this EntComponentFamilyForCreate efcr)
        {
            efcr.ModifyDocument();

            var famDoc = efcr.FamilyDocument;
            var savePath = Path.Combine(Path.GetTempPath(), $"{efcr.Name}.rfa");
            famDoc.SaveAs(savePath);

            Family? fam = null;
            var tx = revitData.Transaction;
            if (tx.GetStatus() == TransactionStatus.Started)
            {
                tx.Commit();
                fam = famDoc.LoadFamily(revitData.Document);
                tx.Start();
            }
            else
            {
                throw new Exception("You need to start a transaction for loading Family!");
            }

            revitData.InstanceElements.Add(fam);
            revitData.Families.Add(fam);
            famDoc.Close();
            File.Delete(savePath);

            return fam;
        }
        #endregion

        #region Method
        public static void ModifyDocument(this EntComponentFamilyForCreate efcr)
        {
            var doc = efcr.FamilyDocument;

            using (var subTx = new Transaction(doc, "Modify Family Document"))
            {
                subTx.Start();

                var view3d = new FilteredElementCollector(doc).WhereElementIsNotElementType().OfType<View3D>().Single();
                view3d.get_Parameter(BuiltInParameter.MODEL_GRAPHICS_STYLE).Set(4);

                efcr.EntComponentFamilyForCreate_Action!(efcr);
                subTx.Commit();
            }
        }
        #endregion
    }
}

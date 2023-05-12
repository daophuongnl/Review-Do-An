using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Structure;
using Autodesk.Revit.UI;
using Model.Entity;
using SingleData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Utility
{
    public static class RebarCreateUtil
    {
        private static StructuralData structuralData => StructuralData.Instance;
        private static RevitData revitData => RevitData.Instance;
        private static RebarData rebarData => RebarData.Instance;


        #region Property

        public static int GetNumber(this RebarCreate rebarCreate)
        {
            var spacing = rebarCreate.Spacing;
            var distLength = rebarCreate.DistributionLength;

            var number = (int)Math.Floor(distLength / spacing) + 1;
            return number;
        }

        public static double GetSpacing(this RebarCreate rebarCreate)
        {
            var number = rebarCreate.Number;
            var distLength = rebarCreate.DistributionLength;

            var spacing = distLength / (number - 1);
            return spacing;
        }

        public static double GetDistributionLength(this RebarCreate rebarCreate)
        {
            var number = rebarCreate.Number;
            var spacing = rebarCreate.Spacing;

            var distLength = (number + 1) * spacing;
            return distLength;
        }

        public static double GetBarDiameter(this RebarCreate rebarCreate)
        {
#if (REVIT2021_OR_LESS)
            return rebarCreate.BarType!.BarDiameter;
#else
            return rebarCreate.BarType!.BarModelDiameter;
#endif
        }

        public static RebarBarType GetBarType(this RebarCreate rebarCreate)
        {
            return rebarData.RebarBarTypes.FirstOrDefault(x =>
#if (REVIT2021_OR_LESS)
            x.BarDiameter
#else
            x.BarModelDiameter
#endif

            .IsEqual(rebarCreate.BarDiameter));
        }

        public static string GetRebarShapeName(this RebarCreate rebarCreate)
        {
            return rebarCreate.RebarShape!.Name;
        }

        public static RebarShape GetRebarShape(this RebarCreate rebarCreate)
        {
            return rebarData.RebarShapes.FirstOrDefault(x => x.Name == rebarCreate.RebarShapeName);
        }

        public static Rebar GetRevitRebar(this RebarCreate rebarCreate)
        {
            var doc = revitData.Document;
            var hostElement = rebarCreate.HostElement;

            var rebar = Rebar.CreateFromCurves(doc, rebarCreate.RebarStyle, rebarCreate.BarType,
              null, null, hostElement, rebarCreate.DistributionDirection, rebarCreate.Curves,
              RebarHookOrientation.Left, RebarHookOrientation.Right, true, true);

            var rsda = rebarCreate.RebarShapeDrivenAccessor = rebar.GetShapeDrivenAccessor();
            var rshape = rebarCreate.RebarShape;
            if (rshape != null)
            {
                rsda.SetRebarShapeId(rshape.Id);
            }

            rebarCreate.SetArray();

            var setting = rebarData.RebarSetting;
            if (setting.IsSetRebarUnObscured)
            {
                var view3D = revitData.Document.ActiveView as View3D;
                if (view3D != null)
                {
                    rebar.SetUnobscuredInView(view3D, true);
#if (REVIT2022_OR_LESS)
                    rebar.SetSolidInView(view3D, true);
#endif
                }
            }

            return rebar;
        }

        #endregion

        #region Method

        public static void SetArray(this RebarCreate rebarCreate)
        {
            var number = rebarCreate.Number;
            if (number == 1) return;

            var distLength = rebarCreate.DistributionLength;
            var spacing = rebarCreate.Spacing;
            var isIncludeFirstBar = rebarCreate.IsIncludeFirstBar;
            var isIncludeLastBar = rebarCreate.IsIncludeLastBar;
            var rsda = rebarCreate.RebarShapeDrivenAccessor;
            switch (rebarCreate.RebarLayoutRule)
            {
                case RebarLayoutRule.FixedNumber:
                    rsda.SetLayoutAsFixedNumber(number, distLength, true, isIncludeFirstBar, isIncludeLastBar);
                    break;
                case RebarLayoutRule.NumberWithSpacing:
                    rsda.SetLayoutAsNumberWithSpacing(number, spacing, true, isIncludeFirstBar, isIncludeLastBar);
                    break;
                case RebarLayoutRule.MaximumSpacing:
                    rsda.SetLayoutAsMaximumSpacing(spacing, distLength, true, isIncludeFirstBar, isIncludeLastBar);
                    break;
            }
        }

        #endregion
    }
}

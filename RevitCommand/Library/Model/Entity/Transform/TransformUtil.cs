using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;
using SingleData;

namespace Utility
{
    public static class TransformUtil
    {
        private static GridData gridData => GridData.Instance;

        public static Transform Purge(this Transform transform)
        {
            var sb = new StringBuilder();
            sb.Append($"Origin: {transform.Origin}\n");

            transform.Origin = transform.Origin.PurgePoint();

            sb.Append($"Origin: {transform.Origin}\n");
            var s = sb.ToString();

            return transform;
        }
    }
}

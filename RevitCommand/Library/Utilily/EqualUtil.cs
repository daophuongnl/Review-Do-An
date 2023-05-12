using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using Autodesk.Revit.DB;

namespace Utility
{
    /// <summary>
    /// Tập hợp các công cụ để so sanh các đối tượng
    /// </summary>
    public static class EqualUtil
    {
        private const double Precision = 0.00001;    //precision when judge whether two doubles are equal

        /// <summary>
        /// Kiểm tra 2 giá trị có bằng nhau hay không trong môi trường Revit
        /// </summary>
        /// <param name="d1"></param>
        /// <param name="d2"></param>
        /// <returns></returns>
        public static bool IsEqual(this double d1, double d2)
        {
            //get the absolute value;
            double diff = Math.Abs(d1 - d2);
            return diff < Precision;
        }

        /// <summary>
        /// Kiểm tra 2 tọa độ 3D có bằng nhau hay không trong môi trường Revit
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public static bool IsEqual(this Autodesk.Revit.DB.XYZ first, Autodesk.Revit.DB.XYZ second)
        {
            bool flag = true;
            flag = flag && IsEqual(first.X, second.X);
            flag = flag && IsEqual(first.Y, second.Y);
            flag = flag && IsEqual(first.Z, second.Z);
            return flag;
        }

        /// <summary>
        /// Kiểm tra giá trị đầu có bằng hoặc lớn hơn giá trị thứ hai hay không trong môi trường Revit
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public static bool IsEqualOrBigger(this double first, double second)
        {
            if (IsEqual(first, second)) return true;
            return first > second;
        }

        /// <summary>
        /// Kiểm tra tọa độ 3D đầu có bằng hay lớn hơn (theo thứ tự Z, Y, Z) tọa độ 3D thứ hai hay không torng môi trường Revit
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public static bool IsEqualOrBigger
            (this Autodesk.Revit.DB.XYZ first, Autodesk.Revit.DB.XYZ second)
        {
            if (IsEqual(first, second)) return true;
            if (IsEqual(first.Z, second.Z))
            {
                if (IsEqual(first.Y, second.Y))
                {
                    return (first.X > second.X);
                }
                return (first.Y > second.Y);
            }
            return (first.Z > second.Z);
        }

        /// <summary>
        /// Kiểm tra giá trị đầu có bằng hoặc nhỏ hơn giá trị thứ hai hay không trong môi trường Revit
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public static bool IsEqualOrSmaller(this double first, double second)
        {
            if (IsEqual(first, second)) return true;
            return first < second;
        }

        /// <summary>
        /// Kiểm tra tọa độ 3D đầu có bằng hay nhỏ hơn (theo thứ tự Z, Y, Z) tọa độ 3D thứ hai hay không torng môi trường Revit
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public static bool IsEqualOrSmaller
            (this Autodesk.Revit.DB.XYZ first, Autodesk.Revit.DB.XYZ second)
        {
            if (IsEqual(first, second)) return true;
            if (IsEqual(first.Z, second.Z))
            {
                if (IsEqual(first.Y, second.Y))
                {
                    return (first.X < second.X);
                }
                return (first.Y < second.Y);
            }
            return (first.Z < second.Z);
        }

        /// <summary>
        /// Kiểm tra 2 tọa độ 2D có bằng nhau hay không trong môi trường Revit
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public static bool IsEqual(this Autodesk.Revit.DB.UV first, Autodesk.Revit.DB.UV second)
        {
            bool flag = true;
            flag = flag && IsEqual(first.U, second.U);
            flag = flag && IsEqual(first.V, second.V);
            return flag;
        }

        /// <summary>
        /// Kiểm tra 2 đường Curve có cùng tọa độ đầu và cuối hay không trong môi trường Revit
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public static bool IsEqual
            (this Autodesk.Revit.DB.Curve first, Autodesk.Revit.DB.Curve second)
        {
            if (IsEqual(first.GetEndPoint(0), second.GetEndPoint(0)))
            {
                return IsEqual(first.GetEndPoint(1), second.GetEndPoint(1));
            }
            if (IsEqual(first.GetEndPoint(1), second.GetEndPoint(0)))
            {
                return IsEqual(first.GetEndPoint(0), second.GetEndPoint(1));
            }
            return false;
        }

        /// <summary>
        /// Kiểm tra tọa độ 3D đầu có nhỏ hơn (theo thứ tự Z, Y, Z) tọa độ 3D thứ hai hay không torng môi trường Revit
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public static bool IsSmaller
            (this Autodesk.Revit.DB.XYZ first, Autodesk.Revit.DB.XYZ second)
        {
            if (IsEqual(first, second)) return false;
            if (IsEqual(first.Z, second.Z))
            {
                if (IsEqual(first.Y, second.Y))
                {
                    return (first.X < second.X);
                }
                return (first.Y < second.Y);
            }
            return (first.Z < second.Z);
        }

        /// <summary>
        /// Kiểm tra giá trị đầu có nhỏ hơn giá trị thứ hai hay không trong môi trường Revit
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static bool IsSmaller(this double x, double y)
        {
            if (IsEqual(x, y)) return false;
            return x < y;
        }

        /// <summary>
        /// Kiểm tra tọa độ 3D đầu có lớn hơn (theo thứ tự Z, Y, Z) tọa độ 3D thứ hai hay không torng môi trường Revit
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public static bool IsBigger
            (this Autodesk.Revit.DB.XYZ first, Autodesk.Revit.DB.XYZ second)
        {
            if (IsEqual(first, second)) return false;
            if (IsEqual(first.Z, second.Z))
            {
                if (IsEqual(first.Y, second.Y))
                {
                    return (first.X > second.X);
                }
                return (first.Y > second.Y);
            }
            return (first.Z > second.Z);
        }

        /// <summary>
        /// Kiểm tra giá trị đầu có lớn hơn giá trị thứ hai hay không trong môi trường Revit
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public static bool IsBigger(this double first, double second)
        {
            if (IsEqual(first, second)) return false;
            return first > second;
        }

        public static double CleanValue(this double value)
        {
            return Math.Round(value / Precision) * Precision;
        }

        public static UV CleanValue(this UV value)
        {
            return new UV(value.U.CleanValue(), value.V.CleanValue());
        }

        public static XYZ CleanValue(this XYZ value)
        {
            return new XYZ(value.X.CleanValue(), value.Y.CleanValue(), value.Z.CleanValue());
        }
    }
}

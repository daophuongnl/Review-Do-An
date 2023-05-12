using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using System.Windows;

namespace Utility
{
    /// <summary>
    /// Tập hợp các công cụ để xử lý chuyển đổi đơn vị
    /// </summary>
    public static class UnitUtil
    {
        private const double FEET_TO_METERS = 0.3048;
        private const double FEET_TO_CENTIMETERS = FEET_TO_METERS * 100;
        private const double FEET_TO_MILIMETERS = FEET_TO_METERS * 1000;

        /// <summary>
        /// Chuyển đổi từ feet sang meter
        /// </summary>
        /// <param name="feet"></param>
        /// <returns></returns>
        public static double feet2Meter(this double feet)
        {
            return feet * FEET_TO_METERS;
        }

        /// <summary>
        /// Chuyển đổi từ feet2 sang meter2 (diện tích)
        /// </summary>
        /// <param name="feet"></param>
        /// <returns></returns>
        public static double feet2MeterSquare(this double feet)
        {
            return feet * FEET_TO_METERS * FEET_TO_METERS;
        }

        /// <summary>
        /// Chuyển đổi từ feet3 sang meter3 (thể tích)
        /// </summary>
        /// <param name="feet"></param>
        /// <returns></returns>
        public static double feet2MeterCubic(this double feet)
        {
            return feet * FEET_TO_METERS * FEET_TO_METERS * FEET_TO_METERS;
        }

        /// <summary>
        /// Chuyển đổi từ feet sang centimeter
        /// </summary>
        /// <param name="feet"></param>
        /// <returns></returns>
        public static double feet2Centimeter(this double feet)
        {
            return feet * FEET_TO_CENTIMETERS;
        }

        /// <summary>
        /// Chuyển đổi từ feet2 sang centimeter2 (diện tích)
        /// </summary>
        /// <param name="feet"></param>
        /// <returns></returns>
        public static double feet2CentimeterSquare(this double feet)
        {
            return feet * FEET_TO_CENTIMETERS * FEET_TO_CENTIMETERS;
        }

        /// <summary>
        /// Chuyển đổi từ feet3 sang centimeter3 (thể tích)
        /// </summary>
        /// <param name="feet"></param>
        /// <returns></returns>
        public static double feet2CentimeterCubic(this double feet)
        {
            return feet * FEET_TO_CENTIMETERS * FEET_TO_CENTIMETERS * FEET_TO_CENTIMETERS;
        }

        /// <summary>
        /// Chuyển đổi từ feet sang milimeter
        /// </summary>
        /// <param name="feet"></param>
        /// <returns></returns>
        public static double feet2Milimeter(this double feet)
        {
            return feet * FEET_TO_MILIMETERS;
        }

        /// <summary>
        /// Chuyển đổi từ feet2 sang milimeter2 (diện tích)
        /// </summary>
        /// <param name="feet"></param>
        /// <returns></returns>
        public static double feet2MilimeterSquare(this double feet)
        {
            return feet * FEET_TO_MILIMETERS * FEET_TO_MILIMETERS;
        }

        /// <summary>
        /// Chuyển đổi từ feet3 sang milimeter3 (thể tích)
        /// </summary>
        /// <param name="feet"></param>
        /// <returns></returns>
        public static double feet2MilimeterCubic(this double feet)
        {
            return feet * FEET_TO_MILIMETERS * FEET_TO_MILIMETERS * FEET_TO_CENTIMETERS;
        }


        /// <summary>
        /// Chuyển đổi từ meter sang feet
        /// </summary>
        /// <param name="meter"></param>
        /// <returns></returns>
        public static double meter2Feet(this double meter)
        {
            return meter / FEET_TO_METERS;
        }

        /// <summary>
        /// Chuyển đổi từ meter2 sang feet2 (diện tích)
        /// </summary>
        /// <param name="meter"></param>
        /// <returns></returns>
        public static double meter2FeetSquare(this double meter)
        {
            return meter / (FEET_TO_METERS * FEET_TO_METERS);
        }

        /// <summary>
        /// Chuyển đổi từ milimeter sang feet
        /// </summary>
        /// <param name="milimeter"></param>
        /// <returns></returns>
        public static double milimeter2Feet(this double milimeter)
        {
            return milimeter / FEET_TO_MILIMETERS;
        }

        /// <summary>
        /// Chuyển đổi từ milimeter2 sang feet2 (diện tích)
        /// </summary>
        /// <param name="milimeter"></param>
        /// <returns></returns>
        public static double milimeter2FeetSquare(this double milimeter)
        {
            return milimeter / (FEET_TO_MILIMETERS * FEET_TO_MILIMETERS);
        }

        /// <summary>
        /// Chuyển đổi từ độ radian sang độ degree
        /// </summary>
        /// <param name="rad"></param>
        /// <returns></returns>
        public static double radian2Degree(this double rad)
        {
            return rad * 180 / Math.PI;
        }

        /// <summary>
        /// Chuyển đổi từ độ degree sang độ radian
        /// </summary>
        /// <param name="deg"></param>
        /// <returns></returns>
        public static double degree2Radian(this double deg)
        {
            return deg * Math.PI / 180;
        }

        public static double Round(this double value, int factor = 1)
        {
            if (factor == 0)
            {
                factor = 1;
            }

            var roundValue = Math.Round(value / (double)factor) * factor;
            return roundValue;
        }

        public static double Floor(this double value, int factor = 1)
        {
            if (factor == 0)
            {
                factor = 1;
            }

            var roundValue = Math.Floor(value / (double)factor) * factor;
            return roundValue;
        }

        public static double Ceiling(this double value, int factor = 1)
        {
            if (factor == 0)
            {
                factor = 1;
            }

            var roundValue = Math.Ceiling(value / (double)factor) * factor;
            return roundValue;
        }

        public static double RoundMM(this double value, int factor = 1)
        {
            if (factor == 0)
            {
                factor = 1;
            }

            var roundValue = value.feet2Milimeter().Round(factor).milimeter2Feet();
            return roundValue;
        }

        public static double FloorMM(this double value, int factor = 1)
        {
            if (factor == 0)
            {
                factor = 1;
            }

            var roundValue = value.feet2Milimeter().Floor(factor).milimeter2Feet();
            return roundValue;
        }

        public static double CeilingMM(this double value, int factor = 1)
        {
            if (factor == 0)
            {
                factor = 1;
            }

            var roundValue = value.feet2Milimeter().Ceiling(factor).milimeter2Feet();
            return roundValue;
        }

        /// <summary>
        /// Làm tròn lên một giá trị
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        public static int RoundUp(this double d)
        {
            return Math.Round(d, 0) < d ? (int)(Math.Round(d, 0) + 1) : (int)(Math.Round(d, 0));
        }

        /// <summary>
        /// Làm tròn xuống một giá trị
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        public static int RoundDown(this double d)
        {
            return Math.Round(d, 0) < d ? (int)(Math.Round(d, 0)) : (int)(Math.Round(d, 0) - 1);
        }
    }
}

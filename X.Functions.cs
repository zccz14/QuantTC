using System;
using System.Collections.Generic;
using System.Linq;

namespace QuantTC
{
    public static partial class X
    {
        /// <summary>
        /// Judge if a value is same signed with another
        /// </summary>
        public static bool IsSameSigned(this double subject, double @object) =>
            subject >= 0 && @object >= 0 || subject < 0 && @object < 0;

        /// <summary>
        /// Judge if a value is same signed with another
        /// </summary>
        public static bool IsSameSigned(this int subject, int @object) =>
            subject > 0 && @object > 0 || subject < 0 && @object < 0 || subject == 0 && @object == 0;

        /// <summary>
        /// Judge if a value is different signed with another
        /// </summary>
        public static bool IsDiffSigned(this double subject, double @object) =>
            subject >= 0 && @object < 0 || subject < 0 && @object >= 0;

        /// <summary>
        /// Judge if a value is different signed with another
        /// </summary>
        public static bool IsDiffSigned(this int subject, int @object) => !subject.IsSameSigned(@object);

        /// <summary>
        /// Action To Func
        /// </summary>
        /// <typeparam name="T">class or ref type</typeparam>
        /// <returns>The Function returning the origin object</returns>
        public static Func<T, T> ToFunc<T>(this Action<T> action) => t =>
        {
            action(t);
            return t;
        };

        public static int Abs(this int v) => Math.Abs(v);
        public static double Abs(this double v) => Math.Abs(v);

        /// <summary>
        /// Judge if a value is between l and u : [l, u]
        /// </summary>
        /// <param name="v"></param>
        /// <param name="l">Lower Bound</param>
        /// <param name="u">Upper Bound</param>
        /// <returns></returns>
        public static bool IsBetween(this double v, double l, double u) => l <= v && v <= u;

        /// <summary>
        /// Used in Indicators' Count Sync
        /// </summary>
        /// <param name="ints">Indicators' count</param>
        /// <returns>minimum count</returns>
        public static int Min(params int[] ints) => ints.Min();

        /// <summary>
        /// Linear Scaled Euclidean Distance:
        /// Math.Sqrt(Math.Pow((x1 - x2) * scaleX, 2) + Math.Pow((y1 - y2) * scaleY, 2))
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <param name="scaleX"></param>
        /// <param name="scaleY"></param>
        /// <returns></returns>
        public static double ScaledDistance(int x1, double y1, int x2, double y2, double scaleX, double scaleY) =>
            Math.Sqrt(Math.Pow((x1 - x2) * scaleX, 2) + Math.Pow((y1 - y2) * scaleY, 2));

        /// <summary>
        /// Linear Scaled Slope:
        /// ((y2 - y1) * scaleY) / ((x2 - x1) * scaleX)
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <param name="scaleX"></param>
        /// <param name="scaleY"></param>
        /// <returns></returns>
        public static double ScaledSlope(int x1, double y1, int x2, double y2, double scaleX, double scaleY) =>
            ((y2 - y1) * scaleY) / ((x2 - x1) * scaleX);

        /// <summary>
        /// Cast deg to arc
        /// </summary>
        /// <param name="deg"></param>
        /// <returns></returns>
        public static double ToArc(this double deg) => deg * Math.PI / 180.0;

        /// <summary>
        /// Cast arc to deg
        /// </summary>
        /// <param name="arc"></param>
        /// <returns></returns>
        public static double ToDeg(this double arc) => arc * 180.0 / Math.PI;
    }
}
using System;

namespace QuantTC
{
    public static partial class X
    {
        public static TR Apply<TT, TR>(this TT This, Func<TT, TR> func) => func(This);

        public static TR Apply<TT, T1, TR>(this TT This, Func<TT, T1, TR> func, T1 arg1) => func(This, arg1);

        public static TR Apply<TT, T1, T2, TR>(this TT This, Func<TT, T1, T2, TR> func, T1 arg1, T2 arg2) =>
            func(This, arg1, arg2);

        public static TR Apply<TT, T1, T2, T3, TR>(this TT This, Func<TT, T1, T2, T3, TR> func, T1 arg1, T2 arg2,
            T3 arg3) => func(This, arg1, arg2, arg3);

        public static TR Apply<TT, T1, T2, T3, T4, TR>(this TT This, Func<TT, T1, T2, T3, T4, TR> func, T1 arg1,
            T2 arg2, T3 arg3, T4 arg4) => func(This, arg1, arg2, arg3, arg4);
    }
}
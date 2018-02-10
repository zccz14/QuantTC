# Crossing Predicates

The concept '**crossing**' is widely used in trading analysis.

For example, *Trend Following* strategies have a common point: be long above a moving average indicator, and close when the price was **downward crossed** the moving average indicator.

Crossing has a direction: **Upward** or **Downward**.

Extract a predicate form: **A** was (**upward** | **downward**) **crossed** **B**.

Take upward crossing for example, **how to define or judge if one was upward crossed another?**

Crossing is a time-related concept. The predicate 'A was upward crossed B' means that A is greater than B **currently**, but A is not greater than B **previously**.

But how to define **currently** and **previously**?

For derived indicators, the 'currently' and 'previously' just simply means 'the current value' and 'the previous value' correspondingly.

For period-based price, the 'currently' usually means 'the current close price', but the 'previously' can be treated as 'the previous close price', 'the current lowest price' or 'the previous lowest price'.

## API

Arbitrary type `T` or `(T, T)` can be upward/downward crossed by a `(IComparable<T>, IComparable<T>)`. Semantically, `(T, T)` means `(previous, current)`.

```C#
// static class QuantTC.X
public static bool IsUx<T>(this (IComparable<T>, IComparable<T>) This, T that);
public static bool IsDx<T>(this (IComparable<T>, IComparable<T>) This, T that);
public static bool IsUx<T>(this (IComparable<T>, IComparable<T>) This, (T, T) that);
public static bool IsDx<T>(this (IComparable<T>, IComparable<T>) This, (T, T) that);
```

For the application on `IReadOnlyList<T>`:

```c#
// static class QuantTC.X
public static bool IsUxAt<T1, T2>(this IReadOnlyList<T1> This, T2 that, int i) where T1 : IComparable<T2>;
public static bool IsDxAt<T1, T2>(this IReadOnlyList<T1> This, T2 that, int i) where T1 : IComparable<T2>;
public static bool IsUxAt<T1, T2>(this IReadOnlyList<T1> This, IReadOnlyList<T2> that, int i) where T1 : IComparable<T2>;
public static bool IsDxAt<T1, T2>(this IReadOnlyList<T1> This, IReadOnlyList<T2> that, int i) where T1 : IComparable<T2>;
```
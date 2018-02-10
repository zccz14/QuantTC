# Iteration

Inspired by C# Linq, abstracting the 'iteration' is a great idea to simplify the program. Forget using the ugly 'for-loop' and 'while-loop' everywhere in your code.

But for some reason, .NET standard doesn't support the API we need. But it's easy to extend by yourself!

## Range Iteration

Intergers are widely treated as 'index', 'shift', 'address', 'numbering', ...etc. So iteration of integer is a plain requirement.

Iterating an integer is usually like this:

```c#
for (var i = left; i < right; i++) {
    // do something with i
}
```

or in a reversed order, from right-end to left-end:

```c#
for (var i = right - 1; i >= left; i--) {
    // do something with i
}
```

Now extract two methods correspondingly, they are both iteration in the interval of [left, right):

```c#
// static class QuantTC.X
public static IEnumerable<int> RangeL(int left, int right); // Left to Right
public static IEnumerable<int> RangeR(int left, int right); // Right to Left
```

Another way to specify the iteration direction is to give a reversed flag to the summary method.

```c#
// static class QuantTC.X
public static IEnumerable<int> Range(int left, int right, bool reversed = false);
```

### Value Tuple

A value tuple of type `(int, int)` can also be treated as range.

```c#
// static class QuantTC.X
public static IEnumerable<int> AsRange(this (int, int) This, bool reversed = false);
public static IEnumerable<int> AsRangeL(this (int, int) This);
public static IEnumerable<int> AsRangeR(this (int, int) This);
```

### IReadOnlyList

For `IReadOnlyList<T>`, Range* can simply apply to this list type:

```c#
// static class QuantTC.X
public static IEnumerable<(T, int)> Range<T>(this IReadOnlyList<T> This, int left, int right, bool reversed = false);
public static IEnumerable<(T, int)> RangeL<T>(this IReadOnlyList<T> This, int left, int right);
public static IEnumerable<(T, int)> RangeR<T>(this IReadOnlyList<T> This, int left, int right);
```

The yield returning type is `(T, int)`, the later means the index `i`, the former is the corresponding value `This[i]`. If you want to discard the index:

```c#
// Using C# 7.0 Discard Syntax
void Test1<T>(IReadOnlyList<T> a, int l, int r)
{
    foreach (var (v, _) in a.RangeL(l, r))
    {
        // do something with v
    }
}
// Using Linq Select API
void Test2<T>(IReadOnlyList<T> a, int l, int r)
{
    foreach (var v in a.RangeL(l, r).Select(t => t.Item1))
    {
        // do something with v
    }
}
```

Actually, the type `IReadOnlyList<T>` has a implicit range [0, Count). So we can ignore some of arguments.

```
// static class QuantTC.X

// left to right: [left, Count) 
public static IEnumerable<(T, int)> FromL<T>(this IReadOnlyList<T> This, int left); 
// right to left: [0, right)
public static IEnumerable<(T, int)> FromR<T>(this IReadOnlyList<T> This, int right);
```

In .NET Standard API, type `IReadOnlyList<T>` already has an extension method named `Reverse`. BUT it was built on `IEnumerable<T>`, so the performance is really bad. The native method cost O(N) space complexity while the `Reverse` **should** only cost O(1) space. In pure algorithm view, the native method is slower because of constructing a value stack. Worse more, the much larger memory that the native method allocated should be free later, which cause a garbage collection (GC) process.

For compatibility to .NET Standard API, there're two reverse methods applied to `IReadOnlyList<T>`:

```c#
public static IEnumerable<T> Reverse<T>(this IReadOnlyList<T> This); // right to left [0, Count)
public static IEnumerable<(T, int)> ReverseX<T>(this IReadOnlyList<T> This); // right to left [0, Count)
```

The redefined method `Reverse` has a higher type declaration than the native `Reverse` method. So the C# **'best matching'** mechanism will automatically switch from the native to the redefined. It enhanced the performance without any cost!

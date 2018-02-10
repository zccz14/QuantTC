# QuantTC

QuantTC (Tool Collection) is a .NET stardard class library, which consists of the most commonly used **static methods**, **indicators** and **datum types**.

## Namespaces

+ QuantTC
  
  The root namespace of QuantTC project.

+ QuantTC.Data

  The core data types, including interfaces and their implementations. There're also the most relative extensions.

+ QuantTC.Indicators

  The stardard defined and implemented technical indicators, such as SMA, EMA, MACD, BOLL, ...etc.

  All of the indicators here are non-generic, but they usually have parameters.

+ QuantTC.Indicators.Generic

  Contains the core design of *QuantTC Indicators Framework*. `IIndicator` and `IIndicator<T>` is the abstraction of technical indicators. For the indicators which use `List<T>` as storage, the generic typed class `Indicator<T>` works well .There're many generic typed indicators, like `Dumb<T>`, `Transformer<T>`, `BinaryOperation<T1, T2, T3>`, `MovingMostValue<T>`, ...etc.

## Extension Methods

Thanks to C#, we can extend types without change its structure and inheritance. That's what we called *Extension Methods*

QuantTC used *Extension Methods* everywhere. Sometimes it seems to be magical effects.

All of the standard QuantTC extensions are written in static **partial** class `X` in each namespace, such as `QuantTC.X`, `QuantTC.Data.X`, `QuantTC.Indicators.X` and `QuantTC.Indicators.Generic.X`.

The reason why the class is **partial** specified is to arrange the extension methods closer to their extending type.

Once using the namespace of QuantTC, the corresponding extension methods are imported.

Anyway, many extension methods are so simple that it's hard to define a reasonable "this" object. For example,

```c#
// In static class QuantTC.X
public static int Min(params int[] args);
```

Hard to find a reasonable "**this**", right?

You can invoke them like a normal static method.

```c#
using QuantTC;
// ...
X.Min(1, 2, 3, -1, 4); // should returning -1
```

or import them statically:

```c#
using static QuantTC.X;
// ...
Min(1, 2, 3, -1, 4); // should returning -1
```
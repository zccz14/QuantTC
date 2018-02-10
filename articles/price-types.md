# Price Types
Keisen (candlestick) is a most commonly used tool to analyze the market price.

Sometimes there's no requirement to use all the data in a keisen datum. Following the interface segregation principle (ISP), it should be defined as following interfaces:

```csharp
public interface IPriceO
{
    double Open { get; }
}

public interface IPriceH
{
    double High { get; }
}

public interface IPriceL
{
    double Low { get; }
}

public interface IPriceC
{
    double Close { get; }
}
```

And then composite them into the following forms:

```csharp
public interface IPriceOH : IPriceO, IPriceH { }
public interface IPriceOL : IPriceO, IPriceL { }
public interface IPriceOC : IPriceO, IPriceC { }
public interface IPriceHL : IPriceH, IPriceL { }
public interface IPriceHC : IPriceH, IPriceC { }
public interface IPriceLC : IPriceL, IPriceC { }
public interface IPriceOHL : IPriceOH, IPriceOL, IPriceHL { }
public interface IPriceOHC : IPriceOH, IPriceOC, IPriceHC { }
public interface IPriceOLC : IPriceOL, IPriceOC, IPriceLC { }
public interface IPriceHLC : IPriceHL, IPriceHC, IPriceLC { }
public interface IPriceOHLC : IPriceOHL, IPriceOHC, IPriceOLC, IPriceHLC { }
```

And make it with other vendors:

```csharp
public interface IPrice : IPriceOHLC
{
    DateTime DateTime { get; }
    int Volume { get; }
    int OpenInterest { get; }
}
```

Finally, let's precisely implement the final interface `IPrice` with class `Price`:

```c#
public class Price: IPrice
{
    // ...data members with setters / getters
}
```

And with the deserialization, similar to `int.Parse` and `double.Parse`, we have `Price.Parse` method, which converts the string representation of `$"{Datetime},{Open},{High},{Low},{Close},{Volume},{OpenInterest}"` to its Price equivalent.

```c#
public static Price Parse(string s);
```

## Minimal-grained Extension

Then, we can define several extension methods to enhance the price type:

```csharp
public static partial class X
{
    public static bool IsBullish(this IPriceOC price) => price.Close > price.Open;
    public static bool IsBearish(this IPriceOC price) => price.Close < price.Open;
    public static double Top(this IPriceOC price) => Math.Max(price.Close, price.Open);
    public static double Bottom(this IPriceOC price) => Math.Min(price.Close, price.Open);
    public static double RealBody(this IPriceOC price) => Math.Abs(price.Close - price.Open);
    public static double UpperShadow(this IPriceOHC price) => price.High - price.Top();
    public static double LowerShadow(this IPriceOLC price) => price.Bottom() - price.Low;
    // ... etc
}
```

## Use As Base Type

We can define `As*` methods to turn the derived types to the base types:

```csharp
public static partial class X
{
    public static IPriceO AsPriceO(this IPriceO price) => price;
    public static IPriceH AsPriceH(this IPriceH price) => price;
    public static IPriceL AsPriceL(this IPriceL price) => price;
    public static IPriceC AsPriceC(this IPriceC price) => price;
    public static IPriceOC AsPriceOC(this IPriceOC price) => price;
    public static IPriceHL AsPriceHL(this IPriceHL price) => price;
    public static IPriceOHC AsPriceOHC(this IPriceOHC price) => price;
    public static IPriceOLC AsPriceOLC(this IPriceOLC price) => price;
    public static IPriceOHLC AsPriceOHLC(this IPriceOHLC price) => price;
}
```

## Validation

so we can define `IsValid` method to validate the datum:

```csharp
public static partial class X
{
    public static bool IsValid(this IPriceO price) => price.Open > 0;
    public static bool IsValid(this IPriceH price) => price.High > 0;
    public static bool IsValid(this IPriceL price) => price.Low > 0;
    public static bool IsValid(this IPriceC price) => price.Close > 0;
    public static bool IsValid(this IPriceOC price) => price.AsPriceO().IsValid() && price.AsPriceC().IsValid();
    public static bool IsValid(this IPriceHL price) => price.AsPriceH().IsValid() && price.AsPriceL().IsValid() && price.High >= price.Low;
    public static bool IsValid(this IPriceOHC price) => price.AsPriceOC().IsValid() && price.AsPriceH().IsValid() && price.High >= price.Open && price.High >= price.Close;
    public static bool IsValid(this IPriceOLC price) => price.AsPriceOC().IsValid() && price.AsPriceL().IsValid() && price.Low <= price.Open && price.Low <= price.Close;
    public static bool IsValid(this IPriceOHLC price) => price.AsPriceOHC().IsValid() && price.AsPriceOLC().IsValid() && price.AsPriceHL().IsValid();
}
```
using System;
using System.Collections.Generic;
using LinqTests;

internal static class WithoutLinq
{
    public static IEnumerable<T> Find<T>(this IEnumerable<T> products, Func<T, bool> predicate)
    {
        foreach (var product in products)
        {
            if (predicate(product))
            {
                yield return product;
            }
        }
    }

    public static IEnumerable<T> Find<T>(this IEnumerable<T> products, Func<T, int, bool> predicate)
    {
        var index = 0;

        foreach (var product in products)
        {
            if (predicate(product, index))
            {
                yield return product;
            }

            index++;
        }
    }

    public static IEnumerable<string> CovertHttpToHttps(IEnumerable<string> urls)
    {
        foreach (var url in urls)
        {
            yield return url.Replace("http://", "https://");
        }
    }

    public static IEnumerable<TResult> Map<TSource, TResult>(this IEnumerable<TSource> source,
        Func<TSource, TResult> selector)
    {
        foreach (var item in source)
        {
            yield return selector(item);
        }
    }

    public static IEnumerable<TSource> CashTake<TSource>(IEnumerable<TSource> employees, int count)
    {
        int index = 0;
        var enumerator = employees.GetEnumerator();

        while (enumerator.MoveNext())
        {
            if (index >= count)
            {
                yield break;
            }

            yield return enumerator.Current;

            index++;
        }
    }

    public static IEnumerable<Employee> CashSkip(IEnumerable<Employee> employees, int count)
    {
        int index = 0;
        var enumerator = employees.GetEnumerator();

        while (enumerator.MoveNext())
        {
            if (index >= count)
            {
                yield return enumerator.Current;
            }

            index++;
        }
    }

    public static IEnumerable<int> CashSum(IEnumerable<Employee> employees, int count)
    {
        int sum = 0;
        int index = 1;
        var enumerator = employees.GetEnumerator();

        while (enumerator.MoveNext())
        {
            sum += enumerator.Current.MonthSalary;

            if (index == count)
            {
                yield return sum;
                sum = 0;
                sum += enumerator.Current.MonthSalary;
                index = 1;
            }

            index++;
        }
    }

    public static IEnumerable<TSource> CashTakeWhile<TSource>(IEnumerable<TSource> source, int count,
        Func<TSource, bool> predicate)
    {
        int index = 0;
        var enumerator = source.GetEnumerator();

        while (enumerator.MoveNext())
        {
            if (index >= count)
            {
                yield break;
            }

            if (predicate(enumerator.Current))
            {
                yield return enumerator.Current;
                index++;
            }
        }
    }

    public static IEnumerable<TSource> CashSkipWhile<TSource>(IEnumerable<TSource> source, int count,
        Func<TSource, bool> predicate)
    {
        int index = 0;
        var enumerator = source.GetEnumerator();

        while (enumerator.MoveNext())
        {
            if (index < count && predicate(enumerator.Current))
            {
                index++;
            }
            else
            {
                yield return enumerator.Current;
            }
        }
    }

    public static bool CashAny<TSource>(IEnumerable<TSource> source, Func<TSource, bool> func)
    {
        var enumerator = source.GetEnumerator();

        while (enumerator.MoveNext())
        {
            if (func.Invoke(enumerator.Current))
            {
                return true;
            }
        }

        return false;
    }

    public static bool CashAny<TSource>(IEnumerable<TSource> source)
    {
        var enumerator = source.GetEnumerator();
        return enumerator.MoveNext();
    }

    public static bool CashAll<TSource>(IEnumerable<TSource> source, Func<TSource, bool> func)
    {
        var enumerator = source.GetEnumerator();

        while (enumerator.MoveNext())
        {
            if (!func.Invoke(enumerator.Current))
            {
                return false;
            }
        }

        return true;
    }

    public static TSource CashFirstOrDefault<TSource>(IEnumerable<TSource> source, Func<TSource, bool> func)
    {
        var enumerator = source.GetEnumerator();

        while (enumerator.MoveNext())
        {
            if (func.Invoke(enumerator.Current))
            {
                return enumerator.Current;
            }
        }

        return default(TSource);
    }

    public static TSource CashFirst<TSource>(IEnumerable<TSource> source, Func<TSource, bool> func)
    {
        var enumerator = source.GetEnumerator();

        while (enumerator.MoveNext())
        {
            if (func.Invoke(enumerator.Current))
            {
                return enumerator.Current;
            }
        }

        throw new ArgumentNullException();
    }

    public static TSource CashSingle<TSource>(IEnumerable<TSource> source, Func<TSource, bool> func)
    {
        var enumerator = source.GetEnumerator();

        var e = default(TSource);
        var idx = 0;
        while (enumerator.MoveNext())
        {
            if (func.Invoke(enumerator.Current))
            {
                e = enumerator.Current;
                idx++;
            }

            if (idx > 1)
            {
                throw new InvalidOperationException();
            }
        }

        if (idx == 0)
        {
            throw new InvalidOperationException();
        }

        return e;
    }

    public static TSource CashSingleOrDefault<TSource>(IEnumerable<TSource> source, Func<TSource, bool> func)
    {
        var enumerator = source.GetEnumerator();

        var e = default(TSource);
        var idx = 0;
        while (enumerator.MoveNext())
        {
            if (func.Invoke(enumerator.Current))
            {
                e = enumerator.Current;
                idx++;
            }

            if (idx > 1)
            {
                return default(TSource);
            }
        }

        if (idx == 0)
        {
            return default(TSource);
        }

        return e;
    }

    public static IEnumerable<TSource> CashDistinct<TSource>(IEnumerable<TSource> source,
        IEqualityComparer<TSource> myCompare = null)
    {
        var compare = myCompare ?? EqualityComparer<TSource>.Default;
        var hashSet = new HashSet<TSource>(compare);
        var enumerator = source.GetEnumerator();

        while (enumerator.MoveNext())
        {
            if (hashSet.Add(enumerator.Current))
            {
                yield return enumerator.Current;
            }
        }
    }

    public static IEnumerable<TSource> CashDefaultIfEmpty<TSource>(IEnumerable<TSource> source, TSource defaultInput)
    {
        var enumerator = source.GetEnumerator();

        if (!enumerator.MoveNext())
        {
            yield return defaultInput;
            yield break;
        }

        yield return enumerator.Current;

        while (enumerator.MoveNext())
        {
            yield return enumerator.Current;
        }
    }

    public static bool CashContain<TSource>(IEnumerable<TSource> source, TSource item,
        IEqualityComparer<TSource> myCompare = null)
    {
        var compare = myCompare ?? EqualityComparer<TSource>.Default;
        var enumerator = source.GetEnumerator();

        while (enumerator.MoveNext())
        {
            if (compare.Equals(enumerator.Current, item))
            {
                return true;
            }
        }

        return false;
    }

    public static bool CashSequence<TSource>(IEnumerable<TSource> source, IEnumerable<TSource> anotherSource, IEqualityComparer<TSource> comparer)
    {
        var myComparer = comparer ?? EqualityComparer<TSource>.Default;
        
        var enumerator = source.GetEnumerator();
        var anotherEnumerator = anotherSource.GetEnumerator();

        while (true)
        {
            var sourceHaveNext = enumerator.MoveNext();
            var anotherSourceHaveNext = anotherEnumerator.MoveNext();

            if (sourceHaveNext && anotherSourceHaveNext)
            {
                if (!myComparer.Equals(enumerator.Current, anotherEnumerator.Current))
                {
                    return false;
                } 
            }
            else
            {
                break;
            }
        }

        return enumerator.Current == null && anotherEnumerator.Current == null;
    }
}
using System;
using System.Collections.Generic;
using System.Linq;

internal static class YourOwnLinq
{
    public static IEnumerable<T> CashWhere<T>(this IEnumerable<T> elements, Func<T, bool> predicate)
    {
        foreach (var element in elements)
        {
            if (predicate(element))
            {
                yield return element;
            }
        }
    }

    public static IEnumerable<T> CashWhere<T>(this IEnumerable<T> elements, Func<T, int, bool> predicate)
    {
        var index = 0;

        foreach (var element in elements)
        {
            if (predicate(element, index))
            {
                yield return element;
            }

            index++;
        }
    }

    public static IEnumerable<TResult> CashSelect<TSource, TResult>(this IEnumerable<TSource> source,
        Func<TSource, TResult> selector)
    {
        foreach (var item in source)
        {
            yield return selector(item);
        }
    }

    public static bool CashAny<TSource>(this IEnumerable<TSource> source, Predicate<TSource> predicate)
    {
        foreach (var item in source)
        {
            if (predicate(item))
            {
                return true;
            }
        }

        return false;
    }

    public static bool CashAny<TSource>(this IEnumerable<TSource> source)
    {
        var enumerator = source.GetEnumerator();
        return enumerator.MoveNext();
    }

    public static bool CashAll<TSource>(this IEnumerable<TSource> source, Predicate<TSource> predicate)
    {
        var enumerator = source.GetEnumerator();

        while (enumerator.MoveNext())
        {
            if (!predicate.Invoke(enumerator.Current))
            {
                return false;
            }
        }

        return true;
    }

    public static TSource CashFirstOrDefault<TSource>(this IEnumerable<TSource> source, Predicate<TSource> predicate)
    {
        var enumerator = source.GetEnumerator();

        while (enumerator.MoveNext())
        {
            if (predicate.Invoke(enumerator.Current))
            {
                return enumerator.Current;
            }
        }

        return default(TSource);
    }

    public static TSource CashFirst<TSource>(this IEnumerable<TSource> source, Predicate<TSource> predicate)
    {
        var enumerator = source.GetEnumerator();

        while (enumerator.MoveNext())
        {
            if (predicate.Invoke(enumerator.Current))
            {
                return enumerator.Current;
            }
        }

        throw new ArgumentNullException();
    }

    public static TSource CashSingle<TSource>(this IEnumerable<TSource> source, Predicate<TSource> predicate)
    {
        var enumerator = source.GetEnumerator();

        var e = default(TSource);
        var idx = 0;

        while (enumerator.MoveNext())
        {
            if (predicate.Invoke(enumerator.Current))
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

    public static TSource CashSingleOrDefault<TSource>(this IEnumerable<TSource> source, Predicate<TSource> predicate)
    {
        var enumerator = source.GetEnumerator();

        var e = default(TSource);
        var idx = 0;

        while (enumerator.MoveNext())
        {
            if (predicate.Invoke(enumerator.Current))
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

    public static IEnumerable<TSource> CashDistinct<TSource>(this IEnumerable<TSource> source,
        IEqualityComparer<TSource> myComparer = null)
    {
        var comparer = myComparer ?? EqualityComparer<TSource>.Default;
        var enumerator = source.GetEnumerator();
        var hashSet = new HashSet<TSource>(comparer);

        while (enumerator.MoveNext())
        {
            if (hashSet.Add(enumerator.Current))
            {
                yield return enumerator.Current;
            }
        }
    }

    public static IEnumerable<TSource> CashDefaultIfEmpty<TSource>(this IEnumerable<TSource> source,
        TSource defaultInput)
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

    public static bool CashContain<TSource>(this IEnumerable<TSource> source, TSource item, IEqualityComparer<TSource> comparer = null)
    {
        var myComparer = comparer ?? EqualityComparer<TSource>.Default;

        var enumerator = source.GetEnumerator();

        while (enumerator.MoveNext())
        {
            if (myComparer.Equals(enumerator.Current, item))
            {
                return true;
            }
        }

        return false;
    }

    public static bool CashSequence<TSource>(this IEnumerable<TSource> source, IEnumerable<TSource> anotherSource, IEqualityComparer<TSource> comparer = null)
    {
        var myComparer = comparer ?? EqualityComparer<TSource>.Default;

        var sourceEnumerator = source.GetEnumerator();
        var anotherEnumerator = anotherSource.GetEnumerator();

        while (true)
        {
            var sourceMoveNext = sourceEnumerator.MoveNext();
            var anotherSourceMoveNext = anotherEnumerator.MoveNext();
            
            if (sourceMoveNext && anotherSourceMoveNext)
            {
                if (!myComparer.Equals(sourceEnumerator.Current, anotherEnumerator.Current))
                {
                    return false;
                }
            } 
            else
            {
               break;
            }
        }

        return sourceEnumerator.Current == null && anotherEnumerator.Current == null;
    }
}
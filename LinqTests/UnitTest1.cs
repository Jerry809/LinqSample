using System;
using ExpectedObjects;
using LinqTests;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using Xunit;

namespace LinqTests
{
    public class UnitTest1
    {
        [Fact]
        public void find_products_that_price_between_200_and_500()
        {
            var products = RepositoryFactory.GetProducts();
            var actual = products.Find(product => product.Price > 200 && product.Price < 500);

            var expected = new List<Product>()
            {
                new Product {Id = 2, Cost = 21, Price = 210, Supplier = "Yahoo"},
                new Product {Id = 3, Cost = 31, Price = 310, Supplier = "Odd-e"},
                new Product {Id = 4, Cost = 41, Price = 410, Supplier = "Odd-e"}
            };

            //foreach (var item in actual)
            //{
            //    Console.WriteLine(item.Price);
            //}

            expected.ToExpectedObject().ShouldEqual(actual.ToList());
        }

        [Fact]
        public void find_employee_that_age_more_30()
        {
            var employees = RepositoryFactory.GetEmployees();
            var actual = employees.Find(e => e.Age > 30);

            var expected = new List<Employee>()
            {
                new Employee {Name = "Joe", Role = RoleType.Engineer, MonthSalary = 100, Age = 44, WorkingYear = 2.6},
                new Employee {Name = "Tom", Role = RoleType.Engineer, MonthSalary = 140, Age = 33, WorkingYear = 2.6},
                new Employee {Name = "Kevin", Role = RoleType.Manager, MonthSalary = 380, Age = 55, WorkingYear = 2.6},
                new Employee {Name = "Bas", Role = RoleType.Engineer, MonthSalary = 280, Age = 36, WorkingYear = 2.6},
                new Employee {Name = "Joey", Role = RoleType.Engineer, MonthSalary = 250, Age = 40, WorkingYear = 2.6},
            };

            //foreach (var item in actual)
            //{
            //    Console.WriteLine(item.Price);
            //}

            expected.ToExpectedObject().ShouldEqual(actual.ToList());
        }

        [Fact]
        public void find_employee_that_age_more_30_item_index_more_2()
        {
            var employees = RepositoryFactory.GetEmployees();
            var actual = employees.Find((e, idx) => e.Age > 30 && idx >= 2);

            var expected = new List<Employee>()
            {
                //new Employee {Name = "Joe", Role = RoleType.Engineer, MonthSalary = 100, Age = 44, WorkingYear = 2.6},
                //new Employee {Name = "Tom", Role = RoleType.Engineer, MonthSalary = 140, Age = 33, WorkingYear = 2.6},
                new Employee {Name = "Kevin", Role = RoleType.Manager, MonthSalary = 380, Age = 55, WorkingYear = 2.6},
                //new Employee {Name = "Andy", Role = RoleType.OP, MonthSalary = 80, Age = 22, WorkingYear = 2.6},
                new Employee {Name = "Bas", Role = RoleType.Engineer, MonthSalary = 280, Age = 36, WorkingYear = 2.6},
                //new Employee {Name = "Mary", Role = RoleType.OP, MonthSalary = 180, Age = 26, WorkingYear = 2.6},
                //new Employee {Name = "Frank", Role = RoleType.Engineer, MonthSalary = 120, Age = 16, WorkingYear = 2.6},
                new Employee {Name = "Joey", Role = RoleType.Engineer, MonthSalary = 250, Age = 40, WorkingYear = 2.6},
            };

            //foreach (var item in actual)
            //{
            //    Console.WriteLine(item.Price);
            //}

            expected.ToExpectedObject().ShouldEqual(actual.ToList());
        }

        [Fact]
        public void find_products_using_where_that_price_between_200_and_500()
        {
            var products = RepositoryFactory.GetProducts();
            var actual = products.Where(a => a.Price > 200 && a.Price < 500 && a.Cost > 30);

            var expected = new List<Product>()
            {
                //new Product {Id = 2, Cost = 21, Price = 210, Supplier = "Yahoo"},
                new Product {Id = 3, Cost = 31, Price = 310, Supplier = "Odd-e"},
                new Product {Id = 4, Cost = 41, Price = 410, Supplier = "Odd-e"}
            };

            expected.ToExpectedObject().ShouldEqual(actual.ToList());
        }

        [Fact]
        public void http_convert_to_https()
        {
            var urls = RepositoryFactory.GetUrls();
            var actual = WithoutLinq.CovertHttpToHttps(urls);

            var expected = new List<string>()
            {
                "https://tw.yahoo.com",
                "https://facebook.com",
                "https://twitter.com",
                "https://github.com"
            };

            expected.ToExpectedObject().ShouldEqual(actual.ToList());
        }

        [Fact]
        public void url_length()
        {
            var urls = RepositoryFactory.GetUrls();
            IEnumerable<int> actual = WithoutLinq.Map(urls, url => url.Length);

            var expected = new List<int>()
            {
                19,
                20,
                19,
                17
            };

            expected.ToExpectedObject().ShouldEqual(actual.ToList());
        }

        [Fact]
        public void employee_age_more_25_return_role_name()
        {
            var employees = RepositoryFactory.GetEmployees();
            var actual = employees.CashWhere(a => a.Age < 25).CashSelect(a => $"{a.Role}:{a.Name}");
            var expected = new List<string>()
            {
                "OP:Andy",
                "Engineer:Frank"
            };
            expected.ToExpectedObject().ShouldEqual(actual.ToList());
        }

        [Fact]
        public void get_employee_2()
        {
            var employees = RepositoryFactory.GetEmployees();
            var actual = WithoutLinq.CashTake(employees, 2);
            var expected = new List<Employee>()
            {
                new Employee {Name = "Joe", Role = RoleType.Engineer, MonthSalary = 100, Age = 44, WorkingYear = 2.6},
                new Employee {Name = "Tom", Role = RoleType.Engineer, MonthSalary = 140, Age = 33, WorkingYear = 2.6},
            };
            expected.ToExpectedObject().ShouldEqual(actual.ToList());
        }

        [Fact]
        public void skip_employee()
        {
            var employees = RepositoryFactory.GetEmployees();
            var actual = WithoutLinq.CashSkip(employees, 6);
            var expected = new List<Employee>()
            {
                new Employee {Name = "Frank", Role = RoleType.Engineer, MonthSalary = 120, Age = 16, WorkingYear = 2.6},
                new Employee {Name = "Joey", Role = RoleType.Engineer, MonthSalary = 250, Age = 40, WorkingYear = 2.6},
            };
            expected.ToExpectedObject().ShouldEqual(actual.ToList());
        }

        [Fact]
        public void take_while_employee()
        {
            var employees = RepositoryFactory.GetEmployees();
            var actual = WithoutLinq.CashTakeWhile(employees, 2, a => a.MonthSalary > 150);
            var expected = new List<Employee>()
            {
                new Employee {Name = "Kevin", Role = RoleType.Manager, MonthSalary = 380, Age = 55, WorkingYear = 2.6},
                new Employee {Name = "Bas", Role = RoleType.Engineer, MonthSalary = 280, Age = 36, WorkingYear = 2.6},
            };
            expected.ToExpectedObject().ShouldEqual(actual.ToList());
        }

        [Fact]
        public void skip_while_employee()
        {
            var employees = RepositoryFactory.GetEmployees();
            var actual = WithoutLinq.CashSkipWhile(employees, 3, a => a.MonthSalary < 150);
            var expected = new List<Employee>()
            {
                new Employee {Name = "Kevin", Role = RoleType.Manager, MonthSalary = 380, Age = 55, WorkingYear = 2.6},
                new Employee {Name = "Bas", Role = RoleType.Engineer, MonthSalary = 280, Age = 36, WorkingYear = 2.6},
                new Employee {Name = "Mary", Role = RoleType.OP, MonthSalary = 180, Age = 26, WorkingYear = 2.6},
                new Employee {Name = "Frank", Role = RoleType.Engineer, MonthSalary = 120, Age = 16, WorkingYear = 2.6},
                new Employee {Name = "Joey", Role = RoleType.Engineer, MonthSalary = 250, Age = 40, WorkingYear = 2.6},
            };
            expected.ToExpectedObject().ShouldEqual(actual.ToList());
        }

        [Fact(Skip = "not completed")]
        public void sum_monthsalary()
        {
            var employees = RepositoryFactory.GetEmployees();
            var actual = WithoutLinq.CashSum(employees, 3);
            var expected = new List<int>()
            {
                620,
                540,
                370
            };
            expected.ToExpectedObject().ShouldEqual(actual.ToList());
        }

        [Fact]
        public void TestAny()
        {
            var employees = RepositoryFactory.GetEmployees();
            Assert.False(WithoutLinq.CashAny(employees, a => a.MonthSalary > 500));

            Assert.False(employees.CashAny(a => a.MonthSalary > 500));
        }

        [Fact]
        public void TestAnyIsTrue()
        {
            var employees = RepositoryFactory.GetEmployees();
            Assert.True(WithoutLinq.CashAny(employees, a => a.MonthSalary < 500));

            Assert.True(employees.CashAny(a => a.MonthSalary < 500));
        }

        [Fact]
        public void TestAny_not_any_condition()
        {
            var employees = RepositoryFactory.GetEmployees();
            Assert.True(WithoutLinq.CashAny(employees));

            Assert.True(employees.CashAny());
        }

        [Fact]
        public void TestAll()
        {
            var employees = RepositoryFactory.GetEmployees();
            Assert.False(WithoutLinq.CashAll(employees, a => a.MonthSalary > 200));

            Assert.False(employees.CashAll(a => a.MonthSalary > 200));
        }

        [Fact]
        public void TestAllisTrue()
        {
            var employees = RepositoryFactory.GetEmployees();
            Assert.True(WithoutLinq.CashAll(employees, a => a.MonthSalary > 0));
            Assert.True(employees.CashAll(a => a.MonthSalary > 0));
        }

        [Fact]
        public void TestFirstOrDefault()
        {
            var employees = RepositoryFactory.GetEmployees();
            Assert.Equal("Kevin", WithoutLinq.CashFirstOrDefault(employees, a => a.MonthSalary > 200).Name);
            Assert.Null(WithoutLinq.CashFirstOrDefault(employees, a => a.MonthSalary > 500));

            Assert.Equal("Kevin", employees.CashFirstOrDefault(a => a.MonthSalary > 200).Name);
            Assert.Null(employees.CashFirstOrDefault(a => a.MonthSalary > 500));
        }

        [Fact]
        public void TestFirst()
        {
            var employees = RepositoryFactory.GetEmployees();
            Assert.Equal("Kevin", WithoutLinq.CashFirst(employees, a => a.MonthSalary > 200).Name);
            Assert.Throws<ArgumentNullException>(() => WithoutLinq.CashFirst(employees, a => a.MonthSalary > 500));

            Assert.Equal("Kevin", employees.CashFirst(a => a.MonthSalary > 200).Name);
            Assert.Throws<ArgumentNullException>(() => employees.CashFirst(a => a.MonthSalary > 500));
        }

        [Fact]
        public void TestSinge()
        {
            var employees = RepositoryFactory.GetEmployees();
            Assert.Equal(RoleType.Manager, WithoutLinq.CashSingle(employees, a => a.Role == RoleType.Manager).Role);

            Assert.Equal(RoleType.Manager, employees.CashSingle(a => a.Role == RoleType.Manager).Role);
        }

        [Fact]
        public void TestSingeOrDefault()
        {
            var employees = RepositoryFactory.GetEmployees();
            Assert.Equal(RoleType.Manager,
                WithoutLinq.CashSingleOrDefault(employees, a => a.Role == RoleType.Manager).Role);

            Assert.Equal(RoleType.Manager, employees.CashSingleOrDefault(a => a.Role == RoleType.Manager).Role);
        }

        [Fact]
        public void TestSingle_not_data()
        {
            var employees = RepositoryFactory.GetEmployees();

            Assert.Throws<InvalidOperationException>(() =>
                WithoutLinq.CashSingle(employees, a => a.Role == RoleType.Unknown));
            Assert.Throws<InvalidOperationException>(() => employees.CashSingle(a => a.Role == RoleType.Unknown));
        }

        [Fact]
        public void TestSingleOrDefault_not_data()
        {
            var employees = RepositoryFactory.GetEmployees();

            Assert.Null(WithoutLinq.CashSingleOrDefault(employees, a => a.Role == RoleType.Unknown));
            Assert.Null(employees.CashSingleOrDefault(a => a.Role == RoleType.Unknown));
        }

        [Fact]
        public void TestSingle_more_then_one_data()
        {
            var employees = RepositoryFactory.GetEmployees();

            Assert.Throws<InvalidOperationException>(() =>
                WithoutLinq.CashSingle(employees, a => a.Role == RoleType.Engineer));
            Assert.Throws<InvalidOperationException>(() => employees.CashSingle(a => a.Role == RoleType.Engineer));
        }

        [Fact]
        public void TestSingleOrDefault_more_then_one_data()
        {
            var employees = RepositoryFactory.GetEmployees();

            Assert.Null(WithoutLinq.CashSingleOrDefault(employees, a => a.Role == RoleType.Engineer));
            Assert.Null(employees.CashSingleOrDefault(a => a.Role == RoleType.Engineer));
        }

        [Fact]
        public void TestDistinct()
        {
            var expected = new List<Employee>
            {
                new Employee {Name = "Joe", Role = RoleType.Engineer, MonthSalary = 100, Age = 44, WorkingYear = 2.6},
                new Employee {Name = "Kevin", Role = RoleType.Manager, MonthSalary = 380, Age = 55, WorkingYear = 2.6},
                new Employee {Name = "Andy", Role = RoleType.OP, MonthSalary = 80, Age = 22, WorkingYear = 2.6},
            }.ToExpectedObject();
            
            expected.ShouldEqual(WithoutLinq.CashDistinct(RepositoryFactory.GetEmployees(), new MyCompareRole()).ToList());     
            expected.ShouldEqual(RepositoryFactory.GetEmployees().CashDistinct(new MyCompareRole()).ToList());     
        }
    }
}

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

    public static IEnumerable<TSource> CashDistinct<TSource>(IEnumerable<TSource> source, IEqualityComparer<TSource> myCompare = null)
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
}

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

    public static IEnumerable<TSource> CashDistinct<TSource>(this IEnumerable<TSource> source, IEqualityComparer<TSource> myComparer = null)
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
}

public class MyCompareRole : IEqualityComparer<Employee>
{
    public bool Equals(Employee x, Employee y)
    {
        return x.Role == y.Role;
    }

    public int GetHashCode(Employee obj)
    {
        return obj.Role.GetHashCode();
    }
}
using System;
using ExpectedObjects;
using LinqTests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using NSubstitute;

namespace LinqTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
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

        [TestMethod]
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

        [TestMethod]
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

        [TestMethod]
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

        [TestMethod]
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

        [TestMethod]
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

        [TestMethod]
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

        [TestMethod]
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

        [TestMethod]
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

        [TestMethod]
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

        [Ignore]
        [TestMethod]
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
}
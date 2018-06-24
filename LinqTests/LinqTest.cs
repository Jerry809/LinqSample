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
    public class LinqTest
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

            expected.ShouldEqual(WithoutLinq.CashDistinct(RepositoryFactory.GetEmployees(), new MyCompareRole())
                .ToList());
            expected.ShouldEqual(RepositoryFactory.GetEmployees().CashDistinct(new MyCompareRole()).ToList());
        }

        [Fact]
        public void TestDefaultIfEmpty_not_data()
        {
            var defaultInput = new Employee
            {
                Name = "Cash"
            };

            var expected = new List<Employee>
            {
                defaultInput
            };

            var younger = RepositoryFactory.GetEmployees().Where(a => a.Age <= 15);

            expected.ToExpectedObject().ShouldEqual(WithoutLinq.CashDefaultIfEmpty(younger, defaultInput).ToList());
            expected.ToExpectedObject().ShouldEqual(younger.CashDefaultIfEmpty(defaultInput).ToList());
        }

        [Fact]
        public void TestDefaultIfEmpty_have_data()
        {
            var defaultInput = new Employee
            {
                Name = "Cash"
            };

            var expected = new List<Employee>
            {
                new Employee {Name = "Joe", Role = RoleType.Engineer, MonthSalary = 100, Age = 44, WorkingYear = 2.6},
                new Employee {Name = "Kevin", Role = RoleType.Manager, MonthSalary = 380, Age = 55, WorkingYear = 2.6},
            };

            var younger = RepositoryFactory.GetEmployees().Where(a => a.Age > 40);

            expected.ToExpectedObject().ShouldEqual(WithoutLinq.CashDefaultIfEmpty(younger, defaultInput).ToList());
            expected.ToExpectedObject().ShouldEqual(younger.CashDefaultIfEmpty(defaultInput).ToList());
        }

        [Fact]
        public void TestContains_have_item()
        {
            var products = RepositoryFactory.GetProducts();

            var product = new Product
            {
                Id = 4,
                Cost = 41,
                Price = 410,
                Supplier = "Odd-e"
            };

            Assert.True(WithoutLinq.CashContain(products, product, new MyCompareProduct()));
            
            Assert.True(products.CashContain(product, new MyCompareProduct()));
        }
        
        [Fact]
        public void TestContains_not_item()
        {
            var products = RepositoryFactory.GetProducts();

            var product = new Product();

            Assert.False(WithoutLinq.CashContain(products, product, new MyCompareProduct()));
            
            Assert.False(products.CashContain(product, new MyCompareProduct()));
        }

        [Fact]
        public void TestSequence_item_have_one_not_equal()
        {
            var products = RepositoryFactory.GetProducts();
            var anotherProducts = RepositoryFactory.GetAnotherProducts();
            
            Assert.False(WithoutLinq.CashSequence(products, anotherProducts, new MyCompareProduct()));
            Assert.False(products.CashSequence(anotherProducts, new MyCompareProduct()));
        }
        
        [Fact]
        public void TestSequence_item_more_than_one()
        {
            var products = RepositoryFactory.GetProducts();
            var anotherProducts = RepositoryFactory.GetProductsS();
            
            Assert.False(WithoutLinq.CashSequence(products, anotherProducts, new MyCompareProduct()));
            Assert.False(products.CashSequence(anotherProducts, new MyCompareProduct()));
        }
        
        [Fact]
        public void TestSequence_equal()
        {
            var products = RepositoryFactory.GetProducts();
            var anotherProducts = RepositoryFactory.GetProducts();
            
            Assert.True(WithoutLinq.CashSequence(products, anotherProducts, new MyCompareProduct()));
            Assert.True(products.CashSequence(anotherProducts, new MyCompareProduct()));
        }
        
        [Fact]
        public void TestSequence_source_more_than_one()
        {
            var products = RepositoryFactory.GetProductsS();
            var anotherProducts = RepositoryFactory.GetProducts();
            
            Assert.False(WithoutLinq.CashSequence(products, anotherProducts, new MyCompareProduct()));
            Assert.False(products.CashSequence(anotherProducts, new MyCompareProduct()));
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

public class MyCompareProduct : IEqualityComparer<Product>
{
    public bool Equals(Product x, Product y)
    {
        return x.Cost == y.Cost
               && x.Id == y.Id
               && x.Price == y.Price
               && x.Supplier == y.Supplier;
    }

    public int GetHashCode(Product obj)
    {
        return obj.GetHashCode();
    }
}
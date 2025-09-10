using static Assignment_Session02_LINQ.ListGenerator;

namespace Assignment_Session02_LINQ
{
    internal class Program
    {
        static void Main(string[] args)
        {
            #region LINQ - Aggregate Operators

            #region 1.Get the total units in stock for each product category.
            // Fluent Syntax
            var Result = ProductsList
                .GroupBy(p => p.Category)
                .Select(g => new
                {
                    Category = g.Key,
                    TotalUnitsInStock = g.Sum(p => p.UnitsInStock)
                });

            // Query Syntax
            Result = from p in ProductsList
                          group p by p.Category 
                          into g
                          select new
                          {
                              Category = g.Key,
                              TotalUnitsInStock = g.Sum(p => p.UnitsInStock)
                          };

            foreach (var item in Result)
                Console.WriteLine($"Category: {item.Category}, Total Units In Stock: {item.TotalUnitsInStock}");
            Console.WriteLine("--------------------------------------------------");
            #endregion

            #region 2.Get the cheapest price among each category's products

            // Fluent Syntax
            var Result01 = ProductsList
                .GroupBy(p => p.Category)
                .Select(g => new
                {
                    Category = g.Key,
                    CheapestPrice = g.Min(p => p.UnitPrice)
                });

            // Query Syntax
            Result01 = from p in ProductsList
                       group p by p.Category
                       into g
                       select new
                       {
                           Category = g.Key,
                           CheapestPrice = g.Min(p => p.UnitPrice)
                       };

            foreach (var item in Result01)
                Console.WriteLine($"Category: {item.Category}, Cheapest Price: {item.CheapestPrice}");
            Console.WriteLine("--------------------------------------------------");
            #endregion

            #region 3.Get the products with the cheapest price in each category(Use Let)

            // Query Syntax
            var Result02 = from p in ProductsList
                       group p by p.Category
                       into g
                       let cheapestPrice = g.Min(p => p.UnitPrice)
                       select new
                       {
                           Category = g.Key,
                           CheapestPrice = cheapestPrice,
                           Products = g.Where(p => p.UnitPrice == cheapestPrice)
                       };

            foreach (var item in Result02)
            {
                Console.WriteLine($"Category: {item.Category}, Cheapest Price: {item.CheapestPrice}");
                foreach (var product in item.Products)
                    Console.WriteLine($"Products: {product.ProductName}");
                Console.WriteLine();
            }
            Console.WriteLine("--------------------------------------------------");
            #endregion

            #region 4.Get the most expensive price among each category's products.

            // Fluent Syntax
            var Result03 = ProductsList
                .GroupBy(p => p.Category)
                .Select(g => new
                {
                    Category = g.Key,
                    MostExpensivePrice = g.Max(p => p.UnitPrice)
                });

            // Query Syntax
            Result03 = from p in ProductsList
                       group p by p.Category
                       into g
                       select new
                       {
                           Category = g.Key,
                           MostExpensivePrice = g.Max(p => p.UnitPrice)
                       };

            foreach (var item in Result03)
                Console.WriteLine($"Category: {item.Category}, Most Expensive Price: {item.MostExpensivePrice}");
            Console.WriteLine("--------------------------------------------------");
            #endregion

            #region 5.Get the products with the most expensive price in each category.

            // Query Syntax
            var Result04 = from p in ProductsList
                           group p by p.Category
                       into g
                           let MostExpensivePrice = g.Max(p => p.UnitPrice)
                           select new
                           {
                               Category = g.Key,
                               MostExpensivePrice = MostExpensivePrice,
                               Products = g.Where(p => p.UnitPrice == MostExpensivePrice)
                           };

            foreach (var item in Result04)
            {
                Console.WriteLine($"Category: {item.Category}, Most Expensive Price: {item.MostExpensivePrice}");
                foreach (var product in item.Products)
                    Console.WriteLine($"Products: {product.ProductName}");
                Console.WriteLine();
            }
            Console.WriteLine("--------------------------------------------------");
            #endregion

            #region 6.Get the average price of each category's products.
            // Fluent Syntax
            var Result05 = ProductsList
                .GroupBy(p => p.Category)
                .Select(g => new
                {
                    Category = g.Key,
                    AveragePrice = g.Average(p => p.UnitPrice)
                });

            // Query Syntax
            Result05 = from p in ProductsList
                       group p by p.Category
                       into g
                       select new
                       {
                           Category = g.Key,
                           AveragePrice = g.Average(p => p.UnitPrice)
                       };

            foreach (var item in Result05)
                Console.WriteLine($"Category: {item.Category}, Average Price: {item.AveragePrice}");
            Console.WriteLine("--------------------------------------------------");
            #endregion

            #endregion

            #region LINQ - Set Operators

            #region 1.Find the unique Category names from Product List

            var Result1 = ProductsList
                .Select(p => p.Category)
                .Distinct();

            foreach (var item in Result1)
                Console.WriteLine(item);
            Console.WriteLine("--------------------------------------------------");

            #endregion

            #region 2.Produce a Sequence containing the unique first letter from both product and customer names.

            var ProductFirstLetters = ProductsList.Select(p => p.ProductName[0]);
            var CustomerFirstLetters = CustomersList.Select(c => c.CustomerName[0]);
            
            var UniqueFirstLetters = ProductFirstLetters.Union(CustomerFirstLetters);
            
            foreach (var item in UniqueFirstLetters)
                Console.WriteLine(item);
            Console.WriteLine("--------------------------------------------------");
            #endregion

            #region 3.Create one sequence that contains the common first letter from both product and customer names.

            var CommonFirstLetters = ProductFirstLetters.Intersect(CustomerFirstLetters);

            foreach (var item in CommonFirstLetters)
                Console.WriteLine(item);
            #endregion

            #region 4.Create one sequence that contains the first letters of product names that are not also first letters of customer names.

            var ProductOnlyFirstLetters = ProductFirstLetters.Except(CustomerFirstLetters);

            foreach (var item in ProductOnlyFirstLetters)
                Console.WriteLine(item);
            #endregion

            #region 5.Create one sequence that contains the last Three Characters in each name of all customers and products, including any duplicates

            var ProductLastThreeChars = ProductsList.Select(p => p.ProductName.Length >= 3 ? p.ProductName.Substring(p.ProductName.Length - 3) : p.ProductName);
            var CustomerLastThreeChars = CustomersList.Select(c => c.CustomerName.Length >= 3 ? c.CustomerName.Substring(c.CustomerName.Length - 3) : c.CustomerName);
            
            var AllLastThreeChars = ProductLastThreeChars.Concat(CustomerLastThreeChars);
            
            foreach (var item in AllLastThreeChars)
                Console.WriteLine(item);
            Console.WriteLine("--------------------------------------------------");
            #endregion

            #endregion

            #region LINQ - Partitioning Operators

            #region 1.Get the first 3 orders from customers in Washington

            var Result06 = CustomersList
                .Where(c => c.Region == "WA")
                .SelectMany(c => c.Orders)
                .Take(3);

            foreach (var item in Result06)
                Console.WriteLine($"OrderID: {item.OrderID}, OrderDate: {item.OrderDate}, Total: {item.Total}");
            Console.WriteLine("--------------------------------------------------");
            #endregion

            #region 2.Get all but the first 2 orders from customers in Washington.

            var Result07 = CustomersList
                .Where(c => c.Region == "WA")
                .SelectMany(c => c.Orders)
                .Skip(2);

            foreach (var item in Result07)
                Console.WriteLine($"OrderID: {item.OrderID}, OrderDate: {item.OrderDate}, Total: {item.Total}");
            Console.WriteLine("--------------------------------------------------");
            #endregion

            #region 3.Return elements starting from the beginning of the array until a number is hit that is less than its position in the array.

            int[] numbers = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };

            var Result08 = numbers.TakeWhile((n, index) => n >= index);
            
            foreach (var item in Result08)
                Console.WriteLine(item);
            Console.WriteLine("--------------------------------------------------");
            #endregion

            #region 4.Get the elements of the array starting from the first element divisible by 3.
            // use numbers
            var Result09 = numbers.SkipWhile(n => n % 3 != 0);
            
            foreach (var item in Result09)
                Console.WriteLine(item);
            Console.WriteLine("--------------------------------------------------");
            #endregion

            #region 5.Get the elements of the array starting from the first element less than its position.
            // use numbers
            var Result10 = numbers.SkipWhile((n, index) => n >= index);

            foreach (var item in Result10)
                Console.WriteLine(item);
            Console.WriteLine("--------------------------------------------------");
            #endregion

            #endregion

            #region LINQ - Quantifiers

            #region 1.Determine if any of the words in dictionary_english.txt(Read dictionary_english.txt into Array of String First) contain the substring 'ei'.

            string[] dictionary = System.IO.File.ReadAllLines("dictionary_english.txt");
            var containsEi = dictionary.Any(word => word.Contains("ei"));
            
            var containsEiWords = dictionary.Where(word => word.Contains("ei"));
            foreach (var word in containsEiWords)
                Console.WriteLine(word);
            Console.WriteLine("--------------------------------------------------");

            #endregion

            #region 2.Return a grouped a list of products only for categories that have at least one product that is out of stock.

            var Result11 = ProductsList
                .GroupBy(p => p.Category)
                .Where(g => g.Any(p => p.UnitsInStock == 0))
                .Select(g => new
                {
                    Category = g.Key,
                    Products = g.ToList()
                });

            foreach (var item in Result11)
            {
                Console.WriteLine($"Category: {item.Category}");
                foreach (var product in item.Products)
                    Console.WriteLine($"Products: {product.ProductName}, Units In Stock: {product.UnitsInStock}");

                Console.WriteLine();
            }
            Console.WriteLine("--------------------------------------------------");
            #endregion

            #region 3.Return a grouped a list of products only for categories that have all of their products in stock.

            var Result12 = ProductsList
                .GroupBy(p => p.Category)
                .Where(g => g.All(p => p.UnitsInStock > 0))
                .Select(g => new
                {
                    Category = g.Key,
                    Products = g.ToList()
                });

            foreach (var item in Result12)
            {
                Console.WriteLine($"Category: {item.Category}");
                foreach (var product in item.Products)
                    Console.WriteLine($"Products: {product.ProductName}, Units In Stock: {product.UnitsInStock}");
                Console.WriteLine();
            }
            Console.WriteLine("--------------------------------------------------");
            #endregion

            #endregion

            #region LINQ – Grouping Operators

            #region 1. Use group by to partition a list of numbers by their remainder when divided by 5
            List<int> PartitionNumbers = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 };

            var Result13 = PartitionNumbers
                .GroupBy(n => n % 5)
                .Select(g => new
                {
                    Remainder = g.Key,
                    Numbers = g.ToList()
                });

            foreach (var item in Result13)
                {
                Console.WriteLine($"Numbers with a remainder of {item.Remainder} when divided by 5:");
                foreach (var number in item.Numbers)
                    Console.WriteLine(number);
                Console.WriteLine();
            }
            Console.WriteLine("--------------------------------------------------");
            #endregion

            #region 2. Uses group by to partition a list of words by their first letter.
            //Use dictionary_english.txt for Input

            var Result14 = dictionary
                .GroupBy(word => word[0])
                .Select(g => new
                {
                    FirstLetter = g.Key,
                    Words = g.ToList()
                });

            foreach (var item in Result14)
            {
                Console.WriteLine($"Words that start with the letter '{item.FirstLetter}':");
                foreach (var word in item.Words)
                    Console.WriteLine(word);
                Console.WriteLine();
            }
            Console.WriteLine("--------------------------------------------------");
            #endregion

            #region 3. Consider this Array as an Input
            string[] Arr = { "from", "salt", "earn", "last", "near", "form" };
           // Use Group By with a custom comparer that matches words that are consists of the same Characters Together.

            var Result15 = Arr
                .GroupBy(word => string.Concat(word.OrderBy(c => c)))
                .Select(g => new
                {
                    Key = g.Key,
                    Words = g.ToList()
                });

            foreach (var item in Result15)
                {
                Console.WriteLine($"Words with the same characters as '{item.Key}':");
                foreach (var word in item.Words)
                    Console.WriteLine(word);
                Console.WriteLine();
            }
            Console.WriteLine("--------------------------------------------------");
            #endregion

            #endregion
        }
    }
}

using Loans.Domain.Applications;
using NUnit.Framework;
using System.Collections.Generic;

namespace Loans.Tests
{
    [TestFixture]
    public class ProductComparerShould
    {
        [Test]
        public void ReturnCorrectNumberOfComparisons()
        {
            var products = new List<LoanProduct>
            {
                new LoanProduct(1, "a", 1),
                new LoanProduct(2, "b", 2),
                new LoanProduct(3, "c", 3),
            };

            var sut = new ProductComparer(new LoanAmount("USD", 200_000m), products);
            List<MonthlyRepaymentComparison> comparisons =
                sut.CompareMonthlyRepayments(new LoanTerm(30));

            /**
             * The first kind of assert we can make against the collection is that they
             * contain the required number of items. Since we got 3 items in the products
             * list, then we expect 3 in the comparison output.
             * Now, we are going to use the Has helper class. This has convience helper
             * methods to help us quickly create constraint instances.
             */
            Assert.That(comparisons, Has.Exactly(3).Items);
        }

        [Test]
        public void NotReturnDuplicateComparisons()
        {
            var products = new List<LoanProduct>
            {
                new LoanProduct(1, "a", 1),
                new LoanProduct(2, "b", 2),
                new LoanProduct(3, "c", 3),
            };

            var sut = new ProductComparer(new LoanAmount("USD", 200_000m), products);
            List<MonthlyRepaymentComparison> comparisons =
                sut.CompareMonthlyRepayments(new LoanTerm(30));

            /**
             * Assert that the collection is unique
             */
            Assert.That(comparisons, Is.Unique);
        }

        [Test]
        public void ReturnComparisonForFirstProduct()
        {
            var products = new List<LoanProduct>
            {
                new LoanProduct(1, "a", 1),
                new LoanProduct(2, "b", 2),
                new LoanProduct(3, "c", 3),
            };

            var sut = new ProductComparer(new LoanAmount("USD", 200_000m), products);
            List<MonthlyRepaymentComparison> comparisons =
                sut.CompareMonthlyRepayments(new LoanTerm(30));

            /**
             * Assert that a specific collection exists. 
             * We need to know the expected monthly repayment (643.28)
             */
            var expectedProduct = new MonthlyRepaymentComparison("a", 1, 643.28m);
            Assert.That(comparisons, Does.Contain(expectedProduct));
        }

        [Test]
        public void ReturnComparisonForFirstProduct_WithPartialKnownExpectedValues()
        {
            var products = new List<LoanProduct>
            {
                new LoanProduct(1, "a", 1),
                new LoanProduct(2, "b", 2),
                new LoanProduct(3, "c", 3),
            };

            var sut = new ProductComparer(new LoanAmount("USD", 200_000m), products);
            List<MonthlyRepaymentComparison> comparisons =
                sut.CompareMonthlyRepayments(new LoanTerm(30));

            /**
             * Assert that a specific collection exists. 
             * We don't care about the expected monthly repayment value. We will be 
             * creating a constraint based on a specified property.
             */            
            Assert.That(comparisons, Has.Exactly(1)
                .Property("ProductName").EqualTo("a")
                .And
                .Property("InterestRate").EqualTo(1)
                .And
                .Property("MonthlyRepayment").GreaterThan(0));

            /**
             * A more type-safe approach
             */
            Assert.That(comparisons, Has.Exactly(1)
                .Matches<MonthlyRepaymentComparison>(item =>
                item.ProductName == "a" && 
                item.InterestRate == 1 && 
                item.MonthlyRepayment > 0));
        }
    }
}

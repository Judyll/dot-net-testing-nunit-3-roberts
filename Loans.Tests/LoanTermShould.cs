using Loans.Domain.Applications;
using NUnit.Framework;

namespace Loans.Tests
{
    /**
     * Indicate to NUnit that this class is a test class and contains test methods
     * by adding an NUnit attribute called TextFixture
     */
    [TestFixture]
    public class LoanTermShould
    {
        /**
         * To tell NUnit that this method contains test codes, then we need to add
         * another NUnit attribute called Test
         */
        [Test]
        public void ReturnTermInMonths()
        {
            /**
             * sut stands for System Under Test
             */
            var sut = new LoanTerm(1);

            Assert.That(sut.ToMonths(), Is.EqualTo(12));

            /**
             * Using the logical Arrange, Act, Assert Test Phase
             */

            // Arrange
            // var sut = new LoanTerm(1);

            // Act
            // var numberOfMonths = sut.ToMonths();

            // Assert
            // Assert.That(numberOfMonths, Is.EqualTo(12));
        }

        [Test]
        public void StoreYears()
        {
            var sut = new LoanTerm(1);

            /**
             * We want to check if the Years property has been set properly.
             * We are now skipping the "Act" logical phase.
             */
            Assert.That(sut.Years, Is.EqualTo(1));
        }
    }
}

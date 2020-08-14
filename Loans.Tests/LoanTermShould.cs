using Loans.Domain.Applications;
using NUnit.Framework;
using System;
using System.Collections.Generic;

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
        [Ignore("Need to complete the update first.")]
        public void ReturnTermInMonths()
        {
            /**
             * sut stands for System Under Test
             */
            var sut = new LoanTerm(1);

            Assert.That(sut.ToMonths(), Is.EqualTo(12), "Months should be 12 * number of years");

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

        [Test]
        public void RespectValueEquality()
        {
            /**
             * LoanTerm inherits from ValueObject class. Ordinarily, to reference types,
             * even if they have the same values, won't be considered equal by the
             * Is.EqualTo method. But, because on the ValueObject class, the "Equals"
             * method has been overridden with some custom logic, that makes use of
             * this GetAtomicValues method. And if we will look into the LoanTerm class,
             * this GetAtomicValues has been overridden and it's returning the Years
             * property value. So, essentially, in our Assert.That.Is.EqualTo assert,
             * if the LoanTerm have the same values for the Years property, then they
             * will be considered equal.
             */
            var a = new LoanTerm(1);
            var b = new LoanTerm(1);

            Assert.That(a, Is.EqualTo(b));
        }

        [Test]
        public void RespectValueInequality()
        {
            var a = new LoanTerm(1);
            var b = new LoanTerm(2);

            Assert.That(a, Is.Not.EqualTo(b));
        }

        [Test]
        public void ReferenceEqualityExample()
        {
            var a = new LoanTerm(1);
            var b = a;
            var c = new LoanTerm(1);

            /**
             * We want to assert that the variables a and b points to the same object
             * in memory.
             */
            Assert.That(a, Is.SameAs(b));
            Assert.That(a, Is.Not.SameAs(c));

            /**
             * SameAs is only concerned with references and not values
             */
            var x = new List<string> { "a", "b" };
            var y = x;
            var z = new List<string> { "a", "b" };

            Assert.That(x, Is.SameAs(y));
            Assert.That(z, Is.Not.SameAs(x));
        }

        [Test]
        public void Double()
        {
            double a = 1.0 / 3.0;

            /**
             * Use Within to specify tolerance that NUnit will use when determining
             * equality.
             */
            Assert.That(a, Is.EqualTo(0.33).Within(0.004));
            /**
             * In addition to specifying a fixed value for the tolerance, we can specify
             * it as a percentage. So, in this example, we are happy to accept the tolerance
             * of 10%.
             */
            Assert.That(a, Is.EqualTo(0.33).Within(10).Percent);
        }

        [Test]
        public void NotAllowZeroYears()
        {
            /**
             * The first parameter is an action that gets executed and should cause
             * an exception to be thrown.
             * In our case, the action would be to create a new LoanTerm instance
             * with the value of 0 years.
             * The second parameter allows us to specify that this action throws an
             * exception.
             */
            Assert.That(() => new LoanTerm(0), Throws.TypeOf<ArgumentOutOfRangeException>());
            /**
             * This uses the Matches method and get the value of
             * the property ParamName of the class ArgumentOutOfRangeException
             */
            Assert.That(() => new LoanTerm(0), Throws.TypeOf<ArgumentOutOfRangeException>()
                .With
                .Matches<ArgumentOutOfRangeException>(ex => ex.ParamName == "years"));
            /**
             * Assert using the message being outputted
             */
            Assert.That(() => new LoanTerm(0), Throws.TypeOf<ArgumentOutOfRangeException>()
                .With
                .Message
                .EqualTo("Please specify a value greater than 0. (Parameter 'years')"));
        }
    }
}

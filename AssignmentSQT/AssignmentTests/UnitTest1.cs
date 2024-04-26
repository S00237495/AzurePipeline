using NUnit.Framework;
using Moq;
using AssignmentTests;
using AssignmentSQT;


namespace AssignmentTests
{
    [TestFixture]
    public class InsuranceServiceTests
    {
        private InsuranceService _insuranceService;

        [SetUp]
        public void SetUp()
        {
            // Create a mock discount service for testing
            var mockDiscountService = new Mock<IDiscountService>();
            mockDiscountService.Setup(ds => ds.GetDiscount()).Returns(0.9); // 10% discount for testing
            _insuranceService = new InsuranceService(mockDiscountService.Object);
        }

        [Test]
        public void TestRuralAgeLessThan18()
        {
            double premium = _insuranceService.CalcPremium(17, "rural");
            Assert.That(premium, Is.EqualTo(0.0));
        }

        [Test]
        public void TestRuralAgeBetween18And29()
        {
            double premium = _insuranceService.CalcPremium(25, "rural");
            Assert.That(premium, Is.EqualTo(5.0));
        }

        [Test]
        public void TestRuralAgeEqual30()
        {
            double premium = _insuranceService.CalcPremium(30, "rural");
            Assert.That(premium, Is.EqualTo(5.0));
        }

        [Test]
        public void TestRuralAgeGreaterThanOrEqualTo31()
        {
            double premium = _insuranceService.CalcPremium(35, "rural");
            Assert.That(premium, Is.EqualTo(2.50));
        }

        [Test]
        public void TestUrbanAgeLessThan18()
        {
            double premium = _insuranceService.CalcPremium(17, "urban");
            Assert.That(premium, Is.EqualTo(0.0));
        }

        [Test]
        public void TestUrbanAgeBetween18And35()
        {
            double premium = _insuranceService.CalcPremium(25, "urban");
            Assert.That(premium, Is.EqualTo(6.0));
        }

        [Test]
        public void TestUrbanAgeEqual36()
        {
            double premium = _insuranceService.CalcPremium(36, "urban");
            Assert.That(premium, Is.EqualTo(5.0));
        }

        [Test]
        public void TestUrbanAgeGreaterThan36()
        {
            double premium = _insuranceService.CalcPremium(40, "urban");
            Assert.That(premium, Is.EqualTo(5.0));
        }

        [Test]
        public void TestUnknownLocationAgeLessThan18()
        {
            double premium = _insuranceService.CalcPremium(17, "unknown");
            Assert.That(premium, Is.EqualTo(0.0));
        }

        [Test]
        public void TestUnknownLocationAgeGreaterThanOrEqualTo18()
        {
            double premium = _insuranceService.CalcPremium(25, "unknown");
            Assert.That(premium, Is.EqualTo(0.0));
        }

        [Test]
        public void TestAgeGreaterThanEqual50WithDiscount()
        {
            double premium = _insuranceService.CalcPremium(55, "rural");
            Assert.That(premium, Is.EqualTo(2.25)); // 10% discount applied
        }
    }
}
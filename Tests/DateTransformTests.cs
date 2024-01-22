using CoreLogic.Contracts;
using CoreLogic.Transform;

namespace Tests
{
    public class DateTransformTests
    {
        private IDateTransform _dateTransform;
        [SetUp]
        public void Setup()
        {
            _dateTransform = new DateTransform();
        }

        [Test]
        public async Task MonthYearToDate1st_ValidInput_ReturnsCorrectDate()
        {
            var result = await _dateTransform.MonthYearToDate1st("JAN22");
            Assert.That(result, Is.EqualTo(new DateOnly(2022, 1, 1)));

            result = await _dateTransform.MonthYearToDate1st("DEC99");
            Assert.That(result, Is.EqualTo(new DateOnly(1999, 12, 1)));
        }

        [Test]
        [TestCase("Jan")]
        [TestCase("XYZ22")]
        [TestCase("J2022")]
        public void MonthYearToDate1st_InvalidMonth_ThrowsArgumentException(string input)
        {
            var ex = Assert.ThrowsAsync<ArgumentException>(async () => await _dateTransform.MonthYearToDate1st(input));
            Assert.That(ex.ParamName, Is.EqualTo("monthYear"));
        }

        [Test]
        public async Task MonthYearToDate1st_EdgeCaseYear_ReturnsCorrectDate()
        {
            var currentYearLastTwoDigits = DateTime.Now.Year % 100;
            var testYear = currentYearLastTwoDigits < 50 ? currentYearLastTwoDigits + 50 : currentYearLastTwoDigits - 50;
            var yearString = testYear.ToString("00");

            var result = await _dateTransform.MonthYearToDate1st($"JAN{yearString}");
            var expectedYear = testYear + (testYear <= currentYearLastTwoDigits ? 2000 : 1900);
            Assert.AreEqual(new DateOnly(expectedYear, 1, 1), result);
        }
    }
}
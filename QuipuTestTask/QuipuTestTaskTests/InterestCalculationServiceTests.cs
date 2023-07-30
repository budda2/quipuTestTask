using QuipuTestTask.Services;

namespace QuipuTestTaskTests;

[TestFixture]
public class InterestCalculationServiceTests
{
    private InterestCalculationService _calculationService;
    
    [SetUp]
    public void Setup()
    {
        _calculationService = new InterestCalculationService();
    }

    [TestCaseSource(nameof(InterestCases))]
    public void InterestService_Should_PerformInterestCalculation(decimal initialAmount,
        int numberOfIterations, decimal interestRate, decimal expectedResult)
    {
        var calculationResult =
            _calculationService.CalculateInterest(initialAmount, interestRate, numberOfIterations, 1);

        Assert.That(Decimal.Abs(calculationResult - expectedResult), Is.LessThan(0.000001m),
            $"Expected result was {expectedResult:N6}, but received {calculationResult:N7}");
    }

    public static object[] InterestCases =
    {
        new object[] { 1000m, 1, 0.1m, 1100m },
        new object[] { 1000m, 2, 0.1m, 1200m },
        new object[] { 1000m, 4, 0.1m, 1400m },
        new object[] { 1000m, 8, 0.1m, 1800m },
        new object[] { 1000m, 16, 0.1m, 2600m },
        new object[] { 1000m, 32, 0.1m, 4200m },
        new object[] { 1000m, 64, 0.1m, 7400m },
    };
}
using QuipuTestTask.Services;

namespace QuipuTestTaskTests;

[TestFixture]
public class CompoundInterestCalculationServiceTests
{
    private CompoundInterestCalculationService _calculationService;
    
    [SetUp]
    public void Setup()
    {
        _calculationService = new CompoundInterestCalculationService();
    }

    [TestCaseSource(nameof(CompoundInterestCases))]
    public void CompoundInterestService_Should_UseCompoundInterestForCalculation(decimal initialAmount,
        int numberOfIterations, decimal interestRate, decimal expectedResult)
    {
        var calculationResult =
            _calculationService.CalculateInterest(initialAmount, interestRate, numberOfIterations, 1);

        Assert.That(Decimal.Abs(calculationResult - expectedResult), Is.LessThan(0.000001m),
            $"Expected result was {expectedResult:N6}, but received {calculationResult:N6}");
    }

    public static object[] CompoundInterestCases =
    {
        new object[] { 1000m, 1, 0.1m, 1100m },
        new object[] { 1000m, 2, 0.1m, 1210m },
        new object[] { 1000m, 4, 0.1m, 1464.1m },
        new object[] { 1000m, 8, 0.1m, 2143.588810m },
        new object[] { 1000m, 16, 0.1m, 4594.972986m },
        new object[] { 1000m, 32, 0.1m, 21113.776745m },
        new object[] { 1000m, 64, 0.1m, 445791.568453m },
    };
}
using QuipuTestTask.Models;

namespace QuipuTestTask.Services;

public class DepositCalculator : IDepositCalculator
{
    private readonly IInterestCalculationServiceProvider _calculationServiceProvider;

    public DepositCalculator(IInterestCalculationServiceProvider calculationServiceProvider)
    {
        _calculationServiceProvider = calculationServiceProvider;
    }

    public decimal CalculateDeposit(decimal initialAmount, decimal interestRate, int months, DepositType depositType)
    {
        var interestCalculator = _calculationServiceProvider.GetInterestCalculationService(depositType);
        var interest = interestCalculator.CalculateInterest(initialAmount, interestRate, months, 1);
        return interest;
    }
}
using QuipuTestTask.Models;

namespace QuipuTestTask.Services;

public interface IDepositCalculator
{
    public decimal CalculateDeposit(decimal initialAmount, decimal interestRate, int months, DepositType depositType);
}
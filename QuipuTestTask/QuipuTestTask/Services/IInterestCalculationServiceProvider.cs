using QuipuTestTask.Models;

namespace QuipuTestTask.Services;

public interface IInterestCalculationServiceProvider
{
    IInterestCalculationService GetInterestCalculationService(DepositType depositType);
}
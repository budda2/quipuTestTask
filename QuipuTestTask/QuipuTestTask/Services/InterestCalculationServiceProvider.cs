using System;
using Microsoft.Extensions.DependencyInjection;
using QuipuTestTask.Models;

namespace QuipuTestTask.Services;

public class InterestCalculationServiceProvider : IInterestCalculationServiceProvider
{
    private readonly IServiceProvider _serviceProvider;

    public InterestCalculationServiceProvider(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public IInterestCalculationService? GetInterestCalculationService(DepositType depositType) =>
        depositType switch
        {
            DepositType.MonthlyCapitalization => _serviceProvider.GetService<CompoundInterestCalculationService>(),
            DepositType.MonthlyPayment => _serviceProvider.GetService<InterestCalculationService>(),
            _ => throw new ArgumentOutOfRangeException(nameof(depositType), depositType, "Deposit type not supported")
        };
}
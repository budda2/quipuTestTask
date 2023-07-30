using System;

namespace QuipuTestTask.Services;

public class CompoundInterestCalculationService : IInterestCalculationService
{
    public decimal CalculateInterest(decimal amount, decimal interest, int numberOfIterations, int frequency) =>
        amount * (decimal)Math.Pow(1 + (double)(interest / frequency), numberOfIterations * frequency);
}
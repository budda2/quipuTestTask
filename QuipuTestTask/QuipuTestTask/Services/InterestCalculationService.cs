namespace QuipuTestTask.Services;

public class InterestCalculationService : IInterestCalculationService
{
    public decimal CalculateInterest(decimal amount, decimal interest, int numberOfIterations, int frequency) =>
        amount + amount * interest * numberOfIterations;
}

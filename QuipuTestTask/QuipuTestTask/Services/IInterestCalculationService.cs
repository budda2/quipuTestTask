namespace QuipuTestTask.Services;

public interface IInterestCalculationService
{
    decimal CalculateInterest(decimal amount, decimal interest, int numberOfIterations, int frequency);
}
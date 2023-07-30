using Microsoft.Extensions.DependencyInjection;

namespace QuipuTestTask.Services.Extensions;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddSingleton<IInterestCalculationServiceProvider, InterestCalculationServiceProvider>();
        services.AddTransient<CompoundInterestCalculationService>();
        services.AddTransient<InterestCalculationService>();
        services.AddTransient<IDepositCalculator, DepositCalculator>();
        return services;
    }
}
using System.Collections.Generic;

namespace QuipuTestTask.Models;

public static class SupportedCurrencies
{
    public static IEnumerable<string> AllCurrencies { get; } = new List<string>()
    {
        "USD",
        "EUR",
        "UAH",
        "GBP",
        "CAD",
        "CNY",
        "DKK",
        "PLN"
    };
}
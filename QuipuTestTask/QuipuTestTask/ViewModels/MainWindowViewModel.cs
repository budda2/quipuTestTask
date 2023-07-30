using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using QuipuTestTask.Services;

namespace QuipuTestTask.ViewModels;

public class MainWindowViewModel : BaseViewModel
{
    private readonly IInterestCalculationServiceProvider _interestCalculationServiceProvider;
    public MainWindowViewModel(IInterestCalculationServiceProvider interestCalculationServiceProvider)
    {
        _interestCalculationServiceProvider = interestCalculationServiceProvider;
    }
    
    private string? _initialAmount;

    public string? InitialAmount
    {
        get => _initialAmount;
        set
        {
            _initialAmount = value;
            OnPropertyChanged();
            CalculationResult = null;
        }
    }

    private string? _months;

    public string? Months
    {
        get => _months;
        set
        {
            _months = value;
            OnPropertyChanged();
            CalculationResult = null;
        }
    }

    private string? _depositMonthlyInterestRate;

    public string? DepositMonthlyInterestRate
    {
        get => _depositMonthlyInterestRate;
        set
        {
            _depositMonthlyInterestRate = value;
            OnPropertyChanged();
            CalculationResult = null;
        }
    }

    public ObservableCollection<DepositType> DepositTypes { get; } =
        new(Enum.GetValues<DepositType>());

    private DepositType? _selectedDepositType;

    public DepositType? SelectedDepositType
    {
        get => _selectedDepositType;
        set
        {
            _selectedDepositType = value;
            OnPropertyChanged();
            CalculationResult = null;
        }
    }

    public ObservableCollection<string> SupportedCurrencies { get; } =
        new(Models.SupportedCurrencies.AllCurrencies);

    private string? _selectedSupportedCurrency;

    public string? SelectedSupportedCurrency
    {
        get => _selectedSupportedCurrency;
        set
        {
            _selectedSupportedCurrency = value;
            OnPropertyChanged();
            CalculationResult = null;
        }
    }

    private ICommand? _calculateCommand;
    public ICommand CalculateCommand => _calculateCommand ??= new RelayCommand(Calculate);

    private string? _calculationHint;

    public string? CalculationHint
    {
        get => _calculationHint;
        set
        {
            _calculationHint = value;
            OnPropertyChanged();
            OnPropertyChanged(nameof(CalculationHintVisibility));
            CalculationResult = null;
        }
    }

    private string? _calculationResult;
    public string? CalculationResult
    {
        get => _calculationResult;
        set
        {
            _calculationResult = value;
            OnPropertyChanged();
            OnPropertyChanged(nameof(CalculationResultVisibility));
        }
    }

    public Visibility CalculationResultVisibility =>
        _calculationResult == null ? Visibility.Hidden : Visibility.Visible;
    
    public Visibility CalculationHintVisibility =>
        CalculationHint == null ? Visibility.Hidden : Visibility.Visible;

    private void Calculate()
    {
        CalculationResult = null;
        var inputValidationResult = ValidateInputs();
        CalculationHint = inputValidationResult;
        if (inputValidationResult != null)
            return;

        var initialAmount = decimal.Parse(InitialAmount);
        var depositDurationInMonths = int.Parse(Months);
        var depositMonthlyInterestRate = decimal.Parse(DepositMonthlyInterestRate);
        var depositCurrency = SelectedSupportedCurrency;
        var interestCalculationService = GetDepositInterestCalculationService(SelectedDepositType);

        var result = interestCalculationService.CalculateInterest(initialAmount, depositMonthlyInterestRate / 100,
            depositDurationInMonths, 1);

        CalculationResult = $"Final deposit amount would be {result:N6} {depositCurrency}";
    }

    private IInterestCalculationService GetDepositInterestCalculationService(DepositType? depositType)
    {
        var modelDepositType = GetDepositType(depositType);
        var depositInterestCalculationService =
            _interestCalculationServiceProvider.GetInterestCalculationService(modelDepositType);
        return depositInterestCalculationService;
    }
    
    private Models.DepositType GetDepositType(DepositType? depositType) =>
        depositType switch
        {
            DepositType.MonthlyCapitalization => Models.DepositType.MonthlyCapitalization,
            DepositType.MonthlyPayment => Models.DepositType.MonthlyPayment,
            _ => throw new ArgumentOutOfRangeException()
        };

    private string? ValidateInputs()
    {
        if (Months == null)
            return "Deposit duration in months not entered";

        if (!int.TryParse(Months, out var depositDurationInMonth) || depositDurationInMonth <= 0)
            return "Deposit duration in months should be a whole non negative number";

        if (InitialAmount == null)
            return "Initial deposit amount not entered";

        if (!decimal.TryParse(InitialAmount, out var initialAmount) || initialAmount < 0)
            return "Initial deposit amount should be a nonnegative number";

        if (DepositMonthlyInterestRate == null)
            return "Deposit monthly interest rate should be entered.";

        if (!decimal.TryParse(DepositMonthlyInterestRate, out var depositMonthlyInterestRate) ||
            depositMonthlyInterestRate <= 0)
            return "Deposit monthly interest rate in % should be a nonnegative number";

        if (SelectedSupportedCurrency == null)
            return "Currency should be selected";

        if (SelectedDepositType == null)
            return "Deposit type should be selected";

        return null;
    }
}